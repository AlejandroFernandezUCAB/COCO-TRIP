using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Models;
using BackOffice_COCO_TRIP.Models.Peticion;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  /// <summary>
  /// Comando que realiza la lógica de negocio para consultar todos los eventos dado una categoría.
  /// </summary>
  public class ComandoConsultarEventos : Comando
  {
    private int id;
    private ArrayList resultado = new ArrayList();

    /// <summary>
    /// Método Execute, ejecuta el comando.
    /// </summary>
    public override void Execute()
    {
      try
      {
        DAO<JObject, Categoria> peticionCategoria = FabricaDAO.GetDAOCategoria();

        IDAOLocalidad peticionLocalidad = FabricaDAO.GetDAOLocalidad();
        JObject respuestaCategoria = ((DAOCategoria)peticionCategoria).GetCategoriasHabilitadas();
        JObject respuestaLocalidad = peticionLocalidad.GetAll();
        if (respuestaCategoria.Property("data") != null)
        {
          resultado.Add(respuestaCategoria["data"].ToObject<List<Categoria>>());
          resultado.Add("Exito en Categoria");
        }

        else
        {
          resultado.Add(new List<Categoria>());
          resultado.Add("Error en la comunicacion o No existen Categorias");
        }

        if (respuestaLocalidad.Property("dato") != null)
        {
          resultado.Add(respuestaLocalidad["dato"].ToObject<List<Localidad>>());
          resultado.Add("Exito en Localidad");
        }

        else
        {
          resultado.Add(new List<Localidad>());
          resultado.Add(" Error en la comunicacion o No existen localidades");
        }
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    /// <summary>
    /// Método GetResult, Obtiene una lista de resultados derivados de la ejecución del comando.
    /// </summary>
    /// <returns></returns>
    public override ArrayList GetResult()
    {
      return resultado;
    }

    /// <summary>
    /// Método SetPropiedad, especifíca algún parámetro para la ejecución del comando.
    /// </summary>
    /// <param name="propiedad">propiedad a específicar</param>
    public override void SetPropiedad(object propiedad)
    {
      id = (int)propiedad;
    }
  }
}
