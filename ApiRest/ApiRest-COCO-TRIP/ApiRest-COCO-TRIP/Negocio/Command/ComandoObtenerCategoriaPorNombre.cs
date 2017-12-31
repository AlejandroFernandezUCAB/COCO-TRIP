using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using NLog;
using System;
using System.Collections.Generic;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoObtenerCategoriaPorNombre:Comando
  {
    private DAO dao = FabricaDAO.CrearDAOCategoria();
    private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();
    private Entidad resultado;
    private string datosCategoria;
    private static Logger log = LogManager.GetCurrentClassLogger();

    public ComandoObtenerCategoriaPorNombre(Entidad entidad)
    {
      this.entidad = entidad;
    }

    public override void Ejecutar()
    {
      try
      {
        datosCategoria = " Nombre: " + ((Categoria)entidad).Nombre;
        resultado = ((DAOCategoria)dao).ObtenerIdCategoriaPorNombre(entidad);
        log.Info("Categoria consultada con exito: " + datosCategoria);
      }
      catch (BaseDeDatosExcepcion e)
      {
          e.DatosAsociados = datosCategoria;
          log.Error(e.Mensaje + " || " + e.DatosAsociados);
          throw e;
      }
      catch (Excepcion e)
      {
          e.DatosAsociados = datosCategoria;
          log.Error(e.Mensaje + " || " + e.DatosAsociados);
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
