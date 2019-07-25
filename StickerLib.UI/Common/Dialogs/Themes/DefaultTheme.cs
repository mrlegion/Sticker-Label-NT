using System.Windows;
using System.Windows.Media;

namespace StickerLib.UI.Common.Dialogs.Themes
{
    public class DefaultTheme : BaseTheme
    {
        public DefaultTheme()
        {
            Background = (SolidColorBrush) Application.Current.Resources["MaterialDesignPaper"];
            Foreground = (SolidColorBrush) Application.Current.Resources["MaterialDesignBody"];
        }
    }
}