using System;
using System.Collections.Generic;

namespace ApiRest_COCO_TRIP.Models.Excepcion
{
  /// <summary>
  /// Excepcion logica que se produce por un casteo invalido entre un tipo de dato del lenguaje C Sharp y
  /// un tipo de dato de la base de datos.
  /// Es bastante comun cuando se pasa por parametro un valor nulo
  /// </summary>
  public class CasteoInvalidoExcepcion : InvalidCastException
  {
    private InvalidCastException excepcion;
    private DateTime fechaHora;
    private List<String> nombreMetodos; //Enlista los metodos que atrapan la excepcion antes de manejarla
    private string datosAsociados; //Datos asociados a la excepcion generada

    /// <summary>
    /// Getters y Setters del atributo NombreMetodos
    /// </summary>
    public InvalidCastException Excepcion
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
    public List<String> NombreMetodos
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
    /// Constructor que recibe la excepcion, instacia los metodos y, registra la hora y fecha de la incidencia
    /// </summary>
    /// <param name="e">Excepcion de la base de datos</param>
    public CasteoInvalidoExcepcion(InvalidCastException e)
    {
      excepcion = e;

      fechaHora = DateTime.Now;
      nombreMetodos = new List<String>();
    }
  }
}
