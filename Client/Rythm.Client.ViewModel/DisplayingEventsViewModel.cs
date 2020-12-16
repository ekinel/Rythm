// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	using BusinessLogic.Interfaces;

	using Common.Network;

	using Prism.Mvvm;

	public class DisplayingEventsViewModel : BindableBase
	{
		#region Fields

		private readonly IDisplayingEventsController _displayingEventsController;

		#endregion

		#region Properties

		public ObservableCollection<DataBaseClient> DataBaseClientsList { get; set; } = new ObservableCollection<DataBaseClient>();

		#endregion

		#region Constructors

		public DisplayingEventsViewModel(IDisplayingEventsController displayingEventsController)
		{
			_displayingEventsController = displayingEventsController;
			_displayingEventsController.UpdatedDataBaseClientsEvent += HandleUpdatedDataBaseClientsEvent;
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
						DataBaseClientsList.Add(new DataBaseClient(client));
					});
			}
		}

		#endregion
	}

	public class DataBaseClient
	{
		#region Properties

		public string Login { get; }

		#endregion

		#region Constructors

		public DataBaseClient(string login)
		{
			Login = login;
		}

		#endregion
	}
}
