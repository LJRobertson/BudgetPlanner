using BudgetPlanner.Models.Memo;
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
    public class MemoController : Controller
    {
        // GET: Memo
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MemoService(userId);
            var model = new MemoListItem[0];
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemoCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateMemoService();

            if (service.CreateMemo(model))
            {
                TempData["SaveResult"] = "Your memo was created.";
                return RedirectToAction("Index");
            };

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateMemoService();
            var model = svc.GetMemoById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateMemoService();
            var detail = service.GetMemoById(id);
            var model =
                new MemoEdit
                {
                    TransactionId = detail.TransactionId,
                    MemoContent = detail.MemoContent
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MemoEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.TransactionId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateMemoService();

            if (service.UpdateMemo(model))
            {
                TempData["SaveResult"] = "Your memo was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your memo could not be updated.");
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var svc = CreateMemoService();
            var model = svc.GetMemoById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateMemoService();

            service.DeleteMemo(id);

            TempData["SaveResult"] = "Your memo was deleted.";

            return RedirectToAction("Index");
        }

        private MemoService CreateMemoService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MemoService(userId);
            return service;
        }
    }
}