using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Printing;
using DevExpress.XtraReports.UI;
using PostSharp.Patterns.Xaml;
using PostSharp.Patterns.Model;

namespace JobCardsTestProject.ViewModels
{
    [NotifyPropertyChanged]
    public class ComboBoxEditTokenTestViewModel
    {
          
        public string Title { get; set; } = "Prism Application";

        public ComboBoxEditTokenTestViewModel()
        {
            Divisions = new ObservableCollection<LookupItem>(LookupItem.GetDivisions());
            SelectedDivisions=new List<LookupItem>
            {
                new LookupItem(){Id = 1,Name = "SIGN 1"},
                new LookupItem(){Id = 2,Name = "METAL 1"},
            };
        }

        public ObservableCollection<LookupItem> Divisions { get; set; }

        public List<LookupItem> SelectedDivisions { get; set; }


        #region RemoveDivisionCommand

        [Command] public ICommand RemoveDivisionCommand { get; set; }


        private void ExecuteRemoveDivision(EditValueChangingEventArgs e)
        {
            var removingItem = ExtractRemovingItem(e);
            if (removingItem != null && removingItem.Name.Contains("METAL 2"))
            {
                e.IsCancel = true;
                e.Handled=true;
                DXMessageBox.Show($"Sorry cannot delete {removingItem.Name} already scheduled",Title,MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }

        private LookupItem ExtractRemovingItem(EditValueChangingEventArgs e)
        {
            if (e.OldValue != null && e.OldValue is List<object> old_Items)
            {
                if (e.NewValue == null)
                {
                    return old_Items.SingleOrDefault() as LookupItem;
                }

                if (e.NewValue is List<object> new_items)
                {
                    var removingItem = old_Items?.Except(new_items).SingleOrDefault() as LookupItem;
                    return removingItem;
                }
            }
            return null;
        }


        protected bool CanExecuteRemoveDivision() => true;

        #endregion




       
    }


    public class LookupItem : IEquatable<LookupItem>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public static List<LookupItem> GetDivisions()
        {
            return new List<LookupItem>
            {
                new LookupItem() {Id = 1, Name = "SIGN 1"},
                new LookupItem() {Id = 2, Name = "METAL 1"},
                new LookupItem() {Id = 3, Name = "METAL 2"},
                new LookupItem() {Id = 4, Name = "METAL 3"},
                new LookupItem() {Id = 5, Name = "METAL 4"},
                new LookupItem() {Id = 6, Name = "SIGN 2"},
                new LookupItem() {Id = 7, Name = "SIGN 3"},
                new LookupItem() {Id = 8, Name = "SING 4"},
            };
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LookupItem);
        }

        public bool Equals(LookupItem other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public static bool operator ==(LookupItem item1, LookupItem item2)
        {
            return EqualityComparer<LookupItem>.Default.Equals(item1, item2);
        }

        public static bool operator !=(LookupItem item1, LookupItem item2)
        {
            return !(item1 == item2);
        }
    }
}
