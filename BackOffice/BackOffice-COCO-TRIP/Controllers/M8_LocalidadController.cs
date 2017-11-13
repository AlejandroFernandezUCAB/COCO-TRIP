using BackOffice_COCO_TRIP.Models;
using BackOffice_COCO_TRIP.Models.Peticion;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackOffice_COCO_TRIP.Controllers
{
    public class M8_LocalidadController : Controller
    {
        private PeticionM8_Localidad peticion = new PeticionM8_Localidad();

      

        // GET: M8_Localidad/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: M8_Localidad/Create
        public ActionResult Create()
        {
            return View();
        }

    // POST: M8_Localidad/Create
    [HttpPost]
    public ActionResult Create(M8_Localidad localidad)
    {

      // TODO: Add insert logic here
      JObject respuesta = peticion.Post(localidad);
      if (respuesta.Property("dato") == null)
      {


        ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");

      }
      else
      {
        return RedirectToAction("Index");
      }

      
      return View();
    }

        // GET: M8_Localidad/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: M8_Localidad/Edit/5
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

        // GET: M8_Localidad/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: M8_Localidad/Delete/5
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
