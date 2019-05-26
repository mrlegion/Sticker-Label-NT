using System.Windows;
using System.Windows.Controls;

namespace StickerLib.UI.Common.Dialogs.Views
{
    /// <summary>
    /// Логика взаимодействия для ContentDialogView.xaml
    /// </summary>
    public partial class ContentDialogView : UserControl
    {
        public ContentDialogView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(ContentDialogView), new PropertyMetadata("Default title for dialog"));

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty DialogContentProperty = 
            DependencyProperty.Register(nameof(DialogContent), typeof(UserControl), typeof(ContentDialogView), new PropertyMetadata());

        public UserControl DialogContent
        {
            get { return (UserControl) GetValue(DialogContentProperty); }
            set { SetValue(DialogContentProperty, value); }
        }
    }
}
