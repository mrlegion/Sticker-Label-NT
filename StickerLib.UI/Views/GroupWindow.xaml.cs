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
using System.Windows.Shapes;

namespace StickerLib.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для GroupWindow.xaml
    /// </summary>
    public partial class GroupWindow : Window
    {
        public GroupWindow()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ViewContentProperty = 
            DependencyProperty.Register(nameof(ViewContent), typeof(Page), typeof(GroupWindow));

        public Page ViewContent
        {
            get { return (Page) GetValue(ViewContentProperty); }
            set { SetValue(ViewContentProperty, value); }
        }
    }
}
