using BackOffice_COCO_TRIP.Datos.Entidades;
using System;

namespace BackOffice_COCO_TRIP.Models
{
  /// <summary>
  /// Clase que contiene los datos asociados a la actividades de los lugares turisticos
  /// </summary>
  public class Actividad: Entidad
  {
    
    private string nombre; //Nombre de la actividad
    private TimeSpan duracion; //Tiempo estimado de duracion de la actividad
    private string descripcion; //Descripcion de la actividad
    private bool activar; //Activar o desactivar actividad

    private Foto foto; //Foto de la actividad

    /// <summary>
    /// Constructor de la clase
    /// </summary>
    public Actividad()
    {
      duracion = new TimeSpan();
      foto = new Foto();
    }

    /// <summary>
    /// Getters y Setters del atributo Nombre
    /// </summary>
    public string Nombre
    {
      get { return nombre; }
      set { nombre = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Duracion
    /// </summary>
    public TimeSpan Duracion
    {
      get { return duracion; }
      set { duracion = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Descripcion
    /// </summary>
    public string Descripcion
    {
      get { return descripcion; }
      set { descripcion = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Activar
    /// </summary>
    public bool Activar
    {
      get { return activar; }
      set { activar = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Foto
    /// </summary>
    public Foto Foto
    {
      get { return foto; }
      set { foto = value; }
    }

    /// <summary>
    /// Compara si dos objetos de tipo Actividad son iguales
    /// </summary>
    /// <param name="obj">Actividad</param>
    /// <returns>(bool) Si son iguales retorna true</returns>
    public override bool Equals(object obj)
    {
      if (obj != null && obj is Actividad)
      {
        var objeto = obj as Actividad;

        if (!foto.Equals(objeto.foto) || nombre != objeto.nombre
            || duracion.ToString("c") != objeto.duracion.ToString("c") ||
            descripcion != objeto.descripcion || activar != objeto.activar)
        {
          return (false);
        }
        else
        {
          return (true);
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
