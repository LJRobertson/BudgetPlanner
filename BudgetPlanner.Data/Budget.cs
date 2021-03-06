using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Data
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Budget name is limited to 30 characters.")]
        public string BudgetName { get; set; }

        [Required]
        public decimal BudgetAmount { get; set; }

        public virtual List<int> ListOfTransactionIds { get; set; } = new List<int>();

        public virtual List<Transaction> ListOfTransactions { get; set; } = new List<Transaction>();

        public virtual List<int> ListOfCategoryIds { get; set; } = new List<int>();

        public virtual List<Category> ListOfCategories { get; set; } = new List<Category>();

    }
}
