using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Public.Controllers
{
    public class ContController : Controller
    {
        // GET: Cont
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logare()
        {
            return View();
        }

        // GET: Cont/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cont/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cont/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cont/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cont/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cont/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
