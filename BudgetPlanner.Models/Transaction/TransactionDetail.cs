using BudgetPlanner.Models.Memo;
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

        [Display(Name = "Merchant Name")]
        public string MerchantName { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Memo")]
        public virtual string MemoContent { get; set; }
    }
}
