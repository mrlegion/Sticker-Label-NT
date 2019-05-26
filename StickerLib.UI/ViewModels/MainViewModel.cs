using System.Threading;
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

        public RelayCommand<string> ShowAlertDialogCommand
        {
            get
            {
                return _showDialogCommand ?? (_showDialogCommand = new RelayCommand<string>((type) =>
                {
                    switch (type)
                    {
                        case "info":
                            _dialogService.ShowInfo("Тестирование инфо диалога", "Someone message");
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

        private RelayCommand<string> _showQuestionDialogCommand;

        public RelayCommand<string> ShowQuestionDialogCommand
        {
            get
            {
                return _showQuestionDialogCommand ?? (_showQuestionDialogCommand = new RelayCommand<string>((type) =>
                {
                    switch (type)
                    {
                        case "true":
                            _dialogService.ShowRequest("You realy need this view?",
                                "Here use default view and content for control buttons");
                            break;
                        case "false":
                            _dialogService.ShowRequest("How you to this view?", "Here using custom naming to buttons",
                                "Cool!", "FUUUUUUU!");
                            break;
                    }
                }));
            }
        }

        private RelayCommand _showLoadingDialogCommand;

        public RelayCommand ShowLoadingDialogCommand
        {
            get
            {
                return _showLoadingDialogCommand ?? (_showLoadingDialogCommand = new RelayCommand(() =>
                {
                    _dialogService.ShowLoading("Example loading dialog message..", () => Thread.Sleep(2000));
                }));
            }
        }
    }
}