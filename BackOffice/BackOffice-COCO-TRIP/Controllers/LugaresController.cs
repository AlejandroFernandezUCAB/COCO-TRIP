using BackOffice_COCO_TRIP.Models;
using BackOffice_COCO_TRIP.Models.Peticion;
using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace BackOffice_COCO_TRIP.Controllers
{
    /// <summary>
    /// Clase controlador de los Views de Lugares Turisticos
    /// </summary>
    public class LugaresController : Controller
    {
        //Horarios del lugar turistico

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
              else if (respuesta != HttpStatusCode.NotFound.ToString())
              {
                ViewBag.Categoria = JsonConvert.DeserializeObject<List<Categoria>>(respuesta);

                ViewBag.SubCategoria = new List<Categoria>();

                foreach(var elemento in ViewBag.Categoria)
                {
                  respuesta = peticion.GetSubCategoria((int)elemento.Id);

                  if (respuesta == HttpStatusCode.InternalServerError.ToString())
                  {
                    return RedirectToAction("PageDown");
                  }
                  else if (respuesta != HttpStatusCode.NotFound.ToString())
                  {
                    var respuestaSubCategoria = JsonConvert.DeserializeObject<List<Categoria>>(respuesta);

                    foreach (var subElemento in respuestaSubCategoria)
                    {
                      subElemento.UpperCategories = (int)elemento.Id;
                      ViewBag.SubCategoria.Add(subElemento);
                    }
                  }
                }
              }
              else
              {
                ViewBag.Categoria = new List<Categoria>(); //No hay categorias ni sub-categorias en la base de datos
                ViewBag.SubCategoria = new List<Categoria>();
              }

                return View();
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
        /// <param name="lugar">Lugar Turistico</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(LugarTuristico lugar)
        {
            peticion = new PeticionLugares();

            try
            {
                    //Parametros estaticos del form
                    var activar= String.Format("{0}", Request.Form["activar"]);
                    var categoriaUno = String.Format("{0}", Request.Form["categoria_1"]);
                    var categoriaDos = String.Format("{0}", Request.Form["categoria_2"]);
                    var categoriaTres = String.Format("{0}", Request.Form["categoria_3"]);
                    var categoriaCuatro = String.Format("{0}", Request.Form["categoria_4"]);
                    var categoriaCinco = String.Format("{0}", Request.Form["categoria_5"]);
                    var subCategoriaUno = String.Format("{0}", Request.Form["subcategoria_1"]);
                    var subCategoriaDos = String.Format("{0}", Request.Form["subcategoria_2"]);
                    var subCategoriaTres = String.Format("{0}", Request.Form["subcategoria_3"]);
                    var subCategoriaCuatro = String.Format("{0}", Request.Form["subcategoria_4"]);
                    var subCategoriaCinco = String.Format("{0}", Request.Form["subcategoria_5"]);

                    //Activar o desactivar lugar turistico
                    if (activar == "Activo")
                    {
                      lugar.Activar = true;
                    }
                    else
                    {
                      lugar.Activar = false;

                    }

                    //Obtener categorias y subcategorias del api rest
                    var respuesta = peticion.GetCategoria();
                    ViewBag.Categoria = JsonConvert.DeserializeObject<List<Categoria>>(respuesta);

                    ViewBag.SubCategoria = new List<Categoria>();

                    foreach (var elemento in ViewBag.Categoria)
                    {
                      respuesta = peticion.GetSubCategoria(elemento.Id);
                      var respuestaSubCategoria = JsonConvert.DeserializeObject<List<Categoria>>(respuesta);

                      foreach (var subElemento in respuestaSubCategoria)
                      {
                         ViewBag.SubCategoria.Add(subElemento);
                      }

                    }

                    //Categorias y subcategorias del lugar turistico
                    var categoria = new Categoria();

                    foreach (var elemento in ViewBag.Categoria)
                    {
                      if(elemento.Nombre == categoriaUno ||
                        elemento.Nombre == categoriaDos ||
                        elemento.Nombre == categoriaTres ||
                        elemento.Nombre == categoriaCuatro ||
                        elemento.Nombre == categoriaCinco)
                       {
                          categoria.Id = elemento.Id;
                          lugar.Categoria.Add(categoria);

                          categoria = new Categoria();
                       }
                    }

                    foreach (var elemento in ViewBag.SubCategoria)
                    {
                      if (elemento.Nombre == subCategoriaUno ||
                        elemento.Nombre == subCategoriaDos ||
                        elemento.Nombre == subCategoriaTres ||
                        elemento.Nombre == subCategoriaCuatro ||
                        elemento.Nombre == subCategoriaCinco)
                      {
                        categoria.Id = elemento.Id;
                        lugar.SubCategoria.Add(categoria);

                        categoria = new Categoria();
                      }
                    }

                    //Dia de los horarios del lugar turistico
                    var contador = 1;

                    foreach (var horario in lugar.Horario)
                    {
                      if (String.Format("{0}", Request.Form["dia_" + contador]) == "Domingo")
                      {
                        horario.DiaSemana = 0;
                      }
                      else if (String.Format("{0}", Request.Form["dia_" + contador]) == "Lunes")
                      {
                        horario.DiaSemana = 1;
                      }
                      else if (String.Format("{0}", Request.Form["dia_" + contador]) == "Martes")
                      {
                        horario.DiaSemana = 2;
                      }
                      else if (String.Format("{0}", Request.Form["dia_" + contador]) == "Miercoles")
                      {
                        horario.DiaSemana = 3;
                      }
                      else if (String.Format("{0}", Request.Form["dia_" + contador]) == "Jueves")
                      {
                        horario.DiaSemana = 4;
                      }
                      else if (String.Format("{0}", Request.Form["dia_" + contador]) == "Viernes")
                      {
                        horario.DiaSemana = 5;
                      }
                      else if (String.Format("{0}", Request.Form["dia_" + contador]) == "Sabado")
                      {
                        horario.DiaSemana = 6;
                      }

                      contador++;
                    }

                    var temporal = new List<Horario>();

                    foreach (var horario in lugar.Horario)
                    {
                        if(horario.HoraApertura == new TimeSpan() && horario.HoraCierre == new TimeSpan())
                        {
                          temporal.Add(horario);
                        }
                    }

                    foreach( var eliminar in temporal)
                    {
                      lugar.Horario.Remove(eliminar);
                    }

                    var temporalDos = new List<Actividad>();

                    foreach (var actividad in lugar.Actividad)
                    {
                        if(string.IsNullOrEmpty(actividad.Nombre))
                        {
                          temporalDos.Add(actividad);
                        }
                    }

                    foreach (var eliminar in temporalDos)
                    {
                        lugar.Actividad.Remove(eliminar);
                    }

                    var respuestaInsercion = peticion.PostLugar(lugar);

                    if(respuestaInsercion == (int) HttpStatusCode.BadRequest * (-1) )
                    {
                      return RedirectToAction("ViewAll");
        }
                    else if (respuestaInsercion == (int) HttpStatusCode.InternalServerError * (-1))
                    {
                      return RedirectToAction("PageDown");
                    }
                    else
                    {
                      return RedirectToAction("ViewAll");
                    }
            }
             catch(SocketException)
            {
              return RedirectToAction("PageDown");
            }


        }

        // GET:Lugares/Modify
        public ActionResult Modify(int id)
        {
            ViewBag.Title = "Modificar Lugar Turístico";

            peticion = new PeticionLugares();

            try
            {
              var respuesta = peticion.GetLugarActividades(id);

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

        // POST:Lugares/Modify
        [HttpPost]
        public ActionResult Modify(LugarTuristico lugar)
        {
            peticion = new PeticionLugares();

            try
            {
              //Parametros estaticos del form
              var activar = String.Format("{0}", Request.Form["activar"]);
              var categoriaUno = String.Format("{0}", Request.Form["categoria_1"]);
              var categoriaDos = String.Format("{0}", Request.Form["categoria_2"]);
              var categoriaTres = String.Format("{0}", Request.Form["categoria_3"]);
              var categoriaCuatro = String.Format("{0}", Request.Form["categoria_4"]);
              var categoriaCinco = String.Format("{0}", Request.Form["categoria_5"]);
              var subCategoriaUno = String.Format("{0}", Request.Form["subcategoria_1"]);
              var subCategoriaDos = String.Format("{0}", Request.Form["subcategoria_2"]);
              var subCategoriaTres = String.Format("{0}", Request.Form["subcategoria_3"]);
              var subCategoriaCuatro = String.Format("{0}", Request.Form["subcategoria_4"]);
              var subCategoriaCinco = String.Format("{0}", Request.Form["subcategoria_5"]);

              //Activar o desactivar lugar turistico
              if (activar == "Activo")
              {
                lugar.Activar = true;
              }
              else
              {
                lugar.Activar = false;

              }

              //Obtener categorias y subcategorias del api rest
              var respuesta = peticion.GetCategoria();
              ViewBag.Categoria = JsonConvert.DeserializeObject<List<Categoria>>(respuesta);

              ViewBag.SubCategoria = new List<Categoria>();

              foreach (var elemento in ViewBag.Categoria)
              {
                respuesta = peticion.GetSubCategoria(elemento.Id);
                var respuestaSubCategoria = JsonConvert.DeserializeObject<List<Categoria>>(respuesta);

                foreach (var subElemento in respuestaSubCategoria)
                {
                  ViewBag.SubCategoria.Add(subElemento);
                }

              }

              //Categorias y subcategorias del lugar turistico
              var categoria = new Categoria();

              foreach (var elemento in ViewBag.Categoria)
              {
                if (elemento.Nombre == categoriaUno ||
                  elemento.Nombre == categoriaDos ||
                  elemento.Nombre == categoriaTres ||
                  elemento.Nombre == categoriaCuatro ||
                  elemento.Nombre == categoriaCinco)
                {
                  categoria.Id = elemento.Id;
                  lugar.Categoria.Add(categoria);

                  categoria = new Categoria();
                }
              }

              foreach (var elemento in ViewBag.SubCategoria)
              {
                if (elemento.Nombre == subCategoriaUno ||
                  elemento.Nombre == subCategoriaDos ||
                  elemento.Nombre == subCategoriaTres ||
                  elemento.Nombre == subCategoriaCuatro ||
                  elemento.Nombre == subCategoriaCinco)
                {
                  categoria.Id = elemento.Id;
                  lugar.SubCategoria.Add(categoria);

                  categoria = new Categoria();
                }
              }

              //Dia de los horarios del lugar turistico
              var contador = 1;

              foreach (var horario in lugar.Horario)
              {
                if (String.Format("{0}", Request.Form["dia_" + contador]) == "Domingo")
                {
                  horario.DiaSemana = 0;
                }
                else if (String.Format("{0}", Request.Form["dia_" + contador]) == "Lunes")
                {
                  horario.DiaSemana = 1;
                }
                else if (String.Format("{0}", Request.Form["dia_" + contador]) == "Martes")
                {
                  horario.DiaSemana = 2;
                }
                else if (String.Format("{0}", Request.Form["dia_" + contador]) == "Miercoles")
                {
                  horario.DiaSemana = 3;
                }
                else if (String.Format("{0}", Request.Form["dia_" + contador]) == "Jueves")
                {
                  horario.DiaSemana = 4;
                }
                else if (String.Format("{0}", Request.Form["dia_" + contador]) == "Viernes")
                {
                  horario.DiaSemana = 5;
                }
                else if (String.Format("{0}", Request.Form["dia_" + contador]) == "Sabado")
                {
                  horario.DiaSemana = 6;
                }

                contador++;
              }


              var respuestaInsercion = peticion.PutLugar(lugar);

              if (respuestaInsercion == HttpStatusCode.BadRequest.ToString())
              {
                return RedirectToAction("ViewAll");
              }
              else if (respuestaInsercion == HttpStatusCode.InternalServerError.ToString())
              {
                return RedirectToAction("PageDown");
              }
              else
              {
                return RedirectToAction("ViewAll");
              }
            }
            catch (SocketException)
            {
              return RedirectToAction("PageDown");
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
              else if (respuesta != HttpStatusCode.NotFound.ToString())
              {
                var listaActividad = JsonConvert.DeserializeObject<List<Actividad>>(respuesta);

                foreach (var actividad in listaActividad)
                {
                  actividad.Foto.Ruta = peticion.DireccionBase + actividad.Foto.Ruta;
                }

                listaActividad[0].Id = id;

                return View(listaActividad);
              }
              else
              {
                return RedirectToAction("ViewAll");
              }
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
