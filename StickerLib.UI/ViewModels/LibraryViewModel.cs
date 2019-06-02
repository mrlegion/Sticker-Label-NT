using System.Collections.ObjectModel;
using System.Windows;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using StickerLib.Domain.Services;
using StickerLib.Infrastructure.Entities;
using StickerLib.UI.Common.Services;
using StickerLib.UI.Views;

namespace StickerLib.UI.ViewModels
{
    public class LibraryViewModel : ViewModelCustom
    {
        private readonly IDialog _dialog;

        public LibraryViewModel(IDialog dialog)
        {
            _dialog = dialog;
            NavigationService = ServiceLocator.Current.GetInstance<IFrameNavigationService>("Library");
            _stickers = new ObservableCollection<Sticker>();

            OnLoadData();
        }

        private void OnLoadData()
        {
            _dialog.ShowLoading("Load data for database..", () =>
            {
                var service = ServiceLocator.Current.GetInstance<IStickerService>();
                Stickers = new ObservableCollection<Sticker>(service.GetAll());
            });
        }

        private ObservableCollection<Sticker> _stickers;

        public ObservableCollection<Sticker> Stickers
        {
            get { return _stickers; }
            set { Set(nameof(Stickers), ref _stickers, value); }
        }

        private RelayCommand _closeWindowCommand;

        public RelayCommand CloseWindowCommand
        {
            get
            {
                return _closeWindowCommand ?? (_closeWindowCommand = new RelayCommand(() =>
                {
                    var main = Application.Current.MainWindow;
                    if (main != null)
                        foreach (Window owned in main.OwnedWindows)
                            if (owned.GetType() == typeof(LibraryWindow))
                                owned.Close();
                }));
            }
        }
    }
}