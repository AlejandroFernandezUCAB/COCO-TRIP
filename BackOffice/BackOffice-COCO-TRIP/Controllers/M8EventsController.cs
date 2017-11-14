using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackOffice_COCO_TRIP.Controllers
{
    public class M8EventsController : Controller
    {
        // GET: M8Events
        public ActionResult Index()
        {
            return View();
        }

        // GET: M8Events/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: M8Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: M8Events/Create
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

        // GET: M8Events/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: M8Events/Edit/5
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

        // GET: M8Events/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: M8Events/Delete/5
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
