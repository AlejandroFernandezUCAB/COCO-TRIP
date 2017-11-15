using System;
using System.IO;
using ApiRest_COCO_TRIP.Models.Excepcion;
using System.Reflection;

namespace ApiRest_COCO_TRIP.Models
{
  /// <summary>
  /// Gestiona las fotos almacenadas en el web service
  /// </summary>
  public class Archivo
  {
    private string rutaCompleta; //Ruta del servidor donde se almacenan las fotos
    private string ruta; //Ruta que se almacenara en la base de datos

    /// <summary>
    /// Getter y setter del atributo RutaCompleta
    /// </summary>
    public string RutaCompleta
    {
      get { return rutaCompleta; }
      set { rutaCompleta = value; }
    }

    /// <summary>
    /// Getter y setter del atributo Ruta
    /// </summary>
    public string Ruta
    {
      get { return ruta; }
      set { ruta = value; }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public Archivo()
    {
      ruta = "/Images/";
      rutaCompleta = Assembly.GetExecutingAssembly().CodeBase;

      var uri = new UriBuilder(rutaCompleta);
      rutaCompleta = Uri.UnescapeDataString(uri.Path);
      rutaCompleta = Path.GetDirectoryName(rutaCompleta);

      rutaCompleta = rutaCompleta.Replace("\\bin", "");
      rutaCompleta += ruta;
    }

    /// <summary>
    /// Crea o sobreescribe el archivo
    /// </summary>
    /// <param name="contenido">Bytes del archivo</param>
    /// <param nombreArchivo="nombreArchivo">Nombre del archivo a escribir</param>
    /// <returns>Retorna si la escritura fue exitosa o no</returns>
    /// <exception cref="ArchivoExcepcion"></exception>
    public void EscribirArchivo(byte[] contenido, string nombreArchivo)
    {
      try
      {
        File.WriteAllBytes(rutaCompleta + nombreArchivo, contenido);
      }
      catch(ArgumentNullException e )
      {
        //El arreglo de bytes es null o el nombre del archivo es nulo
        //Registrar en NLog la incidencia

        var excepcion = new ArchivoExcepcion(e);
        excepcion.NombreArchivo = nombreArchivo;
        excepcion.NombreMetodo = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        excepcion.Ruta = rutaCompleta;

        throw excepcion;
      }
      catch(DirectoryNotFoundException e)
      {
        //Alguien borro la carpeta Images o el nombre del archivo es incorrecto
        //Registrar en NLog la incidencia

        var excepcion = new ArchivoExcepcion(e);
        excepcion.NombreArchivo = nombreArchivo;
        excepcion.NombreMetodo = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        excepcion.Ruta = rutaCompleta;

        throw excepcion;
      }
      catch(IOException e)
      {
        //Error atipico de entrada/salida
        //Registrar en NLog la incidencia

        var excepcion = new ArchivoExcepcion(e);
        excepcion.NombreArchivo = nombreArchivo;
        excepcion.NombreMetodo = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        excepcion.Ruta = rutaCompleta;

        throw excepcion;
      }
    }

    /// <summary>
    /// Elimina el archivo
    /// </summary>
    /// <param name="nombreArchivo">Nombre del archivo</param>
    /// <exception cref="ArchivoExcepcion"></exception>
    public void EliminarArchivo(string nombreArchivo)
    {
      try
      {
        File.Delete(rutaCompleta + nombreArchivo);
      }
      catch (ArgumentNullException e)
      {
        //El nombre del archivo es nulo
        //Registrar en NLog la incidencia

        var excepcion = new ArchivoExcepcion(e);
        excepcion.NombreArchivo = nombreArchivo;
        excepcion.NombreMetodo = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        excepcion.Ruta = rutaCompleta;

        throw excepcion;
      }
      catch (DirectoryNotFoundException e)
      {
        //Alguien borro la carpeta Images o el nombre del archivo es incorrecto
        //Registrar en NLog la incidencia

        var excepcion = new ArchivoExcepcion(e);
        excepcion.NombreArchivo = nombreArchivo;
        excepcion.NombreMetodo = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        excepcion.Ruta = rutaCompleta;

        throw excepcion;
      }
      catch (IOException e)
      {
        //Error atipico de entrada/salida
        //Registrar en NLog la incidencia

        var excepcion = new ArchivoExcepcion(e);
        excepcion.NombreArchivo = nombreArchivo;
        excepcion.NombreMetodo = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        excepcion.Ruta = rutaCompleta;

        throw excepcion;
      }
    }

  }
}
