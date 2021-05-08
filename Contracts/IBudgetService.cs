using BudgetPlanner.Models;
using System;
using System.Collections.Generic;

namespace BudgetPlanner.Services
{
    public interface IBudgetService
    {
        bool CreateBudget(BudgetCreate model);
        bool DeleteBudget(int budgetId, Guid userId);
        BudgetDetail GetBudgetById(int id, Guid userId);
        IEnumerable<BudgetListItem> GetBudgets(Guid id);
        bool UpdateBudget(BudgetEdit model, Guid userId);
    }
}