// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.View
{
	using System.Windows;

	using BusinessLogic;
	using BusinessLogic.Interfaces;

	using Prism.Ioc;
	using Prism.Mvvm;
	using Prism.Unity;

	using ViewModel;

	public partial class App : PrismApplication
	{
		#region Methods

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterSingleton<IChatPanelController, ChatPanelController>();
			containerRegistry.Register<ChatPanelViewModel>();

			containerRegistry.RegisterSingleton<IConnectionController, ConnectionController>();
			containerRegistry.Register<ConnectionParametersViewModel>();

			containerRegistry.RegisterSingleton<IUsersListController, UsersListController>();
			containerRegistry.Register<UsersListViewModel>();

			containerRegistry.RegisterSingleton<IToolPanelDisplayController, ToolPanelDisplayController>();
			containerRegistry.Register<ToolPanelViewModel>();

			containerRegistry.RegisterSingleton<IDisplayingEventsController, DisplayingEventsController>();
			containerRegistry.Register<DisplayingEventsViewModel>();
		}

		protected override void ConfigureViewModelLocator()
		{
			base.ConfigureViewModelLocator();

			ViewModelLocationProvider.Register(typeof(MainWindow).ToString(), () => Container.Resolve<MainWindowViewModel>());
			ViewModelLocationProvider.Register(typeof(ChatPanelView).ToString(), () => Container.Resolve<ChatPanelViewModel>());
			ViewModelLocationProvider.Register(typeof(ConnectionParametersView).ToString(), () => Container.Resolve<ConnectionParametersViewModel>());
			ViewModelLocationProvider.Register(typeof(UsersListView).ToString(), () => Container.Resolve<UsersListViewModel>());
			ViewModelLocationProvider.Register(typeof(ToolPanelView).ToString(), () => Container.Resolve<ToolPanelViewModel>());
			ViewModelLocationProvider.Register(typeof(DisplayingEventsView).ToString(), () => Container.Resolve<DisplayingEventsViewModel>());
		}

		protected override Window CreateShell()
		{
			var mainView = Container.Resolve<MainWindow>();
			return mainView;
		}

		#endregion
	}
}
