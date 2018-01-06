using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  /// <summary>
  /// Comando que realiza la lógica de negocio para modificar una localidad.
  /// </summary>
  public class ComandoModificarLocalidad : Comando
  {
    private Entidad localidad;
    private ArrayList resultado = new ArrayList();

    /// <summary>
    /// Método Execute, ejecuta el comando.
    /// </summary>
    public override void Execute()
    {
      IDAOLocalidad peticion = FabricaDAO.GetDAOLocalidad();
      JObject respuesta = peticion.Put(localidad);
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
      this.localidad = (Localidad)propiedad;
    }
  }
}
