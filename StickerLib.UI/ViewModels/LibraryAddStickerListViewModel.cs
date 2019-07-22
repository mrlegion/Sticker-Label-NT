using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.WindowsAPICodePack.Dialogs;
using StickerLib.UI.Common.Services;

namespace StickerLib.UI.ViewModels
{
    public class LibraryAddStickerListViewModel : ViewModelLibrary
    {
        private readonly IDialog _dialog;

        public LibraryAddStickerListViewModel(IDialog dialog)
        {
            _dialog = dialog;
            _dialog.AlertDialogHost = "AlertLibraryDialogHost";
            _dialog.LoadingDialogHost = "LoadingLibraryDialogHost";
        }

        private RelayCommand<string> _selectFileCommand;

        public RelayCommand<string> SelectFileCommand
        {
            get
            {
                return _selectFileCommand ?? (_selectFileCommand = new RelayCommand<string>((type) =>
                {
                    if (IsEmpty(type)) return;
                    var dialog = OpenFileDialog("Select Titles file", false,
                        new[] { new CommonFileDialogFilter("CVS file", "cvs") });


                }));
            }
        }

        private CommonOpenFileDialog OpenFileDialog(string title, bool multiselect, CommonFileDialogFilter[] filters)
        {
            var dialog = new CommonOpenFileDialog()
            {
                Title = title,
                Multiselect = multiselect,
            };

            foreach (var filter in filters)
                dialog.Filters.Add(filter);

            return (dialog.ShowDialog() == CommonFileDialogResult.Ok) ? dialog : null;
        }

        private bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }
    }
}