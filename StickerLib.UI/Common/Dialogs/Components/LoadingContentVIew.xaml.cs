using System.Windows;
using System.Windows.Controls;

namespace StickerLib.UI.Common.Dialogs.Components
{
    public partial class LoadingContentVIew : UserControl
    {
        public LoadingContentVIew()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(nameof(Message), typeof(string), typeof(LoadingContentVIew), new PropertyMetadata("Please wait..."));

        public string Message
        {
            get { return (string) GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
    }
}
