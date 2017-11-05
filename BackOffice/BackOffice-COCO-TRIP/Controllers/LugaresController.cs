using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackOffice_COCO_TRIP.Controllers
{
    public class LugaresController : Controller
    {
        // GET:Lugares
        public ActionResult Index()
        {
            ViewBag.Title = "Lugares Turísticos";
            return View();
        }

        // GET:Lugares/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET:Lugares/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Lugar Turístico";
            return View();
        }

        // POST:Lugares/Create
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

        // GET:Lugares/Modify
        public ActionResult Modify()
        {
            ViewBag.Title = "Modificar Lugar Turístico";
            return View();
        }

        // POST:Lugares/Modify
        [HttpPost]
        public ActionResult Modify(FormCollection collection)
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

        // GET:Lugares/DetailLugar
        public ActionResult LugarDetail()
        {
            ViewBag.Title = "Detalle de Lugar Turístico";
            return View();
        }

        // POST:Lugares/DetailLugar
        [HttpPost]
        public ActionResult LugarDetail(FormCollection collection)
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

        // GET:Lugares/DetailActivity
        public ActionResult ActivityDetail()
        {
            ViewBag.Title = "Detalle de Actividad";
            return View();
        }

        // POST:Lugares/DetailActivity
        [HttpPost]
        public ActionResult ActivityDetail(FormCollection collection)
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

        // GET:Lugares/ViewAll
        public ActionResult ViewAll()
        {
            ViewBag.Title = "Lugares Turísticos";
            return View();
        }

        // POST:Lugares/ViewAll
        [HttpPost]
        public ActionResult ViewAll(FormCollection collection)
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

        // GET:Lugares/Activate/5
        public ActionResult Activate(int id)
        {
            return View();
        }

        // POST:Lugares/Activate/5
        [HttpPost]
        public ActionResult Activate(int id, FormCollection collection)
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
