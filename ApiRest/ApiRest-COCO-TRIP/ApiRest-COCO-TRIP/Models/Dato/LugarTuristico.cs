using System.Collections.Generic;
using System.Linq;

namespace ApiRest_COCO_TRIP.Models.Dato
{
  /// <summary>
  /// Clase que contiene los datos de lugar turistico
  /// </summary>
  public class LugarTuristico
  {
    private int id; //Identificador unico del lugar turistico
    private string nombre; //Nombre del lugar turistico
    private double costo; //Costo del lugar turistico
    private string descripcion; //Descripcion del lugar turistico
    private string direccion; //Ubicacion del lugar turistico
    private string correo; //Correo electronico del lugar turistico
    private long telefono; //Numero de telefono del lugar turistico
    private double latitud; //Coordenada Google Maps
    private double longitud; //Coordenada Google Maps
    private bool activar; //Activar o desactivar lugar turistico

    private List<Foto> foto; //Fotos del lugar turistico
    private List<Horario> horario; //Horarios del lugar turistico
    private List<Actividad> actividad; //Actividades del lugar turistico
    private List<Categoria> categoria; //Categorias del lugar turistico 
    private List<Categoria> subCategoria; //Subcategorias del lugar turistico

    /// <summary>
    /// Constructor que inicializa los atributos de la clase
    /// </summary>
    public LugarTuristico()
    {
      foto = new List<Foto>();
      horario = new List<Horario>();
      actividad = new List<Actividad>();

      categoria = new List<Categoria>();
      subCategoria = new List<Categoria>();
    }

    /// <summary>
    /// Getters y Setters del atributo ID
    /// </summary>
    public int Id
    {
      get { return id; }
      set { id = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Nombre
    /// </summary>
    public string Nombre
    {
      get { return nombre; }
      set { nombre = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Costo
    /// </summary>
    public double Costo
    {
      get { return costo; }
      set { costo = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Descripcion
    /// </summary>
    public string Descripcion
    {
      get { return descripcion; }
      set { descripcion = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Direccion
    /// </summary>
    public string Direccion
    {
      get { return direccion; }
      set { direccion = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Correo
    /// </summary>
    public string Correo
    {
      get { return correo; }
      set { correo = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Telefono
    /// </summary>
    public long Telefono
    {
      get { return telefono; }
      set { telefono = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Latitud
    /// </summary>
    public double Latitud
    {
      get { return latitud; }
      set { latitud = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Longitud
    /// </summary>
    public double Longitud
    {
      get { return longitud; }
      set { longitud = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Activar
    /// </summary>
    public bool Activar
    {
      get { return activar; }
      set { activar = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Foto
    /// </summary>
    public List<Foto> Foto
    {
      get { return foto; }
      set { foto = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Horario
    /// </summary>
    public List<Horario> Horario
    {
      get { return horario; }
      set { horario = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Horario
    /// </summary>
    public List<Actividad> Actividad
    {
      get { return actividad; }
      set { actividad = value; }

    }

    /// <summary>
    /// Getters y Setters del atributo Categoria
    /// </summary>
    public List<Categoria> Categoria
    {
      get { return categoria; }
      set { categoria = value; }

    }

    /// <summary>
    /// Getters y Setters del atributo SubCategoria
    /// </summary>
    public List<Categoria> SubCategoria
    {
      get { return subCategoria; }
      set { subCategoria = value; }

    }

    /// <summary>
    /// Compara si dos objetos de tipo LugarTuristico son iguales
    /// </summary>
    /// <param name="obj">LugarTuristico</param>
    /// <returns>(bool) Si son iguales retorna true</returns>
    public override bool Equals(object obj)
    {
      if (obj != null && obj is LugarTuristico)
      {
        var objeto = obj as LugarTuristico;

        if (id != objeto.id || nombre != objeto.nombre || costo != objeto.costo || descripcion != objeto.descripcion
            || direccion != objeto.direccion || correo != objeto.correo || telefono != objeto.telefono || latitud != objeto.latitud
            || longitud != objeto.longitud || activar != objeto.activar || !foto.SequenceEqual<Foto>(objeto.foto)
            || !horario.SequenceEqual<Horario>(objeto.horario) || !actividad.SequenceEqual<Actividad>(objeto.actividad)
          /*|| !categoria.SequenceEqual<Categoria>(objeto.Categoria) || !subCategoria.SequenceEqual<Categoria>(objeto.SubCategoria)*/)
        {

          return (false);
        }
        else
        {
          return (true);
        }
      }
      else
      {
        return (false);
      }
    }

    /// <summary>
    /// Sobreescritura recomendada del metodo GetHashCode
    /// </summary>
    /// <returns>ID del objeto</returns>
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
