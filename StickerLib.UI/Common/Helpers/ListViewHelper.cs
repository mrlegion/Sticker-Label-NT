using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace StickerLib.UI.Common.Helpers
{
    public class ListViewHelper
    {
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(ListViewHelper),
                new PropertyMetadata(null, OnSelectedItemsChanged));

        public static IList GetSelectedItems(DependencyObject d)
        {
            return (IList)d.GetValue(SelectedItemsProperty);
        }

        public static void SetSelectedItems(DependencyObject d, IList value)
        {
            d.SetValue(SelectedItemsProperty, value);
        }

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listView = (ListBox)d;
            ReSetSelectedItems(listView);
            listView.SelectionChanged += delegate
            {
                ReSetSelectedItems(listView);
            };
        }

        private static void ReSetSelectedItems(ListBox listView)
        {
            IList selectedItems = GetSelectedItems(listView);
            selectedItems.Clear();
            foreach (var item in listView.SelectedItems)
                selectedItems.Add(item);
        }
    }
}