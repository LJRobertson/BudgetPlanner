﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Models.Memo
{
    public class MemoListItem
    {
        [Display(Name ="Related Transaction")]
        public int TransactionId { get; set; }

        public string MemoContent { get; set; }
    }
}
