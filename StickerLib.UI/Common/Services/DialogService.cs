using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Threading;
using MaterialDesignThemes.Wpf;
using StickerLib.UI.Common.Dialogs.Components;
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
        
        public async void ShowLoading(string message, Action callback)
        {
            LoadingContentVIew content = ServiceLocator.Current.GetInstance<LoadingContentVIew>();
            content.Message = message;
            ContentDialogView dialogContent = ServiceLocator.Current.GetInstance<ContentDialogView>();
            dialogContent.DialogContent = content;
            dialogContent.Title = "Loading..";
            await DialogHost.Show(dialogContent, "AlertDialogHost", delegate(object sender, DialogOpenedEventArgs args)
                {
                    ThreadPool.QueueUserWorkItem(state =>
                    {
                        callback.Invoke();
                        DispatcherHelper.CheckBeginInvokeOnUI(() => args.Session.Close(true));
                    });
                });
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

        public Task<bool> ShowRequest(string title, string message)
        {
            return ShowRequest(title, message, "ACCEPT", "CANCEL");
        }

        public async Task<bool> ShowRequest(string title, string message, string positiveButtonTitle, string negativeButtonTitle)
        {
            QuestionDialogView content = ServiceLocator.Current.GetInstance<QuestionDialogView>();
            content.Title = title;
            content.Message = message;
            content.PositiveButtonTitle = positiveButtonTitle;
            content.NegativeButtonTitle = negativeButtonTitle;
            var request = await DialogHost.Show(content, "AlertDialogHost");
            if (request is bool result)
                return result;
            return false;
        }
    }
}