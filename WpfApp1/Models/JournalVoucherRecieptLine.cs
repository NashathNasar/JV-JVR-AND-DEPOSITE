using Prism.Commands;
using PostSharp.Patterns.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    [NotifyPropertyChanged]
    public  class JournalVoucherRecieptLineModel
    {
        public int Id { get; set; }
        public int VoucherId { get; set; }
     
        public DateTime? Date { get; set; }
        public string AccountNo { get; set; }
        public string AccountCode { get; set; }
        public DateTime Period { get; set; }
        
        public string Description { get; set; }
        public string TransactionType { get; set; }
        
        public decimal Debit { get; set; }
        public string Branch { get; set; }
        public string JournalRef { get; set; }
        public string PreparedBy { get; set; }
        public decimal Credit { get; set; }
        public byte[] RowVersion { get; set; }
      
    }

    




}
