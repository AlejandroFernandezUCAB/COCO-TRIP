using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using System;

namespace ApiRest_COCO_TRIP.Negocio.Comando
{
  public class ComandoObtenerCategorias: Comando
  {
    DAO dao = FabricaDAO.CrearDAOCategoria();
    private Categoria categoria;
    private List<Entidad> resultado = new List<Entidad>();


    public override void Ejecutar()
    {
      try
      {
        ((DAOCategoria)dao).ObtenerCategorias(categoria);
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
      return resultado;
    }

    public void SetPropiedad(Entidad entidad)
    {
      categoria = (Categoria)entidad;
    }
  }
}
