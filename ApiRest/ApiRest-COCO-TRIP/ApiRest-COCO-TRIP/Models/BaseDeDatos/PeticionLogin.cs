using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;

namespace ApiRest_COCO_TRIP.Models.BaseDeDatos
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
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      finally
      {
        conexion.Desconectar();
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
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      finally
      {
        conexion.Desconectar();
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
          //usuario.Correo = leerDatos.GetString(2);
          //usuario.Nombre = leerDatos.GetString(3);
          //usuario.Apellido = leerDatos.GetString(4);
          usuario.Valido = leerDatos.GetBoolean(7);
          try
          {
            usuario.Clave = leerDatos.GetString(9);
          }
          catch (InvalidCastException)
          {
            usuario.Clave = null;
          }

        }
        else
        {
          usuario.Id = 0;
        }
        leerDatos.Close();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      finally
      {
        conexion.Desconectar();
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
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Foto));

        leerDatos = conexion.Comando.ExecuteReader();

        if (leerDatos.Read())
        {
          usuario.Id = leerDatos.GetInt32(0);
          leerDatos.Close();
        }

      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (InvalidCastException e)
      {
        throw e;
      }
      finally
      {
        conexion.Desconectar();
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
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Correo));

        leerDatos = conexion.Comando.ExecuteReader();

        if (leerDatos.Read())
        {
          usuario.Id = leerDatos.GetInt32(0);
          leerDatos.Close();
        }

      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (InvalidCastException e)
      {
        throw e;
      }
      finally {
        conexion.Desconectar();
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
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      finally
      {
        conexion.Desconectar();
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
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      finally
      {
        conexion.Desconectar();
      }
    }

    public void ActualizarUsuario(Usuario usuario)
    {
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "ActualizarUsuario";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.NombreUsuario));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Nombre));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Apellido));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Date, usuario.FechaNacimiento));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Genero));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Correo));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Clave));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Foto));
        conexion.Comando.ExecuteNonQuery();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      finally {
        conexion.Desconectar();
      }
    }

    public void EliminarUsuario(int id) {

      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "EliminarUsuario";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, id));
        conexion.Comando.ExecuteNonQuery();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      finally
      {
        conexion.Desconectar();
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
          usuario.Correo = leerDatos.GetString(2);
          usuario.Nombre = leerDatos.GetString(3);
          usuario.Apellido = leerDatos.GetString(4);
          usuario.FechaNacimiento = leerDatos.GetDateTime(5);
          usuario.Valido = leerDatos.GetBoolean(7);
        }
        else
        {
          usuario.Id = 0;
        }
        leerDatos.Close();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      finally
      {
        conexion.Desconectar();
      }
      return usuario.Id;
    }
    public List<LugarTuristicoPreferencia> ConsultarLugarTuristicoSegunPreferencias(int idUsuario)
    {
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "BuscarLugarTuristicoSegunPreferencias";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
        leerDatos = conexion.Comando.ExecuteReader();
        List<LugarTuristicoPreferencia> ltp = new List<LugarTuristicoPreferencia>();

        while (leerDatos.Read()) //Recorre las filas retornadas de la base de datos
        {
          var lugarPreferencia = new LugarTuristicoPreferencia();
          lugarPreferencia.NombreLT = leerDatos.GetString(0);
          lugarPreferencia.Costo = leerDatos.GetDouble(1);
          lugarPreferencia.Descripcion = leerDatos.GetString(2);
          lugarPreferencia.Direccion = leerDatos.GetString(3);
          //lugarPreferencia.LugarFotoRuta = leerDatos.GetString(4);
          lugarPreferencia.NombreCategoria = leerDatos.GetString(4);
          ltp.Add(lugarPreferencia);
        }
        
        leerDatos.Close();
        return ltp;
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      finally
      {
        conexion.Desconectar();
      }


    }
    public List<EventoPreferencia> ConsultarEventosSegunPreferencias(int idUsuario, DateTime fechaActual) {
        try
        {
            conexion.Conectar();
            conexion.Comando = conexion.SqlConexion.CreateCommand();
            conexion.Comando.CommandText = "BuscarEventoSegunPreferencias";
            conexion.Comando.CommandType = CommandType.StoredProcedure;
            conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario));
            conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Date, fechaActual));
            leerDatos = conexion.Comando.ExecuteReader();
            List<EventoPreferencia> listaEventos = new List<EventoPreferencia>();

        while (leerDatos.Read()) //Recorre las filas retornadas de la base de datos
        {
          var eventoPreferencia = new EventoPreferencia();
          eventoPreferencia.NombreEvento = leerDatos.GetString(0);
          eventoPreferencia.FechaInicio = leerDatos.GetDateTime(1);
          eventoPreferencia.FechaFin = leerDatos.GetDateTime(2);
          eventoPreferencia.HoraInicio = leerDatos.GetTimeSpan(3);
          eventoPreferencia.HoraFin = leerDatos.GetTimeSpan(4);
          eventoPreferencia.Precio = leerDatos.GetDouble(5);
          eventoPreferencia.Descripcion = leerDatos.GetString(6);
          eventoPreferencia.NombreLocal = leerDatos.GetString(8);
          eventoPreferencia.LocalFotoRuta = leerDatos.GetString(7);
          eventoPreferencia.NombreCategoria = leerDatos.GetString(9);
          listaEventos.Add(eventoPreferencia);
        }

        leerDatos.Close();

        return listaEventos;
      }
        catch (NpgsqlException e)
        {
            throw e;
        }
        catch (FormatException e)
        {
            throw e;
        }
      finally
      {
        conexion.Desconectar();
      }

    }
    public int InsertarCategoria(Categoria categoria)
    {
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "InsertarCategoria";
        conexion.Comando.CommandType = CommandType.StoredProcedure;

        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, categoria.Nombre));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, categoria.Descripcion));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Boolean, categoria.Estatus));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, categoria.CategoriaSuperior));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, categoria.Nivel));

        leerDatos = conexion.Comando.ExecuteReader();

        if (leerDatos.Read())
        {
          categoria.Id = leerDatos.GetInt32(0);
          leerDatos.Close();
        }

      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (InvalidCastException e)
      {
        throw e;
      }
      finally
      {
        conexion.Desconectar();
      }
      return categoria.Id;
    }
    public void EliminarCategoria(int id)
    {

      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "EliminarCategoria";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, id));
        conexion.Comando.ExecuteNonQuery();
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      finally
      {
        conexion.Desconectar();
      }
    }
  }


}
