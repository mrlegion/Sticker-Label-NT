using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading;
using GalaSoft.MvvmLight.Command;
using Microsoft.WindowsAPICodePack.Dialogs;
using StickerLib.Domain.Common;
using StickerLib.Domain.Helpers;
using StickerLib.UI.Common.Services;

namespace StickerLib.UI.ViewModels.Library
{
    public class LibraryAddStickerListViewModel : ViewModelLibrary
    {
        private string _titlesFile;
        private string _stickersFile;

        public LibraryAddStickerListViewModel(IDialog dialog) : base(dialog)
        {
            TitleButtonText = "Select File With Titles";
            StickerButtonText = "Select File With Stickers";
            Pages = new ObservableCollection<int>();
        }

        private string _titleButtonText;

        public string TitleButtonText
        {
            get { return _titleButtonText; }
            set { Set(nameof(TitleButtonText), ref _titleButtonText, value); }
        }

        private string _stickerButtonText;

        public string StickerButtonText
        {
            get { return _stickerButtonText; }
            set { Set(nameof(StickerButtonText), ref _stickerButtonText, value); }
        }

        private RelayCommand _selectTitleFileCommand;

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

        private RelayCommand _selectStickerFileCommand;

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

        private RelayCommand _loadCommand;

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
                    }
                    catch (Exception e)
                    {
                        Dialog.ShowError("Error in Loading", e.Message);
                        return;
                    }
                }, () => !IsEmpty(_titlesFile) && !IsEmpty(_stickersFile)));
            }
        }

        private RelayCommand<Title> _deleteCommand;

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
                        
                        Dialog.ShowInfoTiming("Deleted success!", "Deleted select item is success!");
                    }
                        
                }));
            }
        }

        private ObservableCollection<int> _pages;

        public ObservableCollection<int> Pages
        {
            get { return _pages; }
            set { Set(nameof(Pages), ref _pages, value); }
        }
        
        private ObservableCollection<Title> _titles;

        public ObservableCollection<Title> Titles
        {
            get { return _titles; }
            set { Set(nameof(Titles), ref _titles, value); }
        }

        private bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }
    }
}