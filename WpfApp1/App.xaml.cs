using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using WpfApp1.Views;
namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App


    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<JournalVoucherReceiptView>();
            containerRegistry.RegisterForNavigation<JournalVoucherView>();
            containerRegistry.RegisterForNavigation<DepositSlipView>();
        }
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

       
    }
}