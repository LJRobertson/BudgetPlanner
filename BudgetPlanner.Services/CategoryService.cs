using BudgetPlanner.Data;
using BudgetPlanner.Models;
using BudgetPlanner.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;

        public CategoryService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            var entity =
                new Category()
                {
                    UserId = _userId,
                    Name = model.Name,
                    ListOfBudgetIds = model.ListOfBudgetIds
                };

            var amount = model.CategoryAmount;

            using (var ctx = new ApplicationDbContext())
            {
                foreach(int i in entity.ListOfBudgetIds)
                {
                    var budgetCategoryEntity =
                            new BudgetCategory()
                            {
                                BudgetId = i,
                                CategoryId = entity.CategoryId
                            };
                    ctx.BudgetCategory.Add(budgetCategoryEntity);
                }
                ctx.Categories.Add(entity);
                //ctx.BudgetCategory.Add(amount);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CategoryListItem> GetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Categories
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new CategoryListItem
                                {
                                    CategoryId = e.CategoryId,
                                    Name = e.Name
                                }
                               );

                return query.ToArray();
            }
        }

        public CategoryListItem GetCategoryListItemById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryId == id && e.UserId == _userId);

                return
                    new CategoryListItem
                    {
                        CategoryId = entity.CategoryId,
                        Name = entity.Name
                    };
            }
        }

        public CategoryDetail GetCategoryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //find this category
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryId == id);

                //get the list of budget numbers that use this category
                var budgetCategoryList =
                    ctx
                        .BudgetCategory
                        .Where(e => e.CategoryId == id)
                        .ToList();

                //get the amounts used for this category
                var budgetCategoryAmountItem =
                   ctx
                       .BudgetCategory
                       .Where(e => e.CategoryId == id)
                       .Select(e => e.Amount);

                //link to Budget Service
                var bs = new BudgetService();              

                //Extract each Budget from the budgetCategoryList
                List<BudgetListItem> budgetList = new List<BudgetListItem>();
                foreach (var bc in budgetCategoryList)
                {
                    var budgetId = bc.BudgetId;
                    var amount = bc.Amount;
                    var budgetName = bs.GetBudgetListItemById(bc.BudgetId, _userId).BudgetName;
                    budgetList.Add(new BudgetListItem
                    {
                        BudgetId = budgetId,
                        BudgetName = budgetName,
                        BudgetAmount = amount
                    });
                }

                return
                    new CategoryDetail
                    {
                        CategoryId = entity.CategoryId,
                        Name = entity.Name,
                        ListOfBudgets = budgetList
                    };
            }
        }

        public bool UpdateCategory(CategoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryId == model.CategoryId && e.UserId == _userId);

                entity.Name = model.Name;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCategory(int categoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryId == categoryId && e.UserId == _userId);

                ctx.Categories.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
