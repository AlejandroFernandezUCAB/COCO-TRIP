using System;
using System.Net.Mail;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
  /// <summary>
  /// Excepcion logica que recopila informacion sobre los errores generados por SmtpException.
  /// Se genera cuando el servicio web no cuenta con conexion a internet o el servidor SMTP no esta prestando servicio
  /// </summary>
  public class SmtpExcepcion : SmtpException
  {
    private SmtpException excepcion;
    private DateTime fechaHora;
    private string nombreMetodo; //Metodos que atrapan la excepcion antes de manejarla
    private string datoAsociado; //Datos asociados a la excepcion generada
    private string mensaje; //Mensaje asociado al error

    /// <summary>
    /// Getters y Setters del atributo Excepcion
    /// </summary>
    public SmtpException Excepcion
    {
      get { return excepcion; }
      set { excepcion = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo FechaHora
    /// </summary>
    public DateTime FechaHora
    {
      get { return fechaHora; }
      set { fechaHora = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo NombreMetodos
    /// </summary>
    public string NombreMetodos
    {
      get { return nombreMetodo; }
      set { nombreMetodo = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo DatosAsociados
    /// </summary>
    public string DatosAsociados
    {
      get { return datoAsociado; }
      set { datoAsociado = value; }
    }


    /// <summary>
    /// Getters y setters del atributo Mensaje
    /// </summary>
    public string Mensaje
    {
      get { return mensaje; }
      set { mensaje = value; }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="e">Excepcion generica</param>
    public SmtpExcepcion(SmtpException e)
    {
      excepcion = e;

      fechaHora = DateTime.Now;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="e">Excepcion generica</param>
    /// <param name="_mensaje">Mensaje asociado al error</param>
    public SmtpExcepcion(SmtpException e, string _mensaje)
    {
      excepcion = e;
      mensaje = _mensaje;

      fechaHora = DateTime.Now;
    }
  }
}
