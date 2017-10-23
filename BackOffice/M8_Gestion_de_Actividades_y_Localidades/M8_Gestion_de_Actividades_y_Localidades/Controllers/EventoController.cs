using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M8_Gestion_de_Actividades_y_Localidades.Controllers
{
    public class EventoController : Controller
    {
        // POST: Evento
        public ActionResult CrearEvento()
        {
            return View();
        }

        //GET
        public ActionResult ConsultarEvento()
        {
          return View();
        }

        //POST
        public ActionResult ActualizarEvento()
        {
          return View();
        }

        //POST
        public ActionResult EliminarEvento()
        {
          return View();
        }
  }
}
