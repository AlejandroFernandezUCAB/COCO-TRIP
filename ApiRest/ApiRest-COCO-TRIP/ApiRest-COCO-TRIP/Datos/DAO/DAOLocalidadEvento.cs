using ApiRest_COCO_TRIP.Models.Excepcion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using NpgsqlTypes;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
  /**
   * <summary>Clase que recibe todas las peticiones relacionadas a localidades de eventos</summary>
   **/
  public class DAOLocalidadEvento : DAO
  {
    private NpgsqlParameter parametro;
    private NpgsqlDataReader leerDatos;

    private LocalidadEvento localidad;

    private List<Entidad> lista;

    public DAOLocalidadEvento()
    {
      parametro = new NpgsqlParameter();
      lista = new List<Entidad>();
    }
    public override List<Entidad> ConsultarLista(Entidad objeto)
    {
      try
      {
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "consultarlocalidades";
        Comando.CommandType = CommandType.StoredProcedure;

        leerDatos = Comando.ExecuteReader();
        while (leerDatos.Read())
        {
          LocalidadEvento localidad = new LocalidadEvento(leerDatos.GetInt32(0), leerDatos.GetString(1), leerDatos.GetString(2), leerDatos.GetString(3));
          lista.Add(localidad);
        }
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      finally
      {
        leerDatos.Close();
        Desconectar();
      }

      return lista;
    }

    public override Entidad ConsultarPorId(Entidad objeto)
    {
       localidad = (LocalidadEvento)objeto;
      try
      {
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "ConsultarLocalidadPorId";
        Comando.CommandType = CommandType.StoredProcedure;

        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = localidad.Id;
        Comando.Parameters.Add(parametro);


        leerDatos = Comando.ExecuteReader();
        leerDatos.Read();
        localidad.Id = leerDatos.GetInt32(0);
        localidad.Nombre = leerDatos.GetString(1);
        localidad.Descripcion = leerDatos.GetString(2);
        localidad.Coordenadas = leerDatos.GetString(3);
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      finally
      {
        leerDatos.Close();
        Desconectar();
      }

      return localidad;
    }

    public override void Insertar(Entidad objeto)
    {
      int respuesta = 0;
      try
      {
        localidad = (LocalidadEvento)objeto;
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "InsertarLocalidad";
        Comando.CommandType = CommandType.StoredProcedure;

        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = localidad.Nombre;
        Comando.Parameters.Add(parametro);

        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = localidad.Descripcion;
        Comando.Parameters.Add(parametro);

        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = localidad.Coordenadas;
        Comando.Parameters.Add(parametro);

        leerDatos = Comando.ExecuteReader();

        try
        {
          if (leerDatos.Read())
          {
            Int32.TryParse(leerDatos.GetValue(0).ToString(), out respuesta);
          }
          if (respuesta == 0)
          {
            throw new ItemNoEncontradoException($"No se encontro la localidad con el nombre {localidad.Nombre}");
          }
        }
        catch (System.InvalidCastException e)
        {
          Console.WriteLine(e.Message);
        }




      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }

      finally
      {
        leerDatos.Close();
        Desconectar();
      }
    }
    public override void Actualizar(Entidad objeto)
    {
      int respuesta = 0;
      try
      {
        localidad = (LocalidadEvento)objeto;
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "actualizarlocalidadporid";
        Comando.CommandType = CommandType.StoredProcedure;

        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = localidad.Nombre;
        Comando.Parameters.Add(parametro);

        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = localidad.Descripcion;
        Comando.Parameters.Add(parametro);

        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = localidad.Coordenadas;
        Comando.Parameters.Add(parametro);

        leerDatos = Comando.ExecuteReader();

        try
        {
          if (leerDatos.Read())
          {
            Int32.TryParse(leerDatos.GetValue(0).ToString(), out respuesta);
          }
          if (respuesta == 0)
          {
            throw new ItemNoEncontradoException($"No se encontro la localidad con el nombre {localidad.Nombre}");
          }
        }
        catch (System.InvalidCastException e)
        {
          throw new CasteoInvalidoExcepcion(e);
        }

      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }

      finally
      {
        leerDatos.Close();
        Desconectar();
      }
    }

    public override void Eliminar(Entidad objeto)
    {
      try
      {

        localidad = (LocalidadEvento)objeto;
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "EliminarLocalidadporId";
        Comando.CommandType = CommandType.StoredProcedure;

        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = localidad.Id;
        Comando.Parameters.Add(parametro);

        leerDatos = Comando.ExecuteReader();
        leerDatos.Read();
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }

      finally
      {
        leerDatos.Close();
        Desconectar();
      }
    }
  }
  
}

