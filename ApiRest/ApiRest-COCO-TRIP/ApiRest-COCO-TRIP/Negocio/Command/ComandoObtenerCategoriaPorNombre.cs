using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using System;
using System.Collections.Generic;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoObtenerCategoriaPorNombre:Comando
  {
    private DAO dao = FabricaDAO.CrearDAOCategoria();
    private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();
    private Entidad resultado;

    public ComandoObtenerCategoriaPorNombre(Entidad entidad)
    {
      this.entidad = entidad;
    }

    public override void Ejecutar()
    {
      try
      {
        resultado = ((DAOCategoria)dao).ObtenerIdCategoriaPorNombre(entidad);
      }
      catch (Exception e)
      {
        //TERMINAR
        throw e;
      }
    }

    public override Entidad Retornar()
    {
      return resultado;
    }

    public override List<Entidad> RetornarLista()
    {
      throw new System.NotImplementedException();
    }

  }
}
