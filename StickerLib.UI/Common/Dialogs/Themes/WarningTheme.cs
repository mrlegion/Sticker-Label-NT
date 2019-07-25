using System.Windows;
using System.Windows.Media;

namespace StickerLib.UI.Common.Dialogs.Themes
{
    public class WarningTheme : BaseTheme
    {
        public WarningTheme()
        {
            Background = (SolidColorBrush) Application.Current.Resources["WarningThemeBackground"];
            Foreground = (SolidColorBrush) Application.Current.Resources["WarningThemeForeground"];
        }
    }
}