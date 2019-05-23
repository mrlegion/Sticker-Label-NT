using System;
using System.Windows.Controls;

namespace StickerLib.UI.Common.Services
{
    public interface IDialog
    {
        void ShowInfo(string message);
        void ShowSuccess(string message);
        void ShowError(string message);
        void ShowWarning(string message);
        bool ShowRequest(string message);
        void ShowLoading(string message, Action callback);

        void ShowDialog(UserControl content, string title);
    }
}