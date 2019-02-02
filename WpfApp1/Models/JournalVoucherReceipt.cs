using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Xaml;
using Prism.Commands;
using Newtonsoft.Json;
using System.Windows.Forms;
using Humanizer;

namespace WpfApp1
{
    [NotifyPropertyChanged]
    public  class JournalVoucherReceiptModel
    {
        public JournalVoucherReceiptModel()
        {
           
            JournalVoucherRecieptLines = new List<JournalVoucherRecieptLineModel>();
        }
  
        
        public int Id { get; set; }
        public int? ReceiptVoucherId { get; set; }
        public int? DepositSlipId { get; set; }
        public DateTime Date { get; set; }
        public string VoucherNo { get; set; }
        public DateTime Period { get; set; }
        public string Branch { get; set; }
        public string PreparedBy { get; set; }
        public string JournalRef { get; set; }
        public Decimal DebitAmountDr { get; set; }
        public string TransactionType { get; set; }
        public Decimal CreditAmountCr { get; set; }
        public string CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public string Purpose { get; set; }
        public string Reason { get; set; }

        public List<JournalVoucherRecieptLineModel> JournalVoucherRecieptLines { get; set; }

        [SafeForDependencyAnalysis]
        public string AmountToWords => DecimalToWord(JournalVoucherRecieptLines.Sum(x => x.Debit));


        long WholeNumberPart(decimal decimalNumber)
        {
            return (long)decimal.Truncate(decimalNumber);
        }

        int FractionalPart(decimal decimalNumber)
        {
            var result = (int)((decimalNumber % 1) * 100);
            return result;
        }
      
        string DecimalToWord(decimal decimalNumber)
        {
            var fractionalPart = FractionalPart(decimalNumber);
            var fractionToWord = fractionalPart > 0 ? $"& { fractionalPart.ToWords().Titleize()} Fils " : string.Empty;
            var wholeNumberPart = WholeNumberPart(decimalNumber);
            var decimalToWord = $"{wholeNumberPart.ToWords().Titleize()} Dhs {fractionToWord}Only";
            return decimalToWord;
        }














    }

}
