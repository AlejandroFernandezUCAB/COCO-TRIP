using BackOffice_COCO_TRIP.Models;
using BackOffice_COCO_TRIP.Models.Peticion;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackOffice_COCO_TRIP.Controllers
{
  public class CategoriesController : Controller
  {

    private PeticionCategoria peticion = new PeticionCategoria();

    /// <summary>
    /// Metodo que nos permite obtener la lista de las categorias mediante peticiones al servicio web a la hora de cargar
    /// </summary>
    // GET: Categories
    public ActionResult Index(int id = -1)
    {
      ViewBag.Title = "Categorías";
      IList<Categories> listCategories = null;
      JObject respuesta = peticion.Get(id);
      if (respuesta.Property("data") != null)
      {
        listCategories = respuesta["data"].ToObject<List<Categories>>();
      }

      else
      {
        listCategories = new List<Categories>();
        ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");
      }

      TempData["listaCategorias"] = listCategories;
      return View(listCategories);

    }


    /// <summary>
    /// Metodo que nos permite listar las categorias existentes a la hora de agregar una nueva mediante  peticiones al servicio web
    /// </summary>
    // GET: Categories/Create
    public ActionResult Create()
    {
      ViewBag.Title = "Agregar Categoría";
      var listaCategorias = ConsutarCategoriasSelect();
      if (listaCategorias != null)
      {
        listaCategorias = listaCategorias.Where(s => s.Nivel < 3).ToList();
        ViewBag.MyList = listaCategorias;

      }

      else
      {
        listaCategorias = new List<Categories>();
        ViewBag.MyList = listaCategorias;
        ModelState.AddModelError(string.Empty, "Ocurrio un error cargando las categorias, revise su conexion a internet");
      }
      
      Categories categories = null;

      return View(categories);
    }

    /// <summary>
    /// Metodo que nos permite crear una nueva categoria mediante peticiones al servicio web
    /// </summary>
    // POST: Categories/Create
    [HttpPost]
    public ActionResult Create(Categories categories)
    {
      ModelState.Remove("UpperCategories");
      if (ModelState.IsValid)
      {
       
        var idNivel = Request["Categoria superior"].ToString().Split('-');
        categories.UpperCategories = Int32.Parse(idNivel[0]);
        categories.Nivel = Int32.Parse(idNivel[1]) + 1;
        JObject respuesta = peticion.Post(categories);

        if (respuesta.Property("data") != null)
        {
          return RedirectToAction("Index");

        }
        ValidarErrorPorDuplicidad(respuesta);
      }

      var listaCategorias = ConsutarCategoriasSelect();
      if (listaCategorias != null)
      {
        listaCategorias = listaCategorias.Where(s => s.Nivel < 3).ToList();
        ViewBag.MyList = listaCategorias;

      }

      else
      {
        listaCategorias = new List<Categories>();
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
      var listaCategoriasSelect = ConsutarCategoriasSelect();
      if (listaCategoriasSelect != null)
      {
        listaCategoriasSelect = listaCategoriasSelect.Where(s => s.Nivel < 3 && s.Id != id).ToList();
        ViewBag.MyList = listaCategoriasSelect;

      }

      else
      {
        listaCategoriasSelect = new List<Categories>();
        ViewBag.MyList = listaCategoriasSelect;
        ModelState.AddModelError(string.Empty, "Ocurrio un error cargando las categorias, revise su conexion a internet");
      }
      
      Categories categories = null;
      if (TempData["listaCategorias"] != null)
      {
        IList<Categories> listaCategorias = TempData["listaCategorias"] as IList<Categories>;
        categories = listaCategorias.Where(s => s.Id == id).First();
      }

      else
      {
        JObject respuestaCategoria = peticion.GetPorId(id);
        if (respuestaCategoria.Property("data") != null)
        {
          categories = (respuestaCategoria["data"].HasValues ? respuestaCategoria["data"][0].ToObject<Categories>() : null) ;
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
    public ActionResult Edit(int id, Categories categories)
    {
      ModelState.Remove("Id");
      ModelState.Remove("UpperCategories");
      if (ModelState.IsValid)
      {
        var idNivel = Request["Mover a la categoria"].ToString().Split('-');
        categories.UpperCategories = Int32.Parse(idNivel[0]);
        categories.Nivel = Int32.Parse(idNivel[1]) + 1;
        JObject respuesta = peticion.PutEditarCategoria(categories);
        if (respuesta.Property("data") != null)
        {
          return RedirectToAction("Index");

        }
        ValidarErrorPorDuplicidad(respuesta);
      }

      var listaCategorias = ConsutarCategoriasSelect();
      if (listaCategorias != null)
      {
        listaCategorias = listaCategorias.Where(s => s.Nivel < 3 && s.Id != id).ToList();
        ViewBag.MyList = listaCategorias;

      }

      else
      {
        listaCategorias = new List<Categories>();
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
    public ActionResult ChangeStatus(Categories categories)
    {
      JObject respuesta = peticion.Put(categories);
      return Json(respuesta);
    }

    /// <summary>
    /// Metodo que nos permite obtener la lista de las categorias habilitadas mediante una consulta
    /// </summary>
    private IList<Categories> ConsutarCategoriasSelect()
    {
      IList<Categories> listCategories = null;
      JObject respuesta = peticion.GetCategoriasHabilitadas();
      if (respuesta.Property("data") != null)
      {
        listCategories = respuesta["data"].ToObject<IList<Categories>>();
        
      }

      else
      {
        listCategories = null;
       
      }

      return listCategories;
    }

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
  }
}
