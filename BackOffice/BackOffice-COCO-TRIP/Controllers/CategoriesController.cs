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
      ViewBag.Title = "Crear Categoría";
      //buscar categorias para el select
      IList<Categories> MyList = new List<Categories>(){
            new Categories(){Id=1, Name="UK"},
            new Categories(){Id=2, Name="VE"}
      };

      ViewBag.MyList = MyList;
      return View();
    }

    // POST: Categories/Create
    [HttpPost]
    public ActionResult Create(Categories categories)
    {
      ModelState.Remove("UpperCategories");
      if (ModelState.IsValid)
      {
        //categories.UpperCategories = new Categories() { Id = Int32.Parse(Request["categoria superior"]) };
        return RedirectToAction("Index");
      }

      return View(categories);

    }

    // GET: Categories/Edit/5
    public ActionResult Edit(int id)
    {
      ViewBag.Title = "Editar Categoría";
      IList<Categories> MyList = new List<Categories>(){
            new Categories(){Id=1, Name="UK"},
            new Categories(){Id=2, Name="VE"}
      };

      ViewBag.MyList = MyList;
      Categories categories = null;
      if (TempData["listaCategorias"] != null)
      {
        IList<Categories> listaCategorias = TempData["listaCategorias"] as IList<Categories>;
        categories = listaCategorias.Where(s => s.Id == id).First();
      }

      else
      {
        categories = new Categories() { Id = 4, Name = "Sin data", Description = "Sin data", Status = false, UpperCategories = 1 };
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
        categories.UpperCategories = Int32.Parse(Request["Mover a la categoria"]) ;
        return RedirectToAction("Index");
      }


      return View(categories);
    }

    [HttpPost]
    public ActionResult ChangeStatus(Categories categories)
    {
      JObject respuesta = peticion.Put(categories);
      return Json(respuesta);
    }
  }
}
