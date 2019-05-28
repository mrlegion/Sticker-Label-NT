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
using System.Windows.Data;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
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
            _dialog.ShowLoading("Load data..", OnLoad);
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

        private void OnLoad()
        {
            var service = ServiceLocator.Current.GetInstance<IStickerService>();
            var all = service.GetAll();
            _stickers = new ObservableCollection<Sticker>(all);
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                CViewSource.Source = _stickers;
                FilteredItems = CViewSource.View;
            });
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

        private RelayCommand _addGroupCommand;

        public RelayCommand AddGroupCommand
        {
            get
            {
                return _addGroupCommand ?? (_addGroupCommand = new RelayCommand(() =>
                {
                    var group = new Group("Stickers group for " + Count, Count, SelectedItems.Cast<Sticker>());
                    _groups.Add(group);
                }));
            }
        }

        private IList _selectedItems;
        public IList SelectedItems
        {
            get { return _selectedItems; }
            set { Set(nameof(SelectedItems), ref _selectedItems, value); }
        }

    }
}