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
    private int cantidadIntegrantes; //Cantidad de miembros del grupo

    /// <summary>
    /// Getters y Setters del atributo Nombre
    /// </summary>
    public string Nombre
    {
      get { return nombre;  }
      set { nombre = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Foto
    /// </summary>
    public string Foto
    {
      get { return foto; }
      set { foto = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Lider
    /// </summary>
    public int Lider
    {
      get { return lider; }
      set { lider = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Cantidad Integrantes
    /// </summary>
    public int CantidadIntegrantes
    {
      get { return cantidadIntegrantes; }
      set { cantidadIntegrantes = value; }
    }
  }
}
