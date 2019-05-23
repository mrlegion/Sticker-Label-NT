using System;
using System.Windows.Controls;
using System.Windows.Media;
using CommonServiceLocator;
using MaterialDesignThemes.Wpf;
using StickerLib.UI.Common.Dialogs.Views;

namespace StickerLib.UI.Common.Services
{
    public class DialogService : IDialog
    {
        public void ShowInfo(string message)
        {
            throw new NotImplementedException();
        }

        public void ShowSuccess(string message)
        {
            throw new NotImplementedException();
        }

        public void ShowError(string message)
        {
            throw new NotImplementedException();
        }

        public void ShowWarning(string message)
        {
            throw new NotImplementedException();
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

        public async void ShowDialog(string message, PackIcon icon, SolidColorBrush theme)
        {
            AlertDialogView content = ServiceLocator.Current.GetInstance<AlertDialogView>();
            await DialogHost.Show(content, "RootDialogHost");
        }
    }
}