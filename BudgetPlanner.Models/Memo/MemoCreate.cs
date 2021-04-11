using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Models.Memo
{
    public class MemoCreate
    {
        public int TransactionId { get; set; }

        [Required]
        [MaxLength(75, ErrorMessage = "Memo limited to 75 characters.")]
        public string MemoContent { get; set; }
    }
}
