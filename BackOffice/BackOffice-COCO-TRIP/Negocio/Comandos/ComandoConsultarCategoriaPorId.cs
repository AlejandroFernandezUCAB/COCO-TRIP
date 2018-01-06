using System;
using System.Collections;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;

/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///     Comando para obtener las categorias dado el Id.
/// </summary>
namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  /// <summary>
  /// Comando que tiene como funcion obtener una categoria dado el Id de la base de datos.
  /// </summary>
  public class ComandoConsultarCategoriaPorId : Comando
  {
    private int Id;
    private ArrayList resultado = new ArrayList();
    IDAOCategoria dao = FabricaDAO.GetDAOCategoria();

    /// <summary>
    /// Ejecuta el comando al cual fue designado, obtiene una categoria dado el Id.
    /// </summary>
    /// <exception cref="Exception">Error al obtener la categoria por el id.</exception>
    public override void Execute()
    {
      try
      {
        JObject respuesta = dao.GetPorId(Id);
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
      Id = (int)propiedad;
    }
  }
}
