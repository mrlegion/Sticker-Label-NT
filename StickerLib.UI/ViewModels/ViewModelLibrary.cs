using System.Windows;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using StickerLib.UI.Common.Services;
using StickerLib.UI.Views;

namespace StickerLib.UI.ViewModels
{
    public class ViewModelLibrary : ViewModelCustom
    {
        public ViewModelLibrary()
        {
            NavigationService = ServiceLocator.Current.GetInstance<IFrameNavigationService>("Library");
            Window main = Application.Current.MainWindow;
            if (main != null)
                foreach (Window window in main.OwnedWindows)
                    if (window.GetType() == typeof(LibraryWindow))
                        NavigationService.MainWindow = (LibraryWindow)window;
        }
    }
}