using System.Windows;
using System.Windows.Media;

namespace StickerLib.UI.Common.Dialogs.Themes
{
    public class SuccessTheme : BaseTheme
    {
        public SuccessTheme()
        {
            Background = (SolidColorBrush) Application.Current.Resources["SuccessColor"];
            Foreground = (SolidColorBrush) Application.Current.Resources["MaterialDesignPaper"];
        }
    }
}