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

    // GET: Categories/Create
    public ActionResult Create()
    {
      ViewBag.Title = "Agregar Categoría";
      IList<Categories> listCategories = null;
      JObject respuesta = peticion.GetCategoriasHabilitadas();
      if (respuesta.Property("data") != null)
      {
        listCategories = respuesta["data"].ToObject<IList<Categories>>();
        listCategories = listCategories.Where(s => s.Nivel < 3).ToList();
      }

      else
      {
        listCategories = new List<Categories>();
        ModelState.AddModelError(string.Empty, "Error en la conexion.");
      }

      ViewBag.MyList = listCategories;
      Categories categories = null;

      return View(categories);
    }

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
        else
        {
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
          }
          ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");
          return View(categories);
        }
      }

      return View(categories);

    }

    // GET: Categories/Edit/5
    public ActionResult Edit(int id)
    {
      ViewBag.Title = "Editar Categoría";
      IList<Categories> listCategories = null;
      JObject respuesta = peticion.GetCategoriasHabilitadas();
      if (respuesta.Property("data") != null)
      {
        listCategories = respuesta["data"].ToObject<IList<Categories>>();
        listCategories = listCategories.Where(s => s.Nivel <3 && s.Id!=id).ToList();
      }

      else
      {
        listCategories = new List<Categories>();
        ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");
      }

      ViewBag.MyList = listCategories;
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

        else
        {
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
          }
          ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");
          return View(categories);
        } 
      }
      
      return View(categories);
    }

    [HttpPost]
    public ActionResult ChangeStatus(Categories categories)
    {
      JObject respuesta = peticion.Put(categories);
      return Json(respuesta);
    }

    private IList<Categories> ConsutarCategoriasSelect()
    {
      IList<Categories> listCategories = null;
      JObject respuesta = peticion.GetCategoriasHabilitadas();
      if (respuesta.Property("data") != null)
      {
        listCategories = respuesta["data"].ToObject<IList<Categories>>();
        //listCategories = listCategories.Where(s => s.Nivel < 3 && s.Id != id).ToList();
      }

      else
      {
        listCategories = null;
       // ModelState.AddModelError(string.Empty, "Ocurrio un error durante la comunicacion, revise su conexion a internet");
      }

      return listCategories;
    }
  }
}
