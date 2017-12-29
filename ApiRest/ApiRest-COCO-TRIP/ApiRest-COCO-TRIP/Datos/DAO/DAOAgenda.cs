using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using Npgsql;
using System.Data;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
  public class DAOAgenda : DAO
  {
    private NpgsqlDataReader respuesta;
    private NpgsqlCommand comando;
    private Agenda agenda;
    
    public override void Actualizar(Entidad objeto)
    {
      throw new NotImplementedException();
    }

    public override List<Entidad> ConsultarLista(Entidad objeto)
    {
      throw new NotImplementedException();
    }

    public override Entidad ConsultarPorId(Entidad objeto)
    {
      throw new NotImplementedException();
    }

    public override void Eliminar(Entidad objeto)
    {
      try
      {
        agenda = (Agenda)objeto;
        string tipo = "";
        base.Conectar();
        comando = new NpgsqlCommand("del_item_it", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        if (agenda.IdEvento != 0)
        {
          tipo = "Evento";
          comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, tipo);
          comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, agenda.IdEvento);
        }
        else if (agenda.IdActividad != 0)
        {
          tipo = "Actividad";
          comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, tipo);
          comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, agenda.IdActividad);
        }
        else if (agenda.IdItinerario != 0)
        {
          tipo = "Lugar Turistico";
          comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, tipo);
          comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, agenda.IdLugarTuristico);
        }
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, agenda.IdItinerario);
        respuesta = comando.ExecuteReader();
        respuesta.Read();
        Boolean resp = respuesta.GetBoolean(0);
        base.Desconectar();
      }
      catch (NpgsqlException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (InvalidCastException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        base.Desconectar();
        throw e;
      }
    }

    public override void Insertar(Entidad objeto)
    {
      try
      {
        base.Conectar();
        agenda = (Agenda)objeto;
        if (agenda.IdLugarTuristico != 0)
          {
          comando = new NpgsqlCommand("add_lugar_it", base.SqlConexion);
          comando.CommandType = CommandType.StoredProcedure;
          comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, agenda.IdLugarTuristico);
        }
        if (agenda.IdActividad != 0)
          {
          comando = new NpgsqlCommand("add_actividad_it", base.SqlConexion);
          comando.CommandType = CommandType.StoredProcedure;
          comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, agenda.IdActividad);

        }
        if (agenda.IdEvento != 0)
          {
          comando = new NpgsqlCommand("add_evento_it", base.SqlConexion);
          comando.CommandType = CommandType.StoredProcedure;

          comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, agenda.IdEvento);
        }
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, agenda.IdItinerario);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, agenda.FechaInicio);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, agenda.FechaFin);
        respuesta = comando.ExecuteReader();
        respuesta.Read();
        Boolean resp = respuesta.GetBoolean(0);
        base.Desconectar(); 
      }
      catch (NpgsqlException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        base.Desconectar();
        throw e;
      }
    }
  }
}
