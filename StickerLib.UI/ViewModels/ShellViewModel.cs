using StickerLib.UI.Common.Services;

namespace StickerLib.UI.ViewModels
{
    public class ShellViewModel : ViewModelCustom
    {
        public ShellViewModel(IFrameNavigationService navigationService) : base(navigationService) 
        {
            Title = Properties.Settings.Default.TitleForApplication;
        }
        
        private string _title;

        public string Title
        {
            get { return _title; }
            set { Set(nameof(Title), ref _title, value); }
        }
    }
}