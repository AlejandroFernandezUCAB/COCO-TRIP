using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  /// <summary>
  /// Comando que realiza la lógica de negocio para consultar un evento.
  /// </summary>
  public class ComandoConsultarEvento : Comando
  {
    private int id;
    private ArrayList resultado = new ArrayList();
    private Comando comando;
    private Entidad entidad;

    /// <summary>
    /// Método Execute, ejecuta el comando.
    /// </summary>
    public override void Execute()
    {
      IDAOEvento peticion = FabricaDAO.GetDAOEvento();
      JObject respuesta = peticion.GetEvento(id);

      if (respuesta.Property("dato") == null)
      {
        resultado.Add(new Evento());
        resultado.Add("Ocurrio un error durante la comunicacion, revise su conexion a internet");
      }
      else
      {
        entidad = respuesta["dato"].ToObject<Evento>();
        resultado.Add(entidad);

        resultado.Add("");

        comando = FabricaComando.GetComandoConsultarLocalidad();
        comando.SetPropiedad(((Evento)entidad).IdLocalidad);
        comando.Execute();
        resultado.Add(((Localidad)comando.GetResult()[0]).Nombre);

        comando = FabricaComando.GetComandoConsultarCategoriaPorId();
        comando.SetPropiedad(((Evento)entidad).IdCategoria);
        comando.Execute();
        JObject cat = (JObject)comando.GetResult()[0];
        Categoria entidad2 = cat["data"][0].ToObject<Categoria>();
        resultado.Add((entidad2).Name);

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
      this.id = (int)propiedad;
    }
  }
}
