using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMidterm.Models
{
    public class TransactionPolicyClass : PolicyPackage
    {
        public int TID { get; set; }
        public string Client { get; set; }
        public double? MonthlyIncome { get; set; }
        public string PolicyPackage { get; set; }
        public double? MonthlyPremium { get; set; }
    }

}