using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Models.M7.Dato;
using Npgsql;
using System.Data;

namespace ApiRest_COCO_TRIP.Models
{
   public class Itinerario
   {
     int id;
     int idUsuario;
     string nombre;
     DateTime fechaInicio;
     DateTime fechaFin;
     List<LugarTuristico> lugares= new List<LugarTuristico>();
     List<Actividad> actividades = new List<Actividad>();
    //List<Evento> eventos = new List<Evento>();
    public int Id { get => id; set => id = value; }
    public int IdUsuario { get => idUsuario; set => idUsuario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
    public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
    public List<LugarTuristico> Lugares { get => lugares; set => lugares = value; }
    public List<Actividad> Actividades { get => actividades; set => actividades = value; }
    
    public Itinerario()
    {
      id = new int();
      idUsuario = new int();
      nombre = "";
      fechaInicio = new DateTime();
      fechaFin = new DateTime();
    }
    public Itinerario(int id, string nombre, DateTime fechainicio, DateTime fechafin, int idusuario)
    {
      this.id = id;
      this.nombre = nombre;
      fechaInicio = fechainicio;
      fechaFin = fechafin;
      idUsuario = idusuario;
    }

    public Itinerario(string nombre, DateTime fechainicio, DateTime fechafin, int idusuario)
    {
      this.nombre = nombre;
      fechaInicio = fechainicio;
      fechaFin = fechafin;
      idUsuario = idusuario;
    }
    public Itinerario(int id)
    {
      this.id = id;
    }


    public List<Itinerario> ConsultarItinerarios(int id_usuario)
    { 
      List<Itinerario> itinerarios = new List<Itinerario>(); // Lista de itinerarios de un usuario
      try
      {
        ConexionBase con = new ConexionBase();
        con.Conectar();
        NpgsqlCommand comm = new NpgsqlCommand("consultar_itinerarios", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, id_usuario);
        NpgsqlDataReader pgread = comm.ExecuteReader();
        int auxiliar = 0;
        //Recorremos los registros devueltos
        while (pgread.Read())
        { 
          Itinerario iti = new Itinerario(pgread.GetInt32(0), pgread.GetString(2), pgread.GetDateTime(3), pgread.GetDateTime(4), pgread.GetInt32(1));

          //Se revisa si el registro de itinerario en la base ya se encuentra en la lista de itinerarios del usuario
          if (itinerarios.Count == 0) itinerarios.Add(iti);
          foreach (Itinerario itinerario in itinerarios)
          {
            if (itinerario.Id == iti.Id) auxiliar = 1;
          }
          if (auxiliar != 1) itinerarios.Add(iti);
          auxiliar = 0;

          //Agregamos los eventos, actividades y lugares a la lista correspondiente
          //Si existe lugar turistico en este registro
          if (!pgread.IsDBNull(5))
          {
            LugarTuristico lugar = new LugarTuristico();
            lugar.Id = pgread.GetInt32(5);
            lugar.Nombre = pgread.GetString(6);
            lugar.Descripcion = pgread.GetString(7);
            lugar.Costo = pgread.GetDouble(8);
            itinerarios[itinerarios.Count - 1].Lugares.Add(lugar);
          }
          //Si existe actividad en este registro
          if (!pgread.IsDBNull(9))
          {
            Actividad actividad = new Actividad
            {
              Id = pgread.GetInt32(9),
              Nombre = pgread.GetString(10),
              Descripcion = pgread.GetString(11),
              Duracion = pgread.GetTimeSpan(12)
            };
            itinerarios[itinerarios.Count - 1].Actividades.Add(actividad);
          }
        }
        con.Desconectar();
        return itinerarios;
      }
      catch (NpgsqlException e)
      {
        throw e;
      }

    }
  }

}
