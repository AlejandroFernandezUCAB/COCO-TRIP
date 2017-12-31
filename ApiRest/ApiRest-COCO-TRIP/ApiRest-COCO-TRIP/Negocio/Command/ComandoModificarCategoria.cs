using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using System;
using NLog;
using ApiRest_COCO_TRIP.Comun.Excepcion;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoModificarCategoria:Comando
  {
    DAO dao = FabricaDAO.CrearDAOCategoria();
    private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();
    private string datosCategoria;
    private static Logger log = LogManager.GetCurrentClassLogger();


    public ComandoModificarCategoria(Entidad entidad)
    {
      this.entidad = entidad;
    }

    public override void Ejecutar()
    {
      try
      {
        datosCategoria = " ID: "+ ((Categoria)entidad).Id + " Nombre: " + ((Categoria)entidad).Nombre;
        dao.Actualizar(entidad);
        log.Info("Categoria nueva modificada con exito: " + datosCategoria);
      }
      catch (NombreDuplicadoExcepcion e)
      {
          e.DatosAsociados = datosCategoria;
          log.Error(e.Mensaje + " || " + e.DatosAsociados);
          throw e;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = datosCategoria;
        log.Error(e.Mensaje + " || " + e.DatosAsociados);
        throw e;
      }
      catch (HijoConDePendenciaExcepcion e)
      {
         log.Error(e.Mensaje);
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
      throw new System.NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      throw new System.NotImplementedException();
    }

  }
}
