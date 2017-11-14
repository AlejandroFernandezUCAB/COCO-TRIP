using BackOffice_COCO_TRIP.Models.Peticion;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackOffice_COCO_TRIP.Views.Events
{
    public class EventsController : Controller
    {
    private PeticionEvento peticion = new PeticionEvento();

    // GET: Events
    public ActionResult Index()
        {
            return View();
        }

        // GET: Events/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Events/Create
        public ActionResult Create(Evento evento)
        {
        JObject respuesta = peticion.Post(evento);
      if (respuesta.Property("dato") == null)
      {
        ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");
      }
      else
      {
        ModelState.AddModelError(string.Empty, "Se hizo con exito");
      }
         return View();
        }

        // POST: Events/Create
        [HttpPost]
        public ActionResult CreateEvents(FormCollection collection)
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
    // GET: Events/FilterEvent/5
    public ActionResult FilterEvent()
    {
      return View();
    }

    // POST: Events/FilterEvent/5
    [HttpPost]
    public ActionResult FilterEvent(int id, FormCollection collection)
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


    // GET: Events/Edit/5
    public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Events/Edit/5
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

        // GET: Events/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Events/Delete/5
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
