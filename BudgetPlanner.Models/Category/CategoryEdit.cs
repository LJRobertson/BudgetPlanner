using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Models.Category
{
    public class CategoryEdit
    {
        [Display(Name = "ID Number")]
        public int CategoryId { get; set; }

        public string Name { get; set; }
    }
}
