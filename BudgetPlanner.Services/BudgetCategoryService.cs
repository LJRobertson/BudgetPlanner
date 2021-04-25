using BudgetPlanner.Data;
using BudgetPlanner.Models.BudgetCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class BudgetCategoryService
    {
        private readonly Guid _userId;

        public BudgetCategoryService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateBudgetCategory(BudgetCategoryCreate model)
        {
            var entity =
                new BudgetCategory()
                {
                    BudgetId = model.BudgetId,
                    CategoryId = model.CategoryId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.BudgetCategory.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BudgetCategoryListItem> GetBudgetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .BudgetCategory
                        .Where(e => e.Budget.OwnerId == _userId && e.Category.UserId == _userId)
                        .Select(
                            e =>
                                new BudgetCategoryListItem
                                {
                                    BudgetId = e.BudgetId,
                                    CategoryId = e.CategoryId,
                                    Amount = e.Amount
                                }
                                );
                return query.ToArray();
            }
        }

        //Get by Budget Id
        public BudgetCategoryDetail GetBudgetCategoryByIds(int budgetId, int categoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BudgetCategory
                        .Single(e => e.BudgetId == budgetId && e.CategoryId == categoryId);
                return
                    new BudgetCategoryDetail
                    {
                        BudgetId = entity.BudgetId,
                        CategoryId = entity.CategoryId,
                        Amount = entity.Amount
                    };
            }
        }

        public bool UpdateBudgetCategory(BudgetCategoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BudgetCategory
                        .Single(e => e.BudgetId == model.BudgetId && e.Budget.OwnerId == _userId);

                entity.BudgetId = model.BudgetId;
                entity.CategoryId = model.CategoryId;
                entity.Amount = model.Amount;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBudgetCategory(int budgetId, int categoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BudgetCategory
                        .Single(e => e.BudgetId == budgetId && e.CategoryId == categoryId);

                ctx.BudgetCategory.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
