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
                        MerchantName = model.MerchantName,
                        Amount = model.Amount,
                        TransactionDate = model.TransactionDate,
                        BudgetId = model.BudgetId,
                        CategoryId = model.CategoryId,
                    };
            try
            {
                var budgetCategoryItem =
                    ctx.
                        BudgetCategory
                        .Single(e => e.CategoryId == model.CategoryId && e.BudgetId == model.BudgetId);
            }
            catch
            {
                var newBudgetCategory =
                                 new BudgetCategory
                                 {
                                     BudgetId = model.BudgetId,
                                     CategoryId = model.CategoryId
                                 };

                ctx.BudgetCategory.Add(newBudgetCategory);
            }

            ctx.Transactions.Add(entity);
            return ctx.SaveChanges() >= 1;
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
                                    BudgetId = e.BudgetId,
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

                return
                    new TransactionDetail
                    {
                        TransactionId = entity.TransactionId,
                        BudgetId = entity.BudgetId,
                        Amount = entity.Amount,
                        TransactionDate = entity.TransactionDate,
                        MerchantName = entity.MerchantName,
                        CategoryId = entity.CategoryId,
                        MemoContent = memoContentString
                    };
            }
        }

        public bool UpdateTransaction(TransactionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var budgetCategoryList =
                         ctx.
                             BudgetCategory
                             .Where(e => e.CategoryId == model.CategoryId)
                             .ToList();

                if (budgetCategoryList == null)
                {
                    var newBudgetCategory =
                        new BudgetCategory
                        {
                            BudgetId = model.BudgetId,
                            CategoryId = model.CategoryId
                        };
                }

                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionId == model.TransactionId && e.UserId == _userId);

                entity.TransactionId = model.TransactionId;
                entity.BudgetId = model.BudgetId;
                entity.MerchantName = model.MerchantName;
                entity.Amount = model.Amount;
                entity.TransactionDate = model.TransactionDate;
                entity.CategoryId = model.CategoryId;

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

                if (entity.Memo != null)
                {
                    var memoService = new MemoService(_userId);
                    memoService.DeleteMemo(transactionId);
                }

                ctx.Transactions.Remove(entity);

                return ctx.SaveChanges() > 0;
            }
        }

    }
}
