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

	using Common.Network.Messages;

	using Events;

	using Prism.Commands;
	using Prism.Events;
	using Prism.Mvvm;

	public class DisplayingEventsViewModel : BindableBase
	{
		#region Constants

		private const int DEFAULT_TIME_VALUE = 0;

		#endregion

		#region Fields

		private bool _dataGridMessagesVisibility;
		private bool _dataGridEventsVisibility;

		private DateTime _dayFrom;
		private DateTime _dayTo;
		private string _hourFrom;
		private string _hourTo;
		private string _minuteFrom;
		private string _minuteTo;

		private readonly IDisplayingEventsController _displayingEventsController;

		#endregion

		#region Properties

		public DelegateCommand SelectCommand { get; }
		public DelegateCommand ResetCommand { get; }

		public bool DataGridMessagesVisibility
		{
			get => _dataGridMessagesVisibility;
			set => SetProperty(ref _dataGridMessagesVisibility, value);
		}

		public bool DataGridEventsVisibility
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

		public ObservableCollection<DataBaseMessagesViewModel> DataBaseAllOwnMessagesList { get; set; } =
			new ObservableCollection<DataBaseMessagesViewModel>();

		public ObservableCollection<DataBaseMessagesViewModel> DataBaseVisibleOwnMessagesList { get; set; } =
			new ObservableCollection<DataBaseMessagesViewModel>();

		public ObservableCollection<DataBaseEventsViewModel> DataBaseAllEventsList { get; set; } =
			new ObservableCollection<DataBaseEventsViewModel>();

		public ObservableCollection<DataBaseEventsViewModel> DataBaseVisibleEventsList { get; set; } =
			new ObservableCollection<DataBaseEventsViewModel>();

		#endregion

		#region Constructors

		public DisplayingEventsViewModel(IDisplayingEventsController displayingEventsController, IEventAggregator eventAggregator)
		{
			_displayingEventsController = displayingEventsController;
			_displayingEventsController.UpdatedDataBaseMessagesEvent += HandleUpdatedDataBaseMessagesEvent;
			_displayingEventsController.UpdatedDataBaseEventsEvent += HandleUpdatedDataBaseEventsEvent;
			eventAggregator.GetEvent<DataBaseButtonEventsChosen>().Subscribe(HandleDataBaseButtonEventsChosen);
			eventAggregator.GetEvent<DataBaseButtonMessagesChosen>().Subscribe(HandleDataBaseButtonMessagesChosen);
			eventAggregator.GetEvent<PassLoginViewModel>().Subscribe(HandleClientLoginFrom);

			SelectCommand = new DelegateCommand(ExecuteSelectEntriesCommand);
			ResetCommand = new DelegateCommand(ExecuteResetEntriesCommand);

			DataGridMessagesVisibility = true;
			DataGridEventsVisibility = false;

			DayFrom = DateTime.Today;
			DayTo = DateTime.Today;
			HourTo = DEFAULT_TIME_VALUE.ToString();
			HourFrom = DEFAULT_TIME_VALUE.ToString();
			MinuteTo = DEFAULT_TIME_VALUE.ToString();
			MinuteFrom = DEFAULT_TIME_VALUE.ToString();
		}

		#endregion

		#region Methods

		private void ExecuteResetEntriesCommand()
		{
			if (DataGridMessagesVisibility)
			{
				DataBaseVisibleOwnMessagesList.Clear();

				foreach (DataBaseMessagesViewModel element in DataBaseAllOwnMessagesList)
				{
					DataBaseVisibleOwnMessagesList.Add(
						new DataBaseMessagesViewModel(new DataBaseMessage(element.Text, element.Date, element.ClientFrom, element.ClientTo, element.Status)));
				}
			}

			if (DataGridEventsVisibility)
			{
				DataBaseVisibleEventsList.Clear();

				foreach (DataBaseEventsViewModel element in DataBaseAllEventsList)
				{
					DataBaseVisibleEventsList.Add(new DataBaseEventsViewModel(element.EventMessage, element.EventDate));
				}
			}
		}

		private void ExecuteSelectEntriesCommand()
		{
			int intHourTo = CheckingTimeHours(HourTo);
			int intHourFrom = CheckingTimeHours(HourFrom);
			int intMinuteTo = CheckingTimeMinutes(MinuteTo);
			int intMinuteFrom = CheckingTimeMinutes(MinuteFrom);

			HourTo = intHourTo.ToString();
			HourFrom = intHourFrom.ToString();
			MinuteTo = intMinuteTo.ToString();
			MinuteFrom = intMinuteFrom.ToString();

			DayTo = CheckingDay(DayFrom, DayTo);
			var checkingDateFrom = new DateTime(DayFrom.Year, DayFrom.Month, DayFrom.Day, intHourFrom, intMinuteFrom, 0);
			var checkingDateTo = new DateTime(DayTo.Year, DayTo.Month, DayTo.Day, intHourTo, intMinuteTo, 0);

			if (DataGridMessagesVisibility)
			{
				DataBaseVisibleOwnMessagesList.Clear();

				foreach (DataBaseMessagesViewModel element in DataBaseAllOwnMessagesList)
				{
					if (element.Date >= checkingDateFrom && element.Date <= checkingDateTo)
					{
						DataBaseVisibleOwnMessagesList.Add(
							new DataBaseMessagesViewModel(new DataBaseMessage(element.Text, element.Date, element.ClientFrom, element.ClientTo, element.Status)));
					}
				}
			}

			if (DataGridEventsVisibility)
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

		private static int CheckingTimeMinutes(string time)
		{
			if (!int.TryParse(time, out int result))
			{
				return 0;
			}

			if (result >= 0 && result < 60)
			{
				return result;
			}

			return 0;
		}

		private static int CheckingTimeHours(string time)
		{
			if (!int.TryParse(time, out int result))
			{
				return 0;
			}

			if (result >= 0 && result < 24)
			{
				return result;
			}

			return 0;
		}

		private DateTime CheckingDay(DateTime dayFrom, DateTime dayTo)
		{
			return dayTo < dayFrom ? dayFrom : DayTo;
		}

		private void HandleClientLoginFrom(string login)
		{
			_displayingEventsController.Login = login;
		}

		private void HandleUpdatedDataBaseMessagesEvent(List<DataBaseMessage> messagesList)
		{
			DataBaseAllOwnMessagesList.Clear();
			DataBaseVisibleOwnMessagesList.Clear();
			foreach (DataBaseMessage message in messagesList)
			{
				DataBaseAllOwnMessagesList.Add(new DataBaseMessagesViewModel(message));
				DataBaseVisibleOwnMessagesList.Add(new DataBaseMessagesViewModel(message));
			}
		}

		private void HandleUpdatedDataBaseEventsEvent(List<DataBaseEvent> eventMessage)
		{
			DataBaseAllEventsList.Clear();
			DataBaseVisibleEventsList.Clear();
			foreach (DataBaseEvent element in eventMessage)
			{
				DataBaseAllEventsList.Add(new DataBaseEventsViewModel(element.Message, element.Date));
				DataBaseVisibleEventsList.Add(new DataBaseEventsViewModel(element.Message, element.Date));
			}
		}

		private void HandleDataBaseButtonEventsChosen()
		{
			DataGridMessagesVisibility = false;
			DataGridEventsVisibility = true;
		}

		private void HandleDataBaseButtonMessagesChosen()
		{
			DataGridMessagesVisibility = true;
			DataGridEventsVisibility = false;
		}

		#endregion
	}
}
