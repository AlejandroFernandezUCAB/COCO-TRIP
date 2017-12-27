using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BackOffice_COCO_TRIP.Negocio.Componentes.Comandos;

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
    [HttpGet]
    // GET: M8Events/Create
    public ActionResult CreateEvent(int id = -1)
    {
      
      Comando comando = FabricaComando.GetComandoConsultarEventos();
      comando.SetPropiedad(id);
      comando.Execute();
      
      ModelState.AddModelError(string.Empty,(String) comando.GetResult()[1]);
      //ViewBag.ListLocalidad = comando.GetResult()[2];
      ViewBag.ListLocalidades = comando.GetResult()[2];
      ViewBag.ListCategoria = comando.GetResult()[0];
      return View();
    }

    // POST: M8Events/Create
    [HttpPost]
    public ActionResult CreateEvent(Evento evento)
    {
      //Debe funcionar con la siguiente linea:
      evento.IdLocalidad = Int32.Parse(Request["Localidades"].ToString());
      evento.Foto = "jorge"; 
      evento.IdCategoria = Int32.Parse(Request["Categoria"].ToString());
      Comando comando = FabricaComando.GetComandoInsertarEvento();
      comando.SetPropiedad(evento);
      comando.Execute(); 
      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[0]);
      return RedirectToAction("FilterEvent");  // TERMINAR

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
      Comando comando = FabricaComando.GetComandoEliminarEvento();
      comando.SetPropiedad(id);
      comando.Execute();
      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[0]);

      return RedirectToAction("FilterEvent");
    }

    // GET: M8Events
    [HttpGet]
    public ActionResult FilterEvent()
    { 
      ViewBag.Title = "Eventos por Categorias";
      Comando comando = FabricaComando.GetComandoConsultarCategorias();
      comando.Execute();
      ViewBag.MyList = comando.GetResult()[0];
      TempData["listaCategorias"] = comando.GetResult()[0];
      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[1]);
      return View((IList<Evento>)TempData["evento"]);
    }


    [HttpGet]
    public ActionResult enviarFilterEvent()
    {
     int id= Int32.Parse(Request["Mover a la categoria"].ToString());
      Comando comando = FabricaComando.GetComandoFiltrarEventoPorCategoria();
      comando.SetPropiedad(id);
      comando.Execute();
      TempData["evento"] = comando.GetResult()[0];
      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[1]);
      if (comando.GetResult()[0] == null)
        return RedirectToAction("FilterEvent");
      return RedirectToAction("FilterEvent", "M8Events", comando.GetResult()[0]);
    }

    public ActionResult Select(int id)
    {
      ViewBag.Title = "Consultar Evento";
      Comando comando = FabricaComando.GetComandoConsultarEvento();
      comando.SetPropiedad(id);
      comando.Execute();

      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[1]);
      return View(comando.GetResult()[0]);

    }

  }
}
