using System.Windows.Media;
using StickerLib.UI.Common.Services;

namespace StickerLib.UI.Common.Dialogs.Themes
{
    public abstract class BaseTheme : IDialogTheme
    {
        protected SolidColorBrush Background = null;
        protected SolidColorBrush Foreground = null;

        public virtual SolidColorBrush GetBackgroundBrush()
        {
            return Background;
        }

        public virtual SolidColorBrush GetForegroundBrush()
        {
            return Foreground;
        }
    }
}