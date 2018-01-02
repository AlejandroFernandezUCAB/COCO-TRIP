using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  /// <summary>
  /// Comando que realiza la lógica de negocio para agregar un evento.
  /// </summary>
  public class ComandoAgregarEvento : Comando
  {
    private Entidad evento;
    private ArrayList resultado = new ArrayList();

    /// <summary>
    /// Método Execute, ejecuta el comando.
    /// </summary>
    public override void Execute()
    {
      try
      {
        IDAOEvento peticionEvento = FabricaDAO.GetDAOEvento();
        JObject respuesta = peticionEvento.Post(evento);
        if (respuesta.Property("dato") == null)
        {
          resultado.Add("Ocurrio un error durante la comunicacion, revise su conexion a internet");
        }
        else
        {
          resultado.Add("Se hizo con exito");
        }
      }
      catch (NullReferenceException e)
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
      evento = (Evento)propiedad;
    }
  }
}
