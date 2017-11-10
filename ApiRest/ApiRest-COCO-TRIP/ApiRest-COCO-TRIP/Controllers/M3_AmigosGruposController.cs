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
    public string AgregarAmigo(String nombreUsuario1, String nombreUsuario2)
    {
      peticion = new PeticionAmigoGrupo();
      int result = 0;
      result = peticion.AgregarAmigosBD(nombreUsuario1, nombreUsuario2);//acomodar
      return Convert.ToString(result);
    }

    /// <summary>
    /// Metodo que solicita a la base de datos informacion del usuario que se desea visualizar
    /// </summary>
    /// <param name="nombreUsuario"></param>
    /// <returns></returns>
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
      SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
      var mail = new MailMessage();
      mail.From = new MailAddress("cocotrip17@gmail.com");
      mail.To.Add(correoElectronico);
      mail.Subject = "Hola aquiles, te estamos esperando";
      mail.IsBodyHtml = false;
      mail.Body = "Hola, aquiles eres una perra";
      SmtpServer.Port = 587;
      SmtpServer.UseDefaultCredentials = false;
      SmtpServer.Credentials = new System.Net.NetworkCredential("cocotrip17@gmail.com", "arepascocotrip");
      SmtpServer.EnableSsl = true;
      SmtpServer.Send(mail);
      return "";
    }

    [HttpDelete]
    public bool SalirGrupo(string idGrupo, string nombreUsuario)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.SalirGrupoBD(Convert.ToInt32(idGrupo), nombreUsuario);

    }



    /// <summary>
    /// Buscar amigo en la aplicacion
    /// </summary>
    /// <param name="nombre">nombre del amigo a buscar</param>
    /// <returns></returns>
    [HttpGet]
    public List<Usuario> BuscarAmigo(string nombre)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.BuscarAmigo(nombre);

    }

    /// <summary>
    /// Procedimiento para agregar un grupo
    /// </summary>
    /// <param name="nombre">Nombre del grupo</param>
    /// <param name="foto">Foto del grupo</param>
    /// <param name="usuario">Lider del grupo(creador)</param>
    /// <returns></returns>
    [HttpGet]
    public int AgregarGrupo(String nombre, String foto, String nombreusuario)
    {
      peticion = new PeticionAmigoGrupo();

      if (foto != "null")

      { return peticion.AgregarGrupoBD(nombre, Convert.ToByte(foto), nombreusuario);
      }

      else
      {
        return peticion.AgregarGrupoBD(nombre, nombreusuario);
      }
      
    }

    
    /// <summary>
    /// Procedimiento que se encarga de recoger los datos de
    /// la base de datos para visualizar la lista de amigos
    /// </summary>
    /// <param name="nombreUsuario"></param>
    /// <returns></returns>
    [HttpGet]
    public List<Usuario> VisualizarListaAmigos(string nombreUsuario)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.VisualizarListaAmigoBD(nombreUsuario);
    }

    /// <summary>
    /// Procemiento que se encarga de hacer la peticion para
    /// eliminar un amigo de la base de datos
    /// </summary>
    /// <param name="nombreAmigo"></param>
    /// <param name="nombreUsuario"></param>
    /// <returns></returns>
    [HttpGet]
    public int EliminarAmigo(string nombreAmigo, string nombreUsuario)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.EliminarAmigoBD(nombreAmigo, nombreUsuario);
    }

    /// <summary>
    /// Procedimiento que se encarga de hacer la peticion para
    /// eliminar un grupo de la base de datos
    /// </summary>
    /// <param name="nombreUsuario"></param>
    /// <param name="idGrupo"></param>
    /// <returns></returns>
    [HttpGet]
    public int EliminarGrupo(string nombreUsuario, int idGrupo)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.EliminarGrupoBD(nombreUsuario, idGrupo);
    }
    /// <summary>
    /// Procedimiento que se encarga de hacer la peticion para
    /// modificar los datos de un grupo
    /// </summary>
    /// <param name="nombreGrupo">Nombre del grupo</param>
    /// <param name="nombreUsuario">Nombre del usuario que esta modificando</param>
    /// <param name="foto">Foto del grupo</param>
    /// <param name="idGrupo">El identificador del grupo</param>
    /// <returns></returns>
    [HttpGet]
    public int ModificarGrupo(string nombreGrupo, string nombreUsuario, /*byte foto,*/ int idGrupo)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.ModificarGrupoBD(nombreGrupo,nombreUsuario, /*foto, */idGrupo);
    }

    /// <summary>
    /// Consultar lista de grupo del usuario
    /// </summary>
    /// <param name="id">id del usuario</param>
    /// <returns></returns>
    [HttpGet]
    public List<Grupo> ConsultarListaGrupos(string id)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.Listagrupo(Convert.ToInt32(id));

    }
    

    /// <summary>
    /// Procedimiento para visualizar el perfil del grupo
    /// </summary>
    /// <param name="id">Es el de id del grupo por el cual se buscara</param>
    /// <returns></returns>
    [HttpGet]
    public Grupo ConsultarPerfilGrupos(string id)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.ConsultarPerfilGrupo(Convert.ToInt32(id));

    }

    /// <summary>
    /// Procedimiento para agregar un integrante al modificar el grupo
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="nombreUsuario">Nombre del usuario a agregar</param>
    /// <returns></returns>
    [HttpGet]
    public int AgregarIntegranteModificar(int idGrupo, string nombreUsuario)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.AgregarIntegranteModificarBD(idGrupo, nombreUsuario);
    }

    /// <summary>
    /// Procedimiento para eliminar un integrante del grupo al modificar
    /// </summary>
    /// <param name="nombreUsuario"></param>
    /// <param name="idGrupo"></param>
    /// <returns></returns>
    [HttpGet]
    public int EliminarIntegranteModificar(string nombreUsuario, int idGrupo)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.EliminarIntegranteModificarBD(nombreUsuario, idGrupo);
    }

  }


}
