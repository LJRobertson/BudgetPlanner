using BudgetPlanner.Data;
using BudgetPlanner.Models;
using BudgetPlanner.Models.Category;
using BudgetPlanner.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class BudgetService
    {
        private readonly Guid _userId;

        public BudgetService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateBudget(BudgetCreate model)
        {
            //List<int> categoryIdList = new List<int>();
            //foreach (int cId in model.ListOfCategoryIds)
            //{
            //    categoryIdList.Add(cId)
            //}

            var entity =
                new Budget()
                {
                    OwnerId = _userId,
                    BudgetName = model.BudgetName,
                    BudgetAmount = model.BudgetAmount,
                    ListOfCategoryIds = model.ListOfCategoryIds,
                    ListOfTransactionIds = model.ListOfTransactionIds
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Budgets.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BudgetListItem> GetBudgets()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Budgets
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new BudgetListItem
                                {
                                    BudgetId = e.BudgetId,
                                    BudgetName = e.BudgetName,
                                    BudgetAmount = e.BudgetAmount
                                }
                                );
                return query.ToArray();
            }
        }

        public BudgetDetail GetBudgetById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Budgets
                        .Single(e => e.BudgetId == id && e.OwnerId == _userId);

                var transactionList =
                    ctx
                        .Transactions
                        .Where(e => e.BudgetId == id && e.UserId == _userId)
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
                                ).ToList();

                var budgetCategoryList =
                    ctx
                        .BudgetCategory
                        .Where(e => e.BudgetId == id)
                        .ToList();

                var cs = new CategoryService(_userId);
                var categoryList = new List<CategoryListItem>();

                var remainingCategoryAmountList = new List<decimal>();

                foreach (var budgetCategory in budgetCategoryList)
                {
                    var budgetAmount = budgetCategory.Amount;
                    var categoryId = budgetCategory.CategoryId;
                    var category = cs.GetCategoryById(budgetCategory.CategoryId).Name;

                    foreach (TransactionListItem transaction in transactionList.Where(i => i.CategoryId == categoryId))
                    {
                        decimal transactionAmount = transaction.Amount;
                        remainingCategoryAmountList.Add(transactionAmount);
                    }
                    decimal remainingCategoryAmount = remainingCategoryAmountList.Sum();

                    categoryList.Add(new CategoryListItem
                    {
                        CategoryId = categoryId,
                        Name = category,
                        CategoryAmount = budgetAmount,
                        RemainingCategoryAmount = remainingCategoryAmount
                    }); ;
                }

                List<decimal> remainingBudgetAmountList = new List<decimal>();

                foreach (TransactionListItem transaction in transactionList)
                {
                    decimal transactionAmount = transaction.Amount;
                    remainingBudgetAmountList.Add(transactionAmount);
                }

                decimal remainingBudgetAmount = remainingBudgetAmountList.Sum();

                return
                    new BudgetDetail
                    {
                        BudgetId = entity.BudgetId,
                        BudgetName = entity.BudgetName,
                        BudgetAmount = entity.BudgetAmount,
                        RemainingBudgetAmount = remainingBudgetAmount,
                        ListOfCategoryIds = entity.ListOfCategoryIds,
                        ListOfCategories = categoryList,
                        ListOfTransactionIds = entity.ListOfTransactionIds,
                        ListOfTransactions = transactionList
                    };
            }
        }

        public bool UpdateBudget(BudgetEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Budgets
                        .Single(e => e.BudgetId == model.BudgetId && e.OwnerId == _userId);

                entity.BudgetName = model.BudgetName;
                entity.BudgetAmount = model.BudgetAmount;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBudget(int budgetId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Budgets
                        .Single(e => e.BudgetId == budgetId && e.OwnerId == _userId);

                ctx.Budgets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        //public IEnumerable<TransactionListItem> GetTransactionById(int id)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var query =
        //            ctx
        //                .Transactions
        //                .Where 
        //    }
        //}
    }
}
