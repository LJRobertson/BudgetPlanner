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

        [Required]
        [Display(Name = "Marchant Name")]
        [MaxLength(30, ErrorMessage = "Transaction Name cannot be longer than 30 characters.")]
        public string MerchantName { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }

        [Display(Name="Date")]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
