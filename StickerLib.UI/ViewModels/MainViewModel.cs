using System;
using System.Collections;
using System.Collections.Generic;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using StickerLib.UI.Common.Services;
using System.Collections.ObjectModel;
using StickerLib.Infrastructure.Entities;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using StickerLib.Domain.Build;
using StickerLib.Domain.Services;
using StickerLib.Infrastructure;

namespace StickerLib.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Sticker> _stickers;
        private readonly IDialog _dialog;
        private ICollectionView _filteredItems;
        private string _searchName;
        private readonly IGroupService _groups;

        public MainViewModel(IDialog dialog, IGroupService groups)
        {
            _dialog = dialog;
            _groups = groups;

            Count = 100;
            CountList = GenereteCountList(50, 20);

            CViewSource = new CollectionViewSource();
            CViewSource.Filter += NamingFilter;

            OnLoadData();
        }

        private void OnLoadData()
        {
            _dialog.ShowLoading("Load data..", () =>
            {
                var service = ServiceLocator.Current.GetInstance<IStickerService>();
                var all = service.GetAll();
                _stickers = new ObservableCollection<Sticker>(all);
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    CViewSource.Source = _stickers;
                    FilteredItems = CViewSource.View;
                });
            });
        }

        private int[] GenereteCountList(int step, int count)
        {
            var list = new List<int>();
            for (int i = 1; i <= count; i++)
                list.Add(i * step);
            return list.ToArray();
        }

        private CollectionViewSource CViewSource { get; }

        public ICollectionView FilteredItems
        {
            get { return _filteredItems; }
            set { Set(nameof(FilteredItems), ref _filteredItems, value); }
        }

        public string SearchName 
        {
            get { return _searchName; }
            set 
            { 
                Set(nameof(SearchName), ref _searchName, value); 
                CViewSource.View.Refresh();
            }
        }

        private void NamingFilter(object sender, FilterEventArgs args)
        {
            if (args.Item is Sticker sticker)
                if (string.IsNullOrWhiteSpace(SearchName) || SearchName.Length == 0)
                    args.Accepted = true;
                else
                    args.Accepted = (sticker.Name.IndexOf(SearchName, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private int _count;

        public int Count
        {
            get { return _count; }
            set { Set(nameof(Count), ref _count, value); }
        }

        private int[] _countList;

        public int[] CountList
        {
            get { return _countList; }
            set { Set(nameof(CountList), ref _countList, value); }
        }

        private RelayCommand<ListView> _addGroupCommand;

        public RelayCommand<ListView> AddGroupCommand
        {
            get
            {
                return _addGroupCommand ?? (_addGroupCommand = new RelayCommand<ListView>((list) =>
                {
                    if (list == null)
                    {
                        _dialog.ShowError("Cannot resolve object", "Sending object in command to adding group cannot be resolved, please check your code!");
                        return;
                    }

                    var group = new Group("Sticker group for " + Count + " counts", Count,
                        new List<Sticker>(list.SelectedItems.Cast<Sticker>()));
                    _groups.Add(group);
                    // clear selection
                    list.UnselectAll();
                }));
            }
        }

        private IList _selectedItems;
        public IList SelectedItems
        {
            get { return _selectedItems; }
            set { Set(nameof(SelectedItems), ref _selectedItems, value); }
        }

        public ObservableCollection<Group> Groups
        {
            get { return _groups.Groups; }
        }

        private RelayCommand<ListView> _clearAllCommand;

        public RelayCommand<ListView> ClearAllCommand
        {
            get
            {
                return _clearAllCommand ?? (_clearAllCommand = new RelayCommand<ListView>(async (list) =>
                {
                    bool request = await _dialog.ShowRequest("Clear all data",
                        "You really want clear all data? \rUnselect all select stickers\rClear all group collection", "Clear", "Cancel");
                    if (!request) return;
                    _groups.Clear();
                    list.UnselectAll();
                    Count = 100;
                    SearchName = string.Empty;
                }));
            }
        }

        private bool _useShuffle;

        public bool UseShuffle
        {
            get { return _useShuffle; }
            set { Set(nameof(UseShuffle), ref _useShuffle, value); }
        }

        private bool _toPrintPage;

        public bool ToPrintPage
        {
            get { return _toPrintPage; }
            set { Set(nameof(ToPrintPage), ref _toPrintPage, value); }
        }

        private RelayCommand _collectFileCommand;

        public RelayCommand CollectFileCommand
        {
            get
            {
                return _collectFileCommand ?? (_collectFileCommand = new RelayCommand(() =>
                {
                    _dialog.ShowLoading("Collect sticker files..", () =>
                    {
                        var creator = ServiceLocator.Current.GetInstance<Creator>();
                        creator.Create(new List<Group>(_groups.Groups));
                    });
                }));
            }
        }
    }
}