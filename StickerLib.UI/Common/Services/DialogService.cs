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
using StickerLib.UI.Common.Dialogs.Themes;
using StickerLib.UI.Common.Dialogs.Views;

namespace StickerLib.UI.Common.Services
{
    public class DialogService : IDialog
    {
        #region Properties

        public string AlertDialogHost { get; set; } = "AlertDialogHost";
        public string LoadingDialogHost { get; set; } = "LoadingDialogHost";
        public string CustomDialogHost { get; set; } = "CustomDialogHost";

        public int DelayShortValue { get; set; } = 1500;

        #endregion

        #region Dialogs

        #region Info dialog

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
            ShowDialog(title, message, PackIconKind.InformationOutline, DialogThemeType.Info, identifier);
        }

        #endregion

        #region Success dialog

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
            ShowDialog(title, message, PackIconKind.CheckCircleOutline, DialogThemeType.Success, identifier);
        }

        #endregion

        #region Error dialog

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
            ShowDialog(title, message, PackIconKind.ErrorOutline, DialogThemeType.Error, identifier);
        }

        #endregion

        #region Warning dialog

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
            ShowDialog(title, message, PackIconKind.WarningOutline, DialogThemeType.Warning, identifier);
        }

        #endregion

        #region Loading dialog

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

        #endregion

        #region Short Info Dialog

        public void ShowShortInfo(string message)
        {
            ShowShortInfo("Information", message, AlertDialogHost);
        }
        
        public void ShowShortInfo(string title, string message)
        {
            ShowShortInfo(title, message, AlertDialogHost);
        }
        
        public void ShowShortInfo(string title, string message, string identifier)
        {
            ShowShort(title, message, DelayShortValue, DialogThemeType.Info, PackIconKind.InformationOutline, identifier);
        }
        
        #endregion

        #region Short Success Dialog
        
        public void ShowShortSuccess(string message)
        {
            ShowShortSuccess("Success", message, AlertDialogHost);
        }
        
        public void ShowShortSuccess(string title, string message)
        {
            ShowShortSuccess(title, message, AlertDialogHost);
        }
        
        public void ShowShortSuccess(string title, string message, string identifier)
        {
            ShowShort(title, message, DelayShortValue, DialogThemeType.Success, PackIconKind.CheckCircleOutline, identifier);
        }

        #endregion

        #region Short Warning Dialog

        public void ShowShortWarning(string message)
        {
            ShowShortWarning("Warning", message, AlertDialogHost);
        }
        
        public void ShowShortWarning(string title, string message)
        {
            ShowShortWarning(title, message, AlertDialogHost);
        }
        
        public void ShowShortWarning(string title, string message, string identifier)
        {
            ShowShort(title, message, DelayShortValue, DialogThemeType.Warning, PackIconKind.Warning, identifier);
        }

        #endregion

        #region Short Error Dialog

        public void ShowShortError(string message)
        {
            ShowShortError("Error", message, AlertDialogHost);
        }
        
        public void ShowShortError(string title, string message)
        {
            ShowShortError(title, message, AlertDialogHost);
        }

        public void ShowShortError(string title, string message, string identifier)
        {
            ShowShort(title, message, DelayShortValue, DialogThemeType.Error, PackIconKind.ErrorOutline, identifier);
        }

        #endregion

        #region Short Dialog

        public async void ShowShort(string title, string message, int delay = 1500, DialogThemeType theme = DialogThemeType.Default, PackIconKind icon = default, string identifier = null)
        {
            var content = ServiceLocator.Current.GetInstance<InfoContentView>();
            var dialogContent = ServiceLocator.Current.GetInstance<ContentDialogView>();
            content.Message = message;
            dialogContent.DialogContent = content;
            dialogContent.Title = title;
            content.Icon = icon;

            if (theme != DialogThemeType.None)
            {
                IDialogTheme dialogTheme = DialogThemeFactory.CreateTheme(theme);
                content.ThemeForeground = dialogTheme.GetForegroundBrush();
                dialogContent.ThemeBackground = dialogTheme.GetBackgroundBrush();
                dialogContent.ThemeForeground = dialogTheme.GetForegroundBrush();
            }
            
            await DialogHost.Show(dialogContent, identifier ?? AlertDialogHost, delegate(object sender, DialogOpenedEventArgs args)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Thread.Sleep(delay);
                    DispatcherHelper.CheckBeginInvokeOnUI(() => args.Session.Close(true));
                });
            });
        }

        #endregion

        #region Show Dialog

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

        public void ShowDialog(string title, string message, PackIconKind icon, DialogThemeType theme)
        {
            ShowDialog(title, message, icon, theme, AlertDialogHost);
        }

        public async void ShowDialog(string title, string message, PackIconKind icon, DialogThemeType theme,
            string identifier)
        {
            var content = ServiceLocator.Current.GetInstance<AlertDialogView>();
            if (theme != DialogThemeType.None)
            {
                IDialogTheme dialogTheme = DialogThemeFactory.CreateTheme(theme);
                content.ThemeBackground = dialogTheme.GetBackgroundBrush();
                content.ThemeForeground = dialogTheme.GetForegroundBrush();
            }
            content.Title = title;
            content.Message = message;
            content.Icon = icon;
            await DialogHost.Show(content, identifier);
        }

        #endregion

        #region Show Request Dialog

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

        #endregion

        #region Show File or Folder Dialog

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
            return response?.FirstOrDefault();
        }

        public IEnumerable<string> OpenMultiselectFileDialog(string title, IEnumerable<CommonFileDialogFilter> filters)
        {
            return OpenDialog(title, filters, true);
        }

        public string OpenFolderDialog(string title)
        {
            var response = OpenDialog(title, null, false, true);
            return response?.FirstOrDefault();
        }

        public IEnumerable<string> OpenMultiselectFolderDilaog(string title)
        {
            return OpenDialog(title, null, true, true);
        }

        #endregion

        #endregion
    }
}