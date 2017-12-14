using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Models.Peticion;
using BackOffice_COCO_TRIP.Negocio.Componentes.Comandos;
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
      Comando comando = new ComandoConsultarLocalidades();
      comando.Execute();
      TempData["listaLocalidadEvento"] = comando.GetResult();
      return View(comando.GetResult());
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
      Comando comando = new ComandoInsertarLocalidad(localidad);
      comando.Execute();
      ModelState.AddModelError(string.Empty,(String) comando.GetResult());    
      return View();
    }

        // GET: M8_Localidad/Edit/5
    public ActionResult Edit(int id)
    {
      ViewBag.Title = "Editar Localidad";
      Comando comando = new ComandoConsultarLocalidad(id);
      comando.Execute();
      //ModelState.AddModelError(string.Empty, comando.GetMensaje());
      return View(comando.GetResult());
      
    }

    // POST: M8_Localidad/Edit/5
    [HttpPost]
    public ActionResult Edit(LocalidadEvento localidad)
    {
      Comando comando = new ComandoEditarLocalidad(localidad);
      comando.Execute();
      ModelState.AddModelError(string.Empty, (String) comando.GetResult());
      return RedirectToAction("Index");
    }

      

        // POST: M8_Localidad/Delete/5
        
    public ActionResult Delete(int id, FormCollection collection)
    {
      ViewBag.Title = "Localidades de los Eventos";
      Comando comando = new ComandoEliminarLocalidad(id);
      comando.Execute();
      ModelState.AddModelError(string.Empty, (String)comando.GetResult());
      return RedirectToAction("Index");



    }

          // POST: M8_Localidad/Select/5

    public ActionResult Select(int id)
    {
      ViewBag.Title = "Consultar Localidad";
      Comando comando = new ComandoConsultarLocalidad(id);
      comando.Execute();

      // ModelState.AddModelError(string.Empty, comando.getMensaje());
      return View(comando.GetResult());
                     
    }
  }
}
