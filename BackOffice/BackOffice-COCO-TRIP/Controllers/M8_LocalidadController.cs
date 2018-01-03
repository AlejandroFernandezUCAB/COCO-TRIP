using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Comandos;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using System;
using System.Web.Mvc;

namespace BackOffice_COCO_TRIP.Controllers
{
  /// <summary>
  /// Clase encargada de la lógica de la interfaz de Localidades.
  /// </summary>
  public class M8_LocalidadController : Controller
  {

    /// <summary>
    /// Controlador de la vista Index.
    /// </summary>
    /// <returns>Menu principal de Localidades</returns>
    public ActionResult Index()
    {
      ViewBag.Title = "Localidades de los Eventos";
      Comando comando = FabricaComando.GetComandoConsultarLocalidades();
      comando.Execute();
      TempData["listaLocalidadEvento"] = comando.GetResult();
      return View(comando.GetResult()[0]);
    }

    /// <summary>
    /// Controlador de la vista Details.
    /// </summary>
    /// <param name="id"> id de la localidad a detallar</param>
    /// <returns>Vista de la localidad a detallar</returns>
    public ActionResult Details(int id)
    {
      return View();
    }

    /// <summary>
    /// Controlador para cargar la vista Create.
    /// </summary>
    /// <returns>Vista Create</returns>
    public ActionResult Create()
    {
      return View();
    }

    /// <summary>
    /// Controlador de la vista Create al momento de realizar un submit dentro de la misma.
    /// </summary>
    /// <param name="localidad"> localidad a crear</param>
    /// <returns>Vista Create</returns>
    [HttpPost]
    public ActionResult Create(Localidad localidad)
    {
      Comando comando = FabricaComando.GetComandoAgregarLocalidad();
      comando.SetPropiedad(localidad);
      comando.Execute();
      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[0]);
      return View();
    }

    /// <summary>
    /// Controlador que carga la vista Edit.
    /// </summary>
    /// <param name="id">id de la localidad a editar</param>
    /// <returns>Vista Edit</returns>
    public ActionResult Edit(int id)
    {
      ViewBag.Title = "Editar Localidad";
      Comando comando = FabricaComando.GetComandoConsultarLocalidad();
      comando.SetPropiedad(id);
      comando.Execute();
      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[1]);
      return View(comando.GetResult()[0]);

    }

    /// <summary>
    /// Controlador da vista Edit una vez realizado un submit dentro de la misma.
    /// </summary>
    /// <param name="localidad">localidad a editar</param>
    /// <returns> Vista Index</returns>
    [HttpPost]
    public ActionResult Edit(Localidad localidad)
    {
      Comando comando = FabricaComando.GetComandoModificarLocalidad();
      comando.SetPropiedad(localidad);
      comando.Execute();
      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[0]);
      return RedirectToAction("Index");
    }

    /// <summary>
    /// Controlador de la vista Delete.
    /// </summary>
    /// <param name="id">Identificador único de la localidad</param>
    /// <param name="collection"> Colección del formulario</param>
    /// <returns>Vista Index</returns>
    public ActionResult Delete(int id, FormCollection collection)
    {
      ViewBag.Title = "Localidades de los Eventos";
      Comando comando = FabricaComando.GetComandoEliminarLocalidad();
      comando.SetPropiedad(id);
      comando.Execute();
      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[0]);
      return RedirectToAction("Index");



    }

    /// <summary>
    /// Controlador de la vista Select.
    /// </summary>
    /// <param name="id">Identificador único de la Localidad a consultar</param>
    /// <returns>Vista Select</returns>
    public ActionResult Select(int id)
    {
      ViewBag.Title = "Consultar Localidad";
      Comando comando = FabricaComando.GetComandoConsultarLocalidad();
      comando.SetPropiedad(id);
      comando.Execute();

      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[1]);
      return View(comando.GetResult()[0]);

    }
  }
}
