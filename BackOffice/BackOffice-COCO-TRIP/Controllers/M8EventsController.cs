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
  public class M8EventsController : Controller
  {
    private PeticionCategoria peticion = new PeticionCategoria();
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
    [HttpGet]
    // GET: M8Events/Create
    public ActionResult CreateEvent(int id = -1)
    {
      //buscar categorias para el select
      /*IList<Categories> MyList = new List<Categories>(){
                new Categories(){Id=1, Name="UK"},
                new Categories(){Id=2, Name="VE"}
          };
      */
      ViewBag.Title = "Categorías";
      IList<Categories> MyList = null;
      try
      {
        JObject respuesta = peticion.Get(id);
        if (respuesta.Property("data") != null)
        {
          MyList = respuesta["data"].ToObject<List<Categories>>();
        }

        else
        {
          MyList = new List<Categories>();
          ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");
        }

        TempData["listaCategorias"] = MyList;
      }
      catch (Exception e)
      {

        throw e;
      }
      ViewBag.MyList = MyList;
      return View();
    }

    // POST: M8Events/Create
    [HttpPost]
    public ActionResult CreateEvent(FormCollection collection)
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

    // GET: M8Events
    [HttpGet]
    public ActionResult FilterEvent()
    {
      return View();
    }

    /**
    // GET: M8Events
        [HttpPost]
        public ActionResult FilterEvent()
        {
          return View();
        }
    **/
  }
}
