using DevExpress.Xpf.Printing;
using DevExpress.XtraReports.UI;
using WpfApp1.Reports;
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


namespace WpfApp1.ViewModels
{
    [NotifyPropertyChanged]
    public class JournalVoucherViewModel
    {
        public JournalVoucherViewModel()
        {
            CreateNewJV();


            PrintCommand = new DelegateCommand(Print, CanPrint);
            OpenCommand = new DelegateCommand(Open, CanOpen);
            NewCommand = new DelegateCommand(New, CanNew);

            OfflinePath = Properties.Settings.Default.FilePath;


            Transactions = new List<AccountType>
            {
                new AccountType{Id= 1, TypeName="Dr"},
                new AccountType{Id= 2, TypeName="Cr"}
            };


        }

        public AccountType TransactionType { get; set; }

        #region New

        private bool CanNew() => true;

        private void New()
        {
            CreateNewJV();
        }

        #endregion


        private void CreateNewJV()
        {
            Entity = new JournalVoucherModel();

            Entity.Period = DateTime.Today;


           
        }
        [Command]
        public ICommand SaveCommand { get; set; }

        #region Save Command
        private void ExecuteSave()
        {
            JournalVoucherModel journalvoucher = new JournalVoucherModel();

            string jsondata = JsonConvert.SerializeObject(Entity);
            System.IO.File.WriteAllText($@"{OfflinePath}\{Entity.VoucherNo}.abs", jsondata);


        }

        protected bool CanExecuteSave => Entity != null &&
            !string.IsNullOrWhiteSpace(Entity.VoucherNo) && !string.IsNullOrWhiteSpace(OfflinePath);
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
                    Entity = JsonConvert.DeserializeObject<JournalVoucherModel>(json);
                }




            }

        }

        private bool CanOpen() => true;
        #endregion
        #region Print Command

        public DelegateCommand PrintCommand { get; set; }
        private void Print()
        {
            XtraReport report = new JournalVoucherReport();

            report.DataSource = new List<JournalVoucherModel> { Entity };

            DocumentPreviewWindow window = new DocumentPreviewWindow();
            window.PreviewControl.DocumentSource = report;
            report.CreateDocument();
            window.Show();
        }

        public List<AccountType> Transactions { get; set; }

        private bool CanPrint() => true;

        #endregion
        public JournalVoucherModel Entity { get; set; }

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

    public class AccountType
    {

        public string TypeName { get; set; }



        public int Id { get; set; }

        public override string ToString()
        {
            return TypeName;
        }



    }


}
