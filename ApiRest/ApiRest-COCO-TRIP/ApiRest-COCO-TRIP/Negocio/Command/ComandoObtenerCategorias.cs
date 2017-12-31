 using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using NLog;
using ApiRest_COCO_TRIP.Comun.Excepcion;

/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///     Comando para obtener categorias
/// </summary>
namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoObtenerCategorias: Comando
  {
    private DAO dao = FabricaDAO.CrearDAOCategoria();
    private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();
    //TODO: Esta debe ser una lista de Entidades, no de categorias.
    private IList<Categoria> resultado = new List<Categoria>();
    private string datosCategoria;
    private static Logger log = LogManager.GetCurrentClassLogger();

    public ComandoObtenerCategorias(Entidad entidad)
    {
      this.entidad = entidad;
    }

    public override void Ejecutar()
    {
      try
      {
        datosCategoria = " ID: " + ((Categoria)entidad).Id;
        resultado=((DAOCategoria)dao).ObtenerCategorias(entidad);
        log.Info("Categorias consultadas con exito: " + datosCategoria);
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
