using System;
using System.Windows;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using StickerLib.Domain;
using StickerLib.UI.Common.Dialogs.Components;
using StickerLib.UI.Common.Dialogs.Views;
using StickerLib.UI.Common.Services;
using StickerLib.UI.ViewModels.Group;
using StickerLib.UI.ViewModels.Library;
using StickerLib.UI.ViewModels.Preference;
using StickerLib.UI.Views;
using StickerLib.UI.Views.Pages;
using StickerLib.UI.Views.Pages.Group;
using StickerLib.UI.Views.Pages.Library;

namespace StickerLib.UI.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            IContainer container;

            // base init
            var builder = new ContainerBuilder();

            // register module
            builder.RegisterModule<DomainModule>();

            // register views
            builder.RegisterType<ShellView>().SingleInstance();
            builder.RegisterType<MainView>();
            builder.RegisterType<GroupWindow>();
            builder.RegisterType<GroupEditView>();
            builder.RegisterType<GroupAddListView>();
            builder.RegisterType<PreferenceWindow>();

            // register library window
            builder.RegisterType<LibraryWindow>();
            builder.RegisterType<LibraryWindowViewModel>();
            builder.RegisterType<LibraryView>();
            builder.RegisterType<LibraryViewModel>();
            builder.RegisterType<LibraryAddStickerView>();
            builder.RegisterType<LibraryAddStickerViewModel>();
            builder.RegisterType<LibraryAddStickerListView>();
            builder.RegisterType<LibraryAddStickerListViewModel>();
            builder.RegisterType<LibraryBackupView>();
            builder.RegisterType<LibraryBackupViewModel>();

            // register view models
            builder.RegisterType<ShellViewModel>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<GroupEditViewModel>();
            builder.RegisterType<PreferenceViewModel>();

            // dialog
            builder.RegisterType<DialogService>().As<IDialog>();
            builder.RegisterType<AlertDialogView>();
            builder.RegisterType<QuestionDialogView>();
            builder.RegisterType<LoadingContentVIew>();
            builder.RegisterType<InfoContentView>();
            builder.RegisterType<ContentDialogView>();
            builder.RegisterType<PreviewContentView>();

            // register navigation
            var rootNavigationService = new FrameNavigationService("RootFrame");
            rootNavigationService.Configure("main", new Uri("..\\Views\\Pages\\MainView.xaml", UriKind.Relative));
            builder.Register(c => rootNavigationService).Named<IFrameNavigationService>("Root");

            // group navigation service
            var groupNavigationService = new FrameNavigationService("GroupFrame");
            groupNavigationService.Configure("groupEdit", new Uri("..\\Views\\Pages\\Group\\GroupEditView.xaml", UriKind.Relative));
            groupNavigationService.Configure("groupList", new Uri("..\\Views\\Pages\\Group\\GroupAddListView.xaml", UriKind.Relative));
            builder.Register(c => groupNavigationService).Named<IFrameNavigationService>("Group");

            // library navigation service
            var libraryNavigationService = new FrameNavigationService("LibraryFrame");
            libraryNavigationService.Configure("library", new Uri("..\\Views\\Pages\\Library\\LibraryView.xaml", UriKind.Relative));
            libraryNavigationService.Configure("add", new Uri("..\\Views\\Pages\\Library\\LibraryAddStickerView.xaml", UriKind.Relative));
            libraryNavigationService.Configure("addList", new Uri("..\\Views\\Pages\\Library\\LibraryAddStickerListView.xaml", UriKind.Relative));
            libraryNavigationService.Configure("backup", new Uri("..\\Views\\Pages\\Library\\LibraryBackupView.xaml", UriKind.Relative));
            builder.Register(c => libraryNavigationService).Named<IFrameNavigationService>("Library");

            // register ioc
            container = builder.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }

        public ShellViewModel Shell
        {
            get { return ServiceLocator.Current.GetInstance<ShellViewModel>(); }
        }

        public MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public GroupEditViewModel GroupEdit
        {
            get { return ServiceLocator.Current.GetInstance<GroupEditViewModel>(); }
        }

        public PreferenceViewModel Preference
        {
            get { return ServiceLocator.Current.GetInstance<PreferenceViewModel>(); }
        }

        public LibraryViewModel Library
        {
            get { return ServiceLocator.Current.GetInstance<LibraryViewModel>(); }
        }

        public LibraryAddStickerViewModel LibraryAddSticker
        {
            get { return ServiceLocator.Current.GetInstance<LibraryAddStickerViewModel>(); }
        }

        public LibraryAddStickerListViewModel LibraryAddStickers
        {
            get { return ServiceLocator.Current.GetInstance<LibraryAddStickerListViewModel>(); }
        }

        public LibraryBackupViewModel LibraryBackup
        {
            get { return ServiceLocator.Current.GetInstance<LibraryBackupViewModel>(); }
        }

        public LibraryWindowViewModel LibraryWindow
        {
            get { return ServiceLocator.Current.GetInstance<LibraryWindowViewModel>(); }
        }
    }
}