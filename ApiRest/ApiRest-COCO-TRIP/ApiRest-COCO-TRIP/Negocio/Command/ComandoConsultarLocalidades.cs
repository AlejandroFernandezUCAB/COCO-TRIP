using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoConsultarLocalidades : Comando
  {
    private List<Entidad> localidades;

    

    public override void Ejecutar()
    {
      DAO dao = FabricaDAO.CrearDAOLocalidad();
      try
      {
        localidades = dao.ConsultarLista(null);
      }
      catch (BaseDeDatosExcepcion e)
      {
        throw e;
      }
    }

    public override Entidad Retornar()
    {
      throw new NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      return localidades;
    }
  }
}
