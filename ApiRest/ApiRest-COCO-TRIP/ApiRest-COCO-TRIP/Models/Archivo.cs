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
    private string ruta; //Ruta del servidor donde se almacenan las fotos

    /// <summary>
    /// Constructor
    /// </summary>
    public Archivo()
    {
      ruta = Directory.GetCurrentDirectory() + "/Images/";
    }

    /// <summary>
    /// Crea o sobreescribe el archivo 
    /// </summary>
    /// <param name="contenido">Bytes del archivo</param>
    /// <param nombreArchivo="nombreArchivo">Nombre del archivo a escribir</param>
    /// <returns>Retorna si la escritura fue exitosa o no</returns>
    public void EscribirArchivo(byte[] contenido, string nombreArchivo)
    {
      try
      {
        File.WriteAllBytes(ruta + nombreArchivo, contenido);
      }
      catch(ArgumentNullException e )
      {
        //El arreglo de bytes es null o el nombre del archivo es nulo
        //Registrar en NLog la incidencia

        var excepcion = new ArchivoExcepcion(e);
        excepcion.NombreArchivo = nombreArchivo;
        excepcion.NombreMetodo = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        excepcion.Ruta = ruta;

        throw excepcion;
      }
      catch(DirectoryNotFoundException e)
      {
        //Alguien borro la carpeta Images o el nombre del archivo es incorrecto
        //Registrar en NLog la incidencia

        var excepcion = new ArchivoExcepcion(e);
        excepcion.NombreArchivo = nombreArchivo;
        excepcion.NombreMetodo = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        excepcion.Ruta = ruta;

        throw excepcion;
      }
      catch(IOException e)
      {
        //Error atipico de entrada/salida
        //Registrar en NLog la incidencia

        var excepcion = new ArchivoExcepcion(e);
        excepcion.NombreArchivo = nombreArchivo;
        excepcion.NombreMetodo = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        excepcion.Ruta = ruta;

        throw excepcion;
      }
    }

    /// <summary>
    /// Elimina el archivo
    /// </summary>
    /// <param name="nombreArchivo">Nombre del archivo</param>
    public void EliminarArchivo(string nombreArchivo)
    {
      try
      {
        File.Delete(ruta + nombreArchivo);
      }
      catch (ArgumentNullException e)
      {
        //El nombre del archivo es nulo
        //Registrar en NLog la incidencia

        var excepcion = new ArchivoExcepcion(e);
        excepcion.NombreArchivo = nombreArchivo;
        excepcion.NombreMetodo = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        excepcion.Ruta = ruta;

        throw excepcion;
      }
      catch (DirectoryNotFoundException e)
      {
        //Alguien borro la carpeta Images o el nombre del archivo es incorrecto
        //Registrar en NLog la incidencia

        var excepcion = new ArchivoExcepcion(e);
        excepcion.NombreArchivo = nombreArchivo;
        excepcion.NombreMetodo = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        excepcion.Ruta = ruta;

        throw excepcion;
      }
      catch (IOException e)
      {
        //Erorr atipico de entrada/salida
        //Registrar en NLog la incidencia

        var excepcion = new ArchivoExcepcion(e);
        excepcion.NombreArchivo = nombreArchivo;
        excepcion.NombreMetodo = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        excepcion.Ruta = ruta;

        throw excepcion;
      }
    }

  }
}
