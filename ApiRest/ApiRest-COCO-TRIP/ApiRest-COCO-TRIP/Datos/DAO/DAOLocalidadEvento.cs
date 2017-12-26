using ApiRest_COCO_TRIP.Comun.Excepcion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using NpgsqlTypes;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
  /**
   * <summary>Clase que recibe todas las peticiones relacionadas a localidades de eventos</summary>
   **/
  public class DAOLocalidadEvento : DAO
  {
    private NpgsqlParameter parametro;
    private NpgsqlDataReader leerDatos;
    private Entidad localidad;
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
          localidad = FabricaEntidad.CrearEntidadLocalidad();
          ((LocalidadEvento)localidad).Id = leerDatos.GetInt32(0);
          ((LocalidadEvento)localidad).Nombre = leerDatos.GetString(1);
          ((LocalidadEvento)localidad).Descripcion = leerDatos.GetString(2);
          ((LocalidadEvento)localidad).Coordenadas = leerDatos.GetString(3);
          lista.Add(localidad);
        }
        leerDatos.Close();
      }
      catch (NpgsqlException e)
      {
        BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
        ex.NombreMetodos= this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }
      finally
      {
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

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = localidad.Id;
        Comando.Parameters.Add(parametro);


        leerDatos = Comando.ExecuteReader();
        leerDatos.Read();
        localidad.Id = leerDatos.GetInt32(0);
        ((LocalidadEvento)localidad).Nombre = leerDatos.GetString(1);
        ((LocalidadEvento)localidad).Descripcion = leerDatos.GetString(2);
        ((LocalidadEvento)localidad).Coordenadas = leerDatos.GetString(3);
        leerDatos.Close();
      }
      catch (NpgsqlException e)
      {
        BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }
      catch (InvalidCastException e)
      {
        CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }

      catch (InvalidOperationException e) {
        OperacionInvalidaException ex = new OperacionInvalidaException(e);
        ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw ex;
      }
      finally
      {
        Desconectar();
      }

      return localidad;
    }

    public override void Insertar(Entidad objeto)
    {
      try
      {
        localidad = (LocalidadEvento)objeto;
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "InsertarLocalidad";
        Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = ((LocalidadEvento)localidad).Nombre;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = ((LocalidadEvento)localidad).Descripcion;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = ((LocalidadEvento)localidad).Coordenadas;
        Comando.Parameters.Add(parametro);

        leerDatos = Comando.ExecuteReader();
        leerDatos.Close();

      }
      catch (NpgsqlException e)
      {
        BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }
      catch (InvalidCastException e)
      {
        CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }

      finally
      {
        Desconectar();
      }
    }

    public override void Actualizar(Entidad objeto)
    {
      try
      {
        localidad = (LocalidadEvento)objeto;
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "actualizarlocalidadporid";
        Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = localidad.Id;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = ((LocalidadEvento)localidad).Nombre;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = ((LocalidadEvento)localidad).Descripcion;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = ((LocalidadEvento)localidad).Coordenadas;
        Comando.Parameters.Add(parametro);

        leerDatos = Comando.ExecuteReader();
        leerDatos.Close();
      }
      catch (NpgsqlException e)
      {
        BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }
      catch (InvalidCastException e)
      {
        CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }

      finally
      {
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

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = localidad.Id;
        Comando.Parameters.Add(parametro);

        leerDatos = Comando.ExecuteReader();
        leerDatos.Read();
        leerDatos.Close();
      }
      catch (NpgsqlException e)
      {
        BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }
      catch (InvalidCastException e)
      {
        CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }

      finally
      {
        Desconectar();
      }
    }
  }
  
}

