using JobCardsTestProject.Views;
using LookUpEditTokenStyleTest.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace JobCardsTestProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.SignsJobCardView>();
            containerRegistry.RegisterForNavigation<ComboBoxEditTokenTestView>();
        }
    }
}
