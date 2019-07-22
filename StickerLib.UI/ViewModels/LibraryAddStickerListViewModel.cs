using GalaSoft.MvvmLight;
using StickerLib.UI.Common.Services;

namespace StickerLib.UI.ViewModels
{
    public class LibraryAddStickerListViewModel : ViewModelLibrary
    {
        private readonly IDialog _dialog;

        public LibraryAddStickerListViewModel(IDialog dialog)
        {
            _dialog = dialog;
            _dialog.AlertDialogHost = "AlertLibraryDialogHost";
            _dialog.LoadingDialogHost = "LoadingLibraryDialogHost";
        }
    }
}