using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Models
{
  public class PeticionEvento
  {
    private  ConexionBase conexion;
    private  NpgsqlDataReader read;
    private  NpgsqlCommand comando;

    public PeticionEvento()
    {
      conexion.Conectar();
    }

    public  bool AgregarEvento(Evento evento)
    {
      
        comando = new NpgsqlCommand("Add_evento", conexion.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        //Aqui registro los valores
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, evento.Nombre);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, evento.Descripcion);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Double, evento.Precio);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, evento.FechaInicio);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, evento.FechaFin);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Timestamp, evento.HoraInicio);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Timestamp, evento.HoraFin);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, evento.Foto);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, evento.IdCategoria);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, evento.IdLocalidad);
        read = comando.ExecuteReader();
        read.Read();
      //Logica para retornar hablar con noe
      return false;
    }

    internal static bool EliminarEvento(int v, int id)
    {
      throw new NotImplementedException();
    }

    internal static Evento ConsultarEvento(int id)
    {
      throw new NotImplementedException();
    }

    internal static bool EliminarEvento(int id)
    {
      throw new NotImplementedException();
    }

    internal static List<Evento> ListaEventosPorCategoria(int id_categoria)
    {
      throw new NotImplementedException();
    }

  }
}
