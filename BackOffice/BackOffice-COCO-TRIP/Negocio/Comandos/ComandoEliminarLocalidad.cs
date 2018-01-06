using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;
using System.Collections;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  /// <summary>
  /// Comando que realiza la lógica de negocio para Eliminar una localidad.
  /// </summary>
  public class ComandoEliminarLocalidad : Comando
  {
    private int id;
    private ArrayList resultado = new ArrayList();

    /// <summary>
    /// Método Execute, ejecuta el comando.
    /// </summary>
    public override void Execute()
    {
      IDAOLocalidad peticion = FabricaDAO.GetDAOLocalidad();
      JObject respuesta = peticion.Delete(id);
      if (respuesta.Property("dato") == null)
      {
        resultado.Add("Ocurrio un error durante la comunicacion, revise su conexion a internet");
      }
      else
      {
        resultado.Add("Se hizo con exito");
      }

    }

    /// <summary>
    /// Método GetResult, obtiene una lista de resultados derivados de la ejecución del comando.
    /// </summary>
    /// <returns>ArrayList con los resultados</returns>
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
      this.id = (int)propiedad;
    }
  }
}
