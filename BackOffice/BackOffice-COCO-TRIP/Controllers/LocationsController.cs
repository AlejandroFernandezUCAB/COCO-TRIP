using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackOffice_COCO_TRIP.Views.Locations
{
    public class LocationsController : Controller
    {
        // GET: Locations
        public ActionResult Index()
        {
            return View();
        }

        // GET: Locations/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

    // GET: Locations/CreateLocationEvent
    public ActionResult CreateLocationEvent()
        {
            return View();
        }

    // POST: Locations/CreateLocationEvent
    [HttpPost]
        public ActionResult CreateLocationEvent(FormCollection collection)
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

    // GET: Locations/ListLocationEvent
    public ActionResult ListLocationEvent()
        {
          return View();
        }

        // POST: Locations/ListLocationEvent
        [HttpPost]
        public ActionResult ListLocationEvent(FormCollection collection)
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
    // GET: Locations/Edit/5
    public ActionResult Edit(int id)
            {
                return View();
            }

            // POST: Locations/Edit/5
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

        // GET: Locations/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Locations/Delete/5
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
