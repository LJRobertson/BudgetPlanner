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

        [ForeignKey(nameof(Budget))]
        public int? BudgetId { get; set; }

        public virtual Budget Budget { get; set; }
    }
}
