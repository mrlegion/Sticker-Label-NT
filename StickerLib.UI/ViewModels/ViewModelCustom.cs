using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StickerLib.UI.Common.Services;

namespace StickerLib.UI.ViewModels
{
    public class ViewModelCustom : ViewModelBase
    {
        protected readonly IFrameNavigationService NavigationService;

        public ViewModelCustom(IFrameNavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        private RelayCommand<string> _navigateToCommand;

        public RelayCommand<string> NavigateToCommand
        {
            get
            {
                return _navigateToCommand ?? (_navigateToCommand = new RelayCommand<string>((page) =>
                {
                    if (string.IsNullOrEmpty(page) || string.IsNullOrWhiteSpace(page))
                        throw new ArgumentNullException(nameof(page), "Navigation page name cannot be empty or null!");

                    NavigationService.NavigateTo(page);
                }));
            }
        }

        private RelayCommand _goBackCommand;

        public RelayCommand GoBackCommand
        {
            get
            {
                return _goBackCommand ?? (_goBackCommand = new RelayCommand(() =>
                {
                    if (NavigationService.CanGoBack)
                        NavigationService.GoBack();
                }));
            }
        }
    }
}