namespace Rythm.Client.View
{
    using System.Windows;

    using Prism.Ioc;
    using Prism.Unity;
    using Prism.Mvvm;



    using Unity;

    using Rythm.Client.ViewModel;

    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterSingleton<ITextMessagesController, TextMessagesController>();
            containerRegistry.Register<MessageViewModel>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register(typeof(TextMessageView).ToString(), () => Container.Resolve<MessageViewModel>());
        }

        protected override Window CreateShell()
        {
            var mainView = Container.Resolve<MainWindow>();
            return mainView;
        }
    }
}
