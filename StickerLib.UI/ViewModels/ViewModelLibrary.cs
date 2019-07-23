using System.Windows;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using StickerLib.UI.Common.Services;
using StickerLib.UI.Views;

namespace StickerLib.UI.ViewModels
{
    public class ViewModelLibrary : ViewModelCustom
    {
        protected readonly IDialog Dialog;

        public ViewModelLibrary(IDialog dialog)
        {
            Dialog = dialog;
            dialog.AlertDialogHost = "AlertLibraryDialogHost";
            dialog.LoadingDialogHost = "LoadingLibraryDialogHost";
            
            
            NavigationService = ServiceLocator.Current.GetInstance<IFrameNavigationService>("Library");
            Window main = Application.Current.MainWindow;
            if (main != null)
                foreach (Window window in main.OwnedWindows)
                    if (window.GetType() == typeof(LibraryWindow))
                        NavigationService.MainWindow = (LibraryWindow)window;
        }
    }
}