using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Models.Category
{
    public class CategoryCreate
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category Amount")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal CategoryAmount { get; set; }

        [Display(Name = "Budget IDs")]
        public List<int> ListOfBudgetIds { get; set; } = new List<int>();

    }
}
