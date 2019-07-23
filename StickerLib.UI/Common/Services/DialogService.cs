using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Threading;
using MaterialDesignThemes.Wpf;
using Microsoft.WindowsAPICodePack.Dialogs;
using StickerLib.UI.Common.Dialogs.Components;
using StickerLib.UI.Common.Dialogs.Views;

namespace StickerLib.UI.Common.Services
{
    public class DialogService : IDialog
    {
        public string AlertDialogHost { get; set; } = "AlertDialogHost";
        public string LoadingDialogHost { get; set; } = "LoadingDialogHost";
        public string CustomDialogHost { get; set; } = "CustomDialogHost";

        /// <summary>
        /// Show information dialog box
        /// </summary>
        /// <param name="title">Title for dialog box</param>
        /// <param name="message">Information message in dialog box</param>
        public void ShowInfo(string title, string message)
        {
            ShowInfo(title, message, AlertDialogHost);
        }

        public void ShowInfo(string title, string message, string identifier)
        {
            ShowDialog(title, message, PackIconKind.InformationOutline,
                (SolidColorBrush) Application.Current.Resources["InfoColor"], identifier);
        }

        /// <summary>
        /// Show success dialog box
        /// </summary>
        /// <param name="title">Title for dialog box</param>
        /// <param name="message">Success message in dialog box</param>
        public void ShowSuccess(string title, string message)
        {
            ShowSuccess(title, message, AlertDialogHost);
        }

        public void ShowSuccess(string title, string message, string identifier)
        {
            ShowDialog(title, message, PackIconKind.CheckCircleOutline,
                (SolidColorBrush) Application.Current.Resources["SuccessColor"], identifier);
        }

        /// <summary>
        /// Show error dialog box
        /// </summary>
        /// <param name="title">Title for dialog box</param>
        /// <param name="message">Error message in dialog box</param>
        public void ShowError(string title, string message)
        {
            ShowError(title, message, AlertDialogHost);
        }

        public void ShowError(string title, string message, string identifier)
        {
            ShowDialog(title, message, PackIconKind.ErrorOutline,
                (SolidColorBrush) Application.Current.Resources["ErrorColor"], identifier);
        }

        /// <summary>
        /// Show warning dialog box
        /// </summary>
        /// <param name="title">Title for dialog box</param>
        /// <param name="message">Warning message in dialog box</param>
        public void ShowWarning(string title, string message)
        {
            ShowWarning(title, message, AlertDialogHost);
        }

        public void ShowWarning(string title, string message, string identifier)
        {
            ShowDialog(title, message, PackIconKind.WarningOutline,
                (SolidColorBrush) Application.Current.Resources["WarningColor"], identifier);
        }

        /// <summary>
        /// Show loading dialog box when data or your operation work in other thread
        /// </summary>
        /// <param name="message">Message for use in dialog box</param>
        /// <param name="callback">Action for work in other thread</param>
        public void ShowLoading(string message, Action callback)
        {
            ShowLoading(message, callback, LoadingDialogHost);
        }

        public async void ShowLoading(string message, Action callback, string identifier)
        {
            var content = ServiceLocator.Current.GetInstance<LoadingContentVIew>();
            content.Message = message;
            var dialogContent = ServiceLocator.Current.GetInstance<ContentDialogView>();
            dialogContent.DialogContent = content;
            dialogContent.Title = "Loading..";
            await DialogHost.Show(dialogContent, identifier, delegate(object sender, DialogOpenedEventArgs args)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    callback.Invoke();
                    DispatcherHelper.CheckBeginInvokeOnUI(() => args.Session.Close(true));
                });
            });
        }

        public void ShowDialog(UserControl content)
        {
            ShowDialog(content, CustomDialogHost);
        }

        public void ShowDialog(UserControl content, string identifier)
        {
            ShowDialog(content, null, identifier);
        }

        public async void ShowDialog(UserControl content, string title, string identifier)
        {
            var dialogContent = ServiceLocator.Current.GetInstance<ContentDialogView>();
            dialogContent.DialogContent = content;
            dialogContent.Title = title;
            await DialogHost.Show(dialogContent, identifier);
        }

        public void ShowDialog(string title, string message, PackIconKind icon, SolidColorBrush theme)
        {
            ShowDialog(title, message, icon, theme, AlertDialogHost);
        }

        public async void ShowDialog(string title, string message, PackIconKind icon, SolidColorBrush theme,
            string identifier)
        {
            var content = ServiceLocator.Current.GetInstance<AlertDialogView>();
            content.ColorTheme = theme;
            content.Title = title;
            content.Message = message;
            content.Icon = icon;
            await DialogHost.Show(content, identifier);
        }

        public async Task<bool> ShowRequest(string title, string message)
        {
            return await ShowRequest(title, message, AlertDialogHost);
        }

        public async Task<bool> ShowRequest(string title, string message, string identifier)
        { 
            return await ShowRequest(title, message, "ACCEPT", "CANCEL", identifier);
        }

        public async Task<bool> ShowRequest(string title, string message, string positiveButtonTitle,
            string negativeButtonTitle)
        {
            return await ShowRequest(title, message, positiveButtonTitle, negativeButtonTitle, AlertDialogHost);
        }

        public async Task<bool> ShowRequest(string title, string message, string positiveButtonTitle,
            string negativeButtonTitle, string identifier)
        {
            var content = ServiceLocator.Current.GetInstance<QuestionDialogView>();
            content.Title = title;
            content.Message = message;
            content.PositiveButtonTitle = positiveButtonTitle;
            content.NegativeButtonTitle = negativeButtonTitle;
            var request = await DialogHost.Show(content, identifier);
            if (request is bool result)
                return result;
            return false;
        }

        public IEnumerable<string> OpenDialog(string title, IEnumerable<CommonFileDialogFilter> filters,
            bool multiselect = false, bool isFolderPicker = false)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog()
            {
                Multiselect = multiselect,
                Title = title ?? "Default title dialog",
                IsFolderPicker = isFolderPicker,
                AllowNonFileSystemItems = true,
                DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            };

            if (filters != null)
            {
                var filterArray = filters.ToArray();
                if (filterArray.Length > 0)
                    foreach (var filter in filterArray)
                        dialog.Filters.Add(filter);
            }

            return (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                ? dialog.FileNames
                : null;
        }
        
        public string OpenFileDialog(string title, IEnumerable<CommonFileDialogFilter> filters)
        {
            var response = OpenDialog(title, filters);
            return response.FirstOrDefault();
        }

        public IEnumerable<string> OpenMultiselectFileDialog(string title, IEnumerable<CommonFileDialogFilter> filters)
        {
            return OpenDialog(title, filters, true);
        }

        public string OpenFolderDialog(string title)
        {
            var response = OpenDialog(title, null, false, true);
            return response.FirstOrDefault();
        }

        public IEnumerable<string> OpenMultiselectFolderDilaog(string title)
        {
            return OpenDialog(title, null, true, true);
        }
    }
}