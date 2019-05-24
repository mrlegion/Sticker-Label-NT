using System;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace StickerLib.UI.Common.Services
{
    public interface IDialog
    {
        void ShowInfo(string title, string message);
        void ShowSuccess(string title, string message);
        void ShowError(string title, string message);
        void ShowWarning(string title, string message);
        bool ShowRequest(string message);
        void ShowLoading(string message, Action callback);

        void ShowDialog(UserControl content, string title);
        void ShowDialog(string title, string message, PackIconKind icon, SolidColorBrush theme);
    }
}