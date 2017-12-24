using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Models.Peticion;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using BackOffice_COCO_TRIP.Negocio.Componentes.Comandos;
using BackOffice_COCO_TRIP.Negocio;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace BackOffice_COCO_TRIP.Controllers
{
  public class CategoriesController : Controller
  {
    private Comando com;

    /// <summary>
    /// Metodo que nos permite obtener la lista de las categorias mediante peticiones al servicio web a la hora de cargar
    /// </summary>
    // GET: Categories
    public ActionResult Index(int id = -1)
    {
      ViewBag.Title = "Categorías";
      IList<Categoria> listCategories = null;

      com = FabricaComando.GetComandoConsultarListaCategoria();
      com.SetPropiedad(id);
      com.Execute();
      JObject respuestaCategoria = (JObject)com.GetResult()[0];

      if (respuestaCategoria.Property("data") != null)
      {
        listCategories = respuestaCategoria["data"].ToObject<List<Categoria>>();
      }
      else
      {
        listCategories = new List<Categoria>();
        ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");
      }
      TempData["listaCategorias"] = listCategories;
      return View(listCategories);

    }


    /// <summary>
    /// Metodo que carga la pagina de creacion de categoria
    /// </summary>
    // GET: Categories/Create
    public ActionResult Create()
    {
      ViewBag.Title = "Agregar Categoría";
      com = FabricaComando.GetComandoConsultarCategoriaSelect();
      com.Execute();
      var listaCategorias = (List<Categoria>)com.GetResult()[0];
      if (listaCategorias != null)
      {
        listaCategorias = listaCategorias.Where(s => s.Nivel < 3).ToList();
        ViewBag.MyList = listaCategorias;
      }
      else
      {
        listaCategorias = new List<Categoria>();
        ViewBag.MyList = listaCategorias;
        ModelState.AddModelError(string.Empty, "Ocurrio un error cargando las categorias, revise su conexion a internet");
      }
      
      Categoria categories = null;

      return View(categories);
    }

    /// <summary>
    /// Metodo que nos permite crear una nueva categoria mediante peticiones al servicio web
    /// </summary>
    // POST: Categories/Create
    [HttpPost]
    public ActionResult Create(Categoria categories)
    {
      ModelState.Remove("UpperCategories");
      if (ModelState.IsValid && ValidarName(categories.Name) && ValidarDescription(categories.Description))
      {
        string[] idNivel = Request["Categoria superior"].ToString().Split('-');
        categories.UpperCategories = Int32.Parse(idNivel[0]);
        categories.Nivel = Int32.Parse(idNivel[1]) + 1;

        com = FabricaComando.GetComandoAgregarCategoria();
        com.SetPropiedad(categories);
        com.Execute();
        JObject respuesta = (JObject)com.GetResult()[0];
        //JObject respuesta = peticion.Post(categories);

        if (respuesta.Property("data") != null)
        {
          return RedirectToAction("Index");

        }
        ValidarErrorPorDuplicidad(respuesta);
      }

      //reutilizar con create() es el mismo codigo solo que create() return view(null) y aqui return (categories)
      //capaz se puede hacer un metodo que le pases lo que va a retornear
      com = FabricaComando.GetComandoConsultarCategoriaSelect();
      com.Execute();
      var listaCategorias = (List<Categoria>)com.GetResult()[0];
      if (listaCategorias != null)
      {
        listaCategorias = listaCategorias.Where(s => s.Nivel < 3).ToList();
        ViewBag.MyList = listaCategorias;
      }
      else
      {
        listaCategorias = new List<Categoria>();
        ViewBag.MyList = listaCategorias;
        ModelState.AddModelError(string.Empty, "Ocurrio un error cargando las categorias, revise su conexion a internet");
      }
      ViewBag.Title = "Agregar Categoría";
      return View(categories);

    }

    /// <summary>
    /// Metodo que nos permite obtener lista de categorias a la cual se va a editar mediante peticiones al servicio web
    /// </summary>
    // GET: Categories/Edit/5
    public ActionResult Edit(int id)
    {
      ViewBag.Title = "Editar Categoría";
      com = FabricaComando.GetComandoConsultarCategoriaSelect();
      com.Execute();
      var listaCategoriasSelect = (List<Categoria>)com.GetResult()[0];
      if (listaCategoriasSelect != null)
      {
        listaCategoriasSelect = listaCategoriasSelect.Where(s => s.Nivel < 3 && s.Id != id).ToList();
        ViewBag.MyList = listaCategoriasSelect;

      }

      else
      {
        listaCategoriasSelect = new List<Categoria>();
        ViewBag.MyList = listaCategoriasSelect;
        ModelState.AddModelError(string.Empty, "Ocurrio un error cargando las categorias, revise su conexion a internet");
      }
      
      Categoria categories = null;
      if (TempData["listaCategorias"] != null)
      {
        IList<Categoria> listaCategorias = TempData["listaCategorias"] as IList<Categoria>;
        categories = listaCategorias.Where(s => s.Id == id).First();
      }

      else
      {
        com = FabricaComando.GetComandoConsultarCategoriaPorId();
        com.SetPropiedad(id);
        com.Execute();
        JObject respuestaCategoria = (JObject)com.GetResult()[0];
        if (respuestaCategoria.Property("data") != null)
        {
          categories = (respuestaCategoria["data"].HasValues ? respuestaCategoria["data"][0].ToObject<Categoria>() : null) ;
          if (categories == null)
          {
            return RedirectToAction("Index");
          }
        }

        else
        {
          ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");
        }

      }
      
      return View(categories);
      
    }

    /// <summary>
    /// Metodo que nos permite editar una nueva categoria existente mediante peticiones al servicio web
    /// </summary>
    // POST: Categories/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, Categoria categories)
    {
      ModelState.Remove("Id");
      ModelState.Remove("UpperCategories");
      if (ModelState.IsValid)
      {
        var idNivel = Request["Mover a la categoria"].ToString().Split('-');
        categories.UpperCategories = Int32.Parse(idNivel[0]);
        categories.Nivel = Int32.Parse(idNivel[1]) + 1;
        com = FabricaComando.GetComandoModificarCategoria();
        com.SetPropiedad(categories);
        com.Execute();
        JObject respuesta = (JObject)com.GetResult()[0];
        if (respuesta.Property("data") != null)
        {
          return RedirectToAction("Index");

        }
        ValidarErrorPorDuplicidad(respuesta);
      }

      com = FabricaComando.GetComandoConsultarCategoriaSelect();
      com.Execute();
      var listaCategorias = (List<Categoria>)com.GetResult()[0];
      if (listaCategorias != null)
      {
        listaCategorias = listaCategorias.Where(s => s.Nivel < 3 && s.Id != id).ToList();
        ViewBag.MyList = listaCategorias;

      }

      else
      {
        listaCategorias = new List<Categoria>();
        ViewBag.MyList = listaCategorias;
        ModelState.AddModelError(string.Empty, "Ocurrio un error cargando las categorias, revise su conexion a internet");
      }

      ViewBag.Title = "Editar Categoría";
      return View(categories);
    }

    /// <summary>
    /// Metodo que nos permite cambiar el status de una categoria mediante peticiones al servicio web
    /// </summary>
    [HttpPost]
    public ActionResult ChangeStatus(Categoria categories)
    {
      com = FabricaComando.GetComandoEstadoCategoria();
      com.SetPropiedad(categories);
      com.Execute();
      JObject respuesta = (JObject)com.GetResult()[0];
      return Json(respuesta);
    }


    // ######################## Metodos Auxiliares ########################
    
    // ############## Metodos para Validaciones de Vistas #################
    /// <summary>
    /// Metodo que nos permite validar si el nombre existe antes de agregar
    /// </summary>
    private void ValidarErrorPorDuplicidad(JObject respuesta)
    {
      if (respuesta.Property("MensajeError") != null)
      {
        ModelState.AddModelError(string.Empty, respuesta["MensajeError"].ToString());
      }
      else
      {
        ModelState.AddModelError(string.Empty, "Ocurrio un error, revise su conexion a internet");
      }

    }

    private bool ValidarLongitudInputNombre(String input)
    {
      if ((input.Length >= 5) && (input.Length <= 20)){
        return true;
      }
      return false;
    }

    private bool ValidarLongitudInputDescripcion(String input)
    {
      if ((input.Length >= 5) && (input.Length <= 100))
      {
        return true;
      }
      return false;
    }

    private bool ValidarCaracteresEspeciales(String input)
    {
      if (Regex.Match(input, @"^[a-zA-Z]+$").Success)
      {
        return true;
      }
      return false;
    }

    private bool ValidarName(String input)
    {
      if (ValidarCaracteresEspeciales(input) && ValidarLongitudInputNombre(input))
      {
        return true;
      }
      ModelState.AddModelError(string.Empty, "El nombre de la categoria debe tener al menos 5 caracteres y máximo 20. Sólo se permiten letras.");
      return false;
    }

    private bool ValidarDescription(String input)
    {
      if (ValidarCaracteresEspeciales(input) && ValidarLongitudInputDescripcion(input))
      {
        return true;
      }
      ModelState.AddModelError(string.Empty, "El nombre de la categoria debe tener al menos 5 caracteres y máximo 100. Sólo se permiten letras.");
      return false;
    }
  }
}
