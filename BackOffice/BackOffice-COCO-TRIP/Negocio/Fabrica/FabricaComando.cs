using BackOffice_COCO_TRIP.Negocio.Componentes.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice_COCO_TRIP.Negocio.Fabrica
{
  public static class FabricaComando
  {
    public static ComandoConsultarLocalidad GetComandoConsultarLocalidad()
    {

      return new ComandoConsultarLocalidad();
    }
    public static ComandoConsultarLocalidades GetComandoConsultarLocalidades()
    {

      return new ComandoConsultarLocalidades();
    }
    public static ComandoEditarLocalidad GetComandoEditarLocalidad()
    {
      return new ComandoEditarLocalidad();

    }

    public static ComandoEliminarLocalidad GetComandoEliminarLocalidad()
    {
      return new ComandoEliminarLocalidad();
    }

    public static ComandoInsertarLocalidad GetComandoInsertarLocalidad()
    {
      return new ComandoInsertarLocalidad();
    }
    public static ComandoConsultarEventos GetComandoConsultarEventos()
    {
      return new ComandoConsultarEventos();
    }

    public static ComandoInsertarEvento GetComandoInsertarEvento()
    {
      return new ComandoInsertarEvento();
    }
      public static ComandoConsultarCategorias GetComandoConsultarCategorias()
      {
        return new ComandoConsultarCategorias();
      }
    public static ComandoFiltrarEventoPorCategoria GetComandoFiltrarEventoPorCategoria()
    {
      return new ComandoFiltrarEventoPorCategoria();
    }
  }
}
