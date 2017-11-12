using Npgsql;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using System.Collections.Generic;
using System.Web.Http.Cors;

namespace ApiRest_COCO_TRIP.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M2_PerfilPreferenciasController : ApiController
  {

    protected PeticionPerfil peticion;
    Usuario usuario;
    private int idUsuario;

    /// <summary>
    /// Metodo Post para agregar una preferencia del usuario, hará una llamda a base de datos para buscar id de usuario
    /// y id de categoria para agregarlo en la tabla de preferencias
    /// </summary>
    /// <param name="nombreUsuario">Nombre del usuario</param>
    /// <param name="nombrePreferencia">Nombre de la preferencia a agregar</param>
    /// <returns>Lista de preferencias del usuario</returns>
    // POST api/<controller>/<action>/prefencia
    [HttpPut]
    public List<Categoria> AgregarPreferencias ( int idUsuario , int idCategoria)
    {


      List<Categoria> preferencias;
      peticion = new PeticionPerfil();
      peticion.AgregarPreferencia(idUsuario, idCategoria);
      preferencias = peticion.BuscarPreferencias(idUsuario);
      return preferencias; //Retorna una lista de de categorias

    }

    /// <summary>
    /// Metodo Post que devuelve la lista de preferencias actualizada
    /// </summary>
    /// <param name="nombreUsuario">Nombre del usuario</param>
    /// <param name="nombrePreferencia">Nombre de la categoria a eliminar</param>
    /// <returns>Retorna  una lista de  categorias</returns>
    [HttpPost]
    public List<Categoria> EliminarPreferencias(int idUsuario, int idCategoria)
    {

      List<Categoria> preferencias;
      peticion = new PeticionPerfil();
      peticion.EliminarPreferencia(idUsuario, idCategoria);
      preferencias = peticion.BuscarPreferencias(idUsuario);
      return preferencias; //Retorna una lista de de categorias
    }


    /// <summary>
    /// Devuelve la lista de  preferencias de un usuario
    /// </summary>
    /// <param name="nombreUsuario">Nombre del usuario</param>
    /// <returns>Lista de preferencias</returns>
    [HttpGet]
    public List<Categoria> BuscarPreferencias(int idUsuario)
    {

      List<Categoria> preferencias;
      peticion = new PeticionPerfil();
      preferencias = peticion.BuscarPreferencias( idUsuario );
      return preferencias;

    }

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
    [HttpPost]
    public bool ModificarDatosUsuario(string nombreUsuario, string nombre, string apellido, string fechaDeNacimiento, string genero)
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
          peticion.ModificarDatos(idUsuario, nombre, apellido, fechaDeNacimiento, genero);
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
    [HttpPost]
    public bool CambiarPass(string username, string passwordActual, string passwordNuevo)
    {
      peticion = new PeticionPerfil();
      idUsuario = peticion.ConsultarIdDelUsuario(username);
      string storedPassword = peticion.ObtenerPassword(username);

      if (storedPassword != passwordActual)
      {
        return false;
      }

      try
      {
        if (idUsuario == 1)
        {
          return false;
        }
        else
        {
          peticion.CambiarPassword(idUsuario, passwordNuevo);
          return true;
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
    [HttpPost]
    public bool BorrarUsuario(string username, string password)
    {
      peticion = new PeticionPerfil();
      idUsuario = peticion.ConsultarIdDelUsuario(username);

      string storedPassword = peticion.ObtenerPassword(username);

      if (storedPassword != password)
      {
        return false;
      }

      try
      {
        if (idUsuario == -1)
        {
          return false;
        }
        else
        {
          peticion.BorrarUsuario(idUsuario, password);
          return true;
        }
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// Metodo Post para obtener los datos del usuario.
    /// </summary>
    /// <param name="idUsuario"></param>
    /// <returns>Objeto Usuario</returns>
    [HttpPost]
    public Usuario ObtenerDatosUsuario(int idUsuario)
    {
      peticion = new PeticionPerfil();
      usuario = peticion.ObtenerDatosUsuario(idUsuario);
      return usuario;
    }

  }

}
