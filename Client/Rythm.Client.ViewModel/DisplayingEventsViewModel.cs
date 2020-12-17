﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Windows;

	using BusinessLogic.Interfaces;

	using Common.Network;
	using Common.Network.Messages;

	using Events;

	using Prism.Events;
	using Prism.Mvvm;

	public class DisplayingEventsViewModel : BindableBase
	{
		private Visibility _dataGridClientsVisibility = Visibility.Visible;
		private Visibility _dataGridMessagesVisibility = Visibility.Hidden;
		private Visibility _dataGridEventsVisibility = Visibility.Hidden;

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


		#region Properties

		public ObservableCollection<DataBaseClientsViewModel> DataBaseClientsList { get; set; } =
			new ObservableCollection<DataBaseClientsViewModel>();

		public ObservableCollection<DataBaseMessagesViewModel> DataBaseMessagesList { get; set; } = new ObservableCollection<DataBaseMessagesViewModel>();
		public ObservableCollection<DataBaseEventsViewModel> DataBaseEventsList { get; set; } = new ObservableCollection<DataBaseEventsViewModel>();

		#endregion

		#region Constructors

		public DisplayingEventsViewModel(IDisplayingEventsController displayingEventsController, IEventAggregator eventAggregator)
		{
			displayingEventsController.UpdatedDataBaseClientsEvent += HandleUpdatedDataBaseClientsEvent;
			displayingEventsController.UpdatedDataBaseMessagesEvent += HandleUpdatedDataBaseMessagesEvent;
			displayingEventsController.UpdatedDataBaseEventsEvent += HandleUpdatedDataBaseEventsEvent;
			eventAggregator.GetEvent<DataBaseButtonClientsChosen>().Subscribe(HandleDataBaseButtonClientsChosen);
			eventAggregator.GetEvent<DataBaseButtonEventsChosen>().Subscribe(HandleDataBaseButtonEventsChosen);
			eventAggregator.GetEvent<DataBaseButtonMessagesChosen>().Subscribe(HandleDataBaseButtonMessagesChosen);

			DataGridClientsVisibility = Visibility.Visible;
			DataGridMessagesVisibility = Visibility.Hidden;
			DataGridEventsVisibility = Visibility.Hidden;
		}

		#endregion

		#region Methods

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
					DataBaseMessagesList.Clear();
				});

			foreach (DataBaseMessage message in messagesList)
			{
				ApplicationDispatcherHelper.BeginInvoke(
					() =>
					{
						DataBaseMessagesList.Add(new DataBaseMessagesViewModel(message));
					});
			}
		}

		private void HandleUpdatedDataBaseEventsEvent(List<DataBaseEvent> eventMessage)
		{
			ApplicationDispatcherHelper.BeginInvoke(
				() =>
				{
					DataBaseEventsList.Clear();
				});

			foreach (DataBaseEvent element in eventMessage)
			{
				ApplicationDispatcherHelper.BeginInvoke(
					() =>
					{
						DataBaseEventsList.Add(new DataBaseEventsViewModel(element.Message, element.Date));
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
