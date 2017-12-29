using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoAgregarCategoria : Comando
  {
    DAO dao = FabricaDAO.CrearDAOCategoria();
    private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();

    public ComandoAgregarCategoria(Entidad entidad)
    {
      this.entidad = entidad;
    }

    public override void Ejecutar()
    {
      try
      {
        dao.Insertar(entidad);
      }
      catch (Exception e)
      {
        //sql exception
        //null reference exception
        //TERMINAR
        throw e;
      }
    }

    public override Entidad Retornar()
    {
      throw new System.NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      throw new System.NotImplementedException();
    }

  }
}
