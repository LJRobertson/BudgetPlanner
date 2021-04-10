using BudgetPlanner.Data;
using BudgetPlanner.Models;
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
                return
                    new BudgetDetail
                    {
                        BudgetId = entity.BudgetId,
                        BudgetName = entity.BudgetName,
                        BudgetAmount = entity.BudgetAmount,
                        ListOfCategoryIds = entity.ListOfCategoryIds,
                        ListOfTransactionIds = entity.ListOfTransactionIds,
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
                entity.ListOfCategoryIds = model.ListOfCategoryIds;
                entity.ListOfTransactionIds = model.ListOfTransactionIds;

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
    }
}
