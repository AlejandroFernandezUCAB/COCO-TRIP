using BackOffice_COCO_TRIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackOffice_COCO_TRIP.Controllers
{
  public class CategoriesController : Controller
  {
    // GET: Categories
    public ActionResult Index(int id = 0)
    {
      ViewBag.Title = "Categorías";
      IList<Categories> listCategories = null;

      if (id == 0)
      {
        listCategories = new List<Categories>
      {
        new Categories() { Id = 1, Name = "Evento", Description = "Se registran eventos", Status = true, UpperCategories = null },
        new Categories() { Id = 2, Name = "Lugar", Description = "Se registran lugares", Status = false, UpperCategories = null },
        new Categories() { Id = 3, Name = "Turista", Description = "Se registran turistas", Status = true, UpperCategories = null }
      };

      } else {

        listCategories = new List<Categories>
      {
        new Categories() { Id = 1, Name = "Evento 2", Description = "Se registran eventos 2", Status = false, UpperCategories = null },
        new Categories() { Id = 2, Name = "Lugar 2", Description = "Se registran lugares 2", Status = true, UpperCategories = null },
        new Categories() { Id = 3, Name = "Turista 2", Description = "Se registran turistas 2", Status = false, UpperCategories = null }
      };

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
        categories.UpperCategories = new Categories() { Id = Int32.Parse(Request["categoria superior"]) };
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
        categories = new Categories() { Id = 4, Name = "Sin data", Description = "Sin data", Status = false, UpperCategories = null };
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
        categories.UpperCategories = new Categories() { Id = Int32.Parse(Request["categoria superior"]) };
        return RedirectToAction("Index");
      }


      return View(categories);
    }

    [HttpPost]
    public ActionResult ChangeStatus(Categories categories)
    {
      try
      {
        // TODO: Add delete logic here

        return Json(data: categories);
      }
      catch
      {
        return Json(data: "error");
      }
    }
  }
}
