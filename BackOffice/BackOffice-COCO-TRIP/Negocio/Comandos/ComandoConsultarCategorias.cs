using System;
using System.Collections;
using System.Collections.Generic;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;

/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///     Comando para obtener categorias superiores.
/// </summary>
namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  /// <summary>
  /// Comando que tiene como funcion obtener las categorias superiores de la base de datos.
  /// </summary>
  public class ComandoConsultarCategorias : Comando
  {
    private ArrayList resultado = new ArrayList();
    IDAOCategoria dao = FabricaDAO.GetDAOCategoria();

    /// <summary>
    /// Ejecuta el comando al cual fue designado, obtiene una lista de las categorias superiores.
    /// </summary>
    /// <exception cref="Exception">Error al obtener las categorias superiores o subcategorias.</exception>
    public override void Execute()
    {
      try
      {
        JObject respuesta = dao.Get(-1);
        if (respuesta.Property("data") != null)
        {
          resultado.Add(respuesta["data"].ToObject<List<Categoria>>());
          resultado.Add("Exito");
        }
        else
        {
          resultado.Add(new List<Categoria>());
          resultado.Add("Ocurrio un error durante la comunicacion, revise su conexion a internet");
        }
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    /// <summary>
    /// Metodo que obtiene una lista de las categoria superiores de la ejecucion del comando.
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
    /// <exception cref="System.NotImplementedException">Metodo No implementado</exception>
    public override void SetPropiedad(object propiedad)
    {
      throw new NotImplementedException();
    }
  }
}
