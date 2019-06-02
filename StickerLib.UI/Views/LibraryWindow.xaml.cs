using System.Windows;
using System.Windows.Controls;

namespace StickerLib.UI.Views
{
    public partial class LibraryWindow : Window
    {
        public LibraryWindow()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ViewContentProperty =
            DependencyProperty.Register(nameof(ViewContent), typeof(Page), typeof(LibraryWindow));

        public Page ViewContent
        {
            get { return (Page) GetValue(ViewContentProperty); }
            set { SetValue(ViewContentProperty, value); }
        }
    }
}
