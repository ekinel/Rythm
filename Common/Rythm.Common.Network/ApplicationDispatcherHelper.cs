﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network
{
    using System;
    using System.Windows;

    public static class ApplicationDispatcherHelper
    {
        public static void BeginInvoke(Action handler)
        {
            if (Application.Current?.Dispatcher == null)
            {
                handler?.Invoke();
            }
            else
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(handler.Invoke));
            }
        }

        public static void Invoke(Action handler)
        {
            if (Application.Current?.Dispatcher == null)
            {
                handler?.Invoke();
            }
            else
            {
                Application.Current.Dispatcher.Invoke(handler.Invoke);
            }
        }
    }
}
