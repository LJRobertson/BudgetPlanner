using BudgetPlanner.Data;
using BudgetPlanner.Models.BudgetCategory;
using BudgetPlanner.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetPlanner.WebMVC.Controllers
{
    [Authorize]
    public class BudgetCategoryController : Controller
    {
        // GET: BudgetCategory
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BudgetCategoryService(userId);
            var model = service.GetBudgetCategories();

            return View(model);
        }

        public ActionResult Create()
        {
            BudgetService budgetService = new BudgetService();

            Guid userid = Guid.Parse( User.Identity.GetUserId());

            var budget = new SelectList(budgetService.GetBudgets(userid), "BudgetId", "BudgetName");
            ViewBag.Budgets = budget;

            CategoryService categoryService = new CategoryService(userid);

            var category = new SelectList(categoryService.GetCategories(), "CategoryId", "Name");
            ViewBag.Categories = category;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BudgetCategoryCreate model)
        {
            var ctx = new ApplicationDbContext();

            var budget = ctx.Budgets.Find(model.BudgetId);
            if (budget == null)
            {
                return HttpNotFound("Budget not found.");
            }

            var category = ctx.Categories.Find(model.CategoryId);
            if (category == null)
            {
                return HttpNotFound("Category not found.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateBudgetCategoryService();

            if (service.CreateBudgetCategory(model))
            {
                TempData["SaveResult"] = "Your Budget Category was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Budget Category could not be created.");

            return View(model);
        }

        public ActionResult Details(int budgetId, int categoryId)
        {
            var svc = CreateBudgetCategoryService();
            var model = svc.GetBudgetCategoryByIds(budgetId, categoryId);

            return View(model);
        }

        public ActionResult Edit(int budgetId, int categoryId)
        {
            var service = CreateBudgetCategoryService();
            var detail = service.GetBudgetCategoryByIds(budgetId, categoryId);
            var model =
                new BudgetCategoryEdit
                {
                    BudgetId = detail.BudgetId,
                    CategoryId = detail.CategoryId,
                    Amount = detail.Amount
                };

            return View(model);
        }

        [HttpPost]
        [Route("api/BudgetCategory/Edit/{budgetId}/{categoryId}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int budgetId, int categoryId, BudgetCategoryEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.BudgetId != budgetId && model.CategoryId != categoryId)
            {
                ModelState.AddModelError("", "BudgetId or CategoryIdMismatch");
                return View(model);
            }

            var service = CreateBudgetCategoryService();

            if (service.UpdateBudgetCategory(model))
            {
                TempData["SaveResult"] = "Your Budget Category was updated.";
                return RedirectToAction("Index");
            }

            return View();
        }

        [ActionName("Delete")]
        public ActionResult Delete(int budgetId, int categoryId)
        {
            var svc = CreateBudgetCategoryService();
            var model = svc.GetBudgetCategoryByIds(budgetId, categoryId);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        //[Route("BudgetCategoryRoute", "api/BudgetCategory/Delete/{id}/{id2}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBudgetCategory(int budgetId, int categoryId)
        {
            var service = CreateBudgetCategoryService();

            service.DeleteBudgetCategory(budgetId, categoryId);

            TempData["saveResult"] = "Your Budget Category was deleted.";

            return RedirectToAction("Index");
        }

        private BudgetCategoryService CreateBudgetCategoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BudgetCategoryService(userId);
            return service;
        }

    }
}