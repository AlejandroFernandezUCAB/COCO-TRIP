using System.Web.Http;
using System.Web.Http.Cors;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Datos.Entity;

/// <summary>
/// Clase controladora del MODULO 3
/// Se encarga de recibir las peticiones HTTP
/// </summary>
namespace ApiRest_COCO_TRIP.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M3_AmigosGruposController : ApiController
  {
    private Comando comando;

    /// <summary>
    /// Agrega una peticion de amistad
    /// </summary>
    /// <param name="id">ID del usuario que desea agregar un amigo</param>
    /// <param name="nombre">Nombre de usuario que recibira la notificacion</param>
    [HttpPost]
    public void AgregarAmigo(int id, string nombre) //READY
    {
      comando = FabricaComando.CrearComandoAgregarAmigo(id, nombre);
      comando.Ejecutar();
    }
    
    /// <summary>
    /// Metodo que solicita a la base de datos informacion del usuario que se desea visualizar
    /// </summary>
    /// <param name="nombre">Nombre del usuario que se quiere visualizar perfil</param>
    /// <returns>Retorna los datos del usuario para generar el perfil del amigo</returns>
    [HttpGet]
    public Entidad VisualizarPerfilAmigo (string nombre) //READY
    {
      comando = FabricaComando.CrearComandoVisualizarPerfilAmigo(nombre);
      comando.Ejecutar();
      return comando.Retornar();
    }

    /// <summary>
    /// Metodo para enviar un correo recomendando la aplicacion a un usuario
    /// </summary>
    /// <param name="correo">Correo electronico de la persona a la que se le va a recomendar la aplicacion</param>
    /// <param name="id">ID del usuario que envia la notificacion</param>
    /// <param name="nombre">Nombre de usuario al que va destinada la notificacion</param>
    [HttpPost]
    public void EnviarNotificacionCorreo(string correo, int id, string nombre) //READY
    {
      comando = FabricaComando.CrearComandoEnviarNotificacionCorreo(correo, id, nombre);
      comando.Ejecutar();
    }

    /// <summary>
    /// Metodo que valida si un usuario es lider o no para eliminar o salir de un grupo
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo a eliminar/salir</param>
    /// <param name="idUsuario">Identificador del usuario que quiere eliminar o salir del grupo</param>
    /// <returns></returns>
    [HttpDelete]
    public void SalirGrupo (int idGrupo, int idUsuario) //READY
    {
      comando = FabricaComando.CrearComandoSalirGrupo(idGrupo, idUsuario);
      comando.Ejecutar();
    }

    /// <summary>
    /// Metodo que se encarga de obtener la lista de notificaciones pendientes de un usuario
    /// </summary>
    /// <param name="id">Identificador del usuario</param>
    /// <returns>Retorna la lista de notificaciones</returns>
    [HttpGet]
    public List<Entidad> ObtenerListaNotificaciones(int id)
    {
      comando = FabricaComando.CrearComandoObtenerListaNotificaciones(id);
      comando.Ejecutar();
      return comando.RetornarLista();
    }

    /// <summary>
    /// Metodo para rechazar la solicitud de amistad
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario que esta rechazando la notificacion</param>
    /// <param name="nombreUsuarioRechazado">Nombre del usuario rechazado</param>
    /// <returns></returns>

    [HttpDelete]
    public void RechazarNotificacion (int idUsuario, string nombreUsuarioRechazado) //READY
    {
      comando = FabricaComando.CrearComandoRechazarNotificacion(idUsuario, nombreUsuarioRechazado);
      comando.Ejecutar();
    }

    /// <summary>
    /// Metodo para aceptar la solicitud de amistad de un usuario
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario que acepto la solicitud</param>
    /// <param name="nombreUsuarioAceptado">Nombre del usuario aceptado</param>
    /// <returns></returns>
    [HttpPut]
    public void AceptarNotificacion (int idUsuario, string nombreUsuarioAceptado)
    {
      comando = FabricaComando.CrearComandoAceptarNotificacion(idUsuario, nombreUsuarioAceptado);
      comando.Ejecutar();
    }

    /// <summary>
    /// Buscar amigo en la aplicacion
    /// </summary>
    /// <param name="id">Identificador del usuario que esta buscando (Para que no aparezca en la lista del buscador)</param>
    /// <param name="nombre">Nombre del amigo a buscar</param>
    /// <returns></returns>
    [HttpGet]
    public List<Entidad> BuscarAmigos (int id, string nombre)
    {
      comando = FabricaComando.CrearComandoBuscarAmigos(id, nombre);
      comando.Ejecutar();
      return comando.RetornarLista();
    }

    /// <summary>
    /// Procedimiento para agregar un grupo
    /// </summary>
    /// <param name="grupo">Datos del grupo a agregar</param>
    /// <returns></returns>
    [HttpPost]
    public void AgregarGrupo (Grupo grupo)  /*String nombre, String foto, String idusuario*/ //READY
    {
      comando = FabricaComando.CrearComandoAgregarGrupo(grupo);
      comando.Ejecutar();
    }

    /// <summary>
    /// Procedimiento que se encarga de recoger los datos de
    /// la base de datos para visualizar la lista de amigos
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <returns>La lista de amigos de un usuario</returns>
    [HttpGet]
    public List<Entidad> VisualizarListaAmigos(int idUsuario) //READY
    {
      comando = FabricaComando.CrearComandoVisualizarListaAmigos(idUsuario);
      comando.Ejecutar();
      return comando.RetornarLista();
    }

    /// <summary>
    /// Procemiento que se encarga de hacer la peticion para
    /// eliminar un amigo de la base de datos
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario que quiere eliminar</param>
    /// <param name="nombreAmigo">Nombre de usuario del amigo a eliminar</param>
    /// <returns></returns>
    [HttpDelete]
    public void EliminarAmigo(int idUsuario, string nombreAmigo) //READY
    {
      comando = FabricaComando.CrearComandoEliminarAmigo(idUsuario, nombreAmigo);
      comando.Ejecutar();
    }

    /// <summary>
    /// Procedimiento que se encarga de hacer la peticion para
    /// eliminar un grupo de la base de datos
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario que desea eliminar el grupo</param>
    /// <param name="idGrupo">Identificador del grupo a ser eliminado</param>
    /// <returns></returns>
    [HttpDelete]
    public void EliminarGrupo(int idUsuario, int idGrupo)
    {
      comando = FabricaComando.CrearComandoEliminarGrupo(idUsuario, idGrupo);
      comando.Ejecutar();
    }

    /// <summary>
    /// Procedimiento que se encarga de hacer la peticion para
    /// modificar los datos de un grupo
    /// </summary>
    /// <param name="grupo">Datos del grupo</param>
    /// <param name="id">Identificador del usuario que modifica los datos del grupo</param>
    /// <returns></returns>
    [HttpPut]
    public void ModificarGrupo (Grupo grupo, int id) //(string nombreGrupo, int idUsuario, /*byte foto,*/ int idGrupo)
    {
      comando = FabricaComando.CrearComandoModificarGrupo(grupo, id);
      comando.Ejecutar();
    }

    /// <summary>
    /// Consultar lista de grupo del usuario
    /// </summary>
    /// <param name="idUsuario">ID del usuario logeado en la aplicacion</param>
    /// <returns>La lista de grupos de un usuario</returns>
    [HttpGet]
    public List<Entidad> ConsultarListaGrupos(int idUsuario)
    {
      comando = FabricaComando.CrearComandoConsultarListaGrupos(idUsuario);
      comando.Ejecutar();
      return comando.RetornarLista();
    }

    /// <summary>
    /// Metodo que devuelve los integrantes de un grupo
    /// </summary>
    /// <param name="idGrupo">ID del grupo por el cual se devuelven sus integrantes</param>
    /// <returns>Retorna la lista de integrantes de un grupo</returns>
    [HttpGet]
    public List<Entidad> ConsultarMiembroGrupo(int idGrupo)
    {
      comando = FabricaComando.CrearComandoConsultarMiembroGrupo(idGrupo);
      comando.Ejecutar();
      return comando.RetornarLista();
    }

    /// <summary>
    /// Procedimiento para visualizar el perfil del grupo
    /// </summary>
    /// <param name="id">ID del grupo a buscar</param>
    /// <returns>Retorna los datos del grupo para armar el perfil del grupo</returns>
    [HttpGet]
    public Entidad ConsultarPerfilGrupo (int id)
    {
      comando = FabricaComando.CrearComandoConsultarPerfilGrupo(id);
      comando.Ejecutar();
      return comando.Retornar();
    }

    /// <summary>
    /// Procedimiento para agregar un integrante al modificar el grupo
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="nombreUsuario">Nombre del usuario a agregar</param>
    /// <returns></returns>
    [HttpPost]
    public void AgregarIntegrante (int idGrupo, string nombreUsuario)
    {
      comando = FabricaComando.CrearComandoAgregarIntegrante(idGrupo, nombreUsuario);
      comando.Ejecutar();
    }

    /// <summary>
    /// Procedimiento para eliminar un integrante del grupo al modificar
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="nombreUsuario">Nombre del usuario a ser eliminado del grupo</param>
    /// <returns></returns>
    [HttpDelete]
    public void EliminarIntegrante (int idGrupo, string nombreUsuario)
    {
      comando = FabricaComando.CrearComandoEliminarIntegrante(idGrupo, nombreUsuario);
      comando.Ejecutar();
    }

    /// <summary>
    /// Metodo que verifica si un usuario es lider de un grupo o solo un integrante. Si no es lider retorna una excepcion
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <returns></returns>
    [HttpGet]
    public void VerificarLider(int idGrupo, int idUsuario)
    {
      comando = FabricaComando.CrearComandoVerificarLider(idGrupo, idUsuario);
      comando.Ejecutar();
    }

    /// <summary>
    /// Metodo para obtener el usuario lider
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <returns>Los datos del usuario lider</returns>
    [HttpGet]
    public Entidad ConsultarLider(int idGrupo)
    {
      comando = FabricaComando.CrearComandoConsultarLider(idGrupo);
      comando.Ejecutar();
      return comando.Retornar();
    }

    /// <summary>
    /// Metodo que devuelve los integrantes de un grupo sin el integrante lider
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <returns>La lista de integrantes sin el lider</returns>
    [HttpGet]
    public List<Entidad> ConsultarMiembroSinLider(int idGrupo)
    {
      comando = FabricaComando.CrearComandoConsultarMiembroSinLider(idGrupo);
      comando.Ejecutar();
      return comando.RetornarLista();
    }

    /// <summary>
    /// Metodo para obtener la lista de amigos que no estan agregados al grupo
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="idUsuario">Identificador del usuario lider</param>
    /// <returns>La lista de usuarios que no estan agregados en el grupo</returns>
    [HttpGet]
    public List<Entidad> ConsultarMiembroSinGrupo(int idGrupo, int idUsuario)
    {
      comando = FabricaComando.CrearComandoConsultarMiembroSinGrupo(idGrupo, idUsuario);
      comando.Ejecutar();
      return comando.RetornarLista();
    }

    /// <summary>
    /// Metodo para obtener el identificador del ultimo grupo agregado de un usuario
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <returns>Ultimo grupo agregado de un usuario</returns>
    [HttpGet]
    public Entidad ConsultarUltimoGrupo (int idUsuario)
    {
      comando = FabricaComando.CrearComandoConsultarUltimoGrupo(idUsuario);
      comando.Ejecutar();
      return comando.Retornar();
    }
  }
}
