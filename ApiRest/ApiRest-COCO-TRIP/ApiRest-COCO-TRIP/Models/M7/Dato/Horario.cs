using System;

namespace ApiRest_COCO_TRIP.Models.M7.Dato
{
  /// <summary>
  /// Clase que contiene los datos asociados a los horarios de los lugares turisticos
  /// </summary>
  public class Horario
  {
    public enum Dia { Sabado, Domingo, Lunes, Martes, Miercoles, Jueves, Viernes };

    private int id; //Identificador unico del horario
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
    /// Getters y Setters del atributo ID
    /// </summary>
    public int Id
    {
      get { return id; }
      set { id = value; }
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

        if (id != objeto.id || diaSemana != objeto.diaSemana || horaApertura.ToString("c") != objeto.horaApertura.ToString("c")
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
  }
}
