using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

namespace StickerLib.UI.Common.Dialogs.Views
{
    /// <summary>
    /// Логика взаимодействия для AlertDialogView.xaml
    /// </summary>
    public partial class AlertDialogView : UserControl
    {
        public AlertDialogView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(PackIcon), typeof(AlertDialogView), new PropertyMetadata(default(PackIcon)));

        public PackIcon Icon
        {
            get { return (PackIcon) GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(nameof(Message), typeof(string), typeof(AlertDialogView), new PropertyMetadata(default(string)));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty ColorThemeProperty = 
            DependencyProperty.Register(nameof(ColorTheme), typeof(SolidColorBrush), typeof(AlertDialogView), new PropertyMetadata(Application.Current.Resources["InfoColor"],
                (o, args) =>
                {
                    if (o is SolidColorBrush brush) { }
                }));

        public SolidColorBrush ColorTheme
        {
            get { return (SolidColorBrush)GetValue(ColorThemeProperty); }
            set { SetValue(ColorThemeProperty, value); }
        }
    }
}
