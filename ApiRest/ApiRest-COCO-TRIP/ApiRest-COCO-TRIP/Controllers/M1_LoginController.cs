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
    [HttpPost]
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
    [HttpPost]
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
    [HttpPost]
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
    [HttpPost]
    public int RegistrarUsuario(String datos)
    {
      usuario = JsonConvert.DeserializeObject<Usuario>(datos);
      peticion = new PeticionLogin();
      try
      {
        usuario.Id = peticion.ConsultarUsuarioSocial(usuario);
        if (usuario.Id == 0)
        {
          usuario.Id = peticion.ConsultarUsuarioSoloNombre(usuario);
          if (usuario.Id == 0)
          { usuario.Id = peticion.InsertarUsuario(usuario); }
          else
          { usuario.Id = -2; }
        }
        else
        { usuario.Id = -1; }
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
    [HttpPost]
    public HttpStatusCode CorreoRecuperar(String datos) {
      usuario = JsonConvert.DeserializeObject<Usuario>(datos);
      peticion = new PeticionLogin();
      try
      {
        usuario.Clave = peticion.RecuperarContrasena(usuario);
        if (usuario.Clave.Equals(""))
          throw new HttpResponseException(HttpStatusCode.NoContent);
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

        mail.From = new MailAddress("cocotrip17@gmail.com");
        mail.To.Add(usuario.Correo);
        mail.Subject = "Recuperar contrasena";
        mail.Body = "Querido Usuario, hemos recibido una solicitud para recuperar la contrasena de tu cuenta en cocotrip, esta es: "+usuario.Clave;

        SmtpServer.Port = 587;
        SmtpServer.Credentials = new System.Net.NetworkCredential("cocotrip17", "arepascocotrip");
        SmtpServer.EnableSsl = true;

        SmtpServer.Send(mail);

      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return HttpStatusCode.OK;

    }

  }
}
