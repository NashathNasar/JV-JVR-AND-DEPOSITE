using DevExpress.Xpf.Printing;
using Newtonsoft.Json;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Xaml;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using DevExpress.XtraReports.UI;
using Jasmine.Modules.CreditControl.Reports.Print;
using static WpfApp1.ViewModels.DepositSlipLineViewModel;
using System.Drawing;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.LayoutControl;

namespace WpfApp1.ViewModels
{
    [NotifyPropertyChanged]
    public class DepositSlipViewModel
    {
        public DepositSlipViewModel()
        {
            CreateNewDepositVoucher();


            PrintCommand = new DelegateCommand(Print, CanPrint);
            OpenCommand = new DelegateCommand(Open, CanOpen);
            NewCommand = new DelegateCommand(New, CanNew);

            OfflinePath = Properties.Settings.Default.FilePath;

            Companies = new List<Company>
            {
                new Company {CompanyName ="Cicon Building Materials", AccountNumber="4001001132300", BranchName ="Abu Dhabi"},
                new Company {CompanyName ="Cicon Epoxy & Steel Cutting Plant LLC", AccountNumber="4001561093500", BranchName ="Abu Dhabi"},
                new Company {CompanyName ="Cicon Traffic Signs &Requisites Trading Est.", AccountNumber="4001570246500", BranchName ="Abu Dhabi"},
                new Company {CompanyName ="Cicon Building Materials Est.", AccountNumber="3001154282500", BranchName ="Dubai"},
            };



        }



        public Company Branch { get; set; }


        #region New
        private bool CanNew() => true;

        private void New()
        {
            CreateNewDepositVoucher();
        }
        #endregion
        private void CreateNewDepositVoucher()
        {
            Entity = new DepositSlipLineViewModel();

            Entity.Date = DateTime.Today;
            //Entity.DepositVoucherNo = "123456";

            for (int i = 0; i < 6; i++)
            {
                Entity.Lines.Add(new ChequeDepositVoucherLine() { Currency = "AED" });
            }
        }

        [Command]
        public ICommand SaveCommand { get; set; }

        #region Save Command
        private void ExecuteSave()
        {
            DepositSlipLineViewModel deposit = new DepositSlipLineViewModel();

            string jsondata = JsonConvert.SerializeObject(Entity);
            System.IO.File.WriteAllText($@"{OfflinePath}\{Entity.DepositVoucherNo}.abs", jsondata);


        }

        protected bool CanExecuteSave => Entity != null &&
            !string.IsNullOrWhiteSpace(Entity.DepositVoucherNo) && !string.IsNullOrWhiteSpace(OfflinePath);
        #endregion

        public string OfflinePath { get; set; }

        [Command]
        public ICommand SaveOfflinePathCommand { get; set; }

        private void ExecuteSaveOfflinePath()
        {
            Properties.Settings.Default.FilePath = OfflinePath;
            Properties.Settings.Default.Save();
        }

        protected bool CanExecuteSaveOfflinePath => !string.IsNullOrWhiteSpace(OfflinePath);

        public DelegateCommand OpenCommand { get; set; }
        public DelegateCommand NewCommand { get; private set; }
        #region Open
        private void Open()
        {

            OpenFileDialog myfileDlg = new OpenFileDialog();

            if (myfileDlg.ShowDialog() == DialogResult.OK)
            {
                string path = myfileDlg.FileName;

                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    Entity = JsonConvert.DeserializeObject<DepositSlipLineViewModel>(json);
                }




            }

        }

        private bool CanOpen() => true;
        #endregion
        #region Print Command

        public DelegateCommand PrintCommand { get; set; }
        private void Print()
        {
            XtraReport report = new ChequeDepositVoucherReport();
            Entity.AccountNo = Branch.AccountNumber;
            Entity.Branch = Branch.BranchName;
            Entity.CustomerName = Branch.CompanyName;
            report.DataSource = new List<DepositSlipLineViewModel> { Entity };

            DocumentPreviewWindow window = new DocumentPreviewWindow();
            window.PreviewControl.DocumentSource = report;
            report.CreateDocument();
            window.Show();
        }

        public List<Company> Companies { get; set; }

        private bool CanPrint() => true;
        #endregion
        public DepositSlipLineViewModel Entity { get; set; }

        [Command]
        public ICommand BrowseOfflinePathCommand { get; set; }

        private void ExecuteBrowseOfflinePath()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                OfflinePath = dialog.SelectedPath;
            }

        }

        protected bool CanExecuteBrowseOfflinePath() => true;
    }


    public class Company
    {
        public string CompanyName { get; set; }
        public string AccountNumber { get; set; }
        public string BranchName { get; set; }

        public override string ToString()
        {
            return CompanyName;
        }
    }
}

