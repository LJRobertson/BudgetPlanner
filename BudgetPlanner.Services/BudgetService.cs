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
                //var categoryTest;
                //foreach (var category in )

                foreach (var testAmount in budgetCategoryList)
                {
                    var budgetAmount = testAmount.Amount;
                    var categoryId = testAmount.CategoryId;
                    var category = cs.GetCategoryById(testAmount.CategoryId).Name;
                    categoryList.Add(new CategoryListItem
                    {
                        CategoryId = categoryId,
                        Name = category,
                        CategoryAmount = budgetAmount
                    });
                }


                //var categoryTestList = new List<CategoryListItem>();

                //var categoryTestAmount =
                //     ctx
                //         .BudgetCategory
                //         .Where(e => e.BudgetId == id);


                //foreach (var categoryTest in budgetCategoryList)
                //{
                //    var categoryEntity =
                //      ctx
                //          .Categories
                //          .Where(e => e.CategoryId == categoryTest.CategoryId)
                //           .Select(
                //              e =>
                //                  new CategoryListItem
                //                  {
                //                      Name = e.Name,
                //                      //CategoryAmount = 
                //                  }
                //                  );
                //}

                return
                    new BudgetDetail
                    {
                        BudgetId = entity.BudgetId,
                        BudgetName = entity.BudgetName,
                        BudgetAmount = entity.BudgetAmount,
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

        //public bool UpdateBudget2(BudgetEdit model)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var entity =
        //            ctx
        //                .Budgets
        //                .Single(e => e.BudgetId == model.BudgetId && e.OwnerId == _userId);

        //        //compile transaction list of Ids and names
        //        var ts = new TransactionService(_userId);

        //        List<int> transactionIdList = entity.ListOfTransactionIds;

        //        foreach (var transactionId in model.ListOfTransactionIds)
        //        {
        //            transactionIdList.Add(transactionId);
        //        }

        //        //compile category list of ids and names
        //        var cs = new CategoryService(_userId);

        //        var categoryList = new List<CategoryDetail>();

        //        var categoriesToRemove = ctx.BudgetCategory.Where(e => e.BudgetId == entity.BudgetId);
        //        foreach (BudgetCategory bc in categoriesToRemove)
        //        {
        //            ctx.BudgetCategory.Remove(bc);
        //        }

        //        foreach (int category in model.ListOfCategoryIds)
        //        {
        //            var categoryEntity =
        //                new BudgetCategory()
        //                {
        //                    BudgetId = model.BudgetId,
        //                    CategoryId = category
        //                };
        //            ctx.BudgetCategory.Add(categoryEntity);
        //        }

        //        entity.BudgetName = model.BudgetName;
        //        entity.BudgetAmount = model.BudgetAmount;
        //        entity.ListOfCategoryIds = model.ListOfCategoryIds;
        //        //entity.ListOfCategories = categoryList;
        //        entity.ListOfTransactionIds = model.ListOfTransactionIds;
        //        //entity.ListOfTransactions = entity.ListOfTransactionIds;

        //        return ctx.SaveChanges() == 1;
        //    }
        //}

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
