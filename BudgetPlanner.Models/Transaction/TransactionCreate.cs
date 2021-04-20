using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Models.Transaction
{
    public class TransactionCreate
    {
        [Required]
        public int BudgetId { get; set; }

        [MaxLength(20, ErrorMessage = "Transaction Name cannot be longer than 20 characters.")]
        public string Name { get; set; }

        [Required]
        public double Amount { get; set; }

        [Display(Name="Date")]
        public DateTime TransactionDate { get; set; }

        [Required]
        [Display(Name = "Marchant Name")]
        public string MerchantName { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Exclude Transaction")]
        public bool ExcludeTransaction { get; set; }

        //link to Memo
    }
}
