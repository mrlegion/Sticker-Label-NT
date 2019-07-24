using System.Windows;
using System.Windows.Media;

namespace StickerLib.UI.Common.Dialogs.Themes
{
    public class LightTheme : BaseTheme
    {
        public LightTheme()
        {
            Background = (SolidColorBrush) Application.Current.Resources["LightThemeBackground"];
            Foreground = (SolidColorBrush) Application.Current.Resources["LightThemeForeground"];
        }
    }
}