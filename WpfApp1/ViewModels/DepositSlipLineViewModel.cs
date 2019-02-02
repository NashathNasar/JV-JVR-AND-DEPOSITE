using System;
using System.Collections.ObjectModel;
using DevExpress.Xpo;
using DevExpress.XtraReports.UI;
using PostSharp.Patterns.Model;

namespace WpfApp1.ViewModels
{
    [NotifyPropertyChanged]
    public class DepositSlipLineViewModel
    {
        public string DepositVoucherNo { get; set; }
        public string AccountNo { get; set; }
        public string PartnerName { get; set; }
        public string Branch { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public string Depositor { get; set; }
        public string Telephone { get; set; }
        public string ChequeNo { get; set; }
        public decimal Amount { get; set; }
        public string BankName { get; set; }
        public string CustomerName { get; set; }


        public ObservableCollection<ChequeDepositVoucherLine> Lines { get; set; } = new ObservableCollection<ChequeDepositVoucherLine>();

 
        [NotifyPropertyChanged]
        public class ChequeDepositVoucherLine
        {
            public decimal Amount { get; set; }
            public long AmountWholePart { get => WholeNumberPart(Amount); }
            public int AmountFractionPart { get => FractionalPart(Amount); }
            string _amountWholePartDisplay = "---";
            [SafeForDependencyAnalysis]
            public string AmountWholePartDisplay
            {
                get
                {
                    return AmountWholePart > 0 ? AmountWholePart.ToString("N0") : _amountWholePartDisplay;
                }
                set
                {
                    _amountWholePartDisplay = value;
                }
            }
            [SafeForDependencyAnalysis]
            public string AmountFractionPartDisplay { get => AmountWholePart > 0 ? AmountFractionPart.ToString("00") : "---"; }
            public string Currency { get; set; } = "---";
            public string PartnerName { get; set; } = "---";
            public string BankName { get; set; } = "---";
            public string ChequeNo { get; set; } = "---";

            long WholeNumberPart(decimal decimalNumber)
            {
                return (long)decimal.Truncate(decimalNumber);
            }

            int FractionalPart(decimal decimalNumber)
            {
                var result = (int)((decimalNumber % 1) * 100);
                return result;
            }
        }


    }
}