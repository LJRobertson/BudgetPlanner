using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Models.Memo
{
    public class MemoDetail
    {
        [Display(Name = "Related Transaction")]
        public int TransactionId { get; set; }

        [Display(Name = "Memo Content")]
        public string MemoContent { get; set; }
    }
}
