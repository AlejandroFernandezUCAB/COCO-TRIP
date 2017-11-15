using BackOffice_COCO_TRIP.Models;
using BackOffice_COCO_TRIP.Models.Peticion;
using Newtonsoft.Json;
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

        /// <summary>
        /// Metodo GET que se dispara al acceder a la pantalla Create
        /// </summary>
        /// <returns></returns>
        // GET:Lugares/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Lugar Turístico";

            peticion = new PeticionLugares();

            try
            {
              var respuesta = peticion.GetCategoria();

              if(respuesta == HttpStatusCode.InternalServerError.ToString())
              {
                return RedirectToAction("PageDown");
              }
              else if (respuesta == HttpStatusCode.NotFound.ToString())
              {
                return View();
              }
              else
              {
                ViewBag.Categoria = JsonConvert.DeserializeObject<List<Categoria>>(respuesta);
                ViewBag.SubCategoria = new List<Categoria>();

                foreach(var elemento in ViewBag.Categoria)
                {
                  respuesta = peticion.GetSubCategoria(elemento.Id);

                  if (respuesta == HttpStatusCode.InternalServerError.ToString())
                  {
                    return RedirectToAction("PageDown");
                  }
                  else if (respuesta != HttpStatusCode.NotFound.ToString())
                  {
                    var respuestaSubCategoria = JsonConvert.DeserializeObject<List<Categoria>>(respuesta);

                    foreach (var subElemento in respuestaSubCategoria)
                    {
                      ViewBag.SubCategoria.Add(subElemento);
                    }
                  }
                }

                return View();
              }
            }
            catch (SocketException)
            {
              return RedirectToAction("PageDown");
            }
        }

        // POST:Lugares/Create
        /// <summary>
        /// Metodo POST que se dispara al insertar un lugar turistico
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(LugarTuristico lugar/*, string activarTextual*/)
        {
            peticion = new PeticionLugares();

            try
            {
                if(string.IsNullOrEmpty(lugar.Nombre) || string.IsNullOrEmpty(lugar.Correo) || string.IsNullOrEmpty(lugar.Descripcion) ||
                string.IsNullOrEmpty(lugar.Direccion) || lugar.Categoria == null ||
                lugar.Categoria.Count == 0 || lugar.SubCategoria.Count == 0 || lugar.SubCategoria == null || lugar.Foto == null
                || lugar.Foto.Count == 0 || lugar.Horario == null || lugar.Horario.Count == 0 || lugar.Actividad == null || lugar.Actividad.Count == 0)
                {
                  return View("Por favor llene todos los campos correctamente");
                }
                else
                {
                    /*if(activarTextual == "Activo")
                    {
                      lugar.Activar = true;
                    }
                    else
                    {
                      lugar.Activar = false;

                    }*/

                    var respuesta = peticion.PostLugar(lugar);

                    if (respuesta == (int) HttpStatusCode.InternalServerError * -1)
                    {
                      return RedirectToAction("PageDown");
                    }
                    else if (respuesta == (int) HttpStatusCode.BadRequest * -1 )
                    {
                      return View("Por favor llene todos los campos correctamente");
                    }
                    else
                    {
                      return RedirectToAction("ViewAll");
                    }
                }
            }
            catch (SocketException)
            {
              return RedirectToAction("PageDown");
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

            try
            {
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
            catch (SocketException)
            {
              return RedirectToAction("PageDown");
            }

        }

        // GET:Lugares/DetailActivity
        /// <summary>
        /// Metodo GET que se dispara al acceder a la pantalla de detalles de actividades
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

              listaActividad[0].Id = id;

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
