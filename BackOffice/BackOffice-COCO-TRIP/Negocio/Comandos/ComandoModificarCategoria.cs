using System;
using System.Collections;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;

/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///     Comando para modificar una categoria.
/// </summary>
namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  /// <summary>
  /// Comando que tiene como funcion modificar una categora de la base de datos.
  /// </summary>
  public class ComandoModificarCategoria : Comando
  {
    private Entidad categoria = FabricaEntidad.GetCategoria();
    private ArrayList resultado = new ArrayList();
    IDAOCategoria dao = FabricaDAO.GetDAOCategoria();

    /// <summary>
    /// Metodo que ejecuta la accion del comando, modifica una Categoria.
    /// </summary>    
    /// <exception cref="Excepcion">Error inesperado.</exception>
    public override void Execute()
    {
      try
      {
        JObject respuesta = dao.Put(categoria);
        resultado.Add(respuesta);
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    /// <summary>
    /// Metodo que obtiene una lista de Categoria de la ejecucion del comando.
    /// </summary>
    /// <returns>Lista de categoria.</returns>
    public override ArrayList GetResult()
    {
      return resultado;
    }

    /// <summary>
    /// Metodo que fija el parametro para la ejecucion del comando.
    /// </summary>
    /// <param name="propiedad">propiedad a específicar</param>
    public override void SetPropiedad(object propiedad)
    {
      categoria = (Categoria)propiedad;
    }
  }
}
