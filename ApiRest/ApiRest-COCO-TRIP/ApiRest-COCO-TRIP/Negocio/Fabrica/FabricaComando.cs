using ApiRest_COCO_TRIP.Datos.Entity;
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

    /// <summary>
    /// Retorna la instancia de ComandoObtenerListaNotificaciones
    /// </summary>
    /// <param name="id">Identificador del usuario</param>
    /// <returns></returns>
    public static ComandoObtenerListaNotificaciones CrearComandoObtenerListaNotificaciones (int id)
    {
      return new ComandoObtenerListaNotificaciones(id);
    }

    /// <summary>
    /// Retorna la instancia de ComandoRechazarNotificacion
    /// </summary>
    /// <param name="id">Identificador del usuario que esta rechazando la notificacion</param>
    /// <param name="nombreRechazado">Nombre del usuario rechazado</param>
    /// <returns></returns>
    public static ComandoRechazarNotificacion CrearComandoRechazarNotificacion (int id, string nombreRechazado)
    {
      return new ComandoRechazarNotificacion(id, nombreRechazado);
    }

    /// <summary>
    /// Retorna la instancia de ComandoAceptarNotificacion
    /// </summary>
    /// <param name="id">Identificador del usuario que esta aceptando la notificacion</param>
    /// <param name="nombreAceptado">Nombre del usuario aceptado</param>
    /// <returns></returns>
    public static ComandoAceptarNotificacion CrearComandoAceptarNotificacion(int id, string nombreAceptado)
    {
      return new ComandoAceptarNotificacion(id, nombreAceptado);
    }

    /// <summary>
    /// Retorna la instancia de ComandoBuscarAmigos
    /// </summary>
    /// <param name="id">Identificador del usuario que esta buscando</param>
    /// <param name="nombre">Nombre del amigo a buscar</param>
    /// <returns></returns>
    public static ComandoBuscarAmigos CrearComandoBuscarAmigos(int id, string nombre)
    {
      return new ComandoBuscarAmigos(id, nombre);
    }

    /// <summary>
    /// Retorna la instancia de ComandoAgregarGrupo
    /// </summary>
    /// <param name="grupo">Datos del grupo a agregar</param>
    /// <returns></returns>
    public static ComandoAgregarGrupo CrearComandoAgregarGrupo(Entidad grupo)
    {
      return new ComandoAgregarGrupo(grupo);
    }

    /// <summary>
    /// Retorna la instancia de ComandoVisualizarListaAmigos
    /// </summary>
    /// <param name="id">Identificador del usuario</param>
    /// <returns></returns>
    public static ComandoVisualizarListaAmigos CrearComandoVisualizarListaAmigos(int id)
    {
      return new ComandoVisualizarListaAmigos(id);
    }

    /// <summary>
    /// Retorna la instancia de ComandoEliminarAmigo
    /// </summary>
    /// <param name="id">Identificador del usuario que quiere eliminar</param>
    /// <param name="nombreAmigo">Nombre de usuario del amigo a eliminar</param>
    /// <returns></returns>
    public static ComandoEliminarAmigo CrearComandoEliminarAmigo(int id, string nombreAmigo)
    {
      return new ComandoEliminarAmigo(id, nombreAmigo);
    }

    /// <summary>
    /// Retorna la instancia de ComandoEliminarGrupo
    /// </summary>
    /// <param name="idUsuario"></param>
    /// <param name="idGrupo"></param>
    /// <returns></returns>
    public static ComandoEliminarGrupo CrearComandoEliminarGrupo(int idUsuario, int idGrupo)
    {
      return new ComandoEliminarGrupo(idUsuario, idGrupo);
    }

    /// <summary>
    /// Retorna la instancia de ComandoModificarGrupo
    /// </summary>
    /// <param name="grupo">Datos del grupo</param>
    /// <returns></returns>
    public static ComandoModificarGrupo CrearComandoModificarGrupo(Entidad grupo)
    {
      return new ComandoModificarGrupo(grupo);
    }

    /// <summary>
    /// Retorna la instancia de ComandoConsultarListaGrupos
    /// </summary>
    /// <param name="id">ID del usuario logeado en la aplicacion</param>
    /// <returns></returns>
    public static ComandoConsultarListaGrupos CrearComandoConsultarListaGrupos(int id)
    {
      return new ComandoConsultarListaGrupos(id);
    }

    /// <summary>
    /// Retornar la instancia de ComandoConsultarMiembroGrupo
    /// </summary>
    /// <param name="id">ID del grupo por el cual se devuelven sus integrantes</param>
    /// <returns></returns>
    public static ComandoConsultarMiembroGrupo CrearComandoConsultarMiembroGrupo(int id)
    {
      return new ComandoConsultarMiembroGrupo(id);
    }

    /// <summary>
    /// Retorna la instancia de ComandoConsultarPerfilGrupo
    /// </summary>
    /// <param name="id">ID del grupo a buscar</param>
    /// <returns></returns>
    public static ComandoConsultarPerfilGrupo CrearComandoConsultarPerfilGrupo(int id)
    {
      return new ComandoConsultarPerfilGrupo(id);
    }

    /// <summary>
    /// Retorna la instancia de ComandoAgregarIntegrante
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="nombreUsuario">Nombre del usuario a agregar</param>
    /// <returns></returns>
    public static ComandoAgregarIntegrante CrearComandoAgregarIntegrante(int idGrupo, string nombreUsuario)
    {
      return new ComandoAgregarIntegrante(idGrupo, nombreUsuario);
    }

    /// <summary>
    /// Retorna la instancia de ComandoEliminarIntegrante
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="nombreUsuario">Nombre del usuario a ser eliminado del grupo</param>
    /// <returns></returns>
    public static ComandoEliminarIntegrante CrearComandoEliminarIntegrante(int idGrupo, string nombreUsuario)
    {
      return new ComandoEliminarIntegrante(idGrupo, nombreUsuario);
    }
  }

}
