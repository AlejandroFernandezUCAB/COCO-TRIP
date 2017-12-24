using System;

namespace ApiRest_COCO_TRIP.Models.Excepcion
{
  /// <summary>
  /// Excepcion logica que recopila informacion sobre los errores generados en la clase Archivo
  /// </summary>
  public class ArchivoExcepcion: Exception //Por ahora, lo cambiaremos
  {
    private DateTime fechaHora;
    //private Exception excepcion; //Las posibles excepciones son DirectoryNotFound, IOException y ArgumentNullException
    private string nombreArchivo; //Nombre del archivo
    private string ruta; //Ruta del web service donde se almacenan las fotos
    private string nombreMetodo; //Nombre del metodo que genero la excepcion

    /// <summary>
    /// Getters y Setters del atributo FechaHora
    /// </summary>
    public DateTime FechaHora
    {
      get { return fechaHora; }
      set { fechaHora = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo NombreArchivo
    /// </summary>
    public string NombreArchivo
    {
      get { return nombreArchivo; }
      set { nombreArchivo = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Ruta
    /// </summary>
    public string Ruta
    {
      get { return ruta; }
      set { ruta = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo NombreMetodo
    /// </summary>
    public string NombreMetodo
    {
      get { return nombreMetodo; }
      set { nombreMetodo = value; }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="e">Excepcion generica</param>
    public ArchivoExcepcion(Exception e)
    {
      fechaHora = DateTime.Now;
    }

  }
}
