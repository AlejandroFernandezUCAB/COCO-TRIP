using ApiRest_COCO_TRIP.Models.Excepcion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Reflection;

namespace ApiRest_COCO_TRIP.Models
{
  /**
  * <summary>Clase que recibe todas las peticiones relacionadas a eventos</summary>
  **/


  public class PeticionEvento
  {
    private  ConexionBase conexion;
    private  NpgsqlDataReader read;
    private  NpgsqlCommand comando;


    /**
     * <summary>Contructor de la clase</summary>
     * */


    public PeticionEvento()
    {
            try
            {
                conexion = new ConexionBase();
                conexion.Conectar();
            }
            catch (BaseDeDatosExcepcion e)
            {
                e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        e.Mensaje = "Problema al abrir conexion con base de datos en Peticiones de eventos";
                throw e;
            }
        }


      /// <summary>
      /// Metodo que agrega eventos y retorna ecenario de exito y fallo
      /// </summary>
      /// <param name="evento"> Objeto del tipo Evento</param>
      /// <returns> Respuesta de Agregar con exito</returns>
      ///

    public  int AgregarEvento(Evento evento)
    {
            int respuesta = -1;
            try
            {
        
        
            comando = new NpgsqlCommand("InsertarEvento", conexion.SqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                //Aqui registro los valores
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, evento.Nombre);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, evento.Descripcion);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, evento.Precio);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Timestamp, evento.FechaInicio);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Timestamp, evento.FechaFin);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Time, evento.HoraInicio.Hour+":"+evento.HoraInicio.Minute+"00");
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Time, evento.HoraFin.Hour + ":" + evento.HoraFin.Minute + "00");
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, evento.Foto);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, evento.IdLocalidad);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, evento.IdCategoria);
                read = comando.ExecuteReader();
                read.Read();
                respuesta = read.GetInt32(0);
            }
            catch (BaseDeDatosExcepcion e)
            {
                e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
                e.Mensaje = "Problemas en la base de datos, en Peticion Insertar Evento";
                throw e;
            }

      finally
      {
        conexion.Desconectar();
      }
      return respuesta;
           
    }


    /**
     * <summary>Metodo que retorna la Lista de eventos por un id de categoria dada</summary>
     * <params name="id_categoria">Id de la categria</params>
     * <returns>La lista de eventos
     * </returns>
     */


    public List<Evento> ListaEventosPorCategoria(int id_categoria)
    {
      List<Evento> list = new List<Evento>();
      
      try
      {
        comando = new NpgsqlCommand("ConsultarEventoPorIdCategoria", conexion.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer,id_categoria);
        read = comando.ExecuteReader();
        while (read.Read())
        {
          DateTime horaInicio = new DateTime();
          horaInicio.AddHours(read.GetTimeSpan(6).Hours);
          horaInicio.AddMinutes(read.GetTimeSpan(6).Minutes);

          DateTime horaFin = new DateTime();
          horaFin.AddHours(read.GetTimeSpan(7).Hours);
          horaFin.AddMinutes(read.GetTimeSpan(7).Minutes);

          Evento evento = new Evento(read.GetInt32(0), read.GetString(1), read.GetString(2), read.GetInt64(3), read.GetDateTime(4), read.GetDateTime(5),
            horaInicio, horaFin, read.GetString(8), read.GetInt32(9));
          list.Add(evento);
        }
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        e.Mensaje = "Problemas en la base de datos, en ListaEventosPorCategoria";
        throw e;
      }
      finally
      {
        conexion.Desconectar();
      }
      return list;
        }


     /**
     * <summary>Metodo que retorna la Lista de eventos por un nombre de categoria dada</summary>
     * <params name="nombreCategoria">Nombre de la categria</params>
     * <returns>La lista de eventos
     * </returns>
     */


        public List<Evento> ListaEventosPorCategoriaNombre(string nombreCategoria)
        {
            List<Evento> list = new List<Evento>();

            try
            {
                comando = new NpgsqlCommand("ConsultarEventoPorNombreCategoria", conexion.SqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, nombreCategoria);
                read = comando.ExecuteReader();
                while (read.Read())
                {
                    DateTime horaInicio = new DateTime();
                    horaInicio.AddHours(read.GetTimeSpan(6).Hours);
                    horaInicio.AddMinutes(read.GetTimeSpan(6).Minutes);

                    DateTime horaFin = new DateTime();
                    horaFin.AddHours(read.GetTimeSpan(7).Hours);
                    horaFin.AddMinutes(read.GetTimeSpan(7).Minutes);
                    
                    Evento evento = new Evento(read.GetInt32(0), read.GetString(1), read.GetString(2), read.GetInt64(3), read.GetDateTime(4), read.GetDateTime(5),
                      horaInicio, horaFin, read.GetString(8), read.GetInt32(10), read.GetInt32(9));
                    list.Add(evento);
                }
            }
            catch (BaseDeDatosExcepcion e)
            {
                e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
                e.Mensaje = "Problemas en la base de datos, en ListaEventosPorCategoria";
                throw e;
            }
            finally
            {
                conexion.Desconectar();
            }
            return list;
        }

    /**
     * <summary>Metodo que retorna la Lista de todos los eventos en la BD</summary>
     * <returns>La lista de eventos
     * </
    * */

        public List<Evento> ListaEventos()
        {
          List<Evento> list = new List<Evento>();
          
          try
          {
            comando = new NpgsqlCommand("ConsultarEventos", conexion.SqlConexion);
            comando.CommandType = CommandType.StoredProcedure;
            read = comando.ExecuteReader();
            while (read.Read())
            {
              DateTime horaInicio = new DateTime();
              horaInicio.AddHours(read.GetTimeSpan(6).Hours);
              horaInicio.AddMinutes(read.GetTimeSpan(6).Minutes);

              DateTime horaFin = new DateTime();
              horaFin.AddHours(read.GetTimeSpan(7).Hours);
              horaFin.AddMinutes(read.GetTimeSpan(7).Minutes);

              Evento evento = new Evento(read.GetInt32(0), read.GetString(1), read.GetString(2), read.GetInt64(3), read.GetDateTime(4), read.GetDateTime(5),
                horaInicio, horaFin, read.GetString(8), read.GetInt32(10), read.GetInt32(9));
              list.Add(evento);
            }
          }
          catch (BaseDeDatosExcepcion e)
          {
            e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            e.Mensaje = "Problemas en la base de datos, en ListaEventosPorCategoria";
            throw e;
          }
          finally
          {
            conexion.Desconectar();
          }
          return list;
        }


    /**
        * <summary>Metodo que muestra info de un evento dado su id</summary>
        * <params name="id">id del evento</params>
        * <returns>El evento solicitado</returns>
        * */


    public Evento ConsultarEvento(int id)
    {
      Evento evento = new Evento();
      PeticionLocalidadEvento peticionLocalidadEvento = new PeticionLocalidadEvento();
      try
      {
        comando = new NpgsqlCommand("ConsultarEventoPorIdEvento", conexion.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, id);
        read = comando.ExecuteReader();
        read.Read();
        //Creo un objeto de tipo categoria con un solo atributo nombre
        //y Busco el id de la categoria
        Categoria categoriaNombre = new Categoria();
        categoriaNombre.Nombre = read.GetString(9);
        PeticionCategoria peticionCategoria = new PeticionCategoria();
        Categoria categoria = peticionCategoria.ObtenerIdCategoriaPorNombre(categoriaNombre);

        evento.Id = read.GetInt32(0);
        evento.Nombre = read.GetString(1);
        evento.Descripcion = read.GetString(2);
        evento.Precio = read.GetInt64(3);
        evento.FechaInicio = read.GetDateTime(4);
        evento.FechaFin = read.GetDateTime(5);
        DateTime horaInicio = new DateTime();
        horaInicio.AddHours(read.GetTimeSpan(6).Hours);
        horaInicio.AddMinutes(read.GetTimeSpan(6).Minutes);
        evento.HoraInicio = horaInicio;
        DateTime horaFin = new DateTime();
        horaFin.AddHours(read.GetTimeSpan(7).Hours);
        horaFin.AddMinutes(read.GetTimeSpan(7).Minutes);
        evento.HoraFin = horaFin;
        evento.Foto = read.GetString(8);
        evento.IdCategoria = categoria.Id;
        evento.IdLocalidad = peticionLocalidadEvento.ConsultarLocalidadEventoPorNombre(read.GetString(10)).Id;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        e.Mensaje = "Problemas en la base de datos, en ConsultarEvento";

        throw e;
      }
      finally {
        conexion.Desconectar();
      }
      return evento;
    }

    /**
     * <summary>Metodo que muestra info de un evento dado su nombre</summary>
     * <params name="nombreEvento">nombre del evento</params>
     * <returns>El evento solicitado</returns>
     * */


    public Evento ConsultarEventoNombre(string nombreEvento)
    {
      Evento evento = new Evento();
      PeticionLocalidadEvento peticionLocalidadEvento = new PeticionLocalidadEvento();
      try
      {
        comando = new NpgsqlCommand("ConsultarEventoPorIdNombre", conexion.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, nombreEvento);
        read = comando.ExecuteReader();
        read.Read();
        //Creo un objeto de tipo categoria con un solo atributo nombre
        //y Busco el id de la categoria
        Categoria categoriaNombre = new Categoria();
        categoriaNombre.Nombre = read.GetString(9);
        PeticionCategoria peticionCategoria = new PeticionCategoria();
        Categoria categoria = peticionCategoria.ObtenerIdCategoriaPorNombre(categoriaNombre);

        evento.Id = read.GetInt32(0);
        evento.Nombre = read.GetString(1);
        evento.Descripcion = read.GetString(2);
        evento.Precio = read.GetInt64(3);
        evento.FechaInicio = read.GetDateTime(4);
        evento.FechaFin = read.GetDateTime(5);
        DateTime horaInicio = new DateTime();
        horaInicio.AddHours(read.GetTimeSpan(6).Hours);
        horaInicio.AddMinutes(read.GetTimeSpan(6).Minutes);
        evento.HoraInicio = horaInicio;
        DateTime horaFin = new DateTime();
        horaFin.AddHours(read.GetTimeSpan(7).Hours);
        horaFin.AddMinutes(read.GetTimeSpan(7).Minutes);
        evento.HoraFin = horaFin;
        evento.Foto = read.GetString(8);
        evento.IdCategoria = categoria.Id;
        evento.IdLocalidad = peticionLocalidadEvento.ConsultarLocalidadEventoPorNombre(read.GetString(10)).Id;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        e.Mensaje = "Problemas en la base de datos, en ConsultarEvento";

        throw e;
      }
      finally
      {
        conexion.Desconectar();
      }
      return evento;
    }


    /**
     * <summary>Metodo que elimina un evento segun su id</summary>
     * <paramas name="id">id de evento que se quiere eliminar</paramas>
     * <return>True si se elimino y false en caso contrario </return>
     * */


    public bool EliminarEventoId(int id)
    {
      Boolean respuesta = false;
      try
      {
        comando = new NpgsqlCommand("eliminareventoporid", conexion.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, id);
        read = comando.ExecuteReader();
        read.Read();
        respuesta = true;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        e.Mensaje = "Problemas en la base de datos, en Eliminar Evento por iD";
        throw e;
      }

      finally {
        conexion.Desconectar();
      }
      return respuesta;
    }
  

   /**
    * <summary>Metodo que elimina un evento segun su nombre</summary>
    * <paramas name="nombreEvento">nombre de evento que se quiere eliminar</paramas>
    * <return>True si se elimino y false en caso contrario </return>
    * */


        public bool EliminarEventoNombre(string nombreEvento)
        {
            Boolean respuesta = false;
            try
            {
                comando = new NpgsqlCommand("EliminarEventoNombre", conexion.SqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, nombreEvento);
                read = comando.ExecuteReader();
                read.Read();
                respuesta = true;
            }
            catch (BaseDeDatosExcepcion e)
            {
                e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
                e.Mensaje = "Problemas en la base de datos, en Eliminar Evento por Nombre";
                throw e;
            }

            finally
            {
                conexion.Desconectar();
            }
            return respuesta;
        }


        /**
         * <summary>Lista de eventos dado una fecha</summary>
         * <params name=fecha>fecha</params>
         * <returns>Retorna la informacion de todos los eventos a partir de esa fecha</returns>
         * */


        public List<Evento> ListaEventosPorFecha(DateTime fecha)
    {
      List<Evento> list = new List<Evento>();

      try
      {
        comando = new NpgsqlCommand("ConsultarEventosPorFecha", conexion.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Timestamp, fecha);
        read = comando.ExecuteReader();
        while (read.Read())
        {
          //Creo un objeto de tipo categoria con un solo atributo nombre
          //y Busco el id de la categoria
          Categoria categoriaNombre = new Categoria();
          categoriaNombre.Nombre = read.GetString(9);
          PeticionCategoria peticionCategoria = new PeticionCategoria();
          Categoria categoria = peticionCategoria.ObtenerIdCategoriaPorNombre(categoriaNombre);

          PeticionLocalidadEvento peticionLocalidadEvento = new PeticionLocalidadEvento();
          LocalidadEvento localidad = peticionLocalidadEvento.ConsultarLocalidadEventoPorNombre(read.GetString(10));
          DateTime horaInicio = new DateTime();
          horaInicio.AddHours(read.GetTimeSpan(6).Hours);
          horaInicio.AddMinutes(read.GetTimeSpan(6).Minutes);

          DateTime horaFin = new DateTime();
          horaFin.AddHours(read.GetTimeSpan(7).Hours);
          horaFin.AddMinutes(read.GetTimeSpan(7).Minutes);

          //Categoria categoria = peticionCategoria.ObtenerCategorias
          Evento evento = new Evento(read.GetInt32(0), read.GetString(1), read.GetString(2), read.GetInt64(3), read.GetDateTime(4), read.GetDateTime(5),
            horaInicio, horaFin, read.GetString(8), categoria.Id, localidad.Id);
          list.Add(evento);
        }
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        e.Mensaje = "Problemas en la base de datos, en ConsultarEventosPorFecha";
        throw e;
      }

      finally {
        conexion.Desconectar();
      }
      return list;
    }


    public int ActualizarEvento(Evento UEvento)
    {
      int respuesta = 0;
      try
      {
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        comando = new NpgsqlCommand("actualizarEventoPorId", conexion.SqlConexion);

        comando.CommandType = CommandType.StoredProcedure;
        //Aqui se registran los valores de evento
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, UEvento.Id);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, UEvento.Nombre);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, UEvento.Descripcion);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Double, UEvento.Precio);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Timestamp, UEvento.FechaInicio);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Timestamp, UEvento.FechaFin);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Time, UEvento.HoraInicio);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Time, UEvento.HoraFin);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, UEvento.Foto);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, UEvento.IdLocalidad);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, UEvento.IdCategoria);
        read = comando.ExecuteReader();

        try
        {
          if (read.Read())
          {
            Int32.TryParse(read.GetValue(0).ToString(), out respuesta);
          }
          if (respuesta == 0)
          {
            throw new ItemNoEncontradoException($"No se encontro el evento con el nombre {UEvento.Nombre}");
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
