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

    public const string Ruta = "/Images/"; //Ruta relativa que se almacenara en la base de datos
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

      Directory.CreateDirectory(rutaAbsoluta); //Crea la carpeta Images en caso de que no exista
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
        File.WriteAllBytes(rutaAbsoluta + nombre, contenido);
    }

    /// <summary>
    /// Elimina el archivo
    /// </summary>
    /// <param name="nombre">Nombre del archivo</param>
    public void EliminarArchivo(string nombre)
    {
        File.Delete(rutaAbsoluta + nombre);
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
