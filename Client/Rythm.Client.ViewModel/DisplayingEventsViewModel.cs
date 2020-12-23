// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Windows;

	using BusinessLogic.Interfaces;

	using Common.Network;
	using Common.Network.Messages;

	using Events;

	using Prism.Commands;
	using Prism.Events;
	using Prism.Mvvm;

	public class DisplayingEventsViewModel : BindableBase
	{
		#region Fields

		private Visibility _dataGridClientsVisibility = Visibility.Visible;
		private Visibility _dataGridMessagesVisibility = Visibility.Hidden;
		private Visibility _dataGridEventsVisibility = Visibility.Hidden;

		private DateTime _dayFrom;
		private DateTime _dayTo;
		private string _hourFrom;
		private string _hourTo;
		private string _minuteFrom;
		private string _minuteTo;

		private readonly IDisplayingEventsController _displayingEventsController;

		public DelegateCommand SelectCommand { get; }

		#endregion

		#region Properties

		public Visibility DataGridClientsVisibility
		{
			get => _dataGridClientsVisibility;
			set => SetProperty(ref _dataGridClientsVisibility, value);
		}

		public Visibility DataGridMessagesVisibility
		{
			get => _dataGridMessagesVisibility;
			set => SetProperty(ref _dataGridMessagesVisibility, value);
		}

		public Visibility DataGridEventsVisibility
		{
			get => _dataGridEventsVisibility;
			set => SetProperty(ref _dataGridEventsVisibility, value);
		}
		public DateTime DayFrom
		{
			get => _dayFrom;
			set => SetProperty(ref _dayFrom, value);
		}
		public DateTime DayTo
		{
			get => _dayTo;
			set => SetProperty(ref _dayTo, value);
		}
		public string HourTo
		{
			get => _hourTo;
			set => SetProperty(ref _hourTo, value);
		}
		public string HourFrom
		{
			get => _hourFrom;
			set => SetProperty(ref _hourFrom, value);
		}

		public string MinuteTo
		{
			get => _minuteTo;
			set => SetProperty(ref _minuteTo, value);
		}
		public string MinuteFrom
		{
			get => _minuteFrom;
			set => SetProperty(ref _minuteFrom, value);
		}

		public ObservableCollection<DataBaseClientsViewModel> DataBaseClientsList { get; set; } =
			new ObservableCollection<DataBaseClientsViewModel>();

		public ObservableCollection<DataBaseMessagesViewModel> DataBaseAllOwnMessagesList { get; set; } =
			new ObservableCollection<DataBaseMessagesViewModel>();
		public ObservableCollection<DataBaseMessagesViewModel> DataBaseVisibleOwnMessagesList { get; set; } =
			new ObservableCollection<DataBaseMessagesViewModel>();

		public ObservableCollection<DataBaseEventsViewModel> DataBaseAllEventsList { get; set; } = new ObservableCollection<DataBaseEventsViewModel>();
		public ObservableCollection<DataBaseEventsViewModel> DataBaseVisibleEventsList { get; set; } = new ObservableCollection<DataBaseEventsViewModel>();
		private string Login { get; set; }

		#endregion

		#region Constructors

		public DisplayingEventsViewModel(IDisplayingEventsController displayingEventsController, IEventAggregator eventAggregator)
		{
			_displayingEventsController = displayingEventsController;
			_displayingEventsController.UpdatedDataBaseClientsEvent += HandleUpdatedDataBaseClientsEvent;
			_displayingEventsController.UpdatedDataBaseMessagesEvent += HandleUpdatedDataBaseMessagesEvent;
			_displayingEventsController.UpdatedDataBaseEventsEvent += HandleUpdatedDataBaseEventsEvent;
			eventAggregator.GetEvent<DataBaseButtonClientsChosen>().Subscribe(HandleDataBaseButtonClientsChosen);
			eventAggregator.GetEvent<DataBaseButtonEventsChosen>().Subscribe(HandleDataBaseButtonEventsChosen);
			eventAggregator.GetEvent<DataBaseButtonMessagesChosen>().Subscribe(HandleDataBaseButtonMessagesChosen);
			eventAggregator.GetEvent<PassLoginViewModel>().Subscribe(HandleClientLoginFrom);

			SelectCommand = new DelegateCommand(SelectEntryCommand);

			DataGridClientsVisibility = Visibility.Visible;
			DataGridMessagesVisibility = Visibility.Hidden;
			DataGridEventsVisibility = Visibility.Hidden;

			DayFrom = DateTime.Today;
			DayTo = DateTime.Today;
			HourTo = "0";
			HourFrom = "0";
			MinuteTo = "0";
			MinuteFrom = "0";
		}

		private void SelectEntryCommand()
		{
			int intHourTo = CheckingTime(HourTo);
			int intHourFrom = CheckingTime(HourFrom);
			int intMinuteTo = CheckingTime(MinuteTo);
			int intMinuteFrom = CheckingTime(MinuteFrom);

			DayTo = CheckingDay(DayFrom, DayTo);
			var checkingDateFrom = new DateTime(DayFrom.Year, DayFrom.Month, DayFrom.Day, intHourFrom, intMinuteFrom, 0);
			var checkingDateTo = new DateTime(DayTo.Year, DayTo.Month, DayTo.Day, intHourTo, intMinuteTo, 0);

			if (DataGridMessagesVisibility == Visibility.Visible)
			{
				DataBaseVisibleOwnMessagesList.Clear();

				foreach (DataBaseMessagesViewModel element in DataBaseAllOwnMessagesList)
				{
					if (element.Date >= checkingDateFrom && element.Date <= checkingDateTo)
					{
						DataBaseVisibleOwnMessagesList.Add(new DataBaseMessagesViewModel(new DataBaseMessage(element.Text, element.Date, element.ClientFrom, element.ClientTo)));
					}

				}
			}
			if (DataGridEventsVisibility == Visibility.Visible)
			{
				DataBaseVisibleEventsList.Clear();

				foreach (DataBaseEventsViewModel element in DataBaseAllEventsList)
				{
					if (element.EventDate >= checkingDateFrom && element.EventDate <= checkingDateTo)
					{
						DataBaseVisibleEventsList.Add(new DataBaseEventsViewModel(element.EventMessage, element.EventDate));
					}
				}
			}
		}

		private static int CheckingTime(string time)
		{
			if (int.TryParse(time, out int result))
			{
				if (result >= 0 && result < 60)
				{
					return result;
				}
			}
			return 0;
		}

		private DateTime CheckingDay(DateTime dayFrom, DateTime dayTo)
		{
			return dayTo < dayFrom ? dayFrom : DayTo;
		}
		#endregion

		#region Methods

		private void HandleClientLoginFrom(string login)
		{
			Login = login;
			_displayingEventsController.Login = login;
		}

		private void HandleUpdatedDataBaseClientsEvent(List<string> dataBaseClientsList)
		{
			ApplicationDispatcherHelper.BeginInvoke(
				() =>
				{
					DataBaseClientsList.Clear();
				});

			foreach (string client in dataBaseClientsList)
			{
				ApplicationDispatcherHelper.BeginInvoke(
					() =>
					{
						DataBaseClientsList.Add(new DataBaseClientsViewModel(client));
					});
			}
		}

		private void HandleUpdatedDataBaseMessagesEvent(List<DataBaseMessage> messagesList)
		{
			ApplicationDispatcherHelper.BeginInvoke(
				() =>
				{
					DataBaseAllOwnMessagesList.Clear();
				});

			foreach (DataBaseMessage message in messagesList)
			{
				ApplicationDispatcherHelper.BeginInvoke(
					() =>
					{
						DataBaseAllOwnMessagesList.Add(new DataBaseMessagesViewModel(message));
						DataBaseVisibleOwnMessagesList.Add(new DataBaseMessagesViewModel(message));
					});
			}
		}

		private void HandleUpdatedDataBaseEventsEvent(List<DataBaseEvent> eventMessage)
		{
			ApplicationDispatcherHelper.BeginInvoke(
				() =>
				{
					DataBaseAllEventsList.Clear();
				});

			foreach (DataBaseEvent element in eventMessage)
			{
				ApplicationDispatcherHelper.BeginInvoke(
					() =>
					{
						DataBaseAllEventsList.Add(new DataBaseEventsViewModel(element.Message, element.Date));
						DataBaseVisibleEventsList.Add(new DataBaseEventsViewModel(element.Message, element.Date));
					});
			}
		}

		private void HandleDataBaseButtonClientsChosen()
		{
			DataGridClientsVisibility = Visibility.Visible;
			DataGridEventsVisibility = Visibility.Hidden;
			DataGridMessagesVisibility = Visibility.Hidden;
		}

		private void HandleDataBaseButtonEventsChosen()
		{
			DataGridClientsVisibility = Visibility.Hidden;
			DataGridMessagesVisibility = Visibility.Hidden;
			DataGridEventsVisibility = Visibility.Visible;
		}

		private void HandleDataBaseButtonMessagesChosen()
		{
			DataGridClientsVisibility = Visibility.Hidden;
			DataGridEventsVisibility = Visibility.Hidden;
			DataGridMessagesVisibility = Visibility.Visible;
		}

		#endregion
	}
}
