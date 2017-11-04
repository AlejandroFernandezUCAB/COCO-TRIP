using System;
using System.Web.Http;
using Npgsql;
using System.Data;
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Models.M7.Dato;
namespace ApiRest_COCO_TRIP.Controllers
{
  public class M5Controller : ApiController
  {
    /// <summary>returns
    /// Metodo que agrega en la base de datos un nuevo itinerario
    /// </summary>
    /// <param name="it">el itinerario a agregar</param>
    /// <returns>true si agrega existosamente, false en caso de error</>
    [HttpPost]
    public Itinerario AgregarItinerario(Itinerario it)
    {
      try
      {
        ConexionBase con = new ConexionBase();
        con.Conectar();
        NpgsqlCommand comm = new NpgsqlCommand("add_itinerario", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, it.Nombre);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, it.FechaInicio);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, it.FechaFin);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.IdUsuario);
        NpgsqlDataReader pgread = comm.ExecuteReader();
        pgread.Read();
        it.Id = pgread.GetInt16(0);
        con.Desconectar();
        return it;
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

    /// <summary>
    /// Metodo que elimina un itinerario de la base de datos
    /// </summary>
    /// <param name="it">el itinerario a eliminar</param>
    /// <returns>true si elimina existosamente, false en caso de error</returns>
    [HttpGet]
    public Boolean EliminarItinerario(Itinerario it)
    {
      try
      {
        ConexionBase con = new ConexionBase();
        con.Conectar();
        NpgsqlCommand comm = new NpgsqlCommand("del_itinerario", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.Id);
        NpgsqlDataReader pgread = comm.ExecuteReader();
        pgread.Read();
        Boolean resp = pgread.GetBoolean(0);
        con.Desconectar();
        return resp;
      }
      catch (NpgsqlException e)
      {
        return false;
      }

    }

