using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BackOffice_COCO_TRIP.Datos.Entidades
{
  public class Categoria:Entidad
  {
    

   

    public Categoria() { }

    public Categoria(int Id)
    {
      this.Id = Id;
    }

    private string _name;

    [JsonProperty(PropertyName = "nombre")]
    [Required(ErrorMessage = "Debe llenar este campo")]
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }


    private string _description;

    [JsonProperty(PropertyName = "descripcion")]
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
