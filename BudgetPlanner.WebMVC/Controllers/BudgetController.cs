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
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        // GET: Budget
        public ActionResult Index()
        {
            //Guid ownerId = Guid.Empty;
            var user = User.Identity.GetUserId();
            Guid ownerId = new Guid(user);
            //var service = new BudgetService(userId);

            var model = _budgetService.GetBudgets(ownerId);


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
            model.UserId = User.Identity.GetUserId();

            //var service = CreateBudgetService();

            if (_budgetService.CreateBudget(model))
            {
                TempData["SaveResult"] = "Your budget was successfully created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your budget could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var user = User.Identity.GetUserId();
            Guid userId = new Guid(user);

            //var svc = CreateBudgetService();
            var model = _budgetService.GetBudgetById(id, userId);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var user = User.Identity.GetUserId();
            Guid userId = new Guid(user);

            //var service = CreateBudgetService();
            var detail = _budgetService.GetBudgetById(id, userId);
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
        public ActionResult Edit(int id, BudgetEdit model)
        {
            var user = User.Identity.GetUserId();
            Guid userId = new Guid(user);

            if (!ModelState.IsValid) return View(model);

            if (model.BudgetId != id)
            {
                ModelState.AddModelError("", "Budget ID does not match.");

                return View(model);
            }

            //var service = CreateBudgetService();

            if (_budgetService.UpdateBudget(model, userId))
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
            var user = User.Identity.GetUserId();
            Guid userId = new Guid(user);
            //var svc = CreateBudgetService();
            var model = _budgetService.GetBudgetById(id, userId);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBudget(int id)
        {
            var user = User.Identity.GetUserId();
            Guid userId = new Guid(user);

            //var service = CreateBudgetService();

            _budgetService.DeleteBudget(id, userId);

            TempData["SaveResult"] = "Your budget was deleted.";

            return RedirectToAction("Index");
        }


        //replace code below with a User ID property on model and (User.Identity.GetUserId()
        //private BudgetService CreateBudgetService()
        //{
        //    var userId = Guid.Parse(User.Identity.GetUserId());
        //    var service = new BudgetService(userId);
        //    return service;
        //}
    }
}