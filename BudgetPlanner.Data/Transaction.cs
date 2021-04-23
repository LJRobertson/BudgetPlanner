using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Data
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [MaxLength(20, ErrorMessage = "Transaction Name cannot be longer than 20 characters.")]
        public string Name { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        [Required]
        public string MerchantName { get; set; }

        [Required]
        [ForeignKey(nameof(Budget))]
        public int BudgetId { get; set; }

        public virtual Budget Budget { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public bool ExcludeTransaction { get; set; }

        public virtual Memo Memo  { get; set; }
    }
}
