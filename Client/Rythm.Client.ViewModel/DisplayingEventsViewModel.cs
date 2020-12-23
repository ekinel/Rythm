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
			HourTo = "0.00";
			HourFrom = "23.00";
		}

		private void SelectEntryCommand()
		{
			if (DataGridMessagesVisibility == Visibility.Visible)
			{
				foreach (var element in DataBaseAllOwnMessagesList)
				{
					var a = element.Date;
					var b = a.Date;

				}
			}
			if (DataGridEventsVisibility == Visibility.Visible)
			{

			}
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
