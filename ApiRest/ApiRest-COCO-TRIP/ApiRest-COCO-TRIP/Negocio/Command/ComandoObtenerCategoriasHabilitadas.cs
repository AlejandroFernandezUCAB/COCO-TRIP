using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using System;
using NLog;
using ApiRest_COCO_TRIP.Comun.Excepcion;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoObtenerCategoriasHabilitadas:Comando
  {
    DAO dao = FabricaDAO.CrearDAOCategoria();
    private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();
    private IList<Categoria> resultado = new List<Categoria>();
    private static Logger log = LogManager.GetCurrentClassLogger();


    public override void Ejecutar()
    {
      try
      {
        resultado = ((DAOCategoria)dao).ObtenerCategoriasHabilitadas();
        log.Info("Categorias habilitadas consultadas exitosamente ");
      }
      catch (BaseDeDatosExcepcion e)
      {
          log.Error(e.Mensaje);
          throw e;
      }
      catch (Excepcion e)
      {
          log.Error(e.Mensaje);
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
