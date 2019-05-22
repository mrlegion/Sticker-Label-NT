﻿using System;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using StickerLib.UI.Common.Services;
using StickerLib.UI.Views;
using StickerLib.UI.Views.Pages;

namespace StickerLib.UI.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            // base init
            var builder = new ContainerBuilder();

            // register views
            builder.RegisterType<ShellView>();
            builder.RegisterType<MainView>();

            // register view models
            builder.RegisterType<ShellViewModel>();
            builder.RegisterType<MainViewModel>();

            // register navigation
            var navigationService = new FrameNavigationService("RootFrame");
            navigationService.Configure("main", new Uri("..\\Views\\Pages\\MainView.xaml", UriKind.Relative));

            builder.Register(c => navigationService).As<IFrameNavigationService>();

            // register ioc
            var container = builder.Build();
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
    }
}