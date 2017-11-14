using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using Npgsql;
using System.Data;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Web.Http.Cors;

/// <summary>
/// Clase controladora del MODULO 3
/// Se encarga de generar las peticiones http
/// </summary>
namespace ApiRest_COCO_TRIP.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M3_AmigosGruposController : ApiController
  {
    Usuario usuario;
    PeticionAmigoGrupo peticion;

    

    /// <summary>
    /// Metodo para agregar amigos en la base de datos
    /// </summary>
    /// <param name="idUsuario1">ID del usuario que esta usando la aplicacion y desea agregar un amigo</param>
    /// <param name="idUsuario2">ID del usuario que sera agregado</param>
    /// <returns></returns>
    [HttpGet]
    public HttpStatusCode AgregarAmigo(String idUsuario1, String nombreUsuario2)
    {
      try
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
      return HttpStatusCode.OK;
      
    }

    /// <summary>
    /// Metodo que solicita a la base de datos informacion del usuario que se desea visualizar
    /// </summary>
    /// <param name="nombreUsuario">nombre del usuario que se quiere visualizar perfil</param>
    /// <returns>Retorna los datos del usuario para generar el perfil del amigo</returns>
    [HttpGet]
    public List<Usuario> VisualizarPerfilAmigo(String nombreUsuario)
    {
      List<Usuario> lista = new List<Usuario>();
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
      return lista;
    }

    /// <summary>
    /// Metodo para enviar correo que recomnda la aplicacion
    /// </summary>
    /// <param name="correoElectronico">correo electronico de la persona a la que se le va a recomendar
    ///  la app</param>
    /// <returns></returns>
    ///
    [HttpPut]
    public int EnviarNotificacionCorreo( string nombreUsuarioRecibe, string correoElectronico, string idUsuarioEnvia)
    {
      int respuesta = 0;
      try
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
      return respuesta;

    }

    /// <summary>
    /// Metodo que valida si un usuario es lider o no para eliminar o salir de un grupo
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo a eliminar/salir</param>
    /// <param name="idUsuario">Identificador del usuario que quiere eliminar o salir del grupo</param>
    /// <returns></returns>
    [HttpDelete]
    public int EliminarSalirGrupo(string idGrupo, string idUsuario)
    {

      int respuesta = 0;
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

      return respuesta;
    }

    /// <summary>
    /// Metedo que se encarga de sacar del grupo al usuario, eliminando en la bd el registro de la tabla miembro
    /// </summary>
    /// <param name="idGrupo">ID de bd del grupo del que se deseea salir</param>
    /// <param name="idUsuario">id del usuario que desea salir del grupo</param>
    /// <returns>true si sale exitossamente, false en caso contrario</returns>
    [HttpDelete]
    public int SalirGrupo(string idGrupo, string idUsuario)
    {
      int salio = 0;
      try
      {
        peticion = new PeticionAmigoGrupo();
        salio = peticion.SalirGrupoBD(Convert.ToInt32(idGrupo), Convert.ToInt32(idUsuario));
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (InvalidCastException e)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return salio;

    }

    /// <summary>
    /// Metodo que se encarga de obtener la lista de notificaciones pendientes de un usuario
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <returns>Retorna la lista de notificaciones</returns>
    [HttpGet]
    public List<Usuario> ObtenerListaNotificaciones(string idUsuario)
    {
      List<Usuario> respuesta = null;
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
      return respuesta;
    }

    /// <summary>
    /// Metodo para rechazar la notificacion 
    /// </summary>
    /// <param name="nombreUsuarioRechazado">Nombre del usuario rechazado</param>
    /// <param name="idUsuario">Identificador del usuario que esta rechazando la notificacion</param>
    /// <returns></returns>
  
    [HttpDelete]
    public int RechazarNotificacion(string nombreUsuarioRechazado, string idUsuario)
    {
      int respuesta = 0;
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
      return respuesta;
    }

    /// <summary>
    /// Metodo para aceptar la solicitud de un usuario
    /// </summary>
    /// <param name="nombreUsuarioAceptado">Nombre del usuario aceptado</param>
    /// <param name="idUsuario">Identificador del usuario que acepto la solicitud</param>
    /// <returns></returns>
    [HttpPost]
    public int AceptarNotificacion(string nombreUsuarioAceptado, string idUsuario)
    {
      int respuesta = 0;
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
      return respuesta;
    }

    /// <summary>
    /// Buscar amigo en la aplicacion
    /// </summary>
    /// <param name="nombre">nombre del amigo a buscar</param>
    /// <param name="idUsuario">Identificador del usuario que esta buscando (Para que no aparezca
    /// en la lista del buscador)</param>
    /// <returns></returns>
    [HttpGet]
    public List<Usuario> BuscarAmigo(string nombre, string idUsuario)
    {
      try
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
      }
    }

    /// <summary>
    /// Procedimiento para agregar un grupo
    /// </summary>
    /// <param name="nombre">Nombre del grupo</param>
    /// <param name="foto">Foto del grupo</param>
    /// <param name="idusuario">Lider del grupo(creador)</param>
    /// <returns></returns>
    [HttpPut]
    public int AgregarGrupo(String nombre, String foto, String idusuario)
    {
      try
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
      }

    }


    /// <summary>
    /// Procedimiento que se encarga de recoger los datos de
    /// la base de datos para visualizar la lista de amigos
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <returns>La lista de amigos de un usuario</returns>
    [HttpGet]
    public List<Usuario> VisualizarListaAmigos(int idUsuario)
    {
      List<Usuario> lista;
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
      return lista;
    }

    /// <summary>
    /// Procemiento que se encarga de hacer la peticion para
    /// eliminar un amigo de la base de datos
    /// </summary>
    /// <param name="nombreAmigo">Nombre de usuario del amigo a eliminar</param>
    /// <param name="idUsuario">Identificador del usuario que quiere eliminar</param>
    /// <returns></returns>
    [HttpDelete]
    public int EliminarAmigo(string nombreAmigo, int idUsuario)
    {
      int resultado;
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
      return resultado;
    }

    /// <summary>
    /// Procedimiento que se encarga de hacer la peticion para
    /// eliminar un grupo de la base de datos
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario que desea eliminar el grupo</param>
    /// <param name="idGrupo">Identificador del grupo a ser eliminado</param>
    /// <returns></returns>
    [HttpDelete]
    public int EliminarGrupo(int idUsuario, int idGrupo)
    {
      int resultado;
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
      return resultado;
    }
    /// <summary>
    /// Procedimiento que se encarga de hacer la peticion para
    /// modificar los datos de un grupo
    /// </summary>
    /// <param name="nombreGrupo">Nombre del grupo</param>
    /// <param name="idUsuario">Identificador del usuario que esta modificando</param>
    /// <param name="foto">Foto del grupo</param>
    /// <param name="idGrupo">El identificador del grupo</param>
    /// <returns></returns>
    [HttpPost]
    public int ModificarGrupo(string nombreGrupo, int idUsuario, /*byte foto,*/ int idGrupo)
    {
      int resultado;
      try
      {
        peticion = new PeticionAmigoGrupo();
        resultado = peticion.ModificarGrupoBD(nombreGrupo, idUsuario, /*foto, */idGrupo);
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
    }

    /// <summary>
    /// Consultar lista de grupo del usuario
    /// </summary>
    /// <param name="idUsuario">nombre usuario logeado en la app</param>
    /// <returns>La lista de grupos de un usuario</returns>
    [HttpGet]
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

    }
    /// <summary>
    /// Metodo que devulve los integrantes de un grupo
    /// </summary>
    /// <param name="idgrupo">id del grupo por el cual se devuelven sus integrantes</param>
    /// <returns>Retorna la lista de integrantes de un grupo</returns>
    [HttpGet]
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

    }


    /// <summary>
    /// Procedimiento para visualizar el perfil del grupo
    /// </summary>
    /// <param name="id">Es el de id del grupo por el cual se buscara</param>
    /// <returns>Retorna los datos del grupo para armar el perfil del grupo</returns>
    [HttpGet]
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

    }

    /// <summary>
    /// Procedimiento para agregar un integrante al modificar el grupo
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="nombreUsuario">Nombre del usuario a agregar</param>
    /// <returns></returns>
    [HttpPut]
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
    }

    /// <summary>
    /// Procedimiento para eliminar un integrante del grupo al modificar
    /// </summary>
    /// <param name="nombreUsuario">Nombre del usuario a ser eliminado del grupo</param>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <returns></returns>
    [HttpDelete]
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
    }

    /// <summary>
    /// Metodo que verifica si un usuario es lider de un grupo o solo un integrante
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <returns></returns>
    [HttpGet]
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
    }

    /// <summary>
    /// Metodo para obtener el usuario lider
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <returns>Los datos del usuario lider</returns>
    [HttpGet]
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
    }


    /// <summary>
    /// metodo para obtener la lista de integrantes de un grupo sin el integrante lider
    /// </summary>
    /// <param name="idGrupo">identificador del grupo</param>
    /// <returns>La lista de integrantes sin el lider</returns>
    [HttpGet]
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
    }

    [HttpGet]
    public int ConsultarultimoGrupo()
    {
      try
      {
        peticion = new PeticionAmigoGrupo();
        return peticion.ObtenerultimoGrupo();
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
    }



  }

  }
