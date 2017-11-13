using Newtonsoft.Json;
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

    [JsonProperty(PropertyName = "IdCategoria")]
    public int Id
    {
      get { return _id; }
      set { _id = value; }
    }

    private string _name;

    [JsonProperty(PropertyName = "nombre")]
    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "Debe llenar este campo")]
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }


    private string _description;

    [JsonProperty(PropertyName = "descripcion")]
    [Display(Name = "Descripcion")]
    [Required(ErrorMessage = "Debe llenar este campo")]
    public string Description
    {
      get { return _description; }
      set { _description = value; }
    }

    [JsonProperty(PropertyName = "estatus")]
    private bool _status;

    public bool Status
    {
      get { return _status; }
      set { _status = value; }
    }

    private int _categories;

    [JsonProperty(PropertyName = "categoriaSuperior")]
    [Required]
    public int UpperCategories  
    {
      get { return _categories; }
      set { _categories = value; }
    }

    private int nivel;

    [JsonProperty(PropertyName = "nivel")]
    public int Nivel
    {
      get { return nivel; }
      set { nivel = value; }
    }



  }
}
