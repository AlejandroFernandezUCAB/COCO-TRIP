using ApiRest_COCO_TRIP.Datos.Entity;
using System.Collections.Generic;

namespace ApiRest_COCO_TRIP.Negocio.Comando
{
  /// <summary>
  /// Superclase de comandos
  /// </summary>
  public abstract class Comando
  {
    /// <summary>
    /// Ejecuta la logica de negocios del comando
    /// </summary>
    public abstract void Ejecutar();

    /// <summary>
    /// Retorna el resultado del comando
    /// </summary>
    /// <returns>Entidad</returns>
    public abstract Entidad Retornar();

    /// <summary>
    /// Retorna el resultado del comando
    /// </summary>
    /// <returns>Lista de Entidades</returns>
    public abstract List<Entidad> RetornarLista();

  }
}
