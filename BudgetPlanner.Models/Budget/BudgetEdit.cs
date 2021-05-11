using BudgetPlanner.Models.Category;
using BudgetPlanner.Models.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Models
{
    public class BudgetEdit
    {
        public int BudgetId { get; set; }

        [Display(Name = "Budget Name")]
        public string BudgetName { get; set; }

        [Display(Name = "Budgeted Amount")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal BudgetAmount { get; set; }
    }
}
