using System;
using System.Web.Http;
using Npgsql;
using System.Data;
using ApiRest_COCO_TRIP.Models.M5;
namespace ApiRest_COCO_TRIP.Controllers
{
  public class M5Controller : ApiController
  {
    
    [HttpGet]
    public Boolean AgregarItinerario(Itinerario it)
    {
      try
      {
        string stringcon = "Server=127.0.0.1;User Id=postgres; " + "Password=20977974pg;Database=cocotrip;";
        NpgsqlConnection conn = new NpgsqlConnection(stringcon);
        conn.Open();
        NpgsqlCommand comm = new NpgsqlCommand("add_itinerario", conn);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, it.It_nombre);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, it.It_fechaInicio);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, it.It_fechaFin);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.It_idUsuario);
        NpgsqlDataReader pgread = comm.ExecuteReader();
        pgread.Read();
        return pgread.GetBoolean(0);
        conn.Close();
      }
      catch(NpgsqlException e)
      {
        return false;
      }
      
    }
  }
}
