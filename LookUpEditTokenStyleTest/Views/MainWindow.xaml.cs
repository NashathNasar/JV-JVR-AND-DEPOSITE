using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Editors;
using LookUpEditTokenStyleTest.ViewModels;

namespace LookUpEditTokenStyleTest.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BaseEdit_OnEditValueChanging(object sender, EditValueChangingEventArgs e)
        {
            //var oldItems = e.OldValue as List<object>;
            //var newItems = e.NewValue as List<object>;

            //if (oldItems?.Count > 0)
            //{
            //    if (newItems == null)
            //    {

            //    }
            //    var removingItem = oldItems?.Except(newItems).Single();

            //    if (removingItem is LookupItem item)
            //    {
            //        e.IsCancel = item.Name.Contains("METAL");
            //        e.Handled = true;
            //    }
            //}

            //if (e.OldValue != null)
            //{
            //    Debug.WriteLine("======================Old Items==================");
            //    if (e.OldValue is List<object> oldItems)
            //    {
            //        foreach (var oldItem in oldItems)
            //        {
            //            if (oldItem is LookupItem item)
            //            {
            //                Debug.WriteLine(item.Name);
            //            }
            //        }
            //    }

            //    Debug.WriteLine("================================================");
            //}


            //if (e.NewValue != null)
            //{
            //    Debug.WriteLine("======================New Items==================");
            //    if (e.NewValue is List<object> newItems)
            //    {
            //        foreach (var newItem in newItems)
            //        {
            //            if (newItem is LookupItem item)
            //            {
            //                Debug.WriteLine(item.Name);
            //            }
            //        }
            //    }

            //    Debug.WriteLine("================================================");
            //}



            if (e.OldValue != null && e.OldValue is List<object> old_Items)
            {
                if (e.NewValue == null)
                {
                    Debug.WriteLine("===================Removing Item===================");
                    var removingItem = old_Items.SingleOrDefault() as LookupItem;
                    if (removingItem != null)
                    {
                        Debug.WriteLine("===================Removing Item===================");
                        Debug.WriteLine(removingItem.Name);   
                    }
                        
                }
                else
                {
                    if (e.NewValue is List<object> new_items)
                    {
                        var removingItem = old_Items?.Except(new_items).SingleOrDefault() as LookupItem;
                        if (removingItem != null)
                        {
                            Debug.WriteLine("===================Removing Item===================");
                            Debug.WriteLine(removingItem.Name);    
                        }
                    }
                }
            }
            //var removingItem = oldItems?.Except(newItems).Single();

            //if (removingItem is LookupItem item)
            //{
            //    e.IsCancel = item.Name.Contains("METAL");
            //    e.Handled = true;
            //}
        }


     
    }

    public class EditValueChangingEventArgsConverter : EventArgsConverterBase<EditValueChangingEventArgs>
    {
        protected override object Convert(object sender, EditValueChangingEventArgs e)
        {
            if (e.OldValue != null && e.OldValue is List<object> old_Items)
            {
                if (e.NewValue == null)
                {
                    return old_Items.SingleOrDefault();
                }
                else
                {
                    if (e.NewValue is List<object> new_items)
                    {
                        var removingItem = old_Items?.Except(new_items).SingleOrDefault() as LookupItem;
                        return removingItem;
                    }
                }
            }
            return null;
        }
    }


    public class DivisionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<LookupItem> items)
            {
                return items.OfType<object>().ToList();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value is List<object> items)
            {
                return items.OfType<LookupItem>().ToList();
            }

            return null;
        }
    }
}
