using System;
using System.Web.Http;
using Npgsql;
using System.Data;
using ApiRest_COCO_TRIP.Models.M5;
using ApiRest_COCO_TRIP.Models;
namespace ApiRest_COCO_TRIP.Controllers
{
  public class M5Controller : ApiController
  {
    /// <summary>
    /// Metodo que agrega en la base de datos un nuevo itinerario
    /// </summary>
    /// <param name="it">el itinerario a agregar</param>
    /// <returns>true si agrega existosamente, false en caso de error</returns>
    [HttpGet]
    public Boolean AgregarItinerario(Itinerario it)
    {
      try
      {
        ConexionBase con = new ConexionBase();
        con.Conectar();
        NpgsqlCommand comm = new NpgsqlCommand("add_itinerario", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, it.It_nombre);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, it.It_fechaInicio);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, it.It_fechaFin);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.It_idUsuario);
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
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.It_id);
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
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.It_id);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, it.It_nombre);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, it.It_fechaInicio);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, it.It_fechaFin);
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
    /// <param name="it"></param>
    /// <returns></returns>
    [HttpGet]
    public Boolean AgregarEvento_It(Itinerario it,Evento ev)
    {
      try
      {
        ConexionBase con = new ConexionBase();
        con.Conectar();
        NpgsqlCommand comm = new NpgsqlCommand("add_evento_it", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.It_id);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, ev.Id);
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

    }//incompleto

  }
}
