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
    private PeticionEvento peticionEvento = new PeticionEvento();
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
      }
      catch (Exception e)
      {

        throw e;
      }
      ViewBag.ListLocalidades = listaLocalidades;
      ViewBag.ListCategoria = listaCategorias;
      return View();
    }

    // POST: M8Events/Create
    [HttpPost]
    public ActionResult CreateEvent(Evento evento)
    {
      try
      {
       //var idLocalidad = Request["Localidades"].ToString();
        //evento.IdLocalidad = Int32.Parse(idLocalidad);
        evento.IdLocalidad = 1;
        evento.Foto = "jorge";
        System.Console.Write("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        /*evento.FechaInicio = new DateTime(12, 12, 12);
        evento.FechaFin = new DateTime(12, 12, 12);
        evento.HoraInicio.AddHours(12);
        evento.HoraInicio.AddMinutes(12);
        evento.HoraFin.AddHours(12);
        evento.HoraFin.AddMinutes(12);*/

        var id = Request["Categoria"].ToString();
        evento.IdCategoria = Int32.Parse(id);
        System.Console.Write("EL ID DE LA LOCALIDAD ES: "+evento.IdLocalidad);


        JObject respuesta = peticionEvento.Post(evento);
        if (respuesta.Property("dato") == null)
        {

          
          ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");

        }
        else
        {
          
          ModelState.AddModelError(string.Empty, "Se hizo con exito");
          return RedirectToAction("FilterEvent");
        }
      }
      catch (NullReferenceException e)
      {

        throw e;
      }


      return View();
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

     
      ViewBag.Title = "Eventos por Categorias";
      IList<Categories> MyList = null;
      try
      {
        JObject respuesta = peticionCategoria.Get(-1);
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
      IList<Evento> evento;
      evento = (IList<Evento>)TempData["evento"];
      return View(evento);
    }


    [HttpGet]
    public ActionResult enviarFilterEvent()
    {
     var IdCategoria = Request["Mover a la categoria"].ToString();
     int Id= Int32.Parse(IdCategoria);

      JObject respuesta = peticionEvento.Get(Id);
      IList<Evento> evento = null;
      if (respuesta.Property("dato") == null)
      {


        ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");


      }
      else
      {
        ModelState.AddModelError(string.Empty, "Se hizo con exito");

        evento = respuesta["dato"].ToObject<List<Evento>>();

        TempData["evento"] = evento;
        return RedirectToAction("FilterEvent", "M8Events", evento);
      }

      return RedirectToAction("FilterEvent");
    }

  }
}
