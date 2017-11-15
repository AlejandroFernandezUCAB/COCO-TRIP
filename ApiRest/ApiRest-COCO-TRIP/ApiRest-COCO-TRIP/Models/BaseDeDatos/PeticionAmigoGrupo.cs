using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using Npgsql;
using System.Data;
using Newtonsoft.Json;
using NpgsqlTypes;
/// <summary>
/// Clase de peticiones del MODULO 3
/// Se encarga de hacer las peticiones a la base de datos
/// </summary>
namespace ApiRest_COCO_TRIP.Models
{
  public class PeticionAmigoGrupo 
  {
    private ConexionBase conexion;
    private NpgsqlDataReader leerDatos;

    public PeticionAmigoGrupo()
    {
      conexion = new ConexionBase();
    }

    private NpgsqlParameter AgregarParametro(NpgsqlDbType tipoDeDato, object valor)
    {
      var parametro = new NpgsqlParameter();

      parametro.NpgsqlDbType = tipoDeDato;
      parametro.Value = valor;

      return parametro;
    }

    /// <summary>
    /// Peticion para insertar un amigo en la base de datos
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <param name="nombreAmigo">Nombre de usuario del amigo a ser agregado</param>
    /// <returns></returns>
    public int AgregarAmigosBD(int idUsuario, string nombreAmigo)
    {
      int respuesta = 0;
      int idAmigo = ObtenerIdUsuario(nombreAmigo);
      try
      {
        conexion.Conectar();
        
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "AgregarAmigo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idAmigo));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          respuesta = leerDatos.GetInt32(0);
        }
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      return respuesta;
    }

    /// <summary>
    /// Consulta el usuario para obtener el nombre (Para el correo)
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <returns>Reotrna el nombre</returns>
    public string ConsultarUsuario(int idUsuario)
    {
      string resultado = "";
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "ConsultarUsuarioSoloId";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          resultado = leerDatos.GetString(2);
        }
        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      return resultado;
    }

