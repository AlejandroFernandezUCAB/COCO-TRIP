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

    /// <summary>
    /// Metodo para consultar todos los itinerarios, con sus respectivos eventos, de un usuario
    /// </summary>
    /// <param name="id_usuario">id del usuario al cual se le consultara sus itinerarios</param>
    /// <returns>Lista de itinerarios con sus respectivos items</returns>
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
                    iti = new Itinerario(pgread.GetInt32(0), pgread.GetString(2), pgread.GetDateTime(3), pgread.GetDateTime(4), pgread.GetInt32(1), pgread.GetBoolean(7));
                  }
                  else
                  {
                    iti = new Itinerario(pgread.GetInt32(0), pgread.GetString(2), pgread.GetInt32(1), pgread.GetBoolean(7));
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
                    if (!pgread.IsDBNull(8))
                    {
                        dynamic lugar = new System.Dynamic.ExpandoObject();
                        lugar.Id = pgread.GetInt32(8);
                        lugar.Nombre = pgread.GetString(9);
                        lugar.Descripcion = pgread.GetString(10);
                        lugar.Costo = pgread.GetDouble(11);
                        lugar.Tipo = "Lugar Turistico";
                        if ((!pgread.IsDBNull(5)) && (!pgread.IsDBNull(6)))
                        {
                            lugar.FechaInicio = pgread.GetDateTime(5);
                            lugar.FechaFin = pgread.GetDateTime(6);
                        }
                        itinerarios[itinerarios.Count - 1].Items_agenda.Add(lugar);
                    }
                    //Si existe actividad en este registro
                    if (!pgread.IsDBNull(12))
                    {
                      dynamic actividad = new System.Dynamic.ExpandoObject();
                      actividad.Id = pgread.GetInt32(12);
                      actividad.Nombre = pgread.GetString(13);
                      actividad.Descripcion = pgread.GetString(14);
                      actividad.Duracion = pgread.GetTimeSpan(15);
                      actividad.Foto = pgread.GetString(16);
                      actividad.Tipo = "Actividad";
                      if ((!pgread.IsDBNull(5)) && (!pgread.IsDBNull(6)))
                      {
                        actividad.FechaInicio = pgread.GetDateTime(5);
                        actividad.FechaFin = pgread.GetDateTime(6);
                      }
                      itinerarios[itinerarios.Count - 1].Items_agenda.Add(actividad);
                    }
                    //Si existe evento en este registro
                    if (!pgread.IsDBNull(17))
                    {
                      dynamic evento = new System.Dynamic.ExpandoObject();
                      evento.Id = pgread.GetInt32(17);
                      evento.Nombre = pgread.GetString(18);
                      evento.Descripcion = pgread.GetString(19);
                      evento.Precio = pgread.GetInt32(20);
                      evento.FechaInicio = pgread.GetDateTime(21);
                      evento.FechaFin = pgread.GetDateTime(22);
                      evento.HoraInicio = pgread.GetTimeSpan(23);
                      evento.HoraFin = pgread.GetTimeSpan(24);
                      evento.Foto = pgread.GetString(25);
                      evento.Tipo = "Evento";
                      itinerarios[itinerarios.Count - 1].Items_agenda.Add(evento);
                    }
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
            catch (Exception e)
            {
              con.Desconectar();
              throw e;
            }
    }



    /// <summary>
    /// Activar o desactivar la visibilidad del itinerario en el calendario
    /// </summary>
    /// <param name="idusuario">id del usuario al cual le pertenecen los itinerarios</param>
    /// <param name="iditinerario">id del itinerario a Activar/Desactivar</param>
    /// <param name="visible">parametro que determina si se activa(true) o desactiva(false) el itinerario en el calendario</param>
    /// <returns>true si se Activo/Desactivo exitosamente, false de lo contrario</returns>
    public Boolean SetVisible(int idusuario, int iditinerario, Boolean visible)
    {
      Boolean visible_sql = false;
      try
      {
        con = new ConexionBase();
        con.Conectar();
        comm = new NpgsqlCommand("setVisible", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idusuario);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Boolean, visible);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, iditinerario);
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
      catch (Exception e)
      {
        con.Desconectar();
        throw e;
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
          catch (Exception e)
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
            if ((tipo == "Lugar Turistico") || (tipo == "Actividad") || (tipo == "Evento"))
            {
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
            }
            else
            {
              con.Desconectar();
              return false; 
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
            con.Desconectar();
            throw e;
          }
          catch (Exception e)
          {
            con.Desconectar();
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
          catch (Exception e)
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
            catch (Exception e)
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
          catch (Exception e)
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
    public List<Evento> ConsultarEventos(string busqueda, DateTime fechainicio, DateTime fechafin)
    {
      List<Evento> list_eventos = new List<Evento>();
      try
      {
        con = new ConexionBase();
        con.Conectar();
        comm = new NpgsqlCommand("consultar_eventos", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, busqueda);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, fechainicio);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, fechafin);
        pgread = comm.ExecuteReader();

        //Se recorre los registros devueltos.
        while (pgread.Read())
        {
          Evento evento = new Evento(pgread.GetInt32(0), pgread.GetString(1));
          evento.Foto = pgread.GetString(2);
          list_eventos.Add(evento);
        }

        con.Desconectar();
        return list_eventos;
      }
      catch (NpgsqlException e)
      {
        con.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        con.Desconectar();
        throw e;
      }
    }

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
            con.Desconectar();
            throw e;
          }
          catch (Exception e)
          {
            con.Desconectar();
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
          //actividad.Foto = pgread.GetString(2);


          list_actividades.Add(actividad);
        }

        con.Desconectar();
        return list_actividades;
      }
      catch (NpgsqlException e)
      {
        con.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        con.Desconectar();
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
        con.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        con.Desconectar();
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
            
            itinerarios[itinerarios.Count - 1].Items_agenda.Add(lugar);
          }

          //Si existe actividad en este registro
          if (!pgread.IsDBNull(6))
          {
            dynamic actividad = new System.Dynamic.ExpandoObject();
            actividad.Nombre = pgread.GetString(6);
            actividad.Descripcion = pgread.GetString(7);
            actividad.Tipo = "Actividad";
            
            itinerarios[itinerarios.Count - 1].Items_agenda.Add(actividad);
          }

          //Si existe eventos en este registro.
          if (!pgread.IsDBNull(8))
          {
            dynamic evento = new System.Dynamic.ExpandoObject();
            evento.Nombre = pgread.GetString(8);
            evento.Descripcion = pgread.GetString(9);
            evento.Tipo = "Evento";

            itinerarios[itinerarios.Count - 1].Items_agenda.Add(evento);
          }
        }
        con.Desconectar();
        return itinerarios;
      }
      catch (NpgsqlException sql)
      {
        con.Desconectar();
        throw sql;
      }
      catch (ArgumentException arg)
      {
        con.Desconectar();
        throw arg;
      }
      catch (InvalidCastException cast)
      {
        con.Desconectar();
        throw cast;
      }
      catch (Exception e)
      {
        con.Desconectar();
        throw e;
      }
    }

    /// <summary>
    /// Se encarga de buscar todas las notificaciones de eventos, actividad y lugares turisticos
    /// con una semana de intervalo, es decir, buscar esos eventos pendientes entre hoy a una semana,
    /// con el fin de enviarlo por correo.
    /// </summary>
    /// <param name="id_usuario">Id del usuario a quien se le buscara la información</param>
    /// <returns>Devuelve "Exitoso" en caso de no haber incovenientes, y una excepcion en caso contrario</returns>
    public string EnviarCorreo(int id_usuario)
    {
      Usuario usuario;
      List<Itinerario> itinerarios; // Lista de itinerarios de un usuario
      string Body_Correo = "";
      //PeticionLogin peticion;
      //peticion = new PeticionLogin();

      string lugarturistico = "";
      string actividad = "";
      string evento = "";

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
                lugarturistico = lugarturistico + "{0}" +
                                  objeto.Nombre + "{0}" +
                                  objeto.Descripcion;
                break;

              case "Actividad":
                actividad = actividad + "{0}" +
                            objeto.Nombre + "{0}" +
                            objeto.Descripcion; ;
                break;

              case "Evento":
                evento = evento + "{0}" +
                          objeto.Nombre + "{0}" +
                          objeto.Descripcion;
                break;
            }
          }
        }
       
        Body_Correo = string.Format(" Hola " + usuario.NombreUsuario + ", {0}   Este es un correo para recordarte tu agenda en CocoTrip {0}" +
          "     Contenido de su itinerario:{0}" +
          "       Lugares Turisticos agendados:{0}" + lugarturistico + "{0}{0}{0}" +
          "       Actividades agendadas:{0}" + actividad + "{0}{0}{0}" +
          "       Eventos agendadas:{0}" + evento + "{0}{0}{0}", Environment.NewLine);


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
        con.Desconectar();
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        con.Desconectar();
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      catch (ArgumentNullException)
      {
        con.Desconectar();
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      catch (HttpResponseException)
      {
        con.Desconectar();
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (Exception e)
      {
        con.Desconectar();
        throw e;
      }
      return "Exitoso";
    }

    //----------------------------------------------------------------------------------------------
    /// <summary>
    /// Metodo que agrega en la base de datos si el usuario desea recibir notificaciones por correo
    /// </summary>
    /// <param name="id_usuario">Id del usuario a que se le agregara el registro</param>
    /// <returns>true si agrega existosamente, false en caso de error</>
    public bool AgregarNotificacion(int id_usuario)
    {
      bool rs;
      try
      {
        con = new ConexionBase();
        con.Conectar();
        comm = new NpgsqlCommand("agregar_notificacion", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, id_usuario);
        pgread = comm.ExecuteReader();
        pgread.Read();
        rs = pgread.GetBoolean(0);
        con.Desconectar();
        return rs;
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
      catch (Exception e)
      {
        con.Desconectar();
        throw e;
      }

    }

    /// <summary>
    /// Metodo que elimina el registro de la base de datos que determina si el usuario quiere notificaciones por correo
    /// </summary>
    /// <param name="id_usuario">Id del usuario al que se desea eliminar el registro</param>
    /// <returns>true si elimina existosamente, false en caso de error</returns>
    public Boolean EliminarNotificacion(int id_usuario)
    {
      try
      {
        con = new ConexionBase();
        con.Conectar();
        comm = new NpgsqlCommand("eliminar_notificacion", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, id_usuario);
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
      catch (Exception e)
      {
        con.Desconectar();
        throw e;
      }

    }

    /// <summary>
    /// Metodo que modifica si el registro de notificaciones por correo  de la base de datos
    /// </summary>
    /// <param name="id_usuario">Id del usuario al que se desea modificar las notificaciones</param>
    /// <param name="correo">Variable que determina si se desea recibir o no, notificaciones por correo</param>
    /// <returns>True si modifica existosamente, false en caso de error</returns>
    public bool ModificarNotificacion(int id_usuario, bool correo)
    {
      try
      {
        con = new ConexionBase();
        con.Conectar();
        comm = new NpgsqlCommand("modificar_notificacion", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, id_usuario);
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Boolean, correo);
        //La siguiente linea determina si se desea recibir notificaciones push, en caso de implementarlo.
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Boolean, true);
        pgread = comm.ExecuteReader();
        con.Desconectar();
        return true;
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
      catch (Exception e)
      {
        con.Desconectar();
        throw e;
      }
    }

    /// <summary>
    /// Consulta si se desea recibir notificaciones por correo.
    /// </summary>
    /// <param name="id_usuario">Id del usuario al que se desea verificar si desea notificaciones por correo..</param>
    /// <returns>Retorna True en caso que se desee notificaciones y False en caso contrario</returns>
    public bool ConsultarNotificacion(int id_usuario)
    {      
      try
      {
        con = new ConexionBase();
        con.Conectar();
        comm = new NpgsqlCommand("consultar_notificaciones", con.SqlConexion);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, id_usuario);
        pgread = comm.ExecuteReader();

        if (pgread.Read())
        {
          return pgread.GetBoolean(0);
        }
        con.Desconectar();
        return false;
      }
      catch (NpgsqlException e)
      {
        con.Desconectar();
        throw e;
      }
      catch(System.InvalidOperationException e)
      {
        con.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        con.Desconectar();
        throw e;
      }
    }
    //---------------------------------------------------------------------------------------------------
  }
}
