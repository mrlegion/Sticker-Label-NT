using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using StickerLib.UI.Common.Helpers;
using StickerLib.UI.Common.Services;

namespace StickerLib.UI.ViewModels.Library
{
    public class LibraryWindowViewModel : ViewModelLibrary
    {
        public LibraryWindowViewModel(IDialog dialog) : base(dialog)
        {
            
        }

        private RelayCommand _closeEventCommand;

        public RelayCommand CloseEventCommand
        {
            get
            {
                return _closeEventCommand ?? (_closeEventCommand = new RelayCommand(() =>
                {
                    Messenger.Default.Send(new NotificationMessage<CloseEventType>(CloseEventType.Update, "Update sticker list"));
                }));
            }
        }
    }
}