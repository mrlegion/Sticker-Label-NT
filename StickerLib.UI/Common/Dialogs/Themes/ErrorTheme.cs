using System.Windows;
using System.Windows.Media;

namespace StickerLib.UI.Common.Dialogs.Themes
{
    public class ErrorTheme : BaseTheme
    {
        public ErrorTheme()
        {
            Background = (SolidColorBrush) Application.Current.Resources["ErrorColor"];
            Foreground = (SolidColorBrush) Application.Current.Resources["MaterialDesignPaper"];
        }
    }
}