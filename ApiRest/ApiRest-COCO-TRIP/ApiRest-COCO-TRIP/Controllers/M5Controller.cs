using System;
using System.Web.Http;
using Npgsql;
using System.Data;
using ApiRest_COCO_TRIP.Models.M5;
namespace ApiRest_COCO_TRIP.Controllers
{
  public class M5Controller : ApiController
  {
    string stringcon;
    NpgsqlConnection conn;

    [HttpGet]
    public Boolean AgregarItinerario(Itinerario it)
    {
      try
      {
        stringcon = "Server=127.0.0.1;User Id=postgres; " + "Password=20977974pg;Database=cocotrip;";
        conn = new NpgsqlConnection(stringcon);
        conn.Open();
        NpgsqlCommand comm = new NpgsqlCommand("add_itinerario", conn);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, it.It_nombre);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, it.It_fechaInicio);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, it.It_fechaFin);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.It_idUsuario);
        NpgsqlDataReader pgread = comm.ExecuteReader();
        pgread.Read();
        Boolean resp = pgread.GetBoolean(0);
        conn.Close();
        return resp;
      }
      catch (NpgsqlException e)
      {
        return false;
      }

    }
  }
}
