using System;
using System.Collections.Generic;
using Npgsql;
using System.Data;
using ApiRest_COCO_TRIP.Models.Dato;

namespace ApiRest_COCO_TRIP.Models
{
  public class PeticionBusqueda
  {
    private ConexionBase conexion;

    public PeticionBusqueda()
    {
      conexion = new ConexionBase();
    }

    /// <summary>
    /// Consulta los eventos por nombre, o similiares.
    /// </summary>
    /// <param name="busqueda">Palabra cuyo similitud se busca en el nombre del evento que se esta buscando.</param>
    /// <returns></returns>
    public List<Evento> ConsultarEventos(string busqueda)
    {
      List<Evento> list_eventos = new List<Evento>();
      try
      {
        conexion = new ConexionBase();
        conexion.Conectar();
        NpgsqlCommand comm = new NpgsqlCommand("consultar_eventos", conexion.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, busqueda);
        NpgsqlDataReader pgread = comm.ExecuteReader();

        //Se recorre los registros devueltos.
        while (pgread.Read())
        {
          Evento evento = new Evento(pgread.GetInt32(0), pgread.GetString(1));
          list_eventos.Add(evento);
        }

        conexion.Desconectar();
        return list_eventos;
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
    }

    public List<LugarTuristico> ConsultarLugarTuristico(string busqueda)
    {
      List<LugarTuristico> list_lugaresturisticos = new List<LugarTuristico>();
      try
      {
        conexion = new ConexionBase();
        conexion.Conectar();
        NpgsqlCommand comm = new NpgsqlCommand("consultar_lugarturistico", conexion.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, busqueda);
        NpgsqlDataReader pgread = comm.ExecuteReader();

        //Se recorre los registros devueltos.
        while (pgread.Read())
        {
          LugarTuristico lugarTuristico = new LugarTuristico();
          lugarTuristico.Id = pgread.GetInt32(0);
          lugarTuristico.Nombre = pgread.GetString(1);

          list_lugaresturisticos.Add(lugarTuristico);
        }

        conexion.Desconectar();
        return list_lugaresturisticos;
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
    }

    public List<Actividad> ConsultarActividades(string busqueda)
    {
      List<Actividad> list_actividades = new List<Actividad>();
      try
      {
        conexion = new ConexionBase();
        conexion.Conectar();
        NpgsqlCommand comm = new NpgsqlCommand("consultar_actividades", conexion.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, busqueda);
        NpgsqlDataReader pgread = comm.ExecuteReader();

        //Se recorre los registros devueltos.
        while (pgread.Read())
        {
          Actividad actividad = new Actividad();
          actividad.Id = pgread.GetInt32(0);
          actividad.Nombre = pgread.GetString(1);

          list_actividades.Add(actividad);
        }

        conexion.Desconectar();
        return list_actividades;
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
    }
  }
}
