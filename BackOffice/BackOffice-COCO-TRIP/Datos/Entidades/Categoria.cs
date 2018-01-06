using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///      Entidad que almacena la informacion de una Categoria.
/// </summary>
namespace BackOffice_COCO_TRIP.Datos.Entidades
{
  /// <summary>
  /// Entidad que almacena la informacion de una Categoria.
  /// </summary>
  public class Categoria:Entidad
  {
    private string _name;         //Nombre de la categoria.
    private string _description;  //Descripcion de la categoria.
    private bool _status;         //Estado de la categoria.
    private int _categories;      //identificador de la categoria superior.
    private int nivel;            //Nivel de la categoria.

    /// <summary>
    /// Metodo Constructor.
    /// </summary>
    public Categoria() { }

    /// <summary>
    /// Metodo Constructor.
    /// </summary>
    /// <param name="Id">Identificador unico de la categoria.</param>
    public Categoria(int Id)
    {
      this.Id = Id;
    }

    /// <summary>
    /// Getters y Setters del atributo "_name" de la categoria.
    /// </summary>
    [JsonProperty(PropertyName = "nombre")]
    [Required(ErrorMessage = "Debe llenar este campo")]
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo "_description" de la categoria.
    /// </summary>
    [JsonProperty(PropertyName = "descripcion")]
    [Required(ErrorMessage = "Debe llenar este campo")]
    public string Description
    {
      get { return _description; }
      set { _description = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo "_status" de la categoria.
    /// </summary>
    [JsonProperty(PropertyName = "estatus")]
    public bool Status
    {
      get { return _status; }
      set { _status = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo "_categories" de la categoria.
    /// </summary>
    [JsonProperty(PropertyName = "categoriaSuperior")]
    [Required]
    public int UpperCategories
    {
      get { return _categories; }
      set { _categories = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo "nivel" de la categoria.
    /// </summary>
    [JsonProperty(PropertyName = "nivel")]
    public int Nivel
    {
      get { return nivel; }
      set { nivel = value; }
    }
  }
}
