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
    public class BudgetService : IBudgetService
    {
        private readonly Guid _userId;


        //The method below breaks the dependency injection
        //public BudgetService(Guid userId)
        //{
        //    _userId = userId;
        //}

        public bool CreateBudget(BudgetCreate model)
        {
            var entity =
                new Budget()
                {
                    OwnerId = Guid.Parse(model.UserId),
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

        public IEnumerable<BudgetListItem> GetBudgets(Guid id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Budgets
                        .Where(e => e.OwnerId == id)
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

        public BudgetDetail GetBudgetById(int id, Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Budgets
                        .Single(e => e.BudgetId == id && e.OwnerId == userId);

                var transactionList =
                    ctx
                        .Transactions
                        .Where(e => e.BudgetId == id && e.UserId == userId)
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

                var cs = new CategoryService(userId);
                var categoryList = new List<CategoryListItem>();


                foreach (var budgetCategory in budgetCategoryList)
                {
                    var categoryAmountSpentList = new List<decimal>();
                    var budgetAmount = budgetCategory.Amount;
                    var categoryId = budgetCategory.CategoryId;
                    var category = cs.GetCategoryById(budgetCategory.CategoryId).Name;

                    foreach (TransactionListItem transaction in transactionList.Where(i => i.CategoryId == categoryId))
                    {
                        decimal transactionAmount = transaction.Amount;
                        categoryAmountSpentList.Add(transactionAmount);
                    }

                    categoryList.Add(new CategoryListItem
                    {
                        CategoryId = categoryId,
                        Name = category,
                        CategoryAmount = budgetAmount,
                        AmountSpent = categoryAmountSpentList.Sum(),
                        RemainingCategoryAmount = budgetAmount - categoryAmountSpentList.Sum(),
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
                        AmountSpent = remainingBudgetAmount,
                        ListOfCategoryIds = entity.ListOfCategoryIds,
                        ListOfCategories = categoryList,
                        ListOfTransactionIds = entity.ListOfTransactionIds,
                        ListOfTransactions = transactionList
                    };
            }
        }

        public BudgetListItem GetBudgetListItemById(int id, Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Budgets
                        .Single(e => e.BudgetId == id && e.OwnerId == userId);

                return
                    new BudgetListItem
                    {
                        BudgetId = entity.BudgetId,
                        BudgetName = entity.BudgetName,
                        BudgetAmount = entity.BudgetAmount,
                    };
            }
        }


        public bool UpdateBudget(BudgetEdit model, Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Budgets
                        .Single(e => e.BudgetId == model.BudgetId && e.OwnerId == userId);

                entity.BudgetName = model.BudgetName;
                entity.BudgetAmount = model.BudgetAmount;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBudget(int budgetId, Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Budgets
                        .Single(e => e.BudgetId == budgetId && e.OwnerId == userId);

                ctx.Budgets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
