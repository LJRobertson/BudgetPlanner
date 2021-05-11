using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Data
{
    public class Memo

    {
        [Key]
        [ForeignKey(nameof(Transaction))]
        public int TransactionId { get; set; }

        public virtual Transaction Transaction { get; set; }
        
        [Required]
        [MaxLength(75, ErrorMessage = "Memo limited to 75 characters.")]
        public string MemoContent { get; set; }
    }
}
