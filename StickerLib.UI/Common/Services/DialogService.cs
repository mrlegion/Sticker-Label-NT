using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommonServiceLocator;
using MaterialDesignThemes.Wpf;
using StickerLib.UI.Common.Dialogs.Views;

namespace StickerLib.UI.Common.Services
{
    public class DialogService : IDialog
    {
        public void ShowInfo(string title, string message)
        {
            ShowDialog(title, message, PackIconKind.InformationOutline, (SolidColorBrush)Application.Current.Resources["InfoColor"]);
        }

        public void ShowSuccess(string title, string message)
        {
            ShowDialog(title, message, PackIconKind.CheckCircleOutline, (SolidColorBrush)Application.Current.Resources["SuccessColor"]);
        }

        public void ShowError(string title, string message)
        {
            ShowDialog(title, message, PackIconKind.ErrorOutline, (SolidColorBrush)Application.Current.Resources["ErrorColor"]);
        }

        public void ShowWarning(string title, string message)
        {
            ShowDialog(title, message, PackIconKind.WarningOutline, (SolidColorBrush)Application.Current.Resources["WarningColor"]);
        }

        public bool ShowRequest(string message)
        {
            throw new NotImplementedException();
        }

        public void ShowLoading(string message, Action callback)
        {
            throw new NotImplementedException();
        }

        public void ShowDialog(UserControl content, string title)
        {
            throw new NotImplementedException();
        }

        public async void ShowDialog(string title, string message, PackIconKind icon, SolidColorBrush theme)
        {
            AlertDialogView content = ServiceLocator.Current.GetInstance<AlertDialogView>();
            content.ColorTheme = theme;
            content.Title = title;
            content.Message = message;
            content.Icon = icon;
            await DialogHost.Show(content, "AlertDialogHost");
        }
    }
}