using PostSharp.Patterns.Model;
using System;
using System.Collections.Generic;


namespace WpfApp1
{
    [NotifyPropertyChanged]
    public  class JournalVoucherLineModel
    {
        
        public DateTime Period { get; set; }
        public int Id { get; set; }
        public int VoucherId { get; set; }
        public string JournalRef { get; set; }
        public DateTime? Date { get; set; }
        public string AccountCode { get; set; }
        public string AccountNo { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public byte[] RowVersion { get; set; }
        public string PreparedBy { get; set; }
        public string Branch { get; set; }
    }
}
