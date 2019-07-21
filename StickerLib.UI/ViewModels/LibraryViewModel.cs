using System.Collections.ObjectModel;
using System.Windows;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using StickerLib.Domain.Services;
using StickerLib.Infrastructure.Entities;
using StickerLib.UI.Common.Services;
using StickerLib.UI.Views;

namespace StickerLib.UI.ViewModels
{
    public class LibraryViewModel : ViewModelLibrary
    {
        #region Fields

        private readonly IDialog _dialog;

        private ObservableCollection<Sticker> _stickers;

        private RelayCommand _closeWindowCommand;

        private RelayCommand<Sticker> _deleteCommand;

        #endregion

        #region Constructor

        public LibraryViewModel(IDialog dialog)
        {
            _dialog = dialog;
            OnLoadData();
        }

        #endregion

        #region Properties

        public ObservableCollection<Sticker> Stickers
        {
            get { return _stickers; }
            set { Set(nameof(Stickers), ref _stickers, value); }
        }

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

        public RelayCommand<Sticker> DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new RelayCommand<Sticker>(async (sticker) =>
                {
                    if (sticker == null)
                    {
                        _dialog.ShowWarning("Sticker is not selected", "Please check selected sticker for deleted!");
                        return;
                    }

                    var response = await _dialog.ShowRequest("Delete sticker",
                        $"You really want deleted {sticker.Name} sticker?", "Delete", "Cancel");

                    if (response)
                    {
                        _dialog.ShowLoading($"Deleted stickers: {sticker.Name}", () =>
                        {
                            var service = ServiceLocator.Current.GetInstance<IStickerService>();
                            if (service.Delete(sticker))
                            {
                                Stickers = new ObservableCollection<Sticker>(service.GetAll());
                                DispatcherHelper.CheckBeginInvokeOnUI(() => _dialog.ShowSuccess("Deleted success", "Sticker is success deleted!"));
                            }
                        });
                    }
                }));
            }
        }

        #endregion

        #region Private methods

        private void OnLoadData ()
        {
            _dialog.ShowLoading("Load data for database..", () =>
            {
                var service = ServiceLocator.Current.GetInstance<IStickerService>();
                Stickers = new ObservableCollection<Sticker>(service.GetAll());
            });
        }

        #endregion
    }
}