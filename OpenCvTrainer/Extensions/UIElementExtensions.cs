using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace OpenCvTrainer.Extensions
{
    public static class UIElementExtensions
    {
        /// <summary>
        /// Causes the pointer icon to appear when the user hovers over this UI element
        /// </summary>
        /// <param name="element"></param>
        public static void LiftOnHover(this FrameworkElement element, float xScale = 1.2f, float yScale = 1.2f, int duration = 333)
        {
            element.PointerEntered += (sender, args) =>
            {
                element.Scale(xScale, yScale, (float)element.ActualWidth / 2, (float)element.ActualHeight / 2, duration).Start();
            };

            element.PointerExited += (sender, args) =>
            {
                element.Scale(1, 1, (float)element.ActualWidth / 2, (float)element.ActualHeight / 2, duration).Start();
            };
        }

        /// <summary>
        /// Causes the pointer icon to appear when the user hovers over this UI element
        /// </summary>
        /// <param name="element"></param>
        public static void CursorOnHover(this UIElement element, CoreCursorType cursor = Windows.UI.Core.CoreCursorType.Hand)
        {
            element.PointerEntered += (sender, args) =>
            {
                if (element.IsHitTestVisible)
                    Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(cursor, 0);
            };

            element.PointerExited += (sender, args) =>
            {
                Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
            };
        }
    }
}
