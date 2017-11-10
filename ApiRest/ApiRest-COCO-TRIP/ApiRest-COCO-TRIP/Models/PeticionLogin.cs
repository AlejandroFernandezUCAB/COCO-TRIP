using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Models
{
  public class PeticionLogin
  {
    private ConexionBase conexion;
    private NpgsqlDataReader leerDatos;

    public PeticionLogin()
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

    public int ConsultarUsuarioCorreo(Usuario usuario)
    {
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "ConsultarUsuarioCorreo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;

        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Correo));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Clave));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          usuario.Id = leerDatos.GetInt32(0);

        }
        else {
          usuario.Id = 0;
        }

        leerDatos.Close();
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        conexion.Desconectar();
        throw e;
      }
      catch (FormatException e)
      {
        conexion.Desconectar();
        throw e;
      }
      return usuario.Id;
    }

    public int ConsultarUsuarioNombre(Usuario usuario)
    {
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "ConsultarUsuarioNombre";
        conexion.Comando.CommandType = CommandType.StoredProcedure;

        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.NombreUsuario));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Clave));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          usuario.Id = leerDatos.GetInt32(0);

        }
        else
        {
          usuario.Id = 0;
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
      return usuario.Id;
    }

    public int ConsultarUsuarioSocial(Usuario usuario)
    {
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "ConsultarUsuarioSocial";
        conexion.Comando.CommandType = CommandType.StoredProcedure;

        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Correo));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          usuario.Id = leerDatos.GetInt32(0);
        }
        else
        {
          usuario.Id = 0;
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
      return usuario.Id;
    }

    public int InsertarUsuario(Usuario usuario)
    {
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "InsertarUsuario";
        conexion.Comando.CommandType = CommandType.StoredProcedure;

        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.NombreUsuario));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Nombre));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Apellido));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Date, usuario.FechaNacimiento));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Genero));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Correo));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Clave));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Bytea, usuario.Foto));

        leerDatos = conexion.Comando.ExecuteReader();

        if (leerDatos.Read())
        {
          usuario.Id = leerDatos.GetInt32(0);
          leerDatos.Close();
        }

        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        conexion.Desconectar();
        throw e;
      }
      catch (InvalidCastException e)
      {
        conexion.Desconectar();
        throw e;
      }
      return usuario.Id;
    }

    public int InsertarUsuarioFacebook(Usuario usuario)
    {
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "InsertarUsuarioFacebook";
        conexion.Comando.CommandType = CommandType.StoredProcedure;

        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Nombre));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Apellido));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Date, usuario.FechaNacimiento));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Correo));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Bytea, usuario.Foto));

        leerDatos = conexion.Comando.ExecuteReader();

        if (leerDatos.Read())
        {
          usuario.Id = leerDatos.GetInt32(0);
          leerDatos.Close();
        }

        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (InvalidCastException e)
      {
        throw e;
      }
      return usuario.Id;
    }

    public string RecuperarContrasena(Usuario usuario)
    {
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "RecuperarContrasena";
        conexion.Comando.CommandType = CommandType.StoredProcedure;

        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Correo));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          usuario.Clave = leerDatos.GetString(0);

        }
        else
        {
          usuario.Clave = "";
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
      return usuario.Clave;
    }

    public void ValidarUsuario(Usuario usuario)
    {
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "ValidarUsuario";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Correo));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, usuario.Id));
        conexion.Comando.ExecuteNonQuery();
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
    }

    public int ConsultarUsuarioSoloNombre(Usuario usuario)
    {
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "ConsultarUsuarioSoloNombre";
        conexion.Comando.CommandType = CommandType.StoredProcedure;

        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.NombreUsuario));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          usuario.Id = leerDatos.GetInt32(0);
        }
        else
        {
          usuario.Id = 0;
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
      return usuario.Id;
    }
  }

  
}
