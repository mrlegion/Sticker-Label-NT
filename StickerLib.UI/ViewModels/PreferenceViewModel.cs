using System;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace StickerLib.UI.ViewModels
{
    public class PreferenceViewModel : ViewModelBase
    {
        private RelayCommand<Window> _closeWindowCommand;

        public RelayCommand<Window> CloseWindowCommand
        {
            get
            {
                return _closeWindowCommand ?? (_closeWindowCommand = new RelayCommand<Window>((w) =>
                {
                    if (w == null) throw new ArgumentNullException(nameof(w), "Select window element has null");
                    w.Close();
                }));
            }
        }
    }
}