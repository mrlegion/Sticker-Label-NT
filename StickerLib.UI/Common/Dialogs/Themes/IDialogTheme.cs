using System.Windows.Media;

namespace StickerLib.UI.Common.Dialogs.Themes
{
    public interface IDialogTheme
    {
        SolidColorBrush GetBackgroundBrush();
        SolidColorBrush GetForegroundBrush();
    }
}