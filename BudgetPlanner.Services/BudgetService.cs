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

            List<int> transdctionIdList = new List<int>();

            foreach (int tId in model.ListOfTransactionIds)
            {
                transdctionIdList.Add(tId);
            }

            var entity =
                new Budget()
                {
                    OwnerId = _userId,
                    BudgetName = model.BudgetName,
                    BudgetAmount = model.BudgetAmount,
                    ListOfCategoryIds = model.ListOfCategoryIds,
                    ListOfTransactionIds = transdctionIdList
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
            var ts = new TransactionService(_userId);
            List<int> transactionIdList = new List<int>();

            using (var ctx = new ApplicationDbContext())
            {
                foreach (var transaction in
                     ctx
                         .Transactions
                         .Where(t => t.BudgetId == id))
                {
                    transactionIdList.Add(transaction.TransactionId);
                }
                var entity =
                    ctx
                        .Budgets
                        .Single(e => e.BudgetId == id && e.OwnerId == _userId);

                //compile transaction list of Ids and names
                //var transactionList = ts.GetTransactionListItemById(tId);
                //foreach (var transactionId in entity.ListOfTransactionIds)
                //{
                //    var testTransaction = ts.GetTransactionListItemById(transactionId);
                //    transactionList.Add(testTransaction);
                //}


                //if (entity.ListOfTransactionIds != null)
                //{
                //    foreach (TransactionListItem tli in transactionList.Where(e.BudgetId == tli. )
                //    {
                //        entity.ListOfTransactionIds.Add(transaction.TransactionId);
                //    }
                //}
                //compile category list of ids and names

                var cs = new CategoryService(_userId);
                var categoryList = new List<CategoryListItem>();
                if (entity.ListOfCategoryIds != null)
                {
                    foreach (var categoryId in entity.ListOfCategoryIds)
                    {
                        var testCategory = cs.GetCategoryListItemById(categoryId);
                        categoryList.Add(testCategory);
                        //categoryList.Add(ctx.Categories.Find(categoryId).Name);
                    };
                }

                //return the budget information
                return
                    new BudgetDetail
                    {
                        BudgetId = entity.BudgetId,
                        BudgetName = entity.BudgetName,
                        BudgetAmount = entity.BudgetAmount,
                        ListOfCategoryIds = entity.ListOfCategoryIds,
                        ListOfCategories = categoryList,
                        ListOfTransactionIds = entity.ListOfTransactionIds,
                        //ListOfTransactions = entity.ListOfTransactions
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

                //compile transaction list of Ids and names
                var ts = new TransactionService(_userId);

                //remove transactions from Budget
                if (entity.ListOfTransactionIds != null)
                {
                    var transactionsToRemove = entity.ListOfTransactionIds;
                    foreach (var item in transactionsToRemove)
                    {
                        entity.ListOfTransactionIds.Remove(item);
                    }
                }

                //var transactionList = new List<TransactionDetail>();
                var transactionIdList = new List<int>();

                if (entity.ListOfTransactionIds == null)
                {
                    foreach (var transactionId in model.ListOfTransactionIds)
                    {
                        //var testTransaction = ts.GetTransactionById(transactionId);
                        //transactionList.Add(testTransaction);
                        //entity.ListOfTransactionIds.Add(transactionId);
                        transactionIdList.Add(transactionId);
                    }
                }

                //compile category list of ids and names
                var cs = new CategoryService(_userId);

                var categoryList = new List<CategoryDetail>();

                if (entity.ListOfCategoryIds != null)
                {
                    foreach (var categoryId in entity.ListOfCategoryIds)
                    {
                        var testCategory = cs.GetCategoryById(categoryId);
                        categoryList.Add(testCategory);
                        //categoryList.Add(ctx.Categories.Find(categoryId).Name);
                    };
                }

                entity.BudgetName = model.BudgetName;
                entity.BudgetAmount = model.BudgetAmount;
                entity.ListOfCategoryIds = model.ListOfCategoryIds;
                //entity.ListOfCategories = categoryList;
                entity.ListOfTransactionIds = model.ListOfTransactionIds;
                //entity.ListOfTransactions = entity.ListOfTransactionIds;

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
