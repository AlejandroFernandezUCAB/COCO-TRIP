using ApiRest_COCO_TRIP.Comun.Excepcion;
using System;
using System.IO;
using System.Reflection;

namespace ApiRest_COCO_TRIP.Datos.Singleton
{
  /// <summary>
  /// Almacena las fotos en el servicio web
  /// </summary>
  public class Archivo
  {
    private static Archivo instancia;

    public const string Ruta = "\\Images\\"; //Ruta relativa que se almacenara en la base de datos
    public const string Extension = ".jpg";

    //Nombres de las fotos
    public const string FotoGrupo = "GRUPO";

    private string rutaAbsoluta; //Ruta absoluta  del servidor donde se almacenan las fotos

    /// <summary>
    /// Constructor
    /// </summary>
    private Archivo()
    {
      rutaAbsoluta = Assembly.GetExecutingAssembly().CodeBase;

      UriBuilder uri = new UriBuilder(rutaAbsoluta);
      rutaAbsoluta = Uri.UnescapeDataString(uri.Path);
      rutaAbsoluta = Path.GetDirectoryName(rutaAbsoluta);

      rutaAbsoluta = rutaAbsoluta.Replace("\\bin", ""); //Ruta de la carpeta principal del servicio web
      rutaAbsoluta += Ruta; //.../inetpub/cocotrip/Images/

      try
      {
        Directory.CreateDirectory(rutaAbsoluta); //Crea la carpeta Images en caso de que no exista
      }
      catch (IOException e)
      {
        throw new IOExcepcion(e, "Error creando carpeta en " + rutaAbsoluta + ". " + e.Message);
      }
    }

    /// <summary>
    /// Retorna la instancia del Singleton
    /// </summary>
    /// <returns>Correo</returns>
    public static Archivo ObtenerInstancia ()
    {
      if(instancia == null)
      {
        instancia = new Archivo();
      }

      return instancia;
    }

    /// <summary>
    /// Crea o sobreescribe un archivo
    /// </summary>
    /// <param name="contenido">Bytes del archivo</param>
    /// <param name="nombre">Nombre del archivo a escribir</param>
    public void EscribirArchivo(byte[] contenido, string nombre)
    {
      try
      {
        File.WriteAllBytes(rutaAbsoluta + nombre, contenido);
      }
      catch(IOException e)
      {
        throw new IOExcepcion(e, "Error escribiendo archivo en " + rutaAbsoluta + nombre + ". " + e.Message);
      }
      catch(ArgumentNullException e)
      {
        throw new ArgumentoNuloExcepcion(e, "Argumento nulo recibido en Archivo.EscribirArchivo generado por nombre o contenido. " + e.Message);
      }
    }

    /// <summary>
    /// Elimina el archivo
    /// </summary>
    /// <param name="nombre">Nombre del archivo</param>
    public void EliminarArchivo(string nombre)
    {
      try
      {
        File.Delete(rutaAbsoluta + nombre);
      }
      catch (IOException e)
      {
        throw new IOExcepcion(e, "Error eliminando archivo en " + rutaAbsoluta + nombre + ". " + e.Message);
      }
      catch (ArgumentNullException e)
      {
        throw new ArgumentoNuloExcepcion(e, "Argumento nulo recibido en Archivo.EliminarArchivo generado por nombre. " + e.Message);
      }
    }

    /// <summary>
    /// Valida si existe el archivo
    /// </summary>
    /// <param name="nombre">Nombre del archivo</param>
    /// <returns>(true) existe, (false) no existe</returns>
    public bool ExisteArchivo(string nombre)
    {
        return File.Exists(rutaAbsoluta + nombre);
    }
  }

}
