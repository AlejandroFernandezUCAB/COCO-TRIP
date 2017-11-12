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
                  Itinerario iti;
                  if (!pgread.IsDBNull(3) && (!pgread.IsDBNull(4)))
                  {
                    iti = new Itinerario(pgread.GetInt32(0), pgread.GetString(2), pgread.GetDateTime(3), pgread.GetDateTime(4), pgread.GetInt32(1), true);
                  }
                  else
                  {
                    iti = new Itinerario(pgread.GetInt32(0), pgread.GetString(2), pgread.GetInt32(1), true);
                  }
                    
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
                    if (!pgread.IsDBNull(7))
                    {
                        dynamic lugar = new System.Dynamic.ExpandoObject();
                        lugar.Id = pgread.GetInt32(7);
                        lugar.Nombre = pgread.GetString(8);
                        lugar.Descripcion = pgread.GetString(9);
                        lugar.Costo = pgread.GetDouble(10);
                        lugar.Tipo = "Lugar Turistico";
                        if ((!pgread.IsDBNull(5)) && (!pgread.IsDBNull(6)))
                        {
                            lugar.FechaInicio = pgread.GetDateTime(5);
                            lugar.FechaFin = pgread.GetDateTime(6);
                        }
                        itinerarios[itinerarios.Count - 1].Items_agenda.Add(lugar);
                    }
                    //Si existe actividad en este registro
                    if (!pgread.IsDBNull(11))
                    {
                      dynamic actividad = new System.Dynamic.ExpandoObject();
                      actividad.Id = pgread.GetInt32(11);
                      actividad.Nombre = pgread.GetString(12);
                      actividad.Descripcion = pgread.GetString(13);
                      actividad.Duracion = pgread.GetTimeSpan(14);
                      actividad.Tipo = "Actividad";
                      if ((!pgread.IsDBNull(5)) && (!pgread.IsDBNull(6)))
                      {
                        actividad.FechaInicio = pgread.GetDateTime(5);
                        actividad.FechaFin = pgread.GetDateTime(6);
                      }
                      itinerarios[itinerarios.Count - 1].Items_agenda.Add(actividad);
                    }
                    //Falta el caso de que sea un evento...
                }
                con.Desconectar();
                return itinerarios;
            }
            catch (NpgsqlException sql)
            {
                throw sql;
            }catch (ArgumentException arg)
            {
              throw arg;
            }
            catch (InvalidCastException cast)
            {
                throw cast;
            }
        }

    /// <summary>
    /// Metodo que elimina un item existente de un itinerario existente
    /// </summary>
    /// <param name="it">item del cual se elimina el lugar turistico</param>
    /// <param name="lt">item a eliminar del itinerario</param>
    /// <returns>true si se elimino el item exitosamente, false en caso de error</returns>
    public Boolean EliminarItem_It(string tipo,int idit, int iditem)
        {
          try
          {
            con = new ConexionBase();  
            con.Conectar();
            comm = new NpgsqlCommand("del_item_it", con.SqlConexion);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, tipo);
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, iditem);
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idit);
            pgread = comm.ExecuteReader();
            pgread.Read();
            Boolean resp = pgread.GetBoolean(0);
            con.Desconectar();
            return resp;
          }
          catch (NpgsqlException e)
          {
            con.Desconectar();
            throw e;
          }
          catch (InvalidCastException e)
          {
            con.Desconectar();
            throw e;
          }
    }


        /// <summary>
        /// Metodo que agrega un lugar turistico existente a un itinerario existente
        /// </summary>
        /// <param name="it">itinerario al cual se le agrega el lugar turistico</param>
        /// <param name="lt">lugar turistico a agregar en el itinerario</param>
        /// <returns>true si se agrego el lugar turistico exitosamente, false en caso de error</returns>
        public Boolean AgregarLugar_It(int idit, int idlt,DateTime fechaini, DateTime fechafin)
        {
          try
          {
            con = new ConexionBase();
            con.Conectar();
            comm = new NpgsqlCommand("add_lugar_it", con.SqlConexion);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idlt);
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idit);
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, fechaini);
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, fechafin);
            pgread = comm.ExecuteReader();
            pgread.Read();
            Boolean resp = pgread.GetBoolean(0);
            con.Desconectar();
            return resp;
          }
          catch (NpgsqlException e)
          {
            throw e;
          }
        }

        /// <summary>
        /// Metodo que agrega una actividad existente a un itinerario existente
        /// </summary>
        /// <param name="it">itinerario al cual se le agrega la actividad</param>
        /// <param name="ac">actividad a agregar en el itinerario</param>
        /// <returns>true si se agrego la actividad exitosamente, false en caso de error</returns>
        public Boolean AgregarActividad_It(int idit, int idac,DateTime fechaini, DateTime fechafin)
    {
          try
          {
            con = new ConexionBase();
            con.Conectar();
            comm = new NpgsqlCommand("add_actividad_it", con.SqlConexion);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idac);
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idit);
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, fechaini);
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, fechafin);
            pgread = comm.ExecuteReader();
            pgread.Read();
            Boolean resp = pgread.GetBoolean(0);
            con.Desconectar();
            return resp;
          }
          catch (NpgsqlException e)
          {
            throw e;
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
            con.Desconectar();
            throw e;
          }
          catch (InvalidCastException e)
          {
            con.Desconectar();
            throw e;
          }
          catch (NullReferenceException e)
          {
            con.Desconectar();
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
                comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer,id);
                pgread = comm.ExecuteReader();
                pgread.Read();
                Boolean resp = pgread.GetBoolean(0);
                con.Desconectar();
                return resp;
            }
            catch (NpgsqlException e)
            {
              throw e;
            }

        }

        /// <summary>
        /// Metodo que modifica un itinerario de la base de datos
        /// </summary>
        /// <param name="it">el itinerario a modificar</param>
        /// <returns>true si modifica existosamente, false en caso de error</returns>
        public Itinerario ModificarItinerario(Itinerario it)
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
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, it.IdUsuario);
            pgread = comm.ExecuteReader();
            pgread.Read();
            con.Desconectar();
            return it;
          }
          catch (NpgsqlException e)
          {
            con.Desconectar();
            throw e;
          }
          catch (InvalidCastException e)
          {
            con.Desconectar();
            throw e;
          }
    }

        /// <summary>
        /// Consulta los eventos por nombre, o similiares.
        /// </summary>
        /// <param name="busqueda">Palabra cuya similitud se busca en el nombre del evento que se esta buscando.</param>
        /// <returns>Retorna una lista con los eventos que tengan coincidencia.</returns>
     /* public List<Evento> ConsultarEventos(string busqueda)
        {
          List<Evento> list_eventos = new List<Evento>();
          try
          {
            con = new ConexionBase();
            con.Conectar();
            comm = new NpgsqlCommand("consultar_eventos", con.SqlConexion);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, busqueda);
            pgread = comm.ExecuteReader();

            //Se recorre los registros devueltos.
            while (pgread.Read())
            {
              Evento evento = new Evento(pgread.GetInt32(0), pgread.GetString(1));
              list_eventos.Add(evento);
            }

            con.Desconectar();
            return list_eventos;
          }
          catch (NpgsqlException e)
          {
            throw e;
          }
        } */

    /// <summary>
    /// Consulta los lugares turisticos por nombre, o similiares.
    /// </summary>
    /// <param name="busqueda">Palabra cuya similitud se busca en el nombre del lugar turistico que se esta buscando.</param>
    /// <returns>Retorna una lista con los lugares turisticos que tengan coincidencia.</returns>
    public List<LugarTuristico> ConsultarLugarTuristico(string busqueda)
        {
          List<LugarTuristico> list_lugaresturisticos = new List<LugarTuristico>();
          try
          {
            con = new ConexionBase();
            con.Conectar();
            comm = new NpgsqlCommand("consultar_lugarturistico", con.SqlConexion);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, busqueda);
            pgread = comm.ExecuteReader();

            //Se recorre los registros devueltos.
            while (pgread.Read())
            {
              LugarTuristico lugarTuristico = new LugarTuristico();
              lugarTuristico.Id = pgread.GetInt32(0);
              lugarTuristico.Nombre = pgread.GetString(1);

              list_lugaresturisticos.Add(lugarTuristico);
            }

            con.Desconectar();
            return list_lugaresturisticos;
          }
          catch (NpgsqlException e)
          {
            throw e;
          }
        }

    /// <summary>
    ///  Consulta los lugares turisticos por nombre, o similiares.
    /// </summary>
    /// <param name="busqueda">Palabra cuya similitud se busca en el nombre de la actividad que se esta buscando.</param>
    /// <returns>Retorna una lista con las actividades que tengan coincidencia.</returns>
    public List<Actividad> ConsultarActividades(string busqueda)
        {
          List<Actividad> list_actividades = new List<Actividad>();
          try
          {
            con = new ConexionBase();
            con.Conectar();
            comm = new NpgsqlCommand("consultar_actividades", con.SqlConexion);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, busqueda);
            pgread = comm.ExecuteReader();

            //Se recorre los registros devueltos.
            while (pgread.Read())
            {
              Actividad actividad = new Actividad();
              actividad.Id = pgread.GetInt32(0);
              actividad.Nombre = pgread.GetString(1);

              list_actividades.Add(actividad);
            }

            con.Desconectar();
            return list_actividades;
          }
          catch (NpgsqlException e)
          {
            throw e;
          }
        }

  }
}
