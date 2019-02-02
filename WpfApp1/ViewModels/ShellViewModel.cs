using System;
using PostSharp.Patterns.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using Prism.Commands;
using System.IO;
using System.Threading.Tasks;
using PostSharp.Patterns.Xaml;
using Prism.Regions;

namespace WpfApp1.ViewModels
{
    [NotifyPropertyChanged]
    public  class ShellViewModel




    { 
 private readonly IRegionManager _regionManager;

        public ShellViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }


    #region NavigateCommand

    [Command] public ICommand NavigateCommand { get; set; }


    private void ExecuteNavigate(string uri)
    {
        _regionManager.RequestNavigate("ContentRegion", uri);
    }


    protected bool CanExecuteNavigate(string uri) => !string.IsNullOrWhiteSpace(uri);

    #endregion
    }
}
