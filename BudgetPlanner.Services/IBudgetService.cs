using BudgetPlanner.Models;
using System.Collections.Generic;

namespace BudgetPlanner.Services
{
    public interface IBudgetService
    {
        bool CreateBudget(BudgetCreate model);
        bool DeleteBudget(int budgetId);
        BudgetDetail GetBudgetById(int id);
        IEnumerable<BudgetListItem> GetBudgets();
        bool UpdateBudget(BudgetEdit model);
    }
}