using BackOffice_COCO_TRIP.Datos.Entidades;
using System;


namespace BackOffice_COCO_TRIP.Datos.Entidades
{
  /// <summary>
  /// Clase que contiene los datos asociados a los horarios de los lugares turisticos
  /// </summary>
  public class Horario : Entidad
  {

    private int diaSemana; //Dia de la semana a la que pertenece el horario 
    private TimeSpan horaApertura; //Hora de apertura del lugar turistico
    private TimeSpan horaCierre; //Hora de cierre del lugar turistico

    /// <summary>
    /// Constructor de la clase
    /// </summary>
    public Horario()
    {
      horaApertura = new TimeSpan();
      horaCierre = new TimeSpan();
    }

    /// <summary>
    /// Getters y Setters del atributo DiaSemana
    /// </summary>
    public int DiaSemana
    {
      get { return diaSemana; }
      set { diaSemana = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo HoraApertura
    /// </summary>
    public TimeSpan HoraApertura
    {
      get { return horaApertura; }
      set { horaApertura = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo HoraCierre
    /// </summary>
    public TimeSpan HoraCierre
    {
      get { return horaCierre; }
      set { horaCierre = value; }
    }

    /// <summary>
    /// Compara si dos objetos de tipo Horario son iguales
    /// </summary>
    /// <param name="obj">Horario</param>
    /// <returns>(bool) Si son iguales retorna true</returns>
    public override bool Equals(object obj)
    {
      if (obj != null && obj is Horario)
      {
        var objeto = obj as Horario;

        if (diaSemana != objeto.diaSemana || horaApertura.ToString("c") != objeto.horaApertura.ToString("c")
            || horaCierre.ToString("c") != objeto.horaCierre.ToString("c"))
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
