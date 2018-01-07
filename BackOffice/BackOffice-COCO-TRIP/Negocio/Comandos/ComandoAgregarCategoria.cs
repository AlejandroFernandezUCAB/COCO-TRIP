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
///     Comando para insertar una categorias.
/// </summary>
namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  /// <summary>
  /// Comando que tiene como funcion insertar una categoria a la base de datos.
  /// </summary>
  public class ComandoAgregarCategoria : Comando
  {
    private Entidad categoria = FabricaEntidad.GetCategoria();
    IDAOCategoria dao = FabricaDAO.GetDAOCategoria();
    private ArrayList resultado = new ArrayList();

    /// <summary>
    /// Metodo que ejecuta la accion del comando, agrega una nueva Categoria.
    /// </summary>
    /// <exception cref="Exception">Error al agregar la categoria.</exception>
    public override void Execute()
    {
      try
      {
        JObject respuesta = dao.Post(categoria);
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
