using BackOffice_COCO_TRIP.Models;
using BackOffice_COCO_TRIP.Models.Peticion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
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
        /// <summary>
        /// Metodo GET que se dispara al acceder a la pantalla detalle de lugar turistico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult LugarDetail(int id)
        {
            ViewBag.Title = "Detalle de Lugar Turístico";

            peticion = new PeticionLugares();

            var respuesta = peticion.GetLugar(id);

            if (respuesta == HttpStatusCode.InternalServerError.ToString())
            {
              return RedirectToAction("PageDown"); //Error del servicio web
            }

            var lugarTuristico = JsonConvert.DeserializeObject<LugarTuristico>(respuesta);

            foreach (var foto in lugarTuristico.Foto)
            {
                foto.Ruta = peticion.DireccionBase + foto.Ruta;
            }

            return View(lugarTuristico);

        }

        // GET:Lugares/DetailActivity
        /// <summary>
        /// Metodo GET que se dispara al acceder a la pantalla de ver todos los lugares turisticos (ViewAll)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ActivityDetail(int id )
        {
            ViewBag.Title = "Detalle de Actividad";

            peticion = new PeticionLugares();

            try
            {
              var respuesta = peticion.GetActividades(id);

              if (respuesta == HttpStatusCode.InternalServerError.ToString())
              {
                return RedirectToAction("PageDown"); //Error del servicio web
              }

              var listaActividad = JsonConvert.DeserializeObject<List<Actividad>>(respuesta);

              foreach (var actividad in listaActividad)
              {
                actividad.Foto.Ruta = peticion.DireccionBase + actividad.Foto.Ruta;
              }

              return View(listaActividad);

            }
            catch (SocketException)
            {
              return RedirectToAction("PageDown");
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

            try
            {
              var respuesta = peticion.GetLista(1, int.MaxValue);

              if (respuesta == HttpStatusCode.InternalServerError.ToString())
              {
                return RedirectToAction("PageDown"); //Error del servicio web
              }

              var listaLugarTuristico = JsonConvert.DeserializeObject<List<LugarTuristico>>(respuesta);

              foreach (var lugar in listaLugarTuristico)
              {
                foreach (var foto in lugar.Foto)
                {
                  foto.Ruta = peticion.DireccionBase + foto.Ruta;
                }
              }

              return View(listaLugarTuristico);

            }
            catch (SocketException)
            {
                return RedirectToAction("PageDown");
            }
        }

        // PUT:Lugares/ViewAll?id={0}&activar={1}
        /// <summary>
        /// Metodo PUT que se dispara al cambiar el estado de un lugar turistico
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ViewAll(int id, bool activar)
        {
            peticion = new PeticionLugares();

            try
            {

              var respuesta = peticion.PutActivarLugar(id, !activar); //Actualiza el estado
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

              foreach (var lugar in listaLugarTuristico)
              {
                foreach (var foto in lugar.Foto)
                {
                  foto.Ruta = peticion.DireccionBase + foto.Ruta;
                }
              }

              return View(listaLugarTuristico);

            }
            catch (SocketException)
            {
              return RedirectToAction("PageDown");
            }
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
