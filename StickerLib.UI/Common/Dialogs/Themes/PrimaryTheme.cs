using System.Windows;
using System.Windows.Media;

namespace StickerLib.UI.Common.Dialogs.Themes
{
    public class PrimaryTheme : BaseTheme
    {
        public PrimaryTheme()
        {
            Background = (SolidColorBrush) Application.Current.Resources[""];
            Foreground = (SolidColorBrush) Application.Current.Resources[""];
        }
    }
}