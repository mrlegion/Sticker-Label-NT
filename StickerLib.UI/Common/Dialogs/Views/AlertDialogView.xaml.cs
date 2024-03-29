﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            DependencyProperty.Register(nameof(Icon), typeof(PackIconKind), typeof(AlertDialogView), new PropertyMetadata(default(PackIconKind)));

        public PackIconKind Icon
        {
            get { return (PackIconKind) GetValue(IconProperty); }
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
            DependencyProperty.Register(nameof(ColorTheme), typeof(SolidColorBrush), typeof(AlertDialogView), new PropertyMetadata(Application.Current.Resources["InfoColor"]));

        public SolidColorBrush ColorTheme
        {
            get { return (SolidColorBrush)GetValue(ColorThemeProperty); }
            set { SetValue(ColorThemeProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(AlertDialogView), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
    }
}
