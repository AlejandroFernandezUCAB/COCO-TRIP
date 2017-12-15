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
      int respuesta = 0;
      try
      {
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        comando = new NpgsqlCommand("InsertarLocalidad", conexion.SqlConexion);

        comando.CommandType = CommandType.StoredProcedure;
        //Aqui se registran los valores de localidad evento
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, lEvento.Nombre);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, lEvento.Descripcion);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, lEvento.Coordenadas);
        read = comando.ExecuteReader();

        try
        {
          if (read.Read())
          {
            Int32.TryParse(read.GetValue(0).ToString(), out respuesta);
          }
          if (respuesta == 0)
          {
            throw new ItemNoEncontradoException($"No se encontro el evento con el nombre {lEvento.Nombre}");
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

      finally {
        conexion.Desconectar();
      }
      
      return respuesta;
    }


    /**
     * <summary>Metodo para eliminar una localidad de la BBDD</summary>
     * elimina tambien los eventos asociados a esa localidad
     * **/


      public Boolean EliminarLocalidadEvento(int id)
    {
       Boolean respuesta = false;
      try
      {
        comando = new NpgsqlCommand("EliminarLocalidadId", conexion.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, id);
        read = comando.ExecuteReader();
        read.Read();
        respuesta = true;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }

      finally
      {
        conexion.Desconectar();
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
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      finally
      {
        conexion.Desconectar();
      }

      return localidad;
    }

    /**
 * <summary>Metodo para consultar de la base de datos una localidad dado su nombre</summary>
 * retorna solo el id de la localidad
 * **/

   public LocalidadEvento ConsultarLocalidadEventoNombreID(string nombreLocalidad)
    {
      LocalidadEvento localidad = new LocalidadEvento();
      try
      {
        comando = new NpgsqlCommand("LocalidadIdNombre", conexion.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, nombreLocalidad);
        read = comando.ExecuteReader();
        read.Read();
        localidad.Id = read.GetInt32(0);
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      finally
      {
        conexion.Desconectar();
      }

      return localidad;
    }

 /**
  * <summary>Metodo para consultar de la base de datos una localidad dado su nombre</summary>
  * retorna toda la informacion de la localidad
  **/

    public LocalidadEvento ConsultarLocalidadEventoPorNombre(string nombre)
    {
      LocalidadEvento localidad = new LocalidadEvento();
      try
      {
        comando = new NpgsqlCommand("ConsultarLocalidadPorNombre", conexion.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, nombre);
        read = comando.ExecuteReader();
        read.Read();
        localidad.Id = read.GetInt32(0);
        localidad.Nombre = read.GetString(1);
        localidad.Descripcion = read.GetString(2);
        localidad.Coordenadas = read.GetString(3);
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }

      finally
      {
        conexion.Desconectar();
      }
      return localidad;
    }


    /**
     * <summary>Metodo para listar todas las localidades de la base de datos</summary>
     * **/


    public IList<LocalidadEvento> ListaLocalidadEventos()
    {
      IList<LocalidadEvento> localidades = new List<LocalidadEvento>();
      try
      {
        comando = new NpgsqlCommand("consultarlocalidades", conexion.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        read = comando.ExecuteReader();
        while (read.Read())
        {
          LocalidadEvento localidad = new LocalidadEvento(read.GetInt32(0), read.GetString(1), read.GetString(2), read.GetString(3));
          localidades.Add(localidad);
        }
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      finally
      {
        conexion.Desconectar();
      }

      return localidades;
    }


    /**
     * <summary>Metodo para agregar una localidad a la base de datos</summary>
     * **/


    public int ActualizarLocalidadEvento(LocalidadEvento lEvento)
    {
      int respuesta = 0;
      try
      {
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        comando = new NpgsqlCommand("actualizarlocalidadporid", conexion.SqlConexion);

        comando.CommandType = CommandType.StoredProcedure;
        //Aqui se registran los valores de localidad evento
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, lEvento.Id);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, lEvento.Nombre);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, lEvento.Descripcion);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, lEvento.Coordenadas);
        read = comando.ExecuteReader();

        try
        {
          if (read.Read())
          {
            Int32.TryParse(read.GetValue(0).ToString(), out respuesta);
          }
          if (respuesta == 0)
          {
            throw new ItemNoEncontradoException($"No se encontro el evento con el nombre {lEvento.Nombre}");
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
        conexion.Desconectar();
      }

      return respuesta;
    }


  }
}
