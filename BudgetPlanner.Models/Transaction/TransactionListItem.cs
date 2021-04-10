using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Models.Transaction
{
    public class TransactionListItem
    {
        [Display(Name = "Transaction Number")]
        public int TransactionId { get; set; }

        [Display(Name ="Merchant Name")]
        public string MerchantName { get; set; }

        public double Amount { get; set; }

        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
