using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Xaml;
using Prism.Mvvm;
using Prism.Regions;

namespace JobCardsTestProject.ViewModels
{
    [NotifyPropertyChanged]
    public class MainWindowViewModel 
    {
        private readonly IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand=new DelegateCommand<string>(ExecuteNavigate,CanExecuteNavigate);
        }


        public DelegateCommand<string> NavigateCommand { get; set; }

        private void ExecuteNavigate(string uri)
        {
            _regionManager.RequestNavigate("ContentRegion",uri);
        }

        private bool CanExecuteNavigate(string uri) => !string.IsNullOrWhiteSpace(uri);
    }
}
