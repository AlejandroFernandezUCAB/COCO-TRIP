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

    // GET: M8_Localidad/Index
    public ActionResult Index()
    {
      ViewBag.Title = "Localidades de los Eventos";
      IList<LocalidadEvento> listLocalidadEvento = null;
      JObject respuesta = peticion.GetAll();
      if (respuesta.Property("dato") != null)
      {
        listLocalidadEvento = respuesta["dato"].ToObject<List<LocalidadEvento>>();
      }

      else
      {
        listLocalidadEvento = new List<LocalidadEvento>();
        ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");
      }

      TempData["listaLocalidadEvento"] = listLocalidadEvento;

      return View(listLocalidadEvento);
    }

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
    public ActionResult Create(LocalidadEvento localidad)
    {
      
      // TODO: Add insert logic here
      JObject respuesta = peticion.Post(localidad);
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

        // GET: M8_Localidad/Edit/5
        public ActionResult Edit(int id)
        {
          JObject respuesta = peticion.Get(id);

            if (respuesta.Property("dato") == null)
            {


              ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");

            }
            else
            {
              ModelState.AddModelError(string.Empty, "Se hizo con exito");
              LocalidadEvento localidadActualizar = respuesta.ToObject<LocalidadEvento>();
              return View(localidadActualizar);
            }


      return RedirectToAction("Index");
    }

        // POST: M8_Localidad/Edit/5
        [HttpPost]
        public ActionResult Edit(LocalidadEvento localidad)
                {
                      JObject respuesta = peticion.Put(localidad);
              if (respuesta.Property("dato") == null)
              {


                ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");

              }
              else
              {
                ModelState.AddModelError(string.Empty, "Se hizo con exito");
                return RedirectToAction("Index");
              }


              return View();
    }

      

        // POST: M8_Localidad/Delete/5
        
        public ActionResult Delete(int id, FormCollection collection)
        {
            // TODO: Add insert logic here
            JObject respuesta = peticion.Delete(id);
            if (respuesta.Property("dato") == null)
            {


              ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");

            }
            else
            {
              ModelState.AddModelError(string.Empty, "Se hizo con exito");
        
            }


            return RedirectToAction("Index");



         }
    }
}
