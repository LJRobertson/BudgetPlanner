using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Models.Transaction
{
    public class TransactionDetail
    {
        public int TransactionId { get; set; }

        [Display(Name = "Budget ID")]
        public int BudgetId { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        [Display(Name="Date")]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Merchant Name")]
        public string MerchantName { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Exclude Transaction")]
        public bool ExcludeTransaction { get; set; }
    }
}