    /// <summary>
    /// Metodo que obtiene los datos de un amigo para visualizar el perfil
    /// </summary>
    /// <param name="nombreUsuario">Nombre del usuario amigo</param>
    /// <returns>Los datos del usuario amigo</returns>
    public Usuario VisualizarPerfilAmigoBD(string nombreUsuario)
    {
      Usuario usuario = new Usuario();
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "VisualizarPerfilPublico";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, nombreUsuario));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          usuario.Nombre = leerDatos.GetString(0);
          usuario.Apellido = leerDatos.GetString(1);
          usuario.Correo = leerDatos.GetString(2);
          if (!leerDatos.IsDBNull(3))
          {
            usuario.Foto = leerDatos.GetString(3);
          }
          usuario.NombreUsuario = leerDatos.GetString(4);

          
        }
        else
        {
          usuario = null;
        }

        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return usuario;
    }

    /// <summary>
    /// Metodo que verifica si el usuario es el lider del grupo o solo un miembro
    /// </summary>
    /// <param name="idGrupo">id del grupo a verificar</param>
    /// <param name="idUsuario">id del usuario que desea eliminar / salir deel grupo</param>
    /// <returns></returns>
    public bool VerificarLider(int idGrupo, int idUsuario)
    {
      bool respuesta = false;
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "VerificarLider";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idGrupo));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          respuesta = true;
        }
        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      return respuesta;
    }


    /// <summary>
    /// Metodo para eliminar un integrante del grupo (Salir del grupo)
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="idUsuario">Identificador del usuario que se quiere salir del grupo</param>
    /// <returns></returns>
    public int SalirGrupoBD(int idGrupo, int idUsuario)
    {
      int resultado = 0;
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "SalirDeGrupo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idGrupo));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
            resultado = leerDatos.GetInt32(0);
        }
        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      return resultado;
    }


    /// <summary>
    /// Metodo que obtiene la lista de solicitudes de un usuario
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <returns>Datos de los usuarios que quieren amistad</returns>
    public List<Usuario> ObtenerListaNotificacionesBD(int idUsuario)
    {

      List<Usuario> ListaUsuario = new List<Usuario>();
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "obtenerlistadeNotificaciones";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        leerDatos = conexion.Comando.ExecuteReader();

        while (leerDatos.Read())
        {
          Usuario usuario = new Usuario();
          usuario.Nombre = leerDatos.GetString(0);
          usuario.Apellido = leerDatos.GetString(1);
          usuario.NombreUsuario = leerDatos.GetString(2);
          if (!leerDatos.IsDBNull(3))
          {
            usuario.Foto = leerDatos.GetString(3);
          }
          ListaUsuario.Add(usuario);
        }

        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return ListaUsuario;
    }

    /// <summary>
    /// Metodo que elimina una solicitud de amistad (Rechazar la solicitud)
    /// </summary>
    /// <param name="nombreUsuarioRechazado">Nombre del usuario rechazado</param>
    /// <param name="idUsuario">Identificador del usuario que rechaza</param>
    /// <returns></returns>
    public int RechazarNotificacionBD(string nombreUsuarioRechazado, int idUsuario)
    {
      int result = 0;
      int ideUsuarioRechazado = ObtenerIdUsuario(nombreUsuarioRechazado);
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "RechazarNotificacion";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, ideUsuarioRechazado));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          result = leerDatos.GetInt32(0);
        }
        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return result;
    }

    /// <summary>
    /// Metodo para aceptar una solicitud de amistad 
    /// </summary>
    /// <param name="nombreUsuarioAceptado">Nombre del usuario aceptado</param>
    /// <param name="idUsuario">Identificador del usuario que que acepto la solicitud</param>
    /// <returns></returns>
    public int AceptarNotificacionBD(string nombreUsuarioAceptado, int idUsuario)
    {
      int result = 0;
      int ideUsuarioAceptado = ObtenerIdUsuario(nombreUsuarioAceptado);
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "AceptarNotificacion";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, ideUsuarioAceptado));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          result = leerDatos.GetInt32(0);
        }
        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return result;
    }
    /// <summary>
    /// Metodo que verifica que exista una solicitud o que ya
    /// exista la amistad
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <param name="nombreAmigo">Nombre de usuario del amigo</param>
    /// <returns></returns>
    public int ExisteSolicitud(int idUsuario, string nombreAmigo)
    {
      int respuesta = -1;

      int idAmigo = ObtenerIdUsuario(nombreAmigo);
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "ExisteSolicitud";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idAmigo));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          respuesta = leerDatos.GetInt32(0);
        }
        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      return respuesta;
    }


    /// <summary>
    /// Metodo que se encarga de buscar amigos en la app
    /// </summary>
    /// <param name="dato">nombre o iniciales del amigo</param>
    /// <param name="idUsuario">Identificador del usuario que busca (Para que no aparezca en el buscador)</param>
    /// <returns>Retorna la lista de usuarios que no son tus amigos</returns>
    public List<Usuario> BuscarAmigo(string dato, int idUsuario)
    {
      var listausuarios = new List<Usuario>();
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "BuscarAmigos";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, dato));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));

        leerDatos = conexion.Comando.ExecuteReader();
        while (leerDatos.Read())
        {
          var usuario = new Usuario();
          usuario.Nombre = leerDatos.GetString(0);
          usuario.NombreUsuario = leerDatos.GetString(1);
          if (!leerDatos.IsDBNull(2))
          {
            usuario.Foto = leerDatos.GetString(3);
          }

          listausuarios.Add(usuario);

        }

        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return listausuarios;
    }

    /// <summary>
    /// Metodo que se encarga de devolver la lista de grupos a los que pertenece
    /// el usuario
    /// </summary>
    /// <param name="idUsuario">nombre del usuario por el cual se busca el id</param>
    /// <returns></returns>
    public List<Grupo> Listagrupo(int idUsuario)
    {
      
      var listagrupos = new List<Grupo>();
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "ConsultarListaGrupos";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        leerDatos = conexion.Comando.ExecuteReader();
        while (leerDatos.Read())
        {
          var grupo = new Grupo();
          grupo.Id = leerDatos.GetInt32(0);
          grupo.Nombre = leerDatos.GetString(1);
          if (!leerDatos.IsDBNull(2))
          {
            grupo.Foto = leerDatos.GetString(2);
          }

          listagrupos.Add(grupo);

        }

        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return listagrupos;
    }

    /// <summary>
    /// Metodo que se encarga de pedir los datos de los usuarios integrantes
    /// de un grupo
    /// </summary>
    /// <param name="idgrupo">Identificador del grupo</param>
    /// <returns>La lista de usuarios integrantes</returns>
    public List<Usuario> Listamiembro(int idgrupo)
    {
      
      var listamiembro = new List<Usuario>();
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "VisualizarMiembroGrupo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idgrupo));
        leerDatos = conexion.Comando.ExecuteReader();
        while (leerDatos.Read())
        {
          var usuario = new Usuario();
          usuario.Id = leerDatos.GetInt32(0);
          usuario.Nombre = leerDatos.GetString(1);
          usuario.Apellido= leerDatos.GetString(2);
          usuario.NombreUsuario = leerDatos.GetString(3);
          if (!leerDatos.IsDBNull(4))
          {
            usuario.Foto = leerDatos.GetString(3);
          }

          listamiembro.Add(usuario);

        }

        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return listamiembro;
    }
    /// <summary>
    /// Metodo que se encarga de devolver el perfil del grupo
    /// </summary>
    /// <param name="dato">id del grupo</param>
    /// <returns>Los datos para armar el perfil del grupo</returns>
    public List<Grupo> ConsultarPerfilGrupo(int dato)
    {
      var listagrupo = new List<Grupo>();
  
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "ConsultarPerfilGrupo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, dato));
        leerDatos = conexion.Comando.ExecuteReader();

        while (leerDatos.Read())
        {
          var grupo = new Grupo();
          grupo.Nombre = leerDatos.GetString(0);
          if (!leerDatos.IsDBNull(1))
          {
            grupo.Foto = leerDatos.GetString(1);
          }

          listagrupo.Add(grupo);
        }

        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return listagrupo;
    }

    /// <summary>
    /// Metodo que se encarga de Agregar un grupo con foto
    /// </summary>
    /// <param name="nombre">nombre del grupo</param>
    /// <param name="foto">foto del grupo</param>
    /// <param name="nombreusuario">nombre de usuario del creador del grupo</param>
    /// <returns></returns>

    public int AgregarGrupoBD(String nombre, string foto, int idUsuario)
    {
      int result = 0;
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "AgregarGrupo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, nombre));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, foto));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          result = leerDatos.GetInt32(0);
        }
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      return result;
    }

    /// <summary>
    /// Metodo que se encarga de agregar un grupo sin foto
    /// </summary>
    /// <param name="nombre">nombre del grupo</param>
    /// <param name="nombreusuario">nombre de usuario del creador del grupo</param>
    /// <returns></returns>

    public int AgregarGrupoBD(String nombre, int idUsuario)
    {
      
      int result = 0;
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "AgregarGrupoSinFoto";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, nombre));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          result = leerDatos.GetInt32(0);
        }
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      return result;
    }

    /// <summary>
    /// Procedimiento para eliminar amigos de la
    /// base de datos
    /// </summary>
    /// <param name="nombreAmigo">Recibe el nombre de usuario del amigo</param>
    /// <param name="idUsuario">Recibe el id de usuario del emisor</param>
    /// <returns>Retorna 1 si se elimina, 0 si no se elimina</returns>
    public int EliminarAmigoBD(string nombreAmigo, int idUsuario)
    {
      int result = 0;
      int idAmigo = ObtenerIdUsuario(nombreAmigo);
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "eliminaramigo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idAmigo));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          result = leerDatos.GetInt32(0);
        }
        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return result;
    }

    /// <summary>
    /// Procedimiento para eliminar grupos de la
    /// base de datos
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario lider del grupo</param>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <returns>Retorna 1 si se elimina, 0 si no se elimina</returns>
    public int EliminarGrupoBD(int idUsuario, int idGrupo)
    {
      int result = 0;
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "eliminargrupo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idGrupo));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          result = leerDatos.GetInt32(0);
        }
        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return result;
    }


    /// <summary>
    /// Metodo que se encarga de obtener de la base de datos la lista de
    /// amigos de un usuario
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario</param>
    /// <returns>Retorna la lista de amigos del usuario</returns>
    public List<Usuario> VisualizarListaAmigoBD(int idUsuario)
    {
      
      List<Usuario> ListaUsuario = new List<Usuario>();
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "obtenerlistadeamigos";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        leerDatos = conexion.Comando.ExecuteReader();

        while (leerDatos.Read())
        {
          Usuario usuario = new Usuario();
          usuario.Nombre = leerDatos.GetString(0);
          usuario.Apellido = leerDatos.GetString(1);
          usuario.NombreUsuario = leerDatos.GetString(2);
          if (!leerDatos.IsDBNull(3))
          {
            usuario.Foto = leerDatos.GetString(3);
          }
          ListaUsuario.Add(usuario);
        }

        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return ListaUsuario;
    }

    /// <summary>
    /// Metodo encargado de obtener el id del usuario dado
    /// el nombre de usuario
    /// </summary>
    /// <param name="nombreUsuario">Nombre del usuario</param>
    /// <returns>Retorna el id del usuario</returns>
    public int ObtenerIdUsuario(string nombreUsuario)
    {
      int id = 0;
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "ConseguirIdUsuario";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, nombreUsuario));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          id = leerDatos.GetInt32(0);
        }
        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      return id;
    }

    /// <summary>
    /// Metodo para modificar los atributos de un grupo
    /// </summary>
    /// <param name="nombreGrupo">El nombre del grupo</param>
    /// <param name="idUsuario">Identificador del usuario que modificara el grupo</param>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <returns></returns>
    public int ModificarGrupoBD(string nombreGrupo, int idUsuario, /*string foto,*/ int idGrupo)
    {
      int result = 0;
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "modificarGrupo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, nombreGrupo));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        //conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, foto));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idGrupo));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          result = leerDatos.GetInt32(0);
        }
        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      return result;
    }

    /// <summary>
    /// Metodo para eliminar un integrante de un grupo al modificar
    /// </summary>
    /// <param name="nombreUsuario">Nombre del usuario a eliminar</param>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <returns></returns>
    public int EliminarIntegranteModificarBD(string nombreUsuario, int idGrupo)
    {
      int result = 0;
      int idUsuario = ObtenerIdUsuario(nombreUsuario);
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "eliminarintegrante";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idGrupo));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          result = leerDatos.GetInt32(0);
        }
        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return result;
    }

    /// <summary>
    /// Metodo que se encarga de agregar un integrante al grupo
    /// cuando se va a modificar
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="nombreUsuario">Nombre del usuario a agregar</param>
    /// <returns></returns>
    public int AgregarIntegranteModificarBD(int idGrupo, string nombreUsuario)
    {
      int result = 0;
      int idUsuario = ObtenerIdUsuario(nombreUsuario);
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "agregarIntegrante";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idGrupo));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          result = leerDatos.GetInt32(0);
          
        }
        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      return result;
    }

    /// <summary>
    /// Retorna el usuario lider
    /// </summary>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <param name="idUsuario">identificador del usuario</param>
    /// <returns>retorna los datos del usuario lider</returns>
    public List<Usuario> ObtenerLider(int idGrupo, int idUsuario)
    {

      List<Usuario> ListaUsuario = new List<Usuario>();
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "obtenerLider";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idGrupo));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        leerDatos = conexion.Comando.ExecuteReader();

        while (leerDatos.Read())
        {
          Usuario usuario = new Usuario();
          usuario.Nombre = leerDatos.GetString(0);
          usuario.Apellido = leerDatos.GetString(1);
          usuario.NombreUsuario = leerDatos.GetString(2);
          if (!leerDatos.IsDBNull(3))
          {
            usuario.Foto = leerDatos.GetString(3);
          }
          ListaUsuario.Add(usuario);
        }

        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return ListaUsuario;
    }

    /// <summary>
    /// Metodo para obtener la lista de integrantes sin el lider
    /// </summary>
    /// <param name="idGrupo">identificsdor del grupo</param>
    /// <returns>retorna la lista de usuarios integrantes del grupo sin el usuario lider</returns>
    public List<Usuario> ObtenerSinLider(int idGrupo)
    {

      List<Usuario> ListaUsuario = new List<Usuario>();
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "VisualizarMiembroSinLider";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idGrupo));
        leerDatos = conexion.Comando.ExecuteReader();

        while (leerDatos.Read())
        {
          Usuario usuario = new Usuario();
          usuario.Nombre = leerDatos.GetString(1);
          usuario.Apellido = leerDatos.GetString(2);
          usuario.NombreUsuario = leerDatos.GetString(3);
          if (!leerDatos.IsDBNull(4))
          {
            usuario.Foto = leerDatos.GetString(4);
          }
          ListaUsuario.Add(usuario);
        }

        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return ListaUsuario;
    }

    /// <summary>
    /// Metodo para obtener la lista de usuarios que no estan agregados en un cierto grupo
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario lider del grupo</param>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <returns></returns>
    public List<Usuario> ObtenerMiembrosSinGrupo(int idUsuario, int idGrupo)
    {

      List<Usuario> ListaUsuario = new List<Usuario>();
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "listaAmigosSinGrupo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idGrupo));
        leerDatos = conexion.Comando.ExecuteReader();

        while (leerDatos.Read())
        {
          Usuario usuario = new Usuario();
          usuario.Nombre = leerDatos.GetString(0);
          usuario.Apellido = leerDatos.GetString(1);
          usuario.NombreUsuario = leerDatos.GetString(2);
          if (!leerDatos.IsDBNull(3))
          {
            usuario.Foto = leerDatos.GetString(3);
          }
          ListaUsuario.Add(usuario);
        }

        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return ListaUsuario;
    }

    /// <summary>
    /// Metodo para obtener el ultimo grupo agregado de un usuario
    /// </summary>
    /// <param name="IdUsuario">Identificador del usuario</param>
    /// <returns>Ultimo grupo agregado de un usuario</returns>
public int ObtenerultimoGrupo(int IdUsuario)
    {

      Grupo grupo = new Grupo();
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "Consultarultimo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, IdUsuario));
        leerDatos = conexion.Comando.ExecuteReader();

       if (leerDatos.Read())
        {
          grupo.Id = leerDatos.GetInt32(0);
          grupo.Nombre = leerDatos.GetString(1);
          if (!leerDatos.IsDBNull(2))
          {
            grupo.Foto = leerDatos.GetString(2);
          }
        }

        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return grupo.Id;
    }



  }
}
