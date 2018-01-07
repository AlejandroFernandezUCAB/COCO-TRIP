using System;
using System.Collections;
using System.Collections.Generic;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;

/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///     Comando para obtener las categorias habilitadas.
/// </summary>
namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  /// <summary>
  /// Comando que tiene como funcion obtener todas las categorias habilitadas en la base de datos.
  /// </summary>
  public class ComandoConsultarCategoriaSelect : Comando
  {
    private ArrayList resultado = new ArrayList();
    private Comando com = FabricaComando.GetComandoConsultarCategoriaHabilitada();

    /// <summary>
    /// Metodo que nos permite obtener la lista de las categorias habilitadas mediante una consulta.
    /// </summary>
    /// <exception cref="Exception">Error al obtener las categoria habilitadas.</exception>
    public override void Execute()
    {
      try
      {
        IList<Categoria> listCategories = null;
        com.Execute();
        JObject respuesta = (JObject)com.GetResult()[0];
        if (respuesta.Property("data") != null)
        {
          listCategories = respuesta["data"].ToObject<IList<Categoria>>();
        }
        else
        {
          listCategories = null;
        }
        resultado.Add(listCategories);
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    /// <summary>
    /// Metodo que obtiene una lista de Categoria habilitadas de la ejecucion del comando.
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
