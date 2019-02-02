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
using Humanizer;
namespace WpfApp1
{

    [NotifyPropertyChanged]
    public class JournalVoucherModel
    {
        public JournalVoucherModel()
        {




            JournalVoucherLines = new List<JournalVoucherLineModel>();



         
        }


        public int Id { get; set; }
        public int? ReceiptVoucherId { get; set; }
        public int? DepositSlipId { get; set; }
        public string VoucherNo { get; set; }
        public DateTime Period { get; set; }
        public string Branch { get; set; }

        public string PreparedBy { get; set; }

        public string JournalRef { get; set; }

        public string CreatedUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ModifiedUser { get; set; }
   
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public string Purpose { get; set; }
        public string Reason { get; set; }
        public List<JournalVoucherLineModel> JournalVoucherLines { get; set; }

        [SafeForDependencyAnalysis]
        public string AmountToWords => DecimalToWord(JournalVoucherLines.Sum(x => x.Debit));


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
