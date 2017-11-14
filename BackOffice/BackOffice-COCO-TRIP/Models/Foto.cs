using System.Linq;

namespace BackOffice_COCO_TRIP.Models
{
  /// <summary>
  /// Clase que contiene los datos asociados a las fotos
  /// </summary>
  public class Foto
  {
    private int id; //Identificador unico
    private string ruta; //Ruta de la imagen en el servidor
    private byte[] contenido; //Bytes de la foto

    /// <summary>
    /// Getters y Setters del atributo ID
    /// </summary>
    public int Id
    {
      get { return id; }
      set { id = value; }
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
    /// Getters y Setters del atributo Contenido
    /// </summary>
    public byte[] Contenido
    {
      get { return contenido; }
      set { contenido = value; }
    }

    /// <summary>
    /// Compara si dos objetos de tipo Foto son iguales
    /// </summary>
    /// <param name="obj">Foto</param>
    /// <returns>(bool) Si son iguales retorna true</returns>
    public override bool Equals(object obj)
    {
      if (obj != null && obj is Foto)
      {
        var objeto = obj as Foto;

        if (id != objeto.id || ruta != objeto.Ruta)
        {
          return (false);
        }
        else
        {
          if (contenido != null && objeto.Contenido != null)
          {
            if (contenido.Length != objeto.Contenido.Length
               || !contenido.SequenceEqual<byte>(objeto.contenido))
            {
              return (false);
            }
            else
            {
              return (true);
            }
          }
          else if (contenido == null && objeto.Contenido == null)
          {
            return (true);
          }
          else
          {
            return (false);
          }
        }

      }
      else
      {
        return (false);
      }
    }

    /// <summary>
    /// Sobreescritura recomendada del metodo GetHashCode
    /// </summary>
    /// <returns>ID del objeto</returns>
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
