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

namespace ApiRest_COCO_TRIP.Controllers
{
  public class M3_AmigosGruposController : ApiController
  {

    int cmkdf;

    Usuario usuario;
    PeticionAmigoGrupo peticion;

    // GET api/<controller>/<action>/id
    /// <summary>
    /// Metodo para agregar amigos en la base de datos
    /// </summary>
    /// <param name="idUsuario1">ID del usuario que esta usando la aplicacion y desea agregar un amigo</param>
    /// <param name="idUsuario2">ID del usuario que sera agregado</param>
    /// <returns></returns>
    [HttpGet]
    public string AgregarAmigo(String idUsuario1, String idUsuario2)
    {
      peticion = new PeticionAmigoGrupo();
      peticion.AgregarAmigosBD(Convert.ToInt32(idUsuario1), Convert.ToInt32(idUsuario2));
      return "1";
    }

    [HttpGet]
    public Usuario VisualizarPerfilAmigo(String nombreUsuario)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.VisualizarPerfilAmigoBD(nombreUsuario);
    }

    /// <summary>
    /// Metodo para enviar correo que recomnda la aplicacion
    /// </summary>
    /// <param name="correoElectronico">correo electronico de la persona a la que se le va a recomndar
    ///  la app</param>
    /// <returns></returns>
    [HttpGet]
    public string RecomendarApp(String correoElectronico)
    {
      SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
      var mail = new MailMessage();
      mail.From = new MailAddress("oswaldo7365@hotmail.com");
      mail.To.Add(correoElectronico);
      mail.Subject = "Te abu";
      mail.IsBodyHtml = false;
      mail.Body = "Te amo meme desde mi codigo perfecto :)";
      SmtpServer.Port = 587;
      SmtpServer.UseDefaultCredentials = false;
      SmtpServer.Credentials = new System.Net.NetworkCredential("correo@hotmail.com", "clave");
      SmtpServer.EnableSsl = true;
      SmtpServer.Send(mail);
      return "";
    }

    [HttpDelete]
    public bool SalirGrupo(string idGrupo, string idUsuario)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.SalirGrupoBD(Convert.ToInt32(idGrupo), Convert.ToInt32(idUsuario));

    }


    [HttpGet]
    public Usuario BuscarAmigo(string nombre)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.BuscarAmigo(nombre);

    }



    

    [HttpGet]
    public string AgregarGrupo(String nombre, String foto, String usuario)
    {
      peticion = new PeticionAmigoGrupo();

      if (foto != "null")

      { peticion.AgregarGrupoBD(nombre, Convert.ToByte(foto),Convert.ToInt32(usuario));
      }

      else
      {
        peticion.AgregarGrupoBD(nombre, Convert.ToInt32(usuario));
      }
      return "1";
    }




    
    
    
  }
}
