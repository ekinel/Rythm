namespace Rythm.Client.BusinessLogic
{
    using System;
    using System.Windows;

    public static class ApplicationDispatcherHelper
    {

        #region Methods

        //public static void BeginInvoke(Action handler)
        //{
        //    if (Application.Current?.Dispatcher == null)
        //    {
        //        handler?.Invoke();
        //    }
        //    else
        //    {
        //        Application.Current.Dispatcher.BeginInvoke(new Action(handler.Invoke));
        //    }
        //}

        //public static void Invoke(Action handler)
        //{
        //    if (Application.Current?.Dispatcher == null)
        //    {
        //        handler?.Invoke();
        //    }
        //    else
        //    {
        //        Application.Current.Dispatcher.Invoke(handler.Invoke);
        //    }
        //}

        #endregion
    }
}
