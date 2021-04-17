using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Data
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<int> ListOfBudgetIds { get; set; } = new List<int>();
        public virtual List<Budget> ListOfBudgets { get; set; } = new List<Budget>();

        //[ForeignKey(nameof(Budget))]
        //public int? BudgetId { get; set; }

        //public virtual Budget Budget { get; set; }
    }
}
