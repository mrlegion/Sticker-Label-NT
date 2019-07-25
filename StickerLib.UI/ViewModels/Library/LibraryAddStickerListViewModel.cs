using CommonServiceLocator;

using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;

using Microsoft.WindowsAPICodePack.Dialogs;

using StickerLib.Domain.Helpers;
using StickerLib.Domain.Services;
using StickerLib.Infrastructure.Common;
using StickerLib.UI.Common.Services;

using System;
using System.Collections.ObjectModel;
using System.IO;

namespace StickerLib.UI.ViewModels.Library
{
    public class LibraryAddStickerListViewModel : ViewModelLibrary
    {
        #region Fields

        private string _titlesFile;
        private string _stickersFile;
        private string _titleButtonText;
        private string _stickerButtonText;
        private RelayCommand _selectTitleFileCommand;
        private RelayCommand _selectStickerFileCommand;
        private RelayCommand _loadCommand;
        private RelayCommand<Title> _deleteCommand;
        private ObservableCollection<int> _pages;
        private ObservableCollection<Title> _titles;
        private string _loadButtonTooltip;
        private string _acceptButtonTooltip;
        private RelayCommand _acceptCommand;

        #endregion

        #region Ctor

        public LibraryAddStickerListViewModel(IDialog dialog) : base(dialog)
        {
            TitleButtonText = "Select File With Titles";
            StickerButtonText = "Select File With Stickers";
            LoadButtonTooltip = "For Activate: First you need select files for \"Titles\" and \"Stickers\"";
            AcceptButtonTooltip = "For Activate: You need loading data from files using button \"Load\"";
            Pages = new ObservableCollection<int>();
        }

        #endregion

        #region Properties

        public string TitleButtonText
        {
            get { return _titleButtonText; }
            set { Set(nameof(TitleButtonText), ref _titleButtonText, value); }
        }

        public string StickerButtonText
        {
            get { return _stickerButtonText; }
            set { Set(nameof(StickerButtonText), ref _stickerButtonText, value); }
        }

        public RelayCommand SelectTitleFileCommand
        {
            get
            {
                return _selectTitleFileCommand ?? (_selectTitleFileCommand = new RelayCommand(() =>
                {
                    var file = Dialog.OpenFileDialog("Select Titles file",
                        new[]
                        {
                            new CommonFileDialogFilter("Titles file", "*.txt, *.csv"),
                        });

                    if (file == null) return;
                    _titlesFile = file;
                    TitleButtonText = "File Is Selected";
                    LoadCommand.RaiseCanExecuteChanged();
                }));
            }
        }

        public RelayCommand SelectStickerFileCommand
        {
            get
            {
                return _selectStickerFileCommand ?? (_selectStickerFileCommand = new RelayCommand(() =>
                {
                    var file = Dialog.OpenFileDialog("Select Stickers pdf file",
                        new[]
                        {
                            new CommonFileDialogFilter("PDF file", "pdf"),
                        });

                    if (file == null) return;
                    _stickersFile = file;
                    StickerButtonText = "File Is Selected";
                    LoadCommand.RaiseCanExecuteChanged();

                    // Fill pages collection
                    try
                    {
                        Dialog.ShowLoading("Inizialize..", () =>
                        {
                            int count = PdfReadHelper.CountPageInDocument(file);
                            for (int i = 1; i <= count; i++)
                                Pages.Add(i);
                        });
                    }
                    catch (Exception e)
                    {
                        Dialog.ShowError("Error select sticker file", e.Message);
                        return;
                    }
                }));
            }
        }

        public RelayCommand LoadCommand
        {
            get
            {
                return _loadCommand ?? (_loadCommand = new RelayCommand(() =>
                {
                    try
                    {
                        var t = FileReaderHelper.ReadTitlesFile(_titlesFile);
                        Titles = new ObservableCollection<Title>(t);
                        AcceptCommand.RaiseCanExecuteChanged();
                    }
                    catch (Exception e)
                    {
                        Dialog.ShowError("Error in Loading", e.Message);
                        return;
                    }
                }, () => !IsEmpty(_titlesFile) && !IsEmpty(_stickersFile)));
            }
        }

        public RelayCommand<Title> DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new RelayCommand<Title>(async (title) =>
                {
                    if (title != null)
                    {
                        bool response = await Dialog.ShowRequest("Delete element",
                            $"You really want delete selected sticker \"{title.Name}\"",
                            "Delete", "Cancel");

                        if (response)
                            Titles.Remove(title);

                        Dialog.ShowShortSuccess("Deleted success!", "Deleted select item is success!");
                    }
                }));
            }
        }

        public ObservableCollection<int> Pages
        {
            get { return _pages; }
            set { Set(nameof(Pages), ref _pages, value); }
        }

        public ObservableCollection<Title> Titles
        {
            get { return _titles; }
            set { Set(nameof(Titles), ref _titles, value); }
        }

        public string LoadButtonTooltip
        {
            get { return _loadButtonTooltip; }
            set { Set(nameof(LoadButtonTooltip), ref _loadButtonTooltip, value); }
        }

        public string AcceptButtonTooltip
        {
            get { return _acceptButtonTooltip; }
            set { Set(nameof(AcceptButtonTooltip), ref _acceptButtonTooltip, value); }
        }

        public RelayCommand AcceptCommand
        {
            get { return _acceptCommand ?? (_acceptCommand = new RelayCommand(() =>
            {
                if (!File.Exists(_stickersFile))
                {
                    Dialog.ShowError("Marge Operation Error", "File with stickers not exist!\r\nPlease check your select file exist on hard drive?");
                    return;
                }

                if (Titles.Count == 0)
                {
                    Dialog.ShowWarning("Marge Operation Warning", "Marging items cannot be empty. \r\nIn magring list not found items");
                    return;
                }

                Dialog.ShowLoading("Adding stickers to Database", () =>
                {
                    try
                    {
                        var factory = ServiceLocator.Current.GetInstance<StickerFactory>();
                        var stickers = factory.Create(Titles, _stickersFile);
                        var service = ServiceLocator.Current.GetInstance<IStickerService>();
                        service.AddRange(stickers);

                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            Dialog.ShowShortSuccess("Saving success", "Success adding new Stickers to Database");
                            NavigationService.NavigateTo("library");
                        });
                    }
                    catch (Exception e)
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            Dialog.ShowError("Error Saving Stickers", e.Message);
                        });
                    }
                });
            }, () => !IsEmpty(_stickersFile) && Titles != null)); }
        }

        #endregion

        #region Private methods

        private bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        #endregion
    }
}