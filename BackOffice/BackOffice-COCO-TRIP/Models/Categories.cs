using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackOffice_COCO_TRIP.Models
{
  public class Categories
  {

    private int _id;

    public int Id
    {
      get { return _id; }
      set { _id = value; }
    }

    private string _name;

    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "Debe llenar este campo")]
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }


    private string _description;

    [Display(Name = "Descripcion")]
    [Required(ErrorMessage = "Debe llenar este campo")]
    public string Description
    {
      get { return _description; }
      set { _description = value; }
    }

    private bool _status;

    public bool Status
    {
      get { return _status; }
      set { _status = value; }
    }

    private Categories _categories;

    [Required]
    public Categories UpperCategories  
    {
      get { return _categories; }
      set { _categories = value; }
    }


  }
}
