using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoEditarLocalidad : Comando
  {
    private Entidad localidad;
    private ArrayList resultado = new ArrayList();
    public override void Execute()
    {
      IDAOLocalidad peticion = FabricaDAO.GetDAOLocalidad();
      JObject respuesta = peticion.Put(localidad);
      if (respuesta.Property("dato") == null)
      {
        resultado.Add( "Ocurrio un error durante la comunicacion, revise su conexion a internet");
      }
      else
      {
        resultado.Add( "Se hizo con exito");
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
