//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebMidterm.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public int TID { get; set; }
        public Nullable<int> PolicyID { get; set; }
        public string Client { get; set; }
        public Nullable<double> AnnualIncome { get; set; }
        public Nullable<double> MonthlyPremium { get; set; }
    }
}
