using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using StickerLib.Infrastructure.Annotations;

namespace StickerLib.UI.Common.Services
{
    public class FrameNavigationService : IFrameNavigationService
    {
        private readonly Dictionary<string, Uri> _pagesByKey;
        private readonly Stack<string> _historic;

        private string _currentPageKey;

        public FrameNavigationService(string frameName) : base()
        {
            FrameName = frameName;
            _pagesByKey = new Dictionary<string, Uri>();
            _historic = new Stack<string>();
        }

        public string CurrentPageKey
        {
            get { return _currentPageKey; }
            set { Set(ref _currentPageKey, value); }
        }

        public object Parameter { get; private set; }
        public string FrameName { get; }
        public bool CanGoBack
        {
            get { return _historic.Count > 1; }
        }
        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            lock (_pagesByKey)
            {
                if (!_pagesByKey.ContainsKey(pageKey)) throw new ArgumentException($"No such page: {pageKey}", "pageKey");
                if (GetDescendantFromName(Application.Current.MainWindow, FrameName) is Frame frame)
                    frame.Source = _pagesByKey[pageKey];
                Parameter = parameter;
                _historic.Push(pageKey);
                CurrentPageKey = pageKey;
            }
        }

        public void GoBack()
        {
            if (CanGoBack)
            {
                _historic.Pop();
                NavigateTo(_historic.Pop(), null);
            }
        }

        public void Configure(string key, Uri pageType)
        {
            lock (_pagesByKey)
                if (_pagesByKey.ContainsKey(key))
                    _pagesByKey[key] = pageType;
                else _pagesByKey.Add(key, pageType);
        }

        private FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);
            if (count < 1) return null;
            for (int i = 0; i < count; i++)
                if (VisualTreeHelper.GetChild(parent, i) is FrameworkElement frameworkElement)
                {
                    if (frameworkElement.Name == name)
                        return frameworkElement;
                    frameworkElement = GetDescendantFromName(frameworkElement, name);
                    if (frameworkElement != null)
                        return frameworkElement;
                }
            return null;
        }

        #region INotifyPropertyChanged implimintation

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
        }

        #endregion
    }
}