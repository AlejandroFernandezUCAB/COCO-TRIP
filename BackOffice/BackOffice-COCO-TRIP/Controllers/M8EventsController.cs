using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BackOffice_COCO_TRIP.Negocio.Comandos;
using Newtonsoft.Json.Linq;
using System.Web;
using System.IO;

namespace BackOffice_COCO_TRIP.Controllers
{
  /// <summary>
  /// Clase encargada de la lógica de la interfaz de Eventos.
  /// </summary>
  public class M8EventsController : Controller
  {

    /// <summary>
    /// Controlador de la vista Index.
    /// </summary>
    /// <returns>Vista FilterEvent</returns>
    [HttpGet]
    public ActionResult Index()
    {
      ViewBag.Title = "Eventos por Categorias";
      Comando comando = FabricaComando.GetComandoConsultarCategoriaHabilitada();
      comando.Execute();
      ViewBag.MyList = ((JObject)comando.GetResult()[0])["data"].ToObject<IList<Categoria>>();
      TempData["listaCategorias"] = ((JObject)comando.GetResult()[0])["data"].ToObject<IList<Categoria>>();
      return View((IList<Evento>)TempData["evento"]);
    }

    /// <summary>
    /// Controlador de la vista Details.
    /// </summary>
    /// <param name="id"> id del evento a detallar</param>
    /// <returns>Vista del evento a detallar</returns>
    public ActionResult Details(int id)
    {
      return View();
    }

    /// <summary>
    /// Controlador para cargar la vista CreateEvent.
    /// </summary>
    /// <returns>Vista CreateEvent</returns>
    [HttpGet]
    public ActionResult Create()
    {
      Comando comando;
      comando = FabricaComando.GetComandoConsultarCategoriaHabilitada();
      comando.Execute();
      ViewBag.ListCategoria = ((JObject)comando.GetResult()[0])["data"].ToObject<List<Categoria>>();

      comando = FabricaComando.GetComandoConsultarLocalidades();
      comando.Execute();
      ViewBag.ListLocalidades = comando.GetResult()[0];

      TempData["ListLocalidades"] = ViewBag.ListCategoria;
      TempData["ListCategoria"] = comando.GetResult()[0];
      return View();
    }

    /// <summary>
    /// Controlador de la vista CreateEvent al momento de realizar un submit dentro de la misma.
    /// </summary>
    /// <param name="evento"> evento a crear</param>
    /// <param name="file">foto del evento</param>
    /// <returns>Vista CreateEvent</returns>
    [HttpPost]
    public ActionResult Create(Evento evento, HttpPostedFileBase file)
    {
      ViewBag.ListLocalidades = TempData["ListLocalidades"];
      ViewBag.ListCategoria = TempData["ListCategoria"];
      if (ModelState.IsValid)
      {
        String ruta;
        if (file != null && file.ContentLength > 0)
        {
          ruta = Path.GetFileName(evento.Nombre + file.FileName);
        }
        else
          ruta = "";
        evento.Foto = ruta;

        evento.IdLocalidad = Int32.Parse(Request["Localidades"].ToString());
        evento.IdCategoria = Int32.Parse(Request["Categoria"].ToString());

        Comando comando = FabricaComando.GetComandoAgregarEvento();
        comando.SetPropiedad(evento);
        comando.Execute();

        ModelState.AddModelError(string.Empty, (String)comando.GetResult()[0]);
      }
      TempData["ListLocalidades"] = ViewBag.ListLocalidades;
      TempData["ListCategoria"] = ViewBag.ListCategoria;
      return View();  // TERMINAR

    }

    /// <summary>
    /// Controlador que carga la vista Edit.
    /// </summary>
    /// <param name="id">Identificador único del evento a editar</param>
    /// <returns>Vista Edit</returns>
    public ActionResult Edit(int id)
    {
      Comando comando;
      comando = FabricaComando.GetComandoConsultarCategoriaHabilitada();
      comando.Execute();
      ViewBag.ListCategoria = ((JObject)comando.GetResult()[0])["data"].ToObject<List<Categoria>>();

      comando = FabricaComando.GetComandoConsultarLocalidades();
      comando.Execute();
      ViewBag.ListLocalidades = comando.GetResult()[0];
      comando = FabricaComando.GetComandoConsultarEvento();
      comando.SetPropiedad(id);
      comando.Execute();
      TempData["fotovieja"] = ((Evento)comando.GetResult()[0]).Foto;
      TempData["ListLocalidades"] = ViewBag.ListLocalidades;
      TempData["ListCategoria"] = ViewBag.ListCategoria;
      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[1]);
      return View(comando.GetResult()[0]);
    }

    /// <summary>
    /// Controlador da vista Edit una vez realizado un submit dentro de la misma.
    /// </summary>
    /// <param name="localidad">evento a editar</param>
    /// <returns> Vista Index</returns>
    [HttpPost]
    public ActionResult Edit(Evento evento, HttpPostedFileBase file)
    {
      if (ModelState.IsValid)
      {
        evento.IdLocalidad = Int32.Parse(Request["Localidades"].ToString());
        evento.IdCategoria = Int32.Parse(Request["Categoria"].ToString());
        String foto = (String)TempData["fotovieja"];
        String ruta;
        if (file != null && file.ContentLength > 0)
        {
          ruta = Path.GetFileName(evento.Nombre + file.FileName);
        }
        else
          ruta = "";
        evento.Foto = ruta;
        Comando comando = FabricaComando.GetComandoModificarEvento();
        comando.SetPropiedad(evento);
        comando.Execute();
        ModelState.AddModelError(string.Empty, (String)comando.GetResult()[0]);
      }
      ViewBag.ListLocalidades = TempData["ListLocalidades"];
      ViewBag.ListCategoria = TempData["ListCategoria"];
      TempData["ListLocalidades"] = ViewBag.ListLocalidades;
      TempData["ListCategoria"] = ViewBag.ListCategoria;
      return View(evento);
    }

    /// <summary>
    /// Controlador de la vista Delete.
    /// </summary>
    /// <param name="id">Identificador único del evento</param>
    /// <param name="collection"> Colección del formulario</param>
    /// <returns>Vista Index</returns>
    public ActionResult Delete(int id)
    {
      Comando comando = FabricaComando.GetComandoEliminarEvento();
      comando.SetPropiedad(id);
      comando.Execute();
      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[0]);

      return RedirectToAction("Index");
    }

    /// <summary>
    /// Controlador de la vista FilterEvent al momento de realizar un submit dentro de la misma.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult enviarFilterEvent()
    {
      int id = Int32.Parse(Request["Mover a la categoria"].ToString());
      Comando comando = FabricaComando.GetComandoFiltrarEventoPorCategoria();
      comando.SetPropiedad(id);
      comando.Execute();
      TempData["evento"] = comando.GetResult()[0];
      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[1]);
      if (comando.GetResult()[0] == null)
        return RedirectToAction("Index");
      return RedirectToAction("Index", "M8Events", comando.GetResult()[0]);
    }

    /// <summary>
    /// Controlador de la vista Select.
    /// </summary>
    /// <param name="id">Identificador único de la Localidad a consultar</param>
    /// <returns>Vista Select</returns>
    public ActionResult Select(int id)
    {
      ViewBag.Title = "Consultar Evento";
      Comando comando = FabricaComando.GetComandoConsultarEvento();
      comando.SetPropiedad(id);
      comando.Execute();
      ViewData["ncategoria"] = (String)comando.GetResult()[3];
      ViewData["nlocalidad"] = comando.GetResult()[2];

      ModelState.AddModelError(string.Empty, (String)comando.GetResult()[2]);
      return View(comando.GetResult()[0]);

    }

  }
}
