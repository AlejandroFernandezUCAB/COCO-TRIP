using Npgsql;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using System.Collections.Generic;

namespace ApiRest_COCO_TRIP.Controllers
{
    public class M2_EditarDatosController : ApiController
    {

      private PeticionPerfil peticion;
      private int idUsuario;

    /// <summary>
    /// Metodo Post para actualizar la informacion del usuario. hará dos llamdas a base de datos, una para buscar id de usuario
    /// y otra para modificar la informacion con los parametros recibidos
    /// </summary>
    /// <param name="idUsuario">Id del usuario </param>
    /// <param name="nombreUsuario">Id de la categoria</param>
    /// <param name="nombre">Id del usuario </param>
    /// <param name="apellido">Id de la categoria</param>
    /// <param name="fechaDeNacimiento">Id del usuario </param>
    /// <param name="genero">Id de la categoria</param>
    /// <returns>bool</returns>
    public bool ModificarDatosUsuario(string nombreUsuario, string nombre, string apellido, string fechaDeNacimiento , string genero )
    {
      peticion = new PeticionPerfil();  
      idUsuario = peticion.ConsultarIdDelUsuario(nombreUsuario);

      try
      {
        if (idUsuario == -1)
        {
          return false;
        }
        else
        {
          peticion.ModificarDatos(idUsuario, nombreUsuario, nombre, apellido, fechaDeNacimiento, genero[0]);
          return true;
        }
      }
      catch (NpgsqlException e)
      {
        return false;
      }

    }

    /// <summary>
    /// Metodo Post para actualizar la contraseña del usuario del usuario. hará dos llamdas a base de datos, una para buscar id de usuario
    /// y otra para modificar la contraseña con los parametros recibidos
    /// </summary>
    /// <param name="idUsuario">Id del usuario </param>
    /// <param name="username">Username del usuario</param>
    /// <param name="password">Contraseña del usuario </param>
    /// <returns>bool</returns>
    public bool CambiarPass(string username, string password)
    {
      peticion = new PeticionPerfil();
      idUsuario = peticion.ConsultarIdDelUsuario(username);
      try
      {
        if (idUsuario == 1)
        {
          peticion.CambiarPassword(idUsuario,password);
          return true;
        }
        else
        {
          return false;
        }
      }
      catch (NpgsqlException e)
      {
        return false;
      }

      
    }

    /// <summary>
    /// Metodo Post para borrar al usuario. hará dos llamdas a base de datos, una para buscar id de usuario
    /// y otra para borrar al usuario con el parametro usado
    /// </summary>
    /// <param name="idUsuario">Id del usuario </param>
    /// <param name="username">Username del usuario</param>
    /// <param name="password">Contraseña del usuario </param>
    /// <returns>bool</returns>

    public bool BorrarUsuario (string username, string password)
    {
      peticion = new PeticionPerfil();
      idUsuario = peticion.ConsultarIdDelUsuario(username);
      try
      {
        if (idUsuario == 1)
        {
          peticion.BorrarUsuario(idUsuario, password);
          return true;
        }
        else
        {
          return false;
        }
      }
      catch
      {
        return false;
      }
    }


  }


}
