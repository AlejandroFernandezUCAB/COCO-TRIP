using System;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
  /// <summary>
  /// Excepcion logica que recopila informacion sobre los errores generados
  /// </summary>
  public class ArgumentoNuloExcepcion: ArgumentNullException
  {
    private ArgumentException excepcion;
    private DateTime fechaHora;
    private string nombreMetodos; //Enlista los metodos que atrapan la excepcion antes de manejarla
    private string datosAsociados; //Datos asociados a la excepcion generada
    private string mensaje; //Mensaje asociado al error

    /// <summary>
    /// Getters y Setters del atributo Excepcion
    /// </summary>
    public ArgumentException Excepcion
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
      get { return nombreMetodos; }
      set { nombreMetodos = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo DatosAsociados
    /// </summary>
    public string DatosAsociados
    {
      get { return datosAsociados; }
      set { datosAsociados = value; }
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
    public ArgumentoNuloExcepcion(ArgumentException e)
    {
      excepcion = e;

      fechaHora = DateTime.Now;
    }
  }
}
