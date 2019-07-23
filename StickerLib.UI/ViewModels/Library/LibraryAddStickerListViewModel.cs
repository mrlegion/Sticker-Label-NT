using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using Microsoft.WindowsAPICodePack.Dialogs;
using StickerLib.UI.Common.Services;

namespace StickerLib.UI.ViewModels.Library
{
    public class LibraryAddStickerListViewModel : ViewModelLibrary
    {

        public LibraryAddStickerListViewModel(IDialog dialog) : base(dialog)
        {
        }

        private RelayCommand<string> _selectFileCommand;

        public RelayCommand<string> SelectFileCommand
        {
            get
            {
                return _selectFileCommand ?? (_selectFileCommand = new RelayCommand<string>((type) =>
                {
                    if (IsEmpty(type)) return;
                    var file = Dialog.OpenFileDialog("Select file for Titles",
                        new[] {new CommonFileDialogFilter("CSV file", "csv"),});
                    Dialog.ShowInfo("Select file", file);
                }));
            }
        }

        private bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        private ObservableCollection<Item> _collection;

        private ReadOnlyObservableCollection<Item> _stickers;

        public ReadOnlyObservableCollection<Item> Stickers
        {
            get { return _stickers; }
            set { Set(nameof(Stickers), ref _stickers, value); }
        }

        private RelayCommand<Item> _deleteCommand;

        public RelayCommand<Item> DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand<Item>(async (item) =>
                {
                    if (item != null)
                    {
                        var response = await Dialog.ShowRequest("Deleted item",
                            $"You really want deleted selected item: \"{item.Title}\"",
                            "Deleted", "Cancel");
                        if (response)
                        {
                            _collection.Remove(item);
                            Stickers = new ReadOnlyObservableCollection<Item>(_collection);
                        }
                        
                    }
                }));
            }
        }

        private IEnumerable<int> _pages;

        public IEnumerable<int> Pages
        {
            get { return _pages; }
            set { Set(nameof(Pages), ref _pages, value); }
        }

        public class Item
        {
            public string Title { get; }
            public int PageNumber { get; set; }

            public Item(string title, int pageNumber = 0)
            {
                Title = title;
                PageNumber = pageNumber;
            }
        }

        public class PageItem
        {
            public byte[] Content { get; }
            public int Number { get; }

            public PageItem(int number, byte[] content)
            {
                Content = content;
                Number = number;
            }
        }
    }
}