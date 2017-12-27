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
  public class ComandoConsultarEventosPorCategoria : Comando
  {
    private Entidad categoria;
    private DAO daoEvento;
    private DAO daoCategoria;
    private List<Entidad> eventos;

    public ComandoConsultarEventosPorCategoria(int id)
    {
      this.categoria = FabricaEntidad.CrearEntidadCategoria();
      this.categoria.Id = id;
      daoEvento = FabricaDAO.CrearDAOEvento();
      daoCategoria = FabricaDAO.CrearDAOCategoria();
    }

    public override void Ejecutar()
    {
      try
      {
        eventos = daoEvento.ConsultarLista(categoria);
        List<Categoria> categorias = RetornarHijos(categoria);
        foreach (Categoria cate in categorias)
        {
          foreach (Evento ev in daoEvento.ConsultarLista(cate))
          {
            eventos.Add(ev);
          }
        }
      }

      catch (BaseDeDatosExcepcion e)
      {
        throw e;
      }
      catch (CasteoInvalidoExcepcion e)
      {
        throw e;
      }
      catch (OperacionInvalidaException e)
      {
        throw e;
      }
      catch (Exception e)
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
      return eventos;
    }

    private List<Categoria> RetornarHijos(Entidad papa) {
      List<Categoria> hijos = new List<Categoria>();
      foreach (Categoria hijo in ((DAOCategoria)daoCategoria).ObtenerCategorias(papa)) {
        hijos.Add(hijo);
        hijos.AddRange(RetornarHijos(hijo));
      }
      return hijos;
    }
  }
}
