using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using ApiRest_COCO_TRIP.Models.Dato;

namespace ApiRest_COCO_TRIP.Models
{
  public class PeticionItinerario
  {
    private ConexionBase con;
    private NpgsqlDataReader pgread;
    private NpgsqlCommand comm;

        public PeticionItinerario()
        {
          con = new ConexionBase();
        }


        public List<Itinerario> ConsultarItinerarios(int id_usuario)
        {
            List<Itinerario> itinerarios = new List<Itinerario>(); // Lista de itinerarios de un usuario
            try
            {
                con = new ConexionBase();
                con.Conectar();
                comm = new NpgsqlCommand("consultar_itinerarios", con.SqlConexion);
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
                        itinerarios[itinerarios.Count - 1].Items_agenda.Add(lugar);
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
                        itinerarios[itinerarios.Count - 1].Items_agenda.Add(actividad);
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

        /// <summary>
        /// Metodo que elimina un lugar turistico existente de un itinerario existente
        /// </summary>
        /// <param name="it">itinerario del cual se elimina el lugar turistico</param>
        /// <param name="lt">lugar turistico a eliminar del itinerario</param>
        /// <returns>true si se elimino el lugar turistico exitosamente, false en caso de error</returns>
        public Boolean EliminarItem_It(Itinerario it, Agenda ag)
        {
          try
          {
            con = new ConexionBase();
            con.Conectar();
            comm = new NpgsqlCommand("del_lugar_it", con.SqlConexion);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, ag.Id);
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.Id);
            pgread = comm.ExecuteReader();
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
        public Boolean AgregarLugar_It(Itinerario it, LugarTuristico lt)
        {
          try
          {
            con = new ConexionBase();
            con.Conectar();
            comm = new NpgsqlCommand("add_lugar_it", con.SqlConexion);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, lt.Id);
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.Id);
            pgread = comm.ExecuteReader();
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
        public Boolean AgregarActividad_It(Itinerario it, Actividad ac)
        {
          try
          {
            con = new ConexionBase();
            con.Conectar();
            comm = new NpgsqlCommand("add_actividad_it", con.SqlConexion);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, ac.Id);
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.Id);
            pgread = comm.ExecuteReader();
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
     /* public Boolean AgregarEvento_It(Itinerario it,Evento ev)
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
        }*/

        /// <summary>returns
        /// Metodo que agrega en la base de datos un nuevo itinerario
        /// </summary>
        /// <param name="it">el itinerario a agregar</param>
        /// <returns>true si agrega existosamente, false en caso de error</>
        public Itinerario AgregarItinerario(Itinerario it)
        {
          try
          {
            con = new ConexionBase();
            con.Conectar();
            comm = new NpgsqlCommand("add_itinerario", con.SqlConexion);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, it.Nombre);
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.IdUsuario);
            pgread = comm.ExecuteReader();
            pgread.Read();
            it.Id = pgread.GetInt16(0);
            it.Nombre = pgread.GetString(1);
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
        public Boolean EliminarItinerario(int id)
        {
            try
            {
                con = new ConexionBase();
                con.Conectar();
                comm = new NpgsqlCommand("del_itinerario", con.SqlConexion);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, id);
                pgread = comm.ExecuteReader();
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
        public Boolean ModificarItinerario(Itinerario it)
        {
          try
          {
            con = new ConexionBase();
            con.Conectar();
            comm = new NpgsqlCommand("mod_itinerario", con.SqlConexion);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.Id);
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, it.Nombre);
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, it.FechaInicio);
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, it.FechaFin);
            pgread = comm.ExecuteReader();
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
