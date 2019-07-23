using System;
using System.Windows;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StickerLib.UI.Common.Services;
using StickerLib.UI.Views;

namespace StickerLib.UI.ViewModels.Group
{
    public class GroupEditViewModel : ViewModelBase
    {
        private RelayCommand<string> _navigateToCommand;

        public RelayCommand<string> NavigateToCommand
        {
            get
            {
                return _navigateToCommand ?? (_navigateToCommand = new RelayCommand<string>((page) =>
                {
                    var navigate = ServiceLocator.Current.GetInstance<IFrameNavigationService>("Group");
                    // get group window
                    Window main = Application.Current.MainWindow;
                    if (main == null) throw new ArgumentNullException(nameof(main));
                    foreach (Window owned in main.OwnedWindows)
                        if (owned.GetType() == typeof(GroupWindow))
                            navigate.MainWindow = owned;
                    navigate.NavigateTo(page);
                }));
            }
        }
    }
}