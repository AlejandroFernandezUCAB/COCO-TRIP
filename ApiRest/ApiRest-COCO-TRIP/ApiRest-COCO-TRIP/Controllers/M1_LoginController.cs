using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using Npgsql;
using System.Data;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Web.Http.Cors;

namespace ApiRest_COCO_TRIP.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M1_LoginController : ApiController
  {
    private Usuario usuario;
    private PeticionLogin peticion;

    // GET api/<controller>/<action>/id
    [HttpGet]
    public int IniciarSesionCorreo(String datos)
    {
        usuario = JsonConvert.DeserializeObject<Usuario>(datos);
        peticion = new PeticionLogin();
      try
      {
        usuario.Id=peticion.ConsultarUsuarioCorreo(usuario);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      return usuario.Id;
     }
    [HttpGet]
    public int IniciarSesionUsuario(String datos)
    {
        usuario = JsonConvert.DeserializeObject<Usuario>(datos);
        peticion = new PeticionLogin();
      try
      {
        usuario.Id=peticion.ConsultarUsuarioNombre(usuario);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      return usuario.Id;
    }
    [HttpGet]
    public int IniciarSesionSocial(String datos)
    {
      usuario = JsonConvert.DeserializeObject<Usuario>(datos);
      peticion = new PeticionLogin();
      try
      {
        usuario.Id = peticion.ConsultarUsuarioSocial(usuario);
        if(usuario.Id == 0)
          usuario.Id=peticion.InsertarUsuarioFacebook(usuario);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      return usuario.Id;

    }
    [HttpGet]
    public HttpStatusCode CorreoRecuperar(String datos) {
      usuario = JsonConvert.DeserializeObject<Usuario>(datos);
      peticion = new PeticionLogin();
      try
      {
        usuario.Clave = peticion.RecuperarContrasena(usuario);
        if (usuario.Clave.Equals(""))
          throw new HttpResponseException(HttpStatusCode.NoContent);
        MailMessage mail = new MailMessage("cocoSupport@cocotrip.com", usuario.Correo);
        SmtpClient client = new SmtpClient();
        client.Port = 25;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;
        client.Host = "smtp.google.com";
        mail.Subject = "Recuperacion de clave";
        mail.Body = "Hemos recibido una solicitud de recuperacion de clave para tu correo en la aplicacion COCOTRIP. Su clave es : "+usuario.Clave;
        client.Send(mail);

      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      return HttpStatusCode.OK;

    }

  }
}
