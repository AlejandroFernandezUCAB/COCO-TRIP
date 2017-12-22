using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using System;

namespace ApiRest_COCO_TRIP.Negocio.Comando
{
  public class ComandoModificarCategoria:Comando
  {
    DAO dao = FabricaDAO.CrearDAOCategoria();
    private Categoria categoria;

    public ComandoModificarCategoria(Entidad categoria)
    {
      categoria = FabricaEntidad.CrearEntidadCategoria();
      this.categoria =(Categoria)categoria;
    }

    public override void Ejecutar()
    {
      try
      {
        dao.Actualizar(categoria);
      }
      catch (Exception e)
      {
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
