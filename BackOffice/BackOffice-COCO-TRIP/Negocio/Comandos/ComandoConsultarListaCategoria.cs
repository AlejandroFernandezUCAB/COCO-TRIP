using System;
using System.Collections;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;

/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///     Comando para obtener subcategorias dado el id del superior.
/// </summary>
namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  /// <summary>
  /// Comando que tiene como funcion obtener las subcategorias
  /// dado el id del superior de la base de datos.
  /// </summary>
  public class ComandoConsultarListaCategoria : Comando
  {
    private int Id;
    private ArrayList resultado = new ArrayList();
    IDAOCategoria dao = FabricaDAO.GetDAOCategoria();

    /// <summary>
    /// Ejecuta el comando al cual fue designado, obtiene una
    /// lista de las subcategorias dado el id del superior.
    /// </summary>
    /// <exception cref="Exception">Error al obtener las subcategorias.</exception>
    public override void Execute()
    {
      try
      {
        JObject respuesta = dao.Get(Id);
        resultado.Add(respuesta);
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    /// <summary>
    /// Metodo que obtiene una lista de las subCategoria de la ejecucion del comando.
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
      Id = (int)propiedad;
    }
  }
}
