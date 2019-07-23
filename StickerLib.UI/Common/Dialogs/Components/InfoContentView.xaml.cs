using System.Windows;
using System.Windows.Controls;

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
    }
}