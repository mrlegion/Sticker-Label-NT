using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace StickerLib.UI.Common.Dialogs.Components
{
    public partial class InfoContentView : UserControl
    {
        public InfoContentView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(InfoContentView),
                new PropertyMetadata("Please wait..."));

        public string Message
        {
            get => (string) GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public static readonly DependencyProperty ThemeForegroundProperty =
            DependencyProperty.Register("ThemeForeground", typeof(SolidColorBrush), typeof(InfoContentView),
                new PropertyMetadata((SolidColorBrush) Application.Current.Resources["MaterialDesignBody"]));

        public SolidColorBrush ThemeForeground
        {
            get { return (SolidColorBrush) GetValue(ThemeForegroundProperty); }
            set { SetValue(ThemeForegroundProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof(PackIconKind), typeof(InfoContentView), 
            new PropertyMetadata(default(PackIconKind)));

        public PackIconKind Icon
        {
            get { return (PackIconKind) GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
    }
}