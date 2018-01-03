using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  /// <summary>
  /// Comando que realiza la lógica de negocio para consultar todas las localidades.
  /// </summary>
  public class ComandoConsultarLocalidades : Comando
  {
    private ArrayList resultado = new ArrayList();

    /// <summary>
    /// Método Execute, ejecuta el comando.
    /// </summary>
    public override void Execute()
    {
      IDAOLocalidad peticion = FabricaDAO.GetDAOLocalidad();
      JObject respuesta = peticion.GetAll();

      if (respuesta.Property("dato") != null)
      {
        resultado.Add(respuesta["dato"].ToObject<List<Localidad>>());
      }

      else
      {
        resultado.Add(new List<Localidad>());
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
      throw new NotImplementedException();
    }
  }
}
