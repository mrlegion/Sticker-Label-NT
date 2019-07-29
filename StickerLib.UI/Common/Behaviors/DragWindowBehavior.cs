using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace StickerLib.UI.Common.Behaviors
{
    public class DragWindowBehavior : Behavior<UIElement>
    {
        #region Public methods

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDownHandler;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDownHandler;
        }

        #endregion

        #region Private methods

        private void OnMouseLeftButtonDownHandler(object sender, MouseButtonEventArgs e)
        {
            if (sender is Window window) window.DragMove();
            else Window.GetWindow((DependencyObject) sender)?.DragMove();
        }

        #endregion
    }
}