using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StickerLib.UI.Common.Services;

namespace StickerLib.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDialog _dialogService;

        public MainViewModel(IDialog dialog)
        {
            _dialogService = dialog;
        }

        private RelayCommand<string> _showDialogCommand;

        public RelayCommand<string> ShowDialogCommand
        {
            get
            {
                return _showDialogCommand ?? (_showDialogCommand = new RelayCommand<string>((type) =>
                {
                    switch (type)
                    {
                        case "info":
                            _dialogService.ShowInfo("Test info", "Someone message");
                            break;
                        case "success":
                            _dialogService.ShowSuccess(null, "This dialog without title");
                            break;
                        case "warning":
                            _dialogService.ShowWarning("test warning", "Warning message");
                            break;
                        case "error":
                            _dialogService.ShowError("Error dialog without text", null);
                            break;
                    }
                }));
            }
        }
    }
}