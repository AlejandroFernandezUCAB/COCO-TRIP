using ApiRest_COCO_TRIP.Comun.Excepcion;
using System;
using System.Net;
using System.Net.Mail;

namespace ApiRest_COCO_TRIP.Datos.Singleton
{
  /// <summary>
  /// Correo de la aplicacion movil
  /// </summary>
  public class Correo
  {
    private static Correo instancia;

    private SmtpClient servicio;
    private MailAddress correoAplicacion;
    private MailMessage correoMensaje;

    private const string ServicioGmail = "smtp.gmail.com";
    private const int PuertoProtocolo = 587;
    private const string CorreoAplicacion = "cocotrip17@gmail.com";
    private const string NombreCredencial = "cocotrip17";
    private const string ClaveCredencial = "arepascocotrip";

    /// <summary>
    /// Constructor de la clase
    /// </summary>
    private Correo ()
    {
      servicio = new SmtpClient(ServicioGmail);
      servicio.Port = PuertoProtocolo;
      servicio.UseDefaultCredentials = false;
      servicio.Credentials = new NetworkCredential(NombreCredencial, ClaveCredencial);
      servicio.EnableSsl = true;

      correoAplicacion = new MailAddress(CorreoAplicacion);
    }

    /// <summary>
    /// Retorna la instancia del Singleton
    /// </summary>
    /// <returns>Correo</returns>
    public static Correo ObtenerInstancia ()
    {
      if(instancia == null)
      {
        instancia = new Correo ();
      }

      return instancia;
    }

    /// <summary>
    /// Envia un correo a un usuario recomendado la aplicacion
    /// </summary>
    /// <param name="correo">Correo de la persona a la que se le va a recomendar la aplicacion</param>
    /// <param name="destino">Nombre de la persona a quien va dirigido el correo</param>
    /// <param name="remitente">Nombre de la persona que genera la notificacion</param>
    public void RecomendarAplicacion (string correo, string destino, string remitente)
    {
      try
      {
        correoMensaje = new MailMessage();

        correoMensaje.From = correoAplicacion;
        correoMensaje.To.Add(correo);
        correoMensaje.Subject = "[COCO-TRIP] Alguien quiere agregarte como amigo!"; //Encabezado

        correoMensaje.IsBodyHtml = false; //Cuerpo
        correoMensaje.Body = "El usuario " + remitente + " desea agregarte como amigo, puedes aceptarl@ desde la aplicacion de COCO-TRIP";

        servicio.Send(correoMensaje); //Envia
      }
      catch (SmtpException e)
      {
        throw new SmtpExcepcion(e, "Error enviando correo. Servicio SMTP caido o servicio web sin conexion a internet. " + e.Message);
      }
      catch (ArgumentNullException e)
      {
        throw new ArgumentoNuloExcepcion(e, "Argumento nulo recibido en Correo.RecomendarAplicacion generado por correo, destino o remitente. " + e.Message);
      }
    }
  }
}
