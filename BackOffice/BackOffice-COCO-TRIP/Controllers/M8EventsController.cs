using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BackOffice_COCO_TRIP.Negocio.Componentes.Comandos;
using Newtonsoft.Json.Linq;

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

    // GET: M8_Localidad/Edit/5
    public ActionResult Edit(int id)
    {
      ViewBag.Title = "Editar Evento";

      Comando comand = FabricaComando.GetComandoConsultarEventos();
      comand.SetPropiedad(id);
      comand.Execute();

      ModelState.AddModelError(string.Empty, (String)comand.GetResult()[1]);
      ViewBag.ListLocalidades = comand.GetResult()[2];
      ViewBag.ListCategoria = comand.GetResult()[0];

      Comando comando = FabricaComando.GetComandoConsultarEvento();
      comando.SetPropiedad(id);
      comando.Execute();
      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[1]);
      return View(comando.GetResult()[0]);
    }

    // POST: M8_Localidad/Edit/5
    [HttpPost]
    public ActionResult Edit(Evento evento)
    {
      evento.IdLocalidad = Int32.Parse(Request["Localidades"].ToString());
      evento.Foto = "jorge";
      evento.IdCategoria = Int32.Parse(Request["Categoria"].ToString());
      Comando comando = FabricaComando.GetComandoEditarEvento();
      comando.SetPropiedad(evento);
      comando.Execute();
      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[0]);
      return RedirectToAction("FilterEvent");
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
      Comando comando = FabricaComando.GetComandoConsultarCategoriaHabilitada();
      comando.Execute();
      ViewBag.MyList = ((JObject)comando.GetResult()[0])["data"].ToObject<IList<Categoria>>();
      TempData["listaCategorias"] = ((JObject)comando.GetResult()[0])["data"].ToObject<IList<Categoria>>();
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
      ViewData["ncategoria"] = (String)comando.GetResult()[2];
      ViewData["nlocalidad"] = comando.GetResult()[1];
      
      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[1]);
      return View(comando.GetResult()[0]);

    }

  }
}
