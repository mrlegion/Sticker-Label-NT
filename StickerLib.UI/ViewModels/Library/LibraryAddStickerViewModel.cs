using System;
using System.IO;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Microsoft.WindowsAPICodePack.Dialogs;
using StickerLib.Domain.Helpers;
using StickerLib.Domain.Services;
using StickerLib.Infrastructure.Entities;
using StickerLib.UI.Common.Helpers;
using StickerLib.UI.Common.Services;

namespace StickerLib.UI.ViewModels.Library
{
    public class LibraryAddStickerViewModel : ViewModelLibrary
    {
        #region Fields

        private const string FileIsNotSelected = "File is not selected, please select file!";
        private string _name;
        private string _titleForButton;
        private bool _useFileName;
        private string _file;
        private Sticker _sticker;
        private RelayCommand _selectPdfFileCommand;
        private RelayCommand _clearSelectFileCommand;
        private RelayCommand _acceptCommand;
        private readonly OpenWindowType _type = OpenWindowType.Create;
        private string _oldName = "";

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
                {
                    _oldName = Name;
                    Name = FileIsEmpty()
                        ? FileIsNotSelected
                        : Path.GetFileNameWithoutExtension(File);
                }
                else Name = _oldName;
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
            set
            {
                Set(nameof(File), ref _file, value);
                ShowClearButton = !FileIsEmpty();
                ClearSelectFileCommand.RaiseCanExecuteChanged();
                ;
            }
        }

        public RelayCommand SelectPdfFileCommand
        {
            get
            {
                return _selectPdfFileCommand ?? (_selectPdfFileCommand = new RelayCommand(() =>
                {
                    var file = Dialog.OpenFileDialog("Select PDF file for new sticker",
                        new[] {new CommonFileDialogFilter("Pdf files", "pdf")});
                    
                    if (file == null) return;
                    
                    File = file;
                    TitleForButton = "File is selected";
                    if (UseFileName)
                        Name = Path.GetFileNameWithoutExtension(File);
                }));
            }
        }

        public RelayCommand ClearSelectFileCommand
        {
            get
            {
                return _clearSelectFileCommand ?? (_clearSelectFileCommand = new RelayCommand(async () =>
                {
                    var response = await Dialog.ShowRequest("Deleted selected file",
                        "You really want clear selected file for sticker?", "Clear", "Cancel");

                    if (!response) return;

                    Name = (Name == Path.GetFileNameWithoutExtension(File)) ? "" : Name;
                    File = "";
                    TitleForButton = "Select PDF file for sticker";
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
                        Dialog.ShowError("File is Empty",
                            "PDF file for new sticker is not selected! Please click on button \"Select PDF file for sticker\" and select it");
                        return;
                    }

                    if (!System.IO.File.Exists(File))
                    {
                        Dialog.ShowError("Selected file is not found",
                            "Selected file for new sticker not found! Please check path or file on disk");
                        return;
                    }

                    if (NameIsEmpty())
                    {
                        Dialog.ShowError("Sticker name is Empty",
                            $"Field \"Name\" cannot be empty! Please, enter name for adding sticker!");
                        return;
                    }

                    Dialog.ShowLoading("Saving sticker in db...", () =>
                    {
                        var service = ServiceLocator.Current.GetInstance<IStickerService>();
                        switch (_type)
                        {
                            case OpenWindowType.Create:
                                _sticker = new Sticker(Name, PdfReadHelper.ReadToStreamByte(File));
                                service.Add(_sticker);
                                break;
                            case OpenWindowType.Editable:
                                _sticker.Name = Name;
                                if (File != Enum.GetName(typeof(OpenWindowType), OpenWindowType.Editable))
                                    _sticker.File = PdfReadHelper.ReadToStreamByte(File);
                                service.Update(_sticker);
                                break;
                        }

                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            Dialog.ShowSuccess("Save changes", "Success saving sticker changes into database");
                            NavigationService.GoBack();
                        });
                    });
                }));
            }
        }

        #endregion

        #region Constructor

        public LibraryAddStickerViewModel(IDialog dialog) : base(dialog)
        {
            TitleForButton = "Select PDF file for sticker";

            if (NavigationService.Parameter != null)
                if (NavigationService.Parameter is Sticker sticker)
                {
                    _sticker = sticker;
                    Name = _sticker.Name;
                    TitleForButton = "Change file for sticker";
                    _type = OpenWindowType.Editable;
                }
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

        private bool _showClearButton;

        public bool ShowClearButton
        {
            get { return _showClearButton; }
            set { Set(nameof(ShowClearButton), ref _showClearButton, value); }
        }
    }
}