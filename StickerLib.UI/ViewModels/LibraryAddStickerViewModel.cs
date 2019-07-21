using System;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Microsoft.WindowsAPICodePack.Dialogs;
using StickerLib.Domain.Helpers;
using StickerLib.Domain.Services;
using StickerLib.Infrastructure.Entities;
using StickerLib.UI.Common.Services;

namespace StickerLib.UI.ViewModels
{
    public class LibraryAddStickerViewModel : ViewModelLibrary
    {
        #region Fields

        private const string FileIsNotSelected = "File is not selected, please select file!";
        private string _name;
        private readonly IDialog _dialog;
        private string _titleForButton;
        private bool _useFileName;
        private string _file;
        private RelayCommand _selectPdfFileCommand;
        private RelayCommand _clearSelectFileCommand;
        private RelayCommand _acceptCommand;

        #endregion

        #region Properties

        public string TitleForButton
        {
            get => _titleForButton;
            set => Set(nameof(TitleForButton), ref _titleForButton, value);
        }

        public bool UseFileName
        {
            get => _useFileName;
            set
            {
                Set(nameof(UseFileName), ref _useFileName, value);
                if (UseFileName)
                    Name = FileIsEmpty()
                        ? FileIsNotSelected
                        : System.IO.Path.GetFileNameWithoutExtension(File);
                else Name = "";
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (UseFileName)
                    if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                        value = FileIsNotSelected;
                Set(nameof(Name), ref _name, value);
            }
        }

        public string File
        {
            get => _file;
            set => Set(nameof(File), ref _file, value);
        }

        public RelayCommand SelectPdfFileCommand
        {
            get
            {
                return _selectPdfFileCommand ?? (_selectPdfFileCommand = new RelayCommand(() =>
                {
                    var dialog = new CommonOpenFileDialog()
                    {
                        Title = "Select PDF file for new sticker",
                        Multiselect = false,
                        Filters = {new CommonFileDialogFilter("Pdf files", "pdf")},
                        DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    };
                    if (dialog.ShowDialog() != CommonFileDialogResult.Ok) return;
                    File = dialog.FileName;
                    TitleForButton = "File is selected";
                    if (UseFileName)
                        Name = System.IO.Path.GetFileNameWithoutExtension(File);

                    ClearSelectFileCommand.RaiseCanExecuteChanged();
                }));
            }
        }

        public RelayCommand ClearSelectFileCommand
        {
            get
            {
                return _clearSelectFileCommand ?? (_clearSelectFileCommand = new RelayCommand(() =>
                {
                    File = "";
                    Name = "";
                    TitleForButton = "Select PDF file for sticker";
                    ClearSelectFileCommand.RaiseCanExecuteChanged();
                }, () => !FileIsEmpty()));
            }
        }

        public RelayCommand AcceptCommand
        {
            get
            {
                return _acceptCommand ?? (_acceptCommand = new RelayCommand(() =>
                {
                    if (FileIsEmpty())
                    {
                        _dialog.ShowError("File is Empty",
                            "PDF file for new sticker is not selected! Please click on button \"Select PDF file for sticker\" and select it");
                        return;
                    }

                    if (!System.IO.File.Exists(File))
                    {
                        _dialog.ShowError("Selected file is not found",
                            "Selected file for new sticker not found! Please check path or file on disk");
                        return;
                    }

                    if (NameIsEmpty())
                    {
                        _dialog.ShowError("Sticker name is Empty",
                            $"Field \"Name\" cannot be empty! Please, enter name for adding sticker!");
                        return;
                    }

                    _dialog.ShowLoading("Saving sticker in db...", () =>
                    {
                        var sticker = new Sticker(Name, PdfReadHelper.ReadToStreamByte(File));
                        var service = ServiceLocator.Current.GetInstance<IStickerService>();
                        service.Add(sticker);
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            _dialog.ShowSuccess("Adding sticker", "Success adding new sticker to db");
                            NavigationService.GoBack();
                        });
                    });
                }));
            }
        }

        #endregion

        #region Constructor

        public LibraryAddStickerViewModel(IDialog dialog)
        {
            _dialog = dialog;
            TitleForButton = "Select PDF file for sticker";
        }

        #endregion

        #region Private methods

        private bool FileIsEmpty()
        {
            return string.IsNullOrEmpty(File) || string.IsNullOrWhiteSpace(File);
        }

        private bool NameIsEmpty()
        {
            return string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name);
        }

        #endregion
    }
}