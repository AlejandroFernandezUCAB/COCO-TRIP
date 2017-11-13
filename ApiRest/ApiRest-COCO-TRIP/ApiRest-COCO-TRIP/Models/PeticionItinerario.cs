using Npgsql;
using System;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Web.Http.Cors;
using System.Collections.Generic;
using System.Data;
using ApiRest_COCO_TRIP.Models.Dato;
using System.Linq;
using System.Linq.Expressions;

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
              con.Desconectar();
              throw sql;
            }catch (ArgumentException arg)
            {
              con.Desconectar();
              throw arg;
            }
            catch (InvalidCastException cast)
            {
              con.Desconectar();
              throw cast;
            }
        }




    public Boolean setVisible(int idusuario, int iditinerario, Boolean visible)
    {
      Boolean visible_sql = false;
      try
      {
        con = new ConexionBase();
        con.Conectar();
        comm = new NpgsqlCommand("setVisible", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idusuario);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, iditinerario);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Boolean, visible);
        pgread = comm.ExecuteReader();
        pgread.Read();
        visible_sql = pgread.GetBoolean(0);
        con.Desconectar();
        return visible_sql;
      }
      catch (NpgsqlException sql)
      {
        con.Desconectar();
        throw sql;
      }
      catch (InvalidCastException cast)
      {
        con.Desconectar();
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
        public Boolean AgregarItem_It(string tipo, int idit, int iditem,DateTime fechaini, DateTime fechafin)
        {
          try
          {
            con = new ConexionBase();
            con.Conectar();
            if (tipo == "Lugar Turistico")
            {
              comm = new NpgsqlCommand("add_lugar_it", con.SqlConexion);
            }
            if (tipo == "Actividad")
            {
              comm = new NpgsqlCommand("add_actividad_it", con.SqlConexion);
            }
            if (tipo == "Evento")
            {
              comm = new NpgsqlCommand("add_evento_it", con.SqlConexion);
            }
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, iditem);
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
              con.Desconectar();
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

    /// <summary>
    /// Clase provisional para buscar usuario.
    /// </summary>
    /// <param name="id_usuario">id del usuario</param>
    /// <returns>El usuario cuyo id sea igual al parametro</returns>
    public Usuario Buscar_Usuario(int id_usuario)
    {
      Usuario Usuario = new Usuario();
      try
      {
        con = new ConexionBase();
        con.Conectar();
        comm = new NpgsqlCommand("consultarusuariosoloid", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, id_usuario);
        NpgsqlDataReader pgread = comm.ExecuteReader();

        //Se recorre los registros devueltos.
        if(pgread.Read())
        {
          Usuario.NombreUsuario = pgread.GetString(0);
          Usuario.Correo = pgread.GetString(1);
        }
        con.Desconectar();
        return Usuario;

      }
      catch (NpgsqlException e)
      {
        throw e;
      }
    }

    public List<Itinerario> ConsultarItinerariosCorreo(int id_usuario)
    {
      List<Itinerario> itinerarios = new List<Itinerario>(); // Lista de itinerarios de un usuario
      try
      {
        con = new ConexionBase();
        con.Conectar();
        comm = new NpgsqlCommand("consultar_itinerarios_correo", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, id_usuario);
        NpgsqlDataReader pgread = comm.ExecuteReader();
        int auxiliar = 0;
        //Recorremos los registros devueltos
        while (pgread.Read())
        {
          Itinerario iti;
          if (!pgread.IsDBNull(2) && (!pgread.IsDBNull(3)))
          {
            iti = new Itinerario(pgread.GetInt32(0), pgread.GetString(1), pgread.GetDateTime(2), pgread.GetDateTime(3), id_usuario, true);
          }
          else
          {
            iti = new Itinerario(pgread.GetInt32(0), pgread.GetString(1), id_usuario, true);
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
          if (!pgread.IsDBNull(4))
          {
            dynamic lugar = new System.Dynamic.ExpandoObject();
            lugar.Nombre = pgread.GetString(4);
            lugar.Descripcion = pgread.GetString(5);
            lugar.Tipo = "Lugar Turistico";
            /*
            if ((!pgread.IsDBNull(5)) && (!pgread.IsDBNull(6)))
            {
              lugar.FechaInicio = pgread.GetDateTime(5);
              lugar.FechaFin = pgread.GetDateTime(6);
            }
            */
            itinerarios[itinerarios.Count - 1].Items_agenda.Add(lugar);
          }

          //Si existe actividad en este registro
          if (!pgread.IsDBNull(6))
          {
            dynamic actividad = new System.Dynamic.ExpandoObject();
            actividad.Nombre = pgread.GetString(6);
            actividad.Descripcion = pgread.GetString(7);
            actividad.Tipo = "Actividad";
            /*
            if ((!pgread.IsDBNull(5)) && (!pgread.IsDBNull(6)))
            {
              actividad.FechaInicio = pgread.GetDateTime(5);
              actividad.FechaFin = pgread.GetDateTime(6);
            }
            */
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
      }
      catch (ArgumentException arg)
      {
        throw arg;
      }
      catch (InvalidCastException cast)
      {
        throw cast;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="datos"></param>
    /// <returns></returns>
    public string EnviarCorreo(int id_usuario)
    {
      Usuario usuario;
      List<Itinerario> itinerarios; // Lista de itinerarios de un usuario
      string Body_Correo = "";
      //PeticionLogin peticion;
      //peticion = new PeticionLogin();

      string lugarturistico = "l";
      string actividad = "a";
      string evento = "e";

      try
      {
        usuario = Buscar_Usuario(id_usuario);
        usuario.Id = id_usuario;
        itinerarios = ConsultarItinerariosCorreo(id_usuario);

        foreach (Itinerario itinerario in itinerarios)
        {
          foreach (dynamic objeto in itinerario.Items_agenda)
          {
            switch (objeto.Tipo) {
              case "Lugar Turistico":
                lugarturistico = lugarturistico + objeto.Nombre + objeto.Descripcion;
                break;

              case "Actividad":
                actividad = actividad + objeto.Nombre + objeto.Descripcion; ;
                break;

              case "Evento":
                evento = evento + objeto.Nombre + objeto.Descripcion;
                break;
            }
          }
        }
        
        Body_Correo = "Contenido de su itinerario /n" +
          lugarturistico + "/n" +
          actividad + "/n" +
          evento + "/n";

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        mail.From = new MailAddress("cocotrip17@gmail.com");
        mail.To.Add(usuario.Correo);
        mail.Subject = "Recordatorio de itinerario CocoTrip.";
        mail.Body = Body_Correo;

        SmtpServer.Port = 587;
        SmtpServer.Credentials = new System.Net.NetworkCredential("cocotrip17", "arepascocotrip");
        SmtpServer.EnableSsl = true;

        SmtpServer.Send(mail);

      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return "Exitoso";
    }

  }
}
