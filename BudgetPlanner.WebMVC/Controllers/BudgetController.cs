using BudgetPlanner.Models;
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
    public class BudgetController : Controller
    {
        // GET: Budget
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BudgetService(userId);
            var model = service.GetBudgets();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BudgetCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateBudgetService();

            if (service.CreateBudget(model))
            {
                TempData["SaveResult"] = "Your budget was successfully created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your budget could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateBudgetService();
            var model = svc.GetBudgetById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateBudgetService();
            var detail = service.GetBudgetById(id);
            var model =
                new BudgetEdit
                {
                    BudgetId = detail.BudgetId,
                    BudgetName = detail.BudgetName,
                    BudgetAmount = detail.BudgetAmount,
                    //ListOfCategoryIds = detail.ListOfCategoryIds,
                    //ListOfTransactionIds = detail.ListOfTransactionIds
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (int id, BudgetEdit model)
        {
            if(!ModelState.IsValid) return View(model);

            if(model.BudgetId != id)
            {
                ModelState.AddModelError("", "Budget ID does not match.");

                return View(model);
            }

            var service = CreateBudgetService();

            if (service.UpdateBudget(model))
            {
                TempData["SaveResult"] = "Your budget was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your budget could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateBudgetService();
            var model = svc.GetBudgetById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBudget(int id)
        {
            var service = CreateBudgetService();

            service.DeleteBudget(id);

            TempData["SaveResult"] = "Your budget was deleted.";

            return RedirectToAction("Index");
        }

        private BudgetService CreateBudgetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BudgetService(userId);
            return service;
        }
    }
}