using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Models
{
    public class BudgetCreate
    {
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Budget Name")]
        [MaxLength(30, ErrorMessage = "Budget name is limited to 30 characters.")]
        public string BudgetName { get; set; }

        [Required]
        [Display(Name = "Budget Amount")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal BudgetAmount { get; set; }

        public virtual List<int> ListOfTransactionIds { get; set; } = new List<int>();

        public virtual List<int> ListOfCategoryIds { get; set; } = new List<int>();
    }
}
