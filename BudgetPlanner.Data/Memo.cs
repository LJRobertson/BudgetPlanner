using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Data
{
    public class Memo
    {
        [Key]
        public int MemoId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(75, ErrorMessage = "Memo limited to 75 characters.")]
        public string MemoContent { get; set; }
    }
}