    /// <summary>
    /// Metodo que modifica un itinerario de la base de datos
    /// </summary>
    /// <param name="it">el itinerario a modificar</param>
    /// <returns>true si modifica existosamente, false en caso de error</returns>
    [HttpGet]
    public Boolean ModificarItinerario(Itinerario it)
    {
      try
      {
        ConexionBase con = new ConexionBase();
        con.Conectar();
        NpgsqlCommand comm = new NpgsqlCommand("mod_itinerario", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.Id);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, it.Nombre);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, it.FechaInicio);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, it.FechaFin);
        NpgsqlDataReader pgread = comm.ExecuteReader();
        pgread.Read();
        Boolean resp = pgread.GetBoolean(0);
        con.Desconectar();
        return resp;
      }
      catch (NpgsqlException e)
      {
        return false;
      }

    }

    /// <summary>
    /// Metodo que agrega un evento existente a un itinerario existente
    /// </summary>
    /// <param name="it">itinerario al cual se le agrega el evento</param>
    /// <param name="ev">evento a agregar en el itinerario</param>
    /// <returns>true si se agrego el evento exitosamente, false en caso de error</returns>
    [HttpGet]
    public Boolean AgregarEvento_It(Itinerario it,Evento ev)
    {
      try
      {
        ConexionBase con = new ConexionBase();
        con.Conectar();
        NpgsqlCommand comm = new NpgsqlCommand("add_evento_it", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, ev.Ev_id);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.Id);
        NpgsqlDataReader pgread = comm.ExecuteReader();
        pgread.Read();
        Boolean resp = pgread.GetBoolean(0);
        con.Desconectar();
        return resp;
      }
      catch (NpgsqlException e)
      {
        return false;
      }

    }

    /// <summary>
    /// Metodo que agrega una actividad existente a un itinerario existente
    /// </summary>
    /// <param name="it">itinerario al cual se le agrega la actividad</param>
    /// <param name="ac">actividad a agregar en el itinerario</param>
    /// <returns>true si se agrego la actividad exitosamente, false en caso de error</returns>
    [HttpGet]
    public Boolean AgregarActividad_It(Itinerario it, Actividad ac)
    {
      try
      {
        ConexionBase con = new ConexionBase();
        con.Conectar();
        NpgsqlCommand comm = new NpgsqlCommand("add_actividad_it", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, ac.Id);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.Id);
        NpgsqlDataReader pgread = comm.ExecuteReader();
        pgread.Read();
        Boolean resp = pgread.GetBoolean(0);
        con.Desconectar();
        return resp;
      }
      catch (NpgsqlException e)
      {
        return false;
      }

    }

    /// <summary>
    /// Metodo que agrega un lugar turistico existente a un itinerario existente
    /// </summary>
    /// <param name="it">itinerario al cual se le agrega el lugar turistico</param>
    /// <param name="lt">lugar turistico a agregar en el itinerario</param>
    /// <returns>true si se agrego el lugar turistico exitosamente, false en caso de error</returns>
    [HttpGet]
    public Boolean AgregarLugar_It(Itinerario it, LugarTuristico lt)
    {
      try
      {
        ConexionBase con = new ConexionBase();
        con.Conectar();
        NpgsqlCommand comm = new NpgsqlCommand("add_lugar_it", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, lt.Id);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.Id);
        NpgsqlDataReader pgread = comm.ExecuteReader();
        pgread.Read();
        Boolean resp = pgread.GetBoolean(0);
        con.Desconectar();
        return resp;
      }
      catch (NpgsqlException e)
      {
        return false;
      }

    }

    /// <summary>
    /// Metodo que elimina un evento existente de un itinerario existente
    /// </summary>
    /// <param name="it">itinerario del cual se elimina el evento</param>
    /// <param name="ev">evento a eliminar del itinerario</param>
    /// <returns>true si se elimino el evento exitosamente, false en caso de error</returns>
    [HttpGet]
    public Boolean EliminarEvento_It(Itinerario it, Evento ev)
    {
      try
      {
        ConexionBase con = new ConexionBase();
        con.Conectar();
        NpgsqlCommand comm = new NpgsqlCommand("del_evento_it", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, ev.Ev_id);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.Id);
        NpgsqlDataReader pgread = comm.ExecuteReader();
        pgread.Read();
        Boolean resp = pgread.GetBoolean(0);
        con.Desconectar();
        return resp;
      }
      catch (NpgsqlException e)
      {
        return false;
      }

    }

    /// <summary>
    /// Metodo que elimina una actividad existente de un itinerario existente
    /// </summary>
    /// <param name="it">itinerario del cual se elimina la actividad</param>
    /// <param name="ac">actividad a eliminar del itinerario</param>
    /// <returns>true si se elimino la actividad exitosamente, false en caso de error</returns>
    [HttpGet]
    public Boolean EliminarActividad_It(Itinerario it, Actividad ac)
    {
      try
      {
        ConexionBase con = new ConexionBase();
        con.Conectar();
        NpgsqlCommand comm = new NpgsqlCommand("del_actividad_it", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, ac.Id);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.Id);
        NpgsqlDataReader pgread = comm.ExecuteReader();
        pgread.Read();
        Boolean resp = pgread.GetBoolean(0);
        con.Desconectar();
        return resp;
      }
      catch (NpgsqlException e)
      {
        return false;
      }

    }

    /// <summary>
    /// Metodo que elimina un lugar turistico existente de un itinerario existente
    /// </summary>
    /// <param name="it">itinerario del cual se elimina el lugar turistico</param>
    /// <param name="lt">lugar turistico a eliminar del itinerario</param>
    /// <returns>true si se elimino el lugar turistico exitosamente, false en caso de error</returns>
    [HttpGet]
    public Boolean EliminarLugar_It(Itinerario it, LugarTuristico lt)
    {
      try
      {
        ConexionBase con = new ConexionBase();
        con.Conectar();
        NpgsqlCommand comm = new NpgsqlCommand("del_lugar_it", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, lt.Id);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.Id);
        NpgsqlDataReader pgread = comm.ExecuteReader();
        pgread.Read();
        Boolean resp = pgread.GetBoolean(0);
        con.Desconectar();
        return resp;
      }
      catch (NpgsqlException e)
      {
        return false;
      }

    }
  }
}
