using System;
using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Views;

namespace StickerLib.UI.Common.Services
{
    public interface IFrameNavigationService : INavigationService, INotifyPropertyChanged
    {
        object Parameter { get; }
        string FrameName { get; }
        bool CanGoBack { get; }

        Window MainWindow { set; }

        void Configure(string key, Uri pageType);

    }
}