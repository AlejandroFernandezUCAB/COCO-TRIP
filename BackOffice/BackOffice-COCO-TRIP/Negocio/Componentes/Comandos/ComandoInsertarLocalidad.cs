using Newtonsoft.Json.Linq;
using BackOffice_COCO_TRIP.Datos.Entidades;
using System.Collections;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using BackOffice_COCO_TRIP.Datos.DAO;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoInsertarLocalidad : Comando
  {
    private Localidad localidad;
    private ArrayList resultado = new ArrayList();

    public override void Execute()
    {
      DAO<JObject, Localidad> peticion = FabricaDAO.GetDAOLocalidad();
      JObject respuesta = peticion.Post(localidad);
      if (respuesta.Property("dato") == null)
      {
        resultado.Add( "Ocurrio un error durante la comunicacion, revise su conexion a internet");
      }
      else
      {
        resultado.Add("Se hizo con exito");
      }
    }


    public override ArrayList GetResult()
    {
      return resultado;
    }

    public override void SetPropiedad(object propiedad)
    {
      this.localidad = (Localidad)propiedad;
    }
  }
}
