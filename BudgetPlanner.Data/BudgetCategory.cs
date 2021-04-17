using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Data
{
    public class BudgetCategory
    {
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(Budget))]
        public int BudgetId { get; set; }
        public Budget Budget { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
