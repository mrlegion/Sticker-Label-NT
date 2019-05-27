using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace StickerLib.UI.Common.Services
{
    public interface IDialog
    {
        void ShowInfo(string title, string message);
        void ShowInfo(string title, string message, string identifier);
        void ShowSuccess(string title, string message);
        void ShowSuccess(string title, string message, string identifier);
        void ShowError(string title, string message);
        void ShowError(string title, string message, string identifier);
        void ShowWarning(string title, string message);
        void ShowWarning(string title, string message, string identifier);

        Task<bool> ShowRequest(string title, string message);
        Task<bool> ShowRequest(string title, string message, string identifier);
        Task<bool> ShowRequest(string title, string message, string positiveButtonTitle, string negativeButtonTitle);

        Task<bool> ShowRequest(string title, string message, string positiveButtonTitle, string negativeButtonTitle,
            string identifier);
        void ShowLoading(string message, Action callback);
        void ShowLoading(string message, Action callback, string identifier);
        void ShowDialog(UserControl content);
        void ShowDialog(UserControl content, string title);
        void ShowDialog(UserControl content, string title, string identifier);
        void ShowDialog(string title, string message, PackIconKind icon, SolidColorBrush theme);
        void ShowDialog(string title, string message, PackIconKind icon, SolidColorBrush theme, string identifier);
    }
}