using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using Npgsql;
using System.Data;
using System.Net.Mail;
using System.Web.Http;
using System.Net;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
  public class DAONotificacion : DAO
  {
    private Itinerario itinerario;
    private NpgsqlParameter parmetro;
    private NpgsqlDataReader respuesta;
    private NpgsqlCommand comando;
    public override void Actualizar(Entidad objeto)
    {
      Notificacion notificacion = (Notificacion)objeto;
      try
      {
        base.Conectar();
        comando = new NpgsqlCommand("modificar_notificacion", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, notificacion.IdUsuario);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Boolean, notificacion.Correo);
        //La siguiente linea determina si se desea recibir notificaciones push, en caso de implementarlo.
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Boolean, notificacion.Push);
        respuesta = comando.ExecuteReader();
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

    public override List<Entidad> ConsultarLista(Entidad objeto)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Consulta si se desea recibir notificaciones por correo.
    /// </summary>
    /// <param name="objeto">Con el Id del usuario al que se desea verificar si desea notificaciones por correo..</param>
    /// <returns>Retorna True en caso que se desee notificaciones y False en caso contrario</returns>

    public override Entidad ConsultarPorId(Entidad objeto)
    {
      Notificacion notificacion = (Notificacion)objeto;
      try
      {
       base.Conectar();
        comando = new NpgsqlCommand("consultar_notificaciones", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, notificacion.IdUsuario);
        respuesta = comando.ExecuteReader();

        if (respuesta.Read())
        {
          notificacion.Push = respuesta.GetBoolean(0);
          return notificacion;
        }
        base.Desconectar();
        notificacion.Push = false;
        return notificacion;
      }
      catch (NpgsqlException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (System.InvalidOperationException e)
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

    /// <summary>
    /// Metodo que elimina el registro de la base de datos que determina si el usuario quiere notificaciones por correo
    /// </summary>
    /// <param name="objeto">Con el Id del usuario al que se desea eliminar el registro</param>
    /// <returns>true si elimina existosamente, false en caso de error</returns>
    public override void Eliminar(Entidad objeto)
    {
      Notificacion notificacion = (Notificacion)objeto;
      try
      {
        base.Conectar();
        comando = new NpgsqlCommand("eliminar_notificacion", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, notificacion.IdUsuario);
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
    /// <summary>
    /// Metodo que agrega en la base de datos si el usuario desea recibir notificaciones por correo
    /// </summary>
    /// <param name="objeto">Con el Id del usuario a que se le agregara el registro</param>
    /// <returns>true si agrega existosamente, false en caso de error</>
    public override void Insertar(Entidad objeto)
    {
      bool rs;
      Notificacion notificacion = (Notificacion)objeto;
      try
      {
        base.Conectar();
        comando = new NpgsqlCommand("agregar_notificacion", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, notificacion.IdUsuario);
        respuesta = comando.ExecuteReader();
        respuesta.Read();
        rs = respuesta.GetBoolean(0);
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
      catch (NullReferenceException e)
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

    /// <summary>
    /// Se encarga de buscar todas las notificaciones de eventos, actividad y lugares turisticos
    /// con una semana de intervalo, es decir, buscar esos eventos pendientes entre hoy a una semana,
    /// con el fin de enviarlo por correo.
    /// </summary>
    /// <param name="id_usuario">Id del usuario a quien se le buscara la información</param>
    /// <returns>Devuelve "Exitoso" en caso de no haber incovenientes, y una excepcion en caso contrario</returns>
    public Boolean EnviarCorreo(Entidad objeto)
    {
      Usuario usuario = (Usuario)objeto;
      int idUsuario = usuario.Id;
      List<Entidad> itinerarios; // Lista de itinerarios de un usuario
      string Body_Correo = "";
      //PeticionLogin peticion;
      //peticion = new PeticionLogin();

      string lugarturistico = "";
      string actividad = "";
      string evento = "";

      try
      {
        //usuario = Buscar_Usuario(id_usuario);
        DAOUsuario dAOUsuario = Fabrica.FabricaDAO.CrearDAOUsuario();
        usuario = (Usuario)dAOUsuario.ConsultarPorId(usuario);
        usuario.Id = idUsuario;

        itinerarios = (new DAOItinerario()).ConsultarLista(usuario);

        foreach (Itinerario itinerario in itinerarios)
        {
          foreach (dynamic objet in itinerario.Items_agenda)
          {
            
            switch (objet.Tipo)
            {
              case "Lugar Turistico":
                lugarturistico = lugarturistico + "{0}" +
                                  objet.Nombre + "{0}" +
                                  objet.Descripcion;
                break;

              case "Actividad":
                actividad = actividad + "{0}" +
                            objet.Nombre + "{0}" +
                            objet.Descripcion; ;
                break;

              case "Evento":
                evento = evento + "{0}" +
                          objet.Nombre + "{0}" +
                          objet.Descripcion;
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
        return true;
      }
      catch (NpgsqlException)
      {
        base.Desconectar();
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        base.Desconectar();
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      catch (ArgumentNullException)
      {
        base.Desconectar();
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      catch (HttpResponseException)
      {
        base.Desconectar();
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
     /* catch (Exception e)
      {
        base.Desconectar();
        throw e;
      }*/
      
    }
  }
}
