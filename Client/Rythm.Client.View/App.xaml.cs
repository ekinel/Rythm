namespace Rythm.Client.View
{
    using System.Windows;

    using BusinessLogic.Interfaces;

    using Prism.Ioc;
    using Prism.Unity;
    using Prism.Mvvm;

    using Unity;

    using Rythm.Client.ViewModel;
    using Rythm.Client.BusinessLogic;

    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IChatPanelController, ChatPanelController>();
            containerRegistry.Register<ChatPanelViewModel>();

            containerRegistry.RegisterSingleton<IConnectionController, ConnectionController>();
            containerRegistry.Register<ConnectionParametersViewModel>();

            containerRegistry.RegisterSingleton<IUserListController, UserListController>();
            containerRegistry.Register<UserListViewModel>();

            containerRegistry.RegisterSingleton<IUserLoginDisplayController, UserLoginDisplayController>();
            containerRegistry.Register<UserLoginDisplayViewModel>();

            containerRegistry.Register<CommunicationParametersViewModel>();

        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register(typeof(MainWindow).ToString(), () => Container.Resolve<MainWindowViewModel>());
            ViewModelLocationProvider.Register(typeof(ChatPanelView).ToString(), () => Container.Resolve<ChatPanelViewModel>());
            ViewModelLocationProvider.Register(typeof(ConnectionParametersView).ToString(), () => Container.Resolve<ConnectionParametersViewModel>());
            ViewModelLocationProvider.Register(typeof(UserListView).ToString(), () => Container.Resolve<UserListViewModel>());
            ViewModelLocationProvider.Register(typeof(UserLoginDisplayView).ToString(), () => Container.Resolve<UserLoginDisplayViewModel>());
            ViewModelLocationProvider.Register(typeof(CommunicationParametersView).ToString(), () => Container.Resolve<UserLoginDisplayViewModel>());
        }

        protected override Window CreateShell()
        {
            var mainView = Container.Resolve<MainWindow>();
            return mainView;
        }
    }
}
