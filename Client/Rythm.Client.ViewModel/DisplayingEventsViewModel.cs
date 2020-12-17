// ---------------------------------------------------------------------------------------------------------------------------------------------------
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
		public Visibility DataGridClientsVisibility = Visibility.Visible;

		public Visibility DataGridEventsVisibility = Visibility.Hidden;
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

		private void HandleUpdatedDataBaseEventsEvent(List<KeyValuePair<string, string>> eventMessage)
		{
			ApplicationDispatcherHelper.BeginInvoke(
				() =>
				{
					DataBaseEventsList.Clear();
				});

			foreach (KeyValuePair<string, string> element in eventMessage)
			{
				ApplicationDispatcherHelper.BeginInvoke(
					() =>
					{
						DataBaseEventsList.Add(new DataBaseEventsViewModel(element.Key, element.Value));
					});
			}
		}

		private void HandleDataBaseButtonClientsChosen()
		{
			DataGridClientsVisibility = Visibility.Visible;
			DataGridEventsVisibility = Visibility.Hidden;
		}

		private void HandleDataBaseButtonEventsChosen()
		{
			DataGridClientsVisibility = Visibility.Hidden;
			DataGridEventsVisibility = Visibility.Visible;
		}

		#endregion
	}
}
