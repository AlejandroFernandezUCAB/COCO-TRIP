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
    private int idUsuario;

    [HttpGet]
    public List<Categoria> AgregarPreferencia( )
    {
      return new List<Categoria>();
    }
    /// <summary>
    /// Metodo Post para agregar una preferencia del usuario, hará una llamda a base de datos para buscar id de usuario
    /// y id de categoria para agregarlo en la tabla de preferencias
    /// </summary>
    /// <param name="nombreUsuario">Nombre del usuario</param>
    /// <param name="nombrePreferencia">Nombre de la preferencia a agregar</param>
    /// <returns>Lista de preferencias del usuario</returns>
    // POST api/<controller>/<action>/prefencia
    [HttpPost]
    public List<Categoria> AgregarPreferencias ( string nombreUsuario , string nombrePreferencia)
    {

      Usuario usuario = new Usuario();
      List<Categoria> preferencias = new List<Categoria>();
      Categoria categoria = new Categoria();
      PeticionPerfil peticion;

      usuario.NombreUsuario = nombreUsuario;
      usuario.Preferencias = preferencias;

      //Si todo funciona como debe ser el enviará la lista de preferencias del usuario,
      //en caso contrario retornará Null

      try
      {

        peticion = new PeticionPerfil();
        //Busco el nombre de usuario
        usuario.Id = peticion.ConsultarIdDelUsuario( usuario.NombreUsuario );

        if (usuario.Id == -1) 
        {

          return null; //No hay usuario en la bdd con -1

        }

        else //Se agrega al array de objetos y se busca el id en la BDD.
        {

          categoria.Id = peticion.ConsultarIdDeCategoria(categoria.Nombre);
          usuario.AgregarPreferencia(categoria);
          peticion.AgregarPreferencia(usuario.Id, categoria.Id);
          return usuario.Preferencias;

        }
        

      }
      catch (NpgsqlException e)
      {

        return null;

      }

    }

    // GET api/<controller>/<action>/prefencia
    [HttpGet]
    public string Hola() {

      return "Hola";

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

  }

}
