using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Models.BudgetCategory
{
    public class BudgetCategoryDetail
    {
        [Display(Name = "Budget ID")]
        public int BudgetId { get; set; }

        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }
    }
}
