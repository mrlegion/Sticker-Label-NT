﻿using System;
using System.Windows.Controls;

namespace StickerLib.UI.Common.Services
{
    public class DialogService : IDialog
    {
        public void ShowInfo(string message)
        {
            throw new NotImplementedException();
        }

        public void ShowSuccess(string message)
        {
            throw new NotImplementedException();
        }

        public void ShowError(string message)
        {
            throw new NotImplementedException();
        }

        public void ShowWarning(string message)
        {
            throw new NotImplementedException();
        }

        public bool ShowRequest(string message)
        {
            throw new NotImplementedException();
        }

        public void ShowLoading(string message, Action callback)
        {
            throw new NotImplementedException();
        }

        public void ShowDialog(ItemsControl content, string title)
        {
            throw new NotImplementedException();
        }
    }
}