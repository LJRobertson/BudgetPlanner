using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Models.Category
{
    public class CategoryListItem
    {
        [Display(Name="ID Number")]
        public int CategoryId { get; set; }

        public string Name { get; set; }

        [Display(Name = "Category Amount")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal CategoryAmount { get; set; }

        [Display(Name = "Remaining Balance")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal RemainingCategoryAmount { get; set; }
    }
}
