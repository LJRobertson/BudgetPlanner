using BudgetPlanner.Data;
using BudgetPlanner.Models.Transaction;
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
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TransactionService(userId);
            var model = service.GetTransactions();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            var ctx = new ApplicationDbContext();

            var budget = new SelectList(ctx.Budgets.ToList(), "BudgetId", "BudgetName");
            ViewBag.Budgets = budget;

            var category = new SelectList(ctx.Categories.ToList(), "CategoryId", "Name");
            ViewBag.Categories = category;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionCreate model)
        {
            var ctx = new ApplicationDbContext();

            var budget = ctx.Budgets.Find(model.BudgetId);
            if (budget == null)
            {
                return HttpNotFound("Budget not found.");
            }

            var category = ctx.Categories.Find(model.CategoryId);
            if(category == null)
            {
                return HttpNotFound("Category not found.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateTransactionService();

            if (service.CreateTransaction(model))
            {
                TempData["SaveResult"] = "Your transaction was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Transaction could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateTransactionService();
            var model = svc.GetTransactionById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var ctx = new ApplicationDbContext();

            var budget = new SelectList(ctx.Budgets.ToList(), "BudgetId", "BudgetName");
            ViewBag.Budgets = budget;

            var category = new SelectList(ctx.Categories, "CategoryId", "Name");
            ViewBag.CategoryId = category;

            var service = CreateTransactionService();
            var detail = service.GetTransactionById(id);
            var model =
                new TransactionEdit
                {
                    TransactionId = detail.TransactionId,
                    BudgetId = detail.BudgetId,
                    MerchantName = detail.MerchantName,
                    Amount = detail.Amount,
                    TransactionDate = detail.TransactionDate,
                    CategoryId = detail.CategoryId,
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TransactionEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.TransactionId != id)
            {
                var ctx = new ApplicationDbContext();

                var category = new SelectList(ctx.Categories, "CategoryId", "Name");
                ViewBag.CategoryId = category;

                ModelState.AddModelError("", "Transaction ID does not match.");
                return View(model);
            }

            var service = CreateTransactionService();

            if (service.UpdateTransaction(model))
            {
                TempData["SaveResult"] = "Your transaction was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your transaction could not be updated.");
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var svc = CreateTransactionService();
            var model = svc.GetTransactionById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateTransactionService();

            service.DeleteTransaction(id);

            TempData["SaveResult"] = "Your transaction was deleted.";

            return RedirectToAction("Index");
        }

        private TransactionService CreateTransactionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TransactionService(userId);
            return service;
        }
    }
}