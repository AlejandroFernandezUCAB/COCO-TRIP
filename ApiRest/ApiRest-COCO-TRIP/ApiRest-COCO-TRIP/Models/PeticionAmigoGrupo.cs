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

    public int AgregarAmigosBD(string nobmreUsuario, string nombreAmigo)
    {
      int respuesta = 0;
      int idUsuario = ObtenerIdUsuario(nobmreUsuario);
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
            usuario.Foto[0] = leerDatos.GetByte(3);
          }
          //usuario.Foto[0] = leerDatos.GetByte(3);
        }else
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

    public bool SalirGrupoBD(int idGrupo, string nobmreUsuario)
    {
      bool resultado = false;
      int idUsuario = ObtenerIdUsuario(nobmreUsuario);
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
          
          if (leerDatos.GetInt32(0) == 1)
          {
            resultado = true;
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
      return resultado;
    }

    /// <summary>
    /// Metodo que se encarga de buscar amigos en la app
    /// </summary>
    /// <param name="dato">nombre o iniciales del amigo</param>
    /// <returns></returns>
    public List<Usuario> BuscarAmigo(string dato)
    {
      var listausuarios = new List<Usuario>();
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "BuscarAmigos";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, dato));
        leerDatos = conexion.Comando.ExecuteReader();
        while (leerDatos.Read())
        {
          var usuario = new Usuario();
          usuario.Nombre = leerDatos.GetString(0);
          usuario.NombreUsuario = leerDatos.GetString(1);
          if (!leerDatos.IsDBNull(2))
          {
            usuario.Foto[0] = leerDatos.GetByte(3);
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
    /// <param name="nombreusuario">nombre del usuario por el cual se busca el id</param>
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
          grupo.Nombre = leerDatos.GetString(0);
          if (!leerDatos.IsDBNull(1))
          {
            grupo.Foto[0] = leerDatos.GetByte(1);
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
            usuario.Foto[0] = leerDatos.GetByte(3);
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
    /// <returns></returns>
    public List<Grupo> ConsultarPerfilGrupo(int dato)
    {
      var listagrupo = new List<Grupo>();
      //var grupo = new Grupo();
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
            grupo.Foto[0] = leerDatos.GetByte(3);
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

    public int AgregarGrupoBD(String nombre, byte foto, string nombreusuario)
    {
      int idUsuario = ObtenerIdUsuario(nombreusuario);
      int result = 0;
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "AgregarGrupo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, nombre));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Bytea, foto));
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

    public int AgregarGrupoBD(String nombre, string nombreusuario)
    {
      int idUsuario = ObtenerIdUsuario(nombreusuario);
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
    /// <param name="nombreUsuario">Recibe el nombre de usuario del emisor</param>
    /// <returns>Retorna 1 si se elimina, 0 si no se elimina</returns>
    public int EliminarAmigoBD(string nombreAmigo, string nombreUsuario)
    {
      int result = 0;
      int idAmigo = ObtenerIdUsuario(nombreAmigo);
      int idUsuario = ObtenerIdUsuario(nombreUsuario);
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
    /// <param name="nombreUsuario">Nombre de usuario del lider del grupo</param>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <returns>Retorna 1 si se elimina, 0 si no se elimina</returns>
    public int EliminarGrupoBD(string nombreUsuario, int idGrupo)
    {
      int result = 0;
      int idUsuario = ObtenerIdUsuario(nombreUsuario);
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
    /// <param name="nombreUsuario">Nombre de usuario del emisor</param>
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
            usuario.Foto[0] = leerDatos.GetByte(3);
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
    /// <param name="nombreUsuario">Nombre del usuario que modificara el grupo</param>
    /// <param name="idGrupo">Identificador del grupo</param>
    /// <returns></returns>
    public int ModificarGrupoBD(string nombreGrupo, string nombreUsuario, /*byte foto,*/ int idGrupo)
    {
      int result = 0;
      int idUsuario = ObtenerIdUsuario(nombreUsuario);
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "modificarGrupo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, nombreGrupo));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        //conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Bytea, foto));
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


  }
}
