using System.Windows;
using System.Windows.Controls;

namespace StickerLib.UI.Common.Dialogs.Views
{
    public partial class QuestionDialogView : UserControl
    {
        public QuestionDialogView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(QuestionDialogView), new PropertyMetadata("Default title"));

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(nameof(Message), typeof(string), typeof(QuestionDialogView), new PropertyMetadata(default(string)));

        public string Message
        {
            get { return (string) GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty PositiveButtonTitleProperty =
            DependencyProperty.Register(nameof(PositiveButtonTitle), typeof(string), typeof(QuestionDialogView), new PropertyMetadata("Accept"));

        public string PositiveButtonTitle
        {
            get { return (string)GetValue(PositiveButtonTitleProperty); }
            set { SetValue(PositiveButtonTitleProperty, value); }
        }

        public static readonly DependencyProperty NegativeButtonTitleProperty =
            DependencyProperty.Register(nameof(NegativeButtonTitle), typeof(string), typeof(QuestionDialogView), new PropertyMetadata("Cancel"));

        public string NegativeButtonTitle
        {
            get { return (string)GetValue(NegativeButtonTitleProperty); }
            set { SetValue(NegativeButtonTitleProperty, value); }
        }
    }
}
