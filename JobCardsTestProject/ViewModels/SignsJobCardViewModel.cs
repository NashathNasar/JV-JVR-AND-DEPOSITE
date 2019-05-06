using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpf.Printing;
using DevExpress.XtraReports.UI;
using PostSharp.Patterns.Model;
using LookUpEditTokenStyleTest;
using DevExpress.XtraPrinting.Drawing;

namespace JobCardsTestProject.ViewModels
{
    [NotifyPropertyChanged]
    public class SignsJobCardViewModel 
    {
        public SignsJobCardViewModel()
        {
            AluminumSheets.Add("2.03mm");
            AluminumSheets.Add("3.175mm");
            AluminumSheets.Add("3.0mm");
            AluminumSheets.Add("2.0mm");
            AluminumSheets.Add("1.5mm");
            AluminumSheets.Add("1.0mm");

            PrintCommand=new DelegateCommand(Print);
        }

        public List<object> AluminumSheets { get; set; }=new List<object>();
        public string SelectedAluminumSheets { get; set; } = "2.03mm,3.175mm";



        public DelegateCommand PrintCommand { get; set; }
        private void Print()
        {
            XtraReport report = new XtraReport1();

            ReportData = new SignsJobCardReportData();

            ReportData.Lines.AddRange(new[] 
            {
                new SignJobCardReportLine
                {
                    Shape =new ImageSource(Properties.Resources.SL),
                    Size ="750 SL",
                    Qty=20,
                    Unit= "Nos"
                },
                new SignJobCardReportLine { Shape=new ImageSource(Properties.Resources.Circle),
                    Size ="600 Dia",
                    Qty=32,
                    Unit= "Nos"
                },
                new SignJobCardReportLine { Shape=new ImageSource(Properties.Resources.HT),
                    Size ="600 HT",
                    Qty=2,
                    Unit= "Nos"
                },
                new SignJobCardReportLine { Shape=new ImageSource(Properties.Resources.R1),
                    Size ="1200 x 400",
                    Qty=16,
                    Unit= "Nos"
                },
                new SignJobCardReportLine { Shape=new ImageSource(Properties.Resources.R2)},
                new SignJobCardReportLine { Shape=new ImageSource(Properties.Resources.R3)}
            });

            var aluminumSheets = SelectedAluminumSheets.Split(',');
            ReportData.AluminumSheet2_03MM = aluminumSheets.Contains("2.03mm") ? "P" : "";
            ReportData.AluminumSheet3_175MM = aluminumSheets.Contains("3.175mm") ? "P" : "";
            ReportData.AluminumSheet3_0MM =   aluminumSheets.Contains("3.0mm") ? "P" : "";
            ReportData.AluminumSheet2_0MM =   aluminumSheets.Contains("2.0mm") ? "P" : "";
            ReportData.AluminumSheet1_5MM =   aluminumSheets.Contains("1.5mm") ? "P" : "";
            ReportData.AluminumSheet1MM =     aluminumSheets.Contains("1.0mm") ? "P" : "";


            report.DataSource = new List<SignsJobCardReportData> { ReportData };

            DocumentPreviewWindow window = new DocumentPreviewWindow();
            window.PreviewControl.DocumentSource = report;
            report.CreateDocument();
            window.Show();
        }

        public SignsJobCardReportData ReportData { get; set; }
    }

    public class SignsJobCardReportData
    {
        public string AluminumSheet2_03MM { get;set; }
        public string AluminumSheet3_175MM { get;set; }
        public string AluminumSheet3_0MM { get;set; }
        public string AluminumSheet2_0MM { get;set; }
        public string AluminumSheet1_5MM { get;set; }
        public string AluminumSheet1MM { get;set; }

        public List<SignJobCardReportLine> Lines { get; set; } =new List<SignJobCardReportLine>();
    }


    public class SignJobCardReportLine
    {
        public ImageSource Shape { get; set; }
        public string Size { get; set; }

        public decimal Qty { get; set; }

        public string Unit { get; set; }

        public string Remarks { get; set; }
        
    }
}
