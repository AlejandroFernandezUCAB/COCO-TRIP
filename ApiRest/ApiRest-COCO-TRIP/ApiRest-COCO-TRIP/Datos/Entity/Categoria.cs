using Newtonsoft.Json;

namespace ApiRest_COCO_TRIP.Datos.Entity
{
  public class Categoria:Entidad
  {
    private string nombre;

    [JsonProperty(PropertyName = "nombre")]
    public string Nombre
    {
      get { return nombre; }
      set { nombre = value; }
    }

    private string descripcion;

    [JsonProperty(PropertyName = "descripcion")]
    public string Descripcion
    {
      get { return descripcion; }
      set { descripcion = value; }
    }

    private bool estatus;

    [JsonProperty(PropertyName = "estatus")]
    public bool Estatus
    {
      get { return estatus; }
      set { estatus = value; }
    }

    private int nivel;

    [JsonProperty(PropertyName = "nivel")]
    public int Nivel
    {
      get { return nivel; }
      set { nivel = value; }
    }

    private int categoriaSuperior;

    [JsonProperty(PropertyName = "categoriaSuperior")]
    public int CategoriaSuperior
    {
      get { return categoriaSuperior; }
      set { categoriaSuperior = value; }
    }



    public Categoria() { }

    public Categoria(int Id)
    {
      this.Id = Id;
    }
  }
}
