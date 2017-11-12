using ApiRest_COCO_TRIP.Models.Excepcion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ApiRest_COCO_TRIP.Models
{
  /**
   * <summary>Clase que recibe todas las peticiones relacionadas a localidades de eventos</summary>
   **/
  public class PeticionLocalidadEvento
  {
        private ConexionBase conexion;
        private NpgsqlDataReader read;
        private NpgsqlCommand comando;
    private Boolean respuesta = false;
    /**
     * <summary>Constructor que abre la base de datos</summary>
     * **/
        public PeticionLocalidadEvento()
        {
      try
      {
        conexion = new ConexionBase();
        conexion.Conectar();
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }
    /**
     * <summary>Metodo para agregar una localidad a la base de datos</summary>
     * **/
    public int AgregarLocalidadEvento(LocalidadEvento lEvento)
    {
      int respuesta = -1;
      try
      {
        comando = new NpgsqlCommand("InsertarLocalidad", conexion.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        //Aqui se registran los valores de localidad evento
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, lEvento.Nombre);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, lEvento.Descripcion);
        //comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, lEvento.Coordenadas);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, 4);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, 4);
        read = comando.ExecuteReader();
        read.Read();
        respuesta = read.GetInt32(0);
        conexion.Desconectar();
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      return respuesta;
    }
    /**
     * <summary>Metodo para eliminar una localidad de la BBDD</summary>
     * **/
      public Boolean EliminarLocalidadEvento(int id)
    {

      try
      {
        comando = new NpgsqlCommand("EliminarLocalidadPorId", conexion.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, id);
        read = comando.ExecuteReader();
        read.Read();
        respuesta = read.GetBoolean(0);
        conexion.Desconectar();
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      return respuesta;
    }

    /**
     * <summary>Metodo para consultr de la base de datos una localidad dado su id</summary>
     * **/
    public LocalidadEvento ConsultarLocalidadEvento(int id)
    {
      LocalidadEvento localidad = new LocalidadEvento();
      try
      {
        comando = new NpgsqlCommand("ConsultarLocalidadPorId", conexion.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, id);
        read = comando.ExecuteReader();
        read.Read();
        localidad.Id = read.GetInt32(0);
        localidad.Nombre = read.GetString(1);
        localidad.Descripcion = read.GetString(2);
        localidad.Coordenadas = read.GetString(3);
        conexion.Desconectar();
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      return localidad;
    }
    /**
     * <summary>Metodo para listar todas las localidades de la base de datos</summary>
     * **/
    public List<LocalidadEvento> ListaLocalidadEventos()
    {
      List<LocalidadEvento> localidades = new List<LocalidadEvento>();
      try
      {
        comando = new NpgsqlCommand("ConsultarLocalidadesConEventosAsignados", conexion.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        read = comando.ExecuteReader();
        while (read.Read())
        {
          LocalidadEvento localidad = new LocalidadEvento(read.GetString(0), read.GetString(1), read.GetString(2));
          localidades.Add(localidad);
        }
        conexion.Desconectar();
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      return localidades;
    }
  }
}
