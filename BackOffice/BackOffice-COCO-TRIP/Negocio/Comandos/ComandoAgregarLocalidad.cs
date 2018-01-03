using Newtonsoft.Json.Linq;
using BackOffice_COCO_TRIP.Datos.Entidades;
using System.Collections;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  /// <summary>
  /// Comando que realiza la lógica de negocio para agregar una localidad.
  /// </summary>
  public class ComandoAgregarLocalidad : Comando
  {
    private Entidad localidad;
    private ArrayList resultado = new ArrayList();

    /// <summary>
    /// Método Execute, ejecuta el comando.
    /// </summary>
    public override void Execute()
    {
      IDAOLocalidad peticion = FabricaDAO.GetDAOLocalidad();
      JObject respuesta = peticion.Post(localidad);
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
      this.localidad = (Localidad)propiedad;
    }
  }
}
