namespace ApiRest_COCO_TRIP.Datos.Entity
{
  /// <summary>
  /// Entidad que almacena la relacion entre dos usuarios
  /// </summary>
  public class Amigo : Entidad
  {
    private int idUsuarioActivo; //Usuario que envia la solicitud de amistad
    private int idUsuarioPasivo; //Usuario que acepta la solicitud de amistad
    private bool aceptado; //(true) Los usuarios son amigos, (false) es una solicitud de amistad pendiente

    /// <summary>
    /// Getters y Setters del atributo Id del usuario activo
    /// </summary>
    public int Activo
    {
      get { return idUsuarioActivo; }
      set { idUsuarioActivo = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Id del usuario pasivo
    /// </summary>
    public int Pasivo
    {
      get { return idUsuarioPasivo; }
      set { idUsuarioPasivo = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Aceptado
    /// </summary>
    public bool Aceptado
    {
      get { return aceptado; }
      set { aceptado = value; }
    }
  }
}
