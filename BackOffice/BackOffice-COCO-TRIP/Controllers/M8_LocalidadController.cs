using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Componentes.Comandos;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
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

    // GET: M8_Localidad/Index
    public ActionResult Index()
    {
      ViewBag.Title = "Localidades de los Eventos";
      Comando comando = FabricaComando.GetComandoConsultarLocalidades();
      comando.Execute();
      TempData["listaLocalidadEvento"] = comando.GetResult();
      return View(comando.GetResult()[0]);
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
    public ActionResult Create(Localidad localidad)
    {
      Comando comando = FabricaComando.GetComandoInsertarLocalidad();
      comando.SetPropiedad(localidad);
      comando.Execute();
      ModelState.AddModelError(string.Empty,(String) comando.GetResult()[0]);    
      return View();
    }

        // GET: M8_Localidad/Edit/5
    public ActionResult Edit(int id)
    {
      ViewBag.Title = "Editar Localidad";
      Comando comando = FabricaComando.GetComandoConsultarLocalidad();
      comando.SetPropiedad(id);
      comando.Execute();
      ModelState.AddModelError(string.Empty,(String) comando.GetResult()[1]);
      return View(comando.GetResult()[0]);
      
    }

    // POST: M8_Localidad/Edit/5
    [HttpPost]
    public ActionResult Edit(Localidad localidad)
    {
      Comando comando = FabricaComando.GetComandoEditarLocalidad();
      comando.SetPropiedad(localidad); 
      comando.Execute();
      ModelState.AddModelError(string.Empty, (String) comando.GetResult()[0]);
      return RedirectToAction("Index");
    }

      

        // POST: M8_Localidad/Delete/5
        
    public ActionResult Delete(int id, FormCollection collection)
    {
      ViewBag.Title = "Localidades de los Eventos";
      Comando comando = FabricaComando.GetComandoEliminarLocalidad();
      comando.SetPropiedad(id);
      comando.Execute();
      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[0]);
      return RedirectToAction("Index");



    }

          // POST: M8_Localidad/Select/5

    public ActionResult Select(int id)
    {
      ViewBag.Title = "Consultar Localidad";
      Comando comando = FabricaComando.GetComandoConsultarLocalidad();
      comando.SetPropiedad(id);
      comando.Execute();

      ModelState.AddModelError(string.Empty,(String) comando.GetResult()[1]);
      return View(comando.GetResult()[0]);
                     
    }
  }
}
