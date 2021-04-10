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

        public string Name { get; set; }

        [Required]
        public double Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string MerchantName { get; set; }

        public int CategoryId { get; set; }

        public bool ExcludeTransaction { get; set; }

        [ForeignKey(nameof(Memo))]
        public int? MemoId { get; set; }

        public virtual Memo Memo { get; set; }

    }
}
