using BackOffice_COCO_TRIP.Models;
using BackOffice_COCO_TRIP.Models.Peticion;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace BackOffice_COCO_TRIP.Controllers
{
    /// <summary>
    /// Clase controlador de los Views de Lugares Turisticos
    /// </summary>
    public class LugaresController : Controller
    {
        private PeticionLugares peticion; //Objeto que realiza la peticion al servicio web

        // GET:Lugares
        public ActionResult Index()
        {
            ViewBag.Title = "Lugares Turísticos";
            return View();
        }

        // GET:Lugares/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET:Lugares/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Lugar Turístico";
            return View();
        }

        // POST:Lugares/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET:Lugares/Modify
        public ActionResult Modify()
        {
            ViewBag.Title = "Modificar Lugar Turístico";
            return View();
        }

        // POST:Lugares/Modify
        [HttpPost]
        public ActionResult Modify(FormCollection collection)
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

        // GET:Lugares/DetailLugar
        public ActionResult LugarDetail()
        {
            ViewBag.Title = "Detalle de Lugar Turístico";
            return View();
        }

        // POST:Lugares/DetailLugar
        [HttpPost]
        public ActionResult LugarDetail(FormCollection collection)
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

        // GET:Lugares/DetailActivity
        public ActionResult ActivityDetail()
        {
            ViewBag.Title = "Detalle de Actividad";
            return View();
        }

        // POST:Lugares/DetailActivity
        [HttpPost]
        public ActionResult ActivityDetail(FormCollection collection)
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

        //Pantalla ver todos los lugares turisticos

        // GET:Lugares/ViewAll
        /// <summary>
        /// Metodo GET que se dispara al acceder a la pantalla de ver todos los lugares turisticos (ViewAll)
        /// </summary>
        /// <returns>View</returns>
        public ActionResult ViewAll()
        {
            ViewBag.Title = "Lugares Turísticos";

            peticion = new PeticionLugares();

            var respuesta = peticion.GetLista(1,int.MaxValue);

            if (respuesta == HttpStatusCode.InternalServerError.ToString())
            {
              return RedirectToAction("PageDown"); //Error del servicio web
            }

            var listaLugarTuristico = JsonConvert.DeserializeObject<List<LugarTuristico>>(respuesta);

            return View(listaLugarTuristico);
        }

        // PUT:Lugares/ViewAll?id={0}&activar={1}
        /// <summary>
        /// Metodo PUT que se dispara al cambiar el estado de un lugar turistico
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult ViewAll(int id, bool activar)
        {
            peticion = new PeticionLugares();

            var respuesta = peticion.PutActivarLugar(id, activar); //Actualiza el estado
            if (respuesta == HttpStatusCode.InternalServerError.ToString())
            {
              return RedirectToAction("PageDown"); //Error del servicio web al realizar la actualizacion
            }

            respuesta = peticion.GetLista(1, int.MaxValue); //Nuev
            if (respuesta == HttpStatusCode.InternalServerError.ToString())
            {
              return RedirectToAction("PageDown"); //Error del servicio web al solicitar la lista de lugares turisticos
            }

            var listaLugarTuristico = JsonConvert.DeserializeObject<List<LugarTuristico>>(respuesta);

            return View(listaLugarTuristico);
        }

        //

        // GET:Lugares/Activate/5
        public ActionResult Activate(int id)
        {
            return View();
        }

        // POST:Lugares/Activate/5
        [HttpPost]
        public ActionResult Activate(int id, FormCollection collection)
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

        // GET:Lugares/PageDown
        public ActionResult PageDown()
        {
          ViewBag.Title = "Lugares Turísticos";

          return View();
        }
    }
}
