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
    private PeticionCategoria peticionCategoria = new PeticionCategoria();
    private PeticionM8_Localidad peticionLocalidad = new PeticionM8_Localidad();
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
      ViewBag.Title = "Categorías";
      IList<Categories> listaCategorias = null;
      IList<LocalidadEvento> listaLocalidades = null;
      try
      {
        JObject respuestaCategoria = peticionCategoria.Get(id);
        JObject respuestaLocalidad = peticionLocalidad.GetAll();
        if (respuestaCategoria.Property("data") != null)
        {
          listaCategorias = respuestaCategoria["data"].ToObject<List<Categories>>();
        }

        else
        {
          listaCategorias = new List<Categories>();
          ModelState.AddModelError(string.Empty, "Error en la comunicacion o No existen Categorias");
        }

        if (respuestaLocalidad.Property("dato") != null)
        {
          listaLocalidades = respuestaLocalidad["dato"].ToObject<List<LocalidadEvento>>();
        }

        else
        {
          listaLocalidades = new List<LocalidadEvento>();
          ModelState.AddModelError(string.Empty, "Error en la comunicacion o No existen localidades");
        }

        TempData["listaLocalidad"] = listaLocalidades;
      }
      catch (Exception e)
      {

        throw e;
      }
      ViewBag.ListCategoria = listaCategorias;
      ViewBag.ListLocalidades = listaLocalidades;
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

   
  }
}
