using ApiRest_COCO_TRIP.Negocio.Comando;

namespace ApiRest_COCO_TRIP.Negocio.Fabrica
{
  /// <summary>
  /// Fabrica que instancia los comandos
  /// </summary>
  public class FabricaComando
  {
    /// <summary>
    /// Retorna la instancia de ComandoAgregarAmigo
    /// </summary>
    /// <param name="id">ID del usuario</param>
    /// <param name="nombre">Nombre de usuario destino</param>
    /// <returns></returns>
    public static ComandoAgregarAmigo CrearComandoAgregarAmigo (int id, string nombre)
    {
      return new ComandoAgregarAmigo(id, nombre);
    }

    /// <summary>
    /// Retorna la instancia de ComandoVisualizarPerfilAmigo
    /// </summary>
    /// <param name="nombre">Nombre de usuario</param>
    /// <returns></returns>
    public static ComandoVisualizarPerfilAmigo CrearComandoVisualizarPerfilAmigo (string nombre)
    {
      return new ComandoVisualizarPerfilAmigo(nombre);
    }

    /// <summary>
    /// Retorna la instancia de ComandoEnviarNotificacionCorreo
    /// </summary>
    /// <param name="correo">Correo electronico de la persona a la que se le va a recomendar la aplicacion</param>
    /// <param name="id">ID del usuario que envia la notificacion</param>
    /// <param name="nombreDestino">Nombre de usuario al que va destinada la notificacion</param>
    /// <returns></returns>
    public static ComandoEnviarNotificacionCorreo CrearComandoEnviarNotificacionCorreo (string correo, int id, string nombreDestino)
    {
      return new ComandoEnviarNotificacionCorreo(correo, id, nombreDestino);
    }

    /// <summary>
    /// Retorna la instancia de ComandoSalirGrupo
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo a eliminar/salir</param>
    /// <param name="idUsuario">Identificador del usuario que quiere eliminar o salir del grupo</param>
    /// <returns></returns>
    public static ComandoSalirGrupo CrearComandoSalirGrupo (int idGrupo, int idUsuario)
    {
      return new ComandoSalirGrupo(idGrupo, idUsuario);
    }
  }
}
