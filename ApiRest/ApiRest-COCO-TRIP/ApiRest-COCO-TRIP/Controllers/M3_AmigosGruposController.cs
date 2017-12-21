using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Npgsql;
using System.Data;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Web.Http.Cors;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using ApiRest_COCO_TRIP.Negocio.Comando;
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
    /// <param name="usuario">Nombre de usuario que recibira la notificacion</param>
    [HttpPost]
    public void AgregarAmigo(int id, string usuario) //READY
    {
      comando = FabricaComando.CrearComandoAgregarAmigo(id, usuario);
      comando.Ejecutar();

      /*try
      {

        peticion = new PeticionAmigoGrupo();
        if (peticion.ExisteSolicitud( Convert.ToInt32(idUsuario1), nombreUsuario2) == -1 )
        {
          peticion.AgregarAmigosBD(Convert.ToInt32(idUsuario1), nombreUsuario2);
        }
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return HttpStatusCode.OK;*/
      
    }
    
    /// <summary>
    /// Metodo que solicita a la base de datos informacion del usuario que se desea visualizar
    /// </summary>
    /// <param name="usuario">Nombre del usuario que se quiere visualizar perfil</param>
    /// <returns>Retorna los datos del usuario para generar el perfil del amigo</returns>
    [HttpGet]
    public Entidad VisualizarPerfilAmigo (string usuario) //READY
    {
      comando = FabricaComando.CrearComandoVisualizarPerfilAmigo(usuario);
      comando.Ejecutar();
      return comando.Retornar();

      /*List<Usuario> lista = new List<Usuario>();
      Usuario usuario;
      try
      {
        peticion = new PeticionAmigoGrupo();
        usuario = peticion.VisualizarPerfilAmigoBD(nombreUsuario);
        lista.Add(usuario);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return lista;*/
    }

    /// <summary>
    /// Metodo para enviar un correo recomendando la aplicacion a un usuario
    /// </summary>
    /// <param name="correo">Correo electronico de la persona a la que se le va a recomendar la aplicacion</param>
    /// <param name="id">ID del usuario que envia la notificacion</param>
    /// <param name="nombreDestino">Nombre de usuario al que va destinada la notificacion</param>
    [HttpPost]
    public void EnviarNotificacionCorreo(string correo, int id, string nombreDestino) //READY
    {
      comando = FabricaComando.CrearComandoEnviarNotificacionCorreo(correo, id, nombreDestino);
      comando.Ejecutar();

      /*try
      {
        peticion = new PeticionAmigoGrupo();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        var mail = new MailMessage();
        mail.From = new MailAddress("cocotrip17@gmail.com");
        mail.To.Add(correoElectronico);
        mail.Subject = "Hola "+ nombreUsuarioRecibe + " alguien quiere agregarte como amigo!";
        mail.IsBodyHtml = false;
        mail.Body = peticion.ConsultarUsuario(Convert.ToInt32(idUsuarioEnvia)) +
          " desea agregarte como amigo, puedes aceptarl@ desde la app de COCO-Trip ";
        SmtpServer.Port = 587;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Credentials = new System.Net.NetworkCredential("cocotrip17", "arepascocotrip");
        SmtpServer.EnableSsl = true;
        SmtpServer.Send(mail);
        respuesta = 1;
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return respuesta;*/
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

      /*int respuesta = 0;
      try
      {
        peticion = new PeticionAmigoGrupo();
        if (peticion.VerificarLider(Convert.ToInt32(idGrupo), Convert.ToInt32(idUsuario)))
        {
          respuesta = 1;
          respuesta = peticion.EliminarGrupoBD(Convert.ToInt32(idUsuario), Convert.ToInt32(idGrupo));
        }
        else
        {
          respuesta = 2;
          respuesta = peticion.SalirGrupoBD(Convert.ToInt32(idGrupo), Convert.ToInt32(idUsuario));
        }
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }

      return respuesta;*/
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

      /*List<Usuario> respuesta = null;
      peticion = new PeticionAmigoGrupo();
      try
      {
        respuesta = peticion.ObtenerListaNotificacionesBD(Convert.ToInt32(idUsuario));
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return respuesta;*/
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

     /* int respuesta = 0;
      peticion = new PeticionAmigoGrupo();

      try
      {
        respuesta = peticion.RechazarNotificacionBD(nombreUsuarioRechazado, Convert.ToInt32(idUsuario));
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return respuesta;*/
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

      /*int respuesta = 0;
      peticion = new PeticionAmigoGrupo();

      try
      {
        respuesta = peticion.AceptarNotificacionBD(nombreUsuarioAceptado, Convert.ToInt32(idUsuario));
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return respuesta;*/
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

      /*try
      {
        peticion = new PeticionAmigoGrupo();
        return peticion.BuscarAmigo(nombre, Convert.ToInt32(idUsuario));
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }*/
    }

    /// <summary>
    /// Procedimiento para agregar un grupo
    /// </summary>
    /// <param name="grupo">Datos del grupo a agregar</param>
    /// <returns></returns>
    [HttpPost]
    public void AgregarGrupo (Entidad grupo)  /*String nombre, String foto, String idusuario*/ //READY
    {
      comando = FabricaComando.CrearComandoAgregarGrupo(grupo);
      comando.Ejecutar();

      /*try
      {
        peticion = new PeticionAmigoGrupo();

        if (foto != "")

        {
          return peticion.AgregarGrupoBD(nombre, foto, Convert.ToInt32(idusuario));
        }

        else
        {
          return peticion.AgregarGrupoBD(nombre, Convert.ToInt32(idusuario));
        }
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }*/

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

      /*List<Usuario> lista;
      try
      {
        peticion = new PeticionAmigoGrupo();
        lista = peticion.VisualizarListaAmigoBD(idUsuario);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return lista;*/
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

      /*int resultado;
      try
      {
        peticion = new PeticionAmigoGrupo();
        resultado = peticion.EliminarAmigoBD(nombreAmigo, idUsuario);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return resultado;*/
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

      /*int resultado;
      try
      {
        peticion = new PeticionAmigoGrupo();
        resultado = peticion.EliminarGrupoBD(idUsuario, idGrupo);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return resultado;*/
    }

    /// <summary>
    /// Procedimiento que se encarga de hacer la peticion para
    /// modificar los datos de un grupo
    /// </summary>
    /// <param name="grupo">Datos del grupo</param>
    /// <returns></returns>
    [HttpPost]
    public void ModificarGrupo (Entidad grupo) //(string nombreGrupo, int idUsuario, /*byte foto,*/ int idGrupo)
    {


      //int resultado;
      //try
      //{
      //  peticion = new PeticionAmigoGrupo();
      //  resultado = peticion.ModificarGrupoBD(nombreGrupo, idUsuario, /*foto, */idGrupo);
      //}
      /*catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return resultado;*/
    }

    /// <summary>
    /// Consultar lista de grupo del usuario
    /// </summary>
    /// <param name="idUsuario">nombre usuario logeado en la app</param>
    /// <returns>La lista de grupos de un usuario</returns>
    /*[HttpGet]
    public List<Grupo> ConsultarListaGrupos(int idUsuario)
    {
      try
      {
        peticion = new PeticionAmigoGrupo();
        return peticion.Listagrupo(idUsuario);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }

    }*/

    /// <summary>
    /// Metodo que devulve los integrantes de un grupo
    /// </summary>
    /// <param name="idgrupo">id del grupo por el cual se devuelven sus integrantes</param>
    /// <returns>Retorna la lista de integrantes de un grupo</returns>
    /*[HttpGet]
    public List<Usuario> ConsultarMiembroGrupo(string idgrupo)
    {
      try
      {
        peticion = new PeticionAmigoGrupo();
        return peticion.Listamiembro(Convert.ToInt32(idgrupo));
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }

    }*/


    /// <summary>
    /// Procedimiento para visualizar el perfil del grupo
    /// </summary>
    /// <param name="id">Es el de id del grupo por el cual se buscara</param>
    /// <returns>Retorna los datos del grupo para armar el perfil del grupo</returns>
    /*[HttpGet]
    public List<Grupo> ConsultarPerfilGrupos(int id)
    {

      try
      {
        peticion = new PeticionAmigoGrupo();
        return peticion.ConsultarPerfilGrupo(id);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }

    }*/

    /// <summary>
    /// Procedimiento para agregar un integrante al modificar el grupo
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="nombreUsuario">Nombre del usuario a agregar</param>
    /// <returns></returns>
    /*[HttpPut]
    public int AgregarIntegranteModificar(int idGrupo, string nombreUsuario)
    {
      int resultado;
      try
      {
        peticion = new PeticionAmigoGrupo();
        resultado = peticion.AgregarIntegranteModificarBD(idGrupo, nombreUsuario);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return resultado;
    }*/

    /// <summary>
    /// Procedimiento para eliminar un integrante del grupo al modificar
    /// </summary>
    /// <param name="nombreUsuario">Nombre del usuario a ser eliminado del grupo</param>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <returns></returns>
    /*[HttpDelete]
    public int EliminarIntegranteModificar(string nombreUsuario, int idGrupo)
    {
      int resultado;
      try
      {
        peticion = new PeticionAmigoGrupo();
        resultado = peticion.EliminarIntegranteModificarBD(nombreUsuario, idGrupo);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return resultado;
    }*/

    /// <summary>
    /// Metodo que verifica si un usuario es lider de un grupo o solo un integrante
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <returns></returns>
    /*[HttpGet]
    public int VerificarLider(int idGrupo, int idUsuario)
    {
      bool respuesta = false;
      int resultado = 0;
      peticion = new PeticionAmigoGrupo();
      try
      {
        respuesta = peticion.VerificarLider(idGrupo, idUsuario);
        if (respuesta == true)
        {
          resultado = 1;
        }

      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return resultado;
    }*/

    /// <summary>
    /// Metodo para obtener el usuario lider
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <returns>Los datos del usuario lider</returns>
    /*[HttpGet]
    public List<Usuario> ConsultarLider(int idGrupo, int idUsuario)
    {
      try
      {
        peticion = new PeticionAmigoGrupo();
        return peticion.ObtenerLider(idGrupo, idUsuario);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
    }*/


    /// <summary>
    /// metodo para obtener la lista de integrantes de un grupo sin el integrante lider
    /// </summary>
    /// <param name="idGrupo">identificador del grupo</param>
    /// <returns>La lista de integrantes sin el lider</returns>
    /*[HttpGet]
    public List<Usuario> ConsultarMiembrosSinLider(int idGrupo)
    {
      try
      {
        peticion = new PeticionAmigoGrupo();
        return peticion.ObtenerSinLider(idGrupo);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
    }*/

    /// <summary>
    /// Metodo para obtener la lista de amigos que no estan agregados al grupo
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario lider</param>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <returns>La lista de usuarios que no estan agregados en el grupo</returns>
    /*[HttpGet]
    public List<Usuario> ConsultarMiembrosSinGrupo(int idUsuario, int idGrupo)
    {
      try
      {
        peticion = new PeticionAmigoGrupo();
        return peticion.ObtenerMiembrosSinGrupo(idUsuario, idGrupo);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
    }*/

    /// <summary>
    /// Metodo para obtener el identificador del ultimo grupo agregado de un usuario
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <returns>Ultimo grupo agregado de un usuario</returns>
    /*[HttpGet]
    public int ConsultarultimoGrupo(int idUsuario)
    {
      try
      {
        peticion = new PeticionAmigoGrupo();
        return peticion.ObtenerultimoGrupo(idUsuario);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
    }*/


  }
}
