using System.Linq;

namespace ApiRest_COCO_TRIP.Datos.Entity
{
  /// <summary>
  /// Clase que contiene los datos asociados a las fotos
  /// </summary>
  public class Foto : Entidad
  {
    private string ruta; //Ruta de la imagen en el servidor
    private byte[] contenido; //Bytes de la foto
        private string v2;

        public Foto(int id, string ruta)
        {
            this.Id = id;
            this.Ruta = ruta;
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
    /*public override bool Equals(object obj)
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
    }*/

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
