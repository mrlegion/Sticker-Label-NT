using System.Windows;
using System.Windows.Media;

namespace StickerLib.UI.Common.Dialogs.Themes
{
    public class DarkTheme : BaseTheme
    {
        public DarkTheme()
        {
            Background = (SolidColorBrush) Application.Current.Resources["DarkThemeBackground"];
            Foreground = (SolidColorBrush) Application.Current.Resources["DarkThemeForeground"];
        }
    }
}