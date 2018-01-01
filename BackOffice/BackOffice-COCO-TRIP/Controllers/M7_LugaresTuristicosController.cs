using System.Web.Mvc;
using BackOffice_COCO_TRIP.Negocio.Comandos;
using BackOffice_COCO_TRIP.Negocio.Fabrica;

namespace BackOffice_COCO_TRIP.Controllers
{
  public class M7_LugaresTuristicosController : Controller
  {
    public M7_LugaresTuristicosController()
    {
      
    }

    public ActionResult Create()
    {
      ViewBag.Title = "Localidades de los Eventos";
      Comando comando = FabricaComando.GetComandoAgregarLugarTuristico();
      comando.Execute();
      TempData["listaLocalidadEvento"] = comando.GetResult();
      return View(comando.GetResult()[0]);
    }

  }
}
