using System.Data;
using Npgsql;
using System.Collections.Generic;
using System;

namespace ApiRest_COCO_TRIP.Models
{
  public class PeticionPerfil
  {

    public ConexionBase conexion;

    public PeticionPerfil()
    {

      conexion = new ConexionBase();

    }

    /// <summary>
    /// Metodo para consultar el id del usuario a través de su nombre de usuario
    /// </summary>
    /// <param name="nombreUsuario">Nombre de usuario</param>
    /// <returns>Id del usuario, si retorna -1 es que no existe</returns>
    public int ConsultarIdDelUsuario(string nombreUsuario)
    {
      
      int id;
      id = -1; //Coloco -1 para hacer una comparación más abajo y saber si entró ene l ciclo o no.

      conexion.Conectar();
      ConexionBase con = new ConexionBase();
      NpgsqlCommand comm = new NpgsqlCommand("consultarusuariosolonombre", conexion.SqlConexion);
      comm.CommandType = CommandType.StoredProcedure;
      comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, nombreUsuario);
      NpgsqlDataReader pgread = comm.ExecuteReader();

      while (pgread.Read())
      {

        id = pgread.GetInt32(0);

      }
      conexion.Desconectar();

      return id;

    }

    /// <summary>
    /// Metodo para eliminar la preferencia que haya seleccionado el usuario
    /// </summary>
    /// <param name="idUsuario">Id del usuario</param>
    /// <param name="idCategoria">Id de la categoria</param>
    public void EliminarPreferencia(int idUsuario, int idCategoria)
    {

      NpgsqlCommand command;
      NpgsqlDataReader pgread;

      try
      {

        conexion.Conectar();
        command = new NpgsqlCommand("EliminarPreferencia", conexion.SqlConexion);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idUsuario);
        command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idCategoria);
        pgread = command.ExecuteReader();
        pgread.Read();
        conexion.Desconectar();

      }
      catch (NpgsqlException e)
      {



      }
    }

    /// <summary>
    /// Se agrega la preferencia a la base de datos
    /// </summary>
    /// <param name="idUsuario">Id del usuario </param>
    /// <param name="idCategoria">Id de la categoria</param>
    public void AgregarPreferencia( int idUsuario, int idCategoria)
    {

      NpgsqlCommand command;
      NpgsqlDataReader pgread;

      try
      {

        conexion.Conectar();
        command = new NpgsqlCommand("InsertarPreferencia", conexion.SqlConexion);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idUsuario);
        command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idCategoria);
        pgread = command.ExecuteReader();
        pgread.Read();
        conexion.Desconectar();

      }
      catch (NpgsqlException e)
      {

       

      }

    }

    /// <summary>
    /// Metodoque devuelve la lista de preferencias de un usuario
    /// </summary>
    /// <param name="idUsuario">Id del usuario</param>
    /// <returns>Lista de preferencias del usuario</returns>
    public List<Categoria> BuscarPreferencias(int idUsuario)
    {
      NpgsqlCommand command;
      NpgsqlDataReader pgread;
      Categoria categoria;
      Usuario usuario;

      try
      {
        usuario = new Usuario();
        categoria = new Categoria();
        conexion.Conectar();
        command = new NpgsqlCommand("BuscarPreferencias", conexion.SqlConexion);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idUsuario);
        pgread = command.ExecuteReader();

        while (pgread.Read()) {
          categoria = new Categoria();
          categoria.Id = pgread.GetInt32(0);
          categoria.Nombre = pgread.GetString(1);
          categoria.Descripcion = pgread.GetString(2);
          categoria.Estatus = pgread.GetBoolean(3);
          usuario.AgregarPreferencia( categoria );

        }

        conexion.Desconectar();
        return usuario.Preferencias;

      }
      catch (NpgsqlException e)
      {
        return null;
      }

    }


    /// <summary>
    /// Se modifican los datos del usuario en la base de datos
    /// </summary>
    /// <param name="idUsuario">Id del usuario </param>
    /// <param name="nombreUsuario">Username del usuario</param>
    /// <param name="nombre">Nombre del usuario</param>
    /// <param name="apellido">Apellido del usuario</param>
    /// <param name="fechaDeNacimiento">Fecha de nacimiento del usuario </param>
    /// <param name="genero">Genero del usuario</param>
    public void ModificarDatos(int idUsuario, string nombre, string apellido, string fechaDeNacimiento, string genero)
    {

      NpgsqlCommand command;
      NpgsqlDataReader pgread;
      DateTime convertedDate;
      try
      {
        convertedDate = Convert.ToDateTime(fechaDeNacimiento);
      }
      catch (FormatException e)
      {
        throw e;
      }

      conexion.Conectar();
      command = new NpgsqlCommand("ModificarDatosUsuario", conexion.SqlConexion);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idUsuario);
      command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, nombre);
      command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, apellido);
      command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, convertedDate);
      command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Char, genero[0]);
      pgread = command.ExecuteReader();
      pgread.Read();
      conexion.Desconectar();
    }

    /// <summary>
    /// Se modifica la contraseña del usuario en la base de datos
    /// </summary>
    /// <param name="idUsuario">Id del usuario </param>
    /// <param name="password">Password del usuario </param>
    public void CambiarPassword (int idUsuario, string password)
    {
      NpgsqlCommand command;
      NpgsqlDataReader pgread;

      conexion.Conectar();
      command = new NpgsqlCommand("ModificarPass", conexion.SqlConexion);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idUsuario);
      command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, password);
      pgread = command.ExecuteReader();
      pgread.Read();
      conexion.Desconectar();
    }

    /// <summary>
    /// Se borra al usuario de la base de datos
    /// </summary>
    /// <param name="idUsuario">Id del usuario </param>
    /// <param name="password">Password del usuario </param>
    public void BorrarUsuario (int idUsuario, string password)
    {
      NpgsqlCommand command;
      NpgsqlDataReader pgread;

      conexion.Conectar();
      command = new NpgsqlCommand("BorrarUsuario", conexion.SqlConexion);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idUsuario);
      command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, password);
      pgread = command.ExecuteReader();
      pgread.Read();
      conexion.Desconectar();
    }

    /// <summary>
    /// Se obtiene de la base de datos el password actual del usuario
    /// </summary>
    /// <param name="username"></param>
    public string ObtenerPassword(string username)
    {
      NpgsqlCommand command;
      NpgsqlDataReader pgread;
      string result = "";

      conexion.Conectar();
      command = new NpgsqlCommand("ConsultarContrasena", conexion.SqlConexion);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, username);
      pgread = command.ExecuteReader();
      pgread.Read();
      result = pgread.GetString(0);
      conexion.Desconectar();
      return result;
    }

    /// <summary>
    /// Se obtiene de la base de datos los datos del usuario
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>Obteto Usuario</returns>
    public Usuario ObtenerDatosUsuario(int userId)
    {
      NpgsqlCommand command;
      NpgsqlDataReader pgread;
      Usuario user = new Usuario();

      try
      {
        conexion.Conectar();
        command = new NpgsqlCommand("ConsultarUsuarioSoloId", conexion.SqlConexion);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, userId);
        pgread = command.ExecuteReader();
        pgread.Read();
        user.NombreUsuario = pgread.GetString(0);
        user.Correo = pgread.GetString(1);
        user.Nombre = pgread.GetString(2);
        user.Apellido = pgread.GetString(3);
        user.FechaNacimiento = pgread.GetDateTime(4);
        user.Genero = pgread.GetString(5);
        conexion.Desconectar();
        return user;
      }
      catch (NpgsqlException e)
      {
        return null;
      }

    }

    public List<Categoria> ObtenerCategorias(int idUsuario, string preferencia)
    {
      NpgsqlCommand command;
      NpgsqlDataReader pgread;
      Categoria categoria;
      Usuario usuario;

      try
      {
        usuario = new Usuario();
        categoria = new Categoria();
        conexion.Conectar();
        command = new NpgsqlCommand("BuscarListaPreferenciasPorCategoria", conexion.SqlConexion);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idUsuario);
        command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, preferencia);
        pgread = command.ExecuteReader();

        while (pgread.Read())
        {
          categoria.Id = pgread.GetInt32(0);
          categoria.Nombre = pgread.GetString(1);
          usuario.AgregarPreferencia(categoria);

        }

        conexion.Desconectar();
        return usuario.Preferencias;

      }
      catch (NpgsqlException e)
      {
        return null;
      }
      catch (Exception e)
      {
        return null;
      }
    }

  }
}
