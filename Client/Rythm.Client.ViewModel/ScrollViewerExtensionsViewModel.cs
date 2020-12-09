// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ScrollViewerExtensionsViewModel
    {
        #region Fields

        public static readonly DependencyProperty AlwaysScrollToEndProperty = DependencyProperty.RegisterAttached(
            "AlwaysScrollToEnd",
            typeof(bool),
            typeof(ScrollViewerExtensionsViewModel),
            new PropertyMetadata(false, AlwaysScrollToEndChanged));

        private static bool _autoScroll;

        #endregion

        #region Methods

        public static bool GetAlwaysScrollToEnd(ScrollViewer scroll)
        {
            if (scroll == null)
            {
                throw new ArgumentNullException("scroll");
            }

            return (bool)scroll.GetValue(AlwaysScrollToEndProperty);
        }

        public static void SetAlwaysScrollToEnd(ScrollViewer scroll, bool alwaysScrollToEnd)
        {
            if (scroll == null)
            {
                throw new ArgumentNullException("scroll");
            }

            scroll.SetValue(AlwaysScrollToEndProperty, alwaysScrollToEnd);
        }

        private static void AlwaysScrollToEndChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var scroll = sender as ScrollViewer;
            if (scroll != null)
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
            var scroll = sender as ScrollViewer;
            if (scroll == null)
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
        }

        #endregion
    }
}
