using System.Windows;
using System.Windows.Media;

namespace StickerLib.UI.Common.Dialogs.Themes
{
    public class InfoTheme : BaseTheme
    {
        public InfoTheme()
        {
            Background = (SolidColorBrush) Application.Current.Resources["InfoThemeBackground"];
            Foreground = (SolidColorBrush) Application.Current.Resources["InfoThemeForeground"];
        }
    }
}