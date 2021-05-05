using BudgetPlanner.Data;
using BudgetPlanner.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class TransactionService
    {
        private readonly Guid _userId;

        public TransactionService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateTransaction(TransactionCreate model)
        {
            var ctx = new ApplicationDbContext();

            var entity =
                    new Transaction()
                    {
                        UserId = _userId,
                        Name = model.Name,
                        Amount = model.Amount,
                        TransactionDate = model.TransactionDate,
                        BudgetId = model.BudgetId,
                        MerchantName = model.MerchantName,
                        CategoryId = model.CategoryId,
                        ExcludeTransaction = model.ExcludeTransaction
                    };

            var budgetCategoryEntity =
                ctx.
                    BudgetCategory
                    .ToList()
                    .SingleOrDefault(e => e.CategoryId == model.CategoryId);

            if (budgetCategoryEntity == null)
            {
                var newBudgetCategory =
                    new BudgetCategory
                    {
                        BudgetId = model.BudgetId,
                        CategoryId = model.CategoryId
                    };

                ctx.BudgetCategory.Add(newBudgetCategory);
                ctx.SaveChanges();
            };
            
                ctx.Transactions.Add(entity);
                return ctx.SaveChanges() == 1;
        }

        public IEnumerable<TransactionListItem> GetTransactions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Transactions
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new TransactionListItem
                                {
                                    TransactionId = e.TransactionId,
                                    MerchantName = e.MerchantName,
                                    Amount = e.Amount,
                                    TransactionDate = e.TransactionDate,
                                    CategoryId = e.CategoryId
                                }
                                );
                return query.ToArray();
            }
        }

        //GET TRANSACTIONS BY BUDGET ID
        public IEnumerable<TransactionListItem> GetTransactionsByBudgetId(int budgetId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Transactions
                        .Where(e => e.BudgetId == budgetId && e.UserId == _userId)
                        .Select(
                            e =>
                                new TransactionListItem
                                {
                                    TransactionId = e.TransactionId,
                                    MerchantName = e.MerchantName,
                                    Amount = e.Amount,
                                    TransactionDate = e.TransactionDate,
                                    CategoryId = e.CategoryId
                                }
                                );
                return query.ToArray();
            }
        }


        public TransactionListItem GetTransactionListItemById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionId == id && e.UserId == _userId);

                return
                    new TransactionListItem
                    {
                        TransactionId = entity.TransactionId,
                        MerchantName = entity.MerchantName,
                        Amount = entity.Amount,
                        TransactionDate = entity.TransactionDate,
                        CategoryId = entity.CategoryId
                    };
            }
        }

        public TransactionDetail GetTransactionById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionId == id && e.UserId == _userId);

                var memoService = new MemoService(_userId);

                var transactionMemo = memoService.GetMemoById(id);
                string memoContentString;
                if (transactionMemo == null)
                {
                    memoContentString = null;
                }

                else
                {
                    memoContentString = transactionMemo.MemoContent;
                }
                //send memos to a list, then look for the ID

                //string memo = ctx.Memos.SingleOrDefault(e => e.TransactionId == id).MemoContent;

                //var memo =
                //    ctx
                //        .Memos
                //        .Single(e => e.TransactionId == id);

                return
                    new TransactionDetail
                    {
                        TransactionId = entity.TransactionId,
                        BudgetId = entity.BudgetId,
                        Amount = entity.Amount,
                        TransactionDate = entity.TransactionDate,
                        MerchantName = entity.MerchantName,
                        CategoryId = entity.CategoryId,
                        ExcludeTransaction = entity.ExcludeTransaction,
                        MemoContent = memoContentString 
                        //Add nullable memo 
                    };
            }
        }

        public bool UpdateTransaction(TransactionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionId == model.TransactionId && e.UserId == _userId);

                entity.Name = model.Name;
                entity.BudgetId = model.BudgetId;
                entity.Amount = model.Amount;
                entity.TransactionDate = model.TransactionDate;
                entity.MerchantName = model.MerchantName;
                entity.CategoryId = model.CategoryId;
                entity.ExcludeTransaction = model.ExcludeTransaction;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTransaction(int transactionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Transactions
                    .Single(e => e.TransactionId == transactionId && e.UserId == _userId);

                ctx.Transactions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
