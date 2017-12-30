 using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using System;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoObtenerCategorias: Comando
  {
    private DAO dao = FabricaDAO.CrearDAOCategoria();
    private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();
    private IList<Categoria> resultado = new List<Categoria>();

    public ComandoObtenerCategorias(Entidad entidad)
    {
      this.entidad = entidad;
    }

    public override void Ejecutar()
    {
      try
      {
        resultado=((DAOCategoria)dao).ObtenerCategorias(entidad);
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

    public IList<Categoria> RetornarLista2()
    {
      return resultado;
    }

  }
}
