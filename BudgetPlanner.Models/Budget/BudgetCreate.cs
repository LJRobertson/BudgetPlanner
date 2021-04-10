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
        [Required]
        [MaxLength(30, ErrorMessage = "Budget name is limited to 30 characters.")]
        public string BudgetName { get; set; }

        [Required]
        public double BudgetAmount { get; set; }

        public List<int> ListOfTransactionIds { get; set; } = new List<int>();

        public List<int> ListOfCategoryIds { get; set; } = new List<int>();
    }
}
