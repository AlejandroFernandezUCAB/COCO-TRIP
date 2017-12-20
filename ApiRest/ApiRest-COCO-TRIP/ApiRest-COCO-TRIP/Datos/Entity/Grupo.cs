namespace ApiRest_COCO_TRIP.Datos.Entity
{
  /// <summary>
  /// Entidad que almacena los datos asociados a los Grupos de Amigos
  /// </summary>
  public class Grupo: Entidad
  {
    private string nombre; //Nombre del grupo
    private string foto; //Ruta de la foto
    private int lider; //ID del creador del grupo

    /// <summary>
    /// Getters y Setters del atributo nombre
    /// </summary>
    public string Nombre
    {
      get { return nombre;  }
      set { nombre = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo foto
    /// </summary>
    public string Foto
    {
      get { return foto; }
      set { foto = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo lider
    /// </summary>
    public int Lider
    {
      get { return lider; }
      set { lider = value; }
    }
  }

}
