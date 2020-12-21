﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using Events;

    using Prism.Events;

    public class ScrollViewerExtensionsViewModel
    {
        #region Fields

        public static readonly DependencyProperty AlwaysScrollToEndProperty = DependencyProperty.RegisterAttached(
            "AlwaysScrollToEnd",
            typeof(bool),
            typeof(ScrollViewerExtensionsViewModel),
            new PropertyMetadata(false, AlwaysScrollToEndChanged));

        private static bool _autoScroll;

        public static event Action ScrollAtTheTop;

        #endregion

        #region Methods

        public static bool GetAlwaysScrollToEnd(ScrollViewer scroll)
        {
            if (scroll == null)
            {
                throw new ArgumentNullException("ScrollViewer is not exist");
            }

            return (bool)scroll.GetValue(AlwaysScrollToEndProperty);
        }

        public static void SetAlwaysScrollToEnd(ScrollViewer scroll, bool alwaysScrollToEnd)
        {
            if (scroll == null)
            {
                throw new ArgumentNullException("ScrollViewer is not exist");
            }

            scroll.SetValue(AlwaysScrollToEndProperty, alwaysScrollToEnd);
        }

        private static void AlwaysScrollToEndChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
	        if (sender is ScrollViewer scroll)
            {
                bool alwaysScrollToEnd = e.NewValue != null && (bool)e.NewValue;
                if (alwaysScrollToEnd)
                {
                    scroll.ScrollToEnd();
                    scroll.ScrollChanged += ScrollChanged;
                }
                else
                {
                    scroll.ScrollChanged -= ScrollChanged;
                }
            }
            else
            {
                throw new InvalidOperationException("The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances.");
            }
        }

        private static void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
	        if (!(sender is ScrollViewer scroll))
            {
                throw new InvalidOperationException("The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances.");
            }

            if (e.ExtentHeightChange == 0)
            {
                _autoScroll = scroll.VerticalOffset == scroll.ScrollableHeight;
            }

            if (_autoScroll && e.ExtentHeightChange != 0)
            {
                scroll.ScrollToVerticalOffset(scroll.ExtentHeight);
            }

            //if (e.VerticalOffset == 0 && e.VerticalChange != 0)
            //{
            //    // _eventAggregator.GetEvent<ScrollAtTheTop>().Publish();
            //    ScrollAtTheTop?.Invoke();
            //}
        }

        #endregion
    }
}
