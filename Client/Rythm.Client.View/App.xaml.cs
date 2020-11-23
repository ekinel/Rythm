namespace Rythm.Client.View
{
    using System.Windows;

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
            containerRegistry.RegisterSingleton<IMessagingServiceController, MessagingServiceController>();
            containerRegistry.Register<MessageViewModel>();

            containerRegistry.RegisterSingleton<IConnectionServiceController, ConnectionServiceController>();
            containerRegistry.Register<ConnectionParametersViewModel>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register(typeof(TextMessageView).ToString(), () => Container.Resolve<MessageViewModel>());
            ViewModelLocationProvider.Register(typeof(ConnectionParametersView).ToString(), () => Container.Resolve<ConnectionParametersViewModel>());

        }

        protected override Window CreateShell()
        {
            var mainView = Container.Resolve<MainWindow>();
            return mainView;
        }
    }
}
