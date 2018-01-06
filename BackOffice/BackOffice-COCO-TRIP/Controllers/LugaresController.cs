using BackOffice_COCO_TRIP.Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BackOffice_COCO_TRIP.Negocio.Comandos;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;

namespace BackOffice_COCO_TRIP.Controllers
{
    /// <summary>
    /// Clase controlador de los Views de Lugares Turisticos
    /// </summary>
    public class LugaresController : Controller
    {
    
    private Comando com;
    private IList<Categoria> _categorias;

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
    /// GET:Lugares/Create
    public ActionResult Create()
    {
      JObject respuestaCategoria;
      ViewBag.Title = "Agregar Lugar Turístico";
      List<Categoria> listCategories = new List<Categoria>() ;

      
      com = FabricaComando.GetComandoConsultarCategoriaHabilitada();
      com.Execute();
      respuestaCategoria = (JObject)com.GetResult()[0];
      if (respuestaCategoria.Property("data")!= null)
      {

        listCategories = respuestaCategoria["data"].ToObject<List<Categoria>>();
        _categorias = listCategories;
        ViewBag.Categoria = _categorias;

      }
      else
      {

        ViewBag.Categoria = new List<Categoria>();

      }

      return View();
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

      com = FabricaComando.GetComandoAgregarLugarTuristico();
      LlenadoLugarTuristico();
      com.Execute();
      return RedirectToAction("ViewAll");

    }

    [HttpPost]
    public ActionResult Modify(LugarTuristico lugar)
    {
      com = FabricaComando.GetComandoModificarLugarTuristico();
      LlenadoLugarTuristico();
      com.SetPropiedad(Request.Form["Id"]);
      com.Execute();
      return RedirectToAction("ViewAll");
    }

        // GET:Lugares/Modify
        public ActionResult Modify(int id)
        {
      ViewBag.Title = "Modificar Lugar Turistico";
          JObject respuesta;
          com = FabricaComando.GetComandoConsultarLugarTuristico();
          com.SetPropiedad(id);
          com.Execute();
          respuesta = (JObject)com.GetResult()[0];
          var lugarTuristico = respuesta["data"].ToObject<LugarTuristico>();
          return View(lugarTuristico);
        }

      
        // GET:Lugares/DetailLugar
        /// <summary>
        /// Metodo GET que se dispara al acceder a la pantalla detalle de lugar turistico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
    public ActionResult LugarDetail(int id)
    {

      JObject respuesta;
      ViewBag.Title = "Detalle de Actividad";
      com = FabricaComando.GetComandoConsultarLugarTuristico();
      com.SetPropiedad(id);
      com.Execute();
      respuesta  = (JObject)com.GetResult()[0];
      var lugarTuristico = respuesta["data"].ToObject<LugarTuristico>();
      return View(lugarTuristico);

     }

      
    //Pantalla ver todos los lugares turisticos

    // GET:Lugares/ViewAll
    /// <summary>
    /// Metodo GET que se dispara al acceder a la pantalla de ver todos los lugares turisticos (ViewAll)
    /// </summary>
    /// <returns>View</returns>
    public ActionResult ViewAll()
    {
      JObject respuesta;
      ViewBag.Title = "Lugares Turísticos";
      com = FabricaComando.GetComandoConsultarLugaresTuristicos();
      com.Execute();
      respuesta = (JObject)com.GetResult()[0];
      List<LugarTuristico> _lugaresTuristicos = respuesta["data"].ToObject<List<LugarTuristico>>();
      return View(_lugaresTuristicos);
    }
     


     // PUT:Lugares/ViewAll?id={0}&activar={1}
     /// <summary>
     /// Metodo PUT que se dispara al cambiar el estado de un lugar turistico
     /// </summary>
     /// <param name="id">Id del lugar turistico</param>
     /// <param name="activar">Estado del lugar turistico</param>
     /// <returns></returns>
    [HttpPost]
    public ActionResult ViewAll(int id, bool activar)
    {
      com = FabricaComando.GetComandoActualizarEstadoLugarTuristico();
      com.SetPropiedad(id);
      com.SetPropiedad(activar);
      com.Execute();

      JObject respuesta;
      ViewBag.Title = "Lugares Turísticos";
      com = FabricaComando.GetComandoConsultarLugaresTuristicos();
      com.Execute();
      respuesta = (JObject)com.GetResult()[0];
      List<LugarTuristico> _lugaresTuristicos = respuesta["data"].ToObject<List<LugarTuristico>>();
      return View(_lugaresTuristicos);

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

    public void LlenadoLugarTuristico()
    {
      //Seteando la foto    
      com.SetPropiedad (Request.Form["fotoLugar"]);

      //Seteando el nombre
      com.SetPropiedad(Request.Form["Nombre"]);

      //Seteando el costo
      Double.TryParse(Request.Form["Costo"], out double costo);
      com.SetPropiedad(costo);

      //Seteando el status
      com.SetPropiedad(Request.Form["activar"]);
      
      //Seteando las categorias
      for (int i=0; i <= 3; i++)
      {

        com.SetPropiedad(String.Format("{0}", Request.Form["categoria_"+i]));       
       
      }

      //Seteando los horarios
      for(int i = 0; i <= 6; i++)
      {

        com.SetPropiedad( Request.Form["dia_"+i]);
        com.SetPropiedad( Request.Form["Horario["+i+"].HoraApertura"]);
        com.SetPropiedad( Request.Form["Horario[" + i + "].HoraCierre"]);
        
      }

      //Seteando las actividades
      for (int i = 0; i <= 3; i++)
      {
        com.SetPropiedad(Request.Form["Actividad[" + i + "].Activar"]);
        com.SetPropiedad(Request.Form["fotoActividad_" + i ]);
        com.SetPropiedad(Request.Form["Actividad[" + i + "].Nombre"]);
        com.SetPropiedad(Request.Form["Actividad[" + i + "].Descripcion"]);
        com.SetPropiedad(Request.Form["Actividad[" + i + "].Duracion"]);
      }

      //Seteando los ultimos parametros

      com.SetPropiedad(Request.Form["Direccion"]);
      com.SetPropiedad(Request.Form["Correo"]);
      com.SetPropiedad(Request.Form["Telefono"]);
      com.SetPropiedad(Request.Form["Descripcion"]);
      com.SetPropiedad(Request.Form["Latitud"]);
      com.SetPropiedad(Request.Form["Longitud"]);

      
    }


   

    }
}
