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
        public double BudgetAmount { get; set; }
       
        //public List<int> ListOfCategoryIds { get; set; } = new List<int>();

        //public virtual List<CategoryListItem> ListOfCategories { get; set; } = new List<CategoryListItem>();

        //public List<int> ListOfTransactionIds { get; set; } = new List<int>();

        //public virtual List<TransactionListItem> ListOfTransactions { get; set; } = new List<TransactionListItem>();
    }
}
