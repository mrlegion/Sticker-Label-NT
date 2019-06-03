using System.Windows;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Command;
using StickerLib.UI.Common.Services;
using StickerLib.UI.Views;
using StickerLib.UI.Views.Pages;

namespace StickerLib.UI.ViewModels
{
    public class ShellViewModel : ViewModelCustom
    {
        public ShellViewModel()
        {
            Title = Properties.Settings.Default.TitleForApplication;
        }
        
        private string _title;

        public string Title
        {
            get { return _title; }
            set { Set(nameof(Title), ref _title, value); }
        }

        private RelayCommand _openPreferenceCommand;

        public RelayCommand OpenPreferenceCommand
        {
            get
            {
                return _openPreferenceCommand ?? (_openPreferenceCommand = new RelayCommand(() =>
                {
                    Window preference = ServiceLocator.Current.GetInstance<PreferenceWindow>();
                    preference.Owner = Application.Current.MainWindow;
                    preference.ShowDialog();
                }));
            }
        }

        private RelayCommand _openLibraryCommand;

        public RelayCommand OpenLibraryCommand
        {
            get
            {
                return _openLibraryCommand ?? (_openLibraryCommand = new RelayCommand(() =>
                {
                    LibraryWindow library = ServiceLocator.Current.GetInstance<LibraryWindow>();
                    library.Owner = Application.Current.MainWindow;
                    library.DataContext = ServiceLocator.Current.GetInstance<LibraryWindowViewModel>();
                    library.ShowDialog();
                }));
            }
        }
    }
}