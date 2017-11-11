using System;
using System.IO;

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
    public bool EscribirArchivo(byte[] contenido, string nombreArchivo)
    {
      try
      {
        File.WriteAllBytes(ruta + nombreArchivo, contenido);
 
        return true;
      }
      catch(ArgumentNullException)
      {
        //El arreglo de bytes es null o el nombre del archivo es nulo
        //Registrar en NLog la incidencia
        return false;
      }
      catch(DirectoryNotFoundException)
      {
        //Alguien borro la carpeta Images o el nombre del archivo es incorrecto
        //Registrar en NLog la incidencia
        return false;
      }
      catch(IOException)
      {
        //Erorr atipico de entrada/salida
        //Registrar en NLog la incidencia
        return false;
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
      catch (ArgumentNullException)
      {
        //El nombre del archivo es nulo
        //Registrar en NLog la incidencia
      }
      catch (DirectoryNotFoundException)
      {
        //Alguien borro la carpeta Images o el nombre del archivo es incorrecto
        //Registrar en NLog la incidencia
      }
      catch (IOException)
      {
        //Erorr atipico de entrada/salida
        //Registrar en NLog la incidencia
      }
    }

  }
}
