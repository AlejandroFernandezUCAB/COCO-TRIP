using BackOffice_COCO_TRIP.Negocio.Comandos;

namespace BackOffice_COCO_TRIP.Negocio.Fabrica
{
  /// <summary>
  /// Clase que representa la Fabrica de comandos, aqui se debe retornar todos los comandos existentes del sistema.
  /// </summary>
  public static class FabricaComando
  {
    /// <summary>
    /// Metodo que retorna el comando ComandoConsultarLocalidad
    /// </summary>
    /// <returns>El comando ComandoConsultarLocalidad</returns>
    public static ComandoConsultarLocalidad GetComandoConsultarLocalidad()
    {
      return new ComandoConsultarLocalidad();
    }

    /// <summary>
    /// Metodo que retorna el comando ComandoConsultarLocalidades
    /// </summary>
    /// <returns>El comando ComandoConsultarLocalidades</returns>
    public static ComandoConsultarLocalidades GetComandoConsultarLocalidades()
    {
      return new ComandoConsultarLocalidades();
    }

    /// <summary>
    /// Metodo que retorna el comando ComandoModificarLocalidad
    /// </summary>
    /// <returns>El comando ComandoModificarLocalidad</returns>
    public static ComandoModificarLocalidad GetComandoModificarLocalidad()
    {
      return new ComandoModificarLocalidad();
    }

    /// <summary>
    /// Metodo que retorna el comando ComandoModificarEvento
    /// </summary>
    /// <returns>El comando ComandoModificarEvento</returns>
    public static ComandoModificarEvento GetComandoModificarEvento()
    {
      return new ComandoModificarEvento();
    }

    /// <summary>
    /// Metodo que retorna el comando ComandoEliminarLocalidad
    /// </summary>
    /// <returns>El comando ComandoEliminarLocalidad</returns>
    public static ComandoEliminarLocalidad GetComandoEliminarLocalidad()
    {
      return new ComandoEliminarLocalidad();
    }

    /// <summary>
    /// Metodo que retorna el comando ComandoAgregarLocalidad
    /// </summary>
    /// <returns>El comando ComandoAgregarLocalidad</returns>
    public static ComandoAgregarLocalidad GetComandoAgregarLocalidad()
    {
      return new ComandoAgregarLocalidad();
    }
    /// <summary>
    /// Metodo que retorna el comando ComandoConsultarEventos
    /// </summary>
    /// <returns>El comando ComandoConsultarEventos</returns>
    public static ComandoConsultarEventos GetComandoConsultarEventos()
    {
      return new ComandoConsultarEventos();
    }

    /// <summary>
    /// Metodo que retorna el comando ComandoConsultarEvento
    /// </summary>
    /// <returns>El comando ComandoConsultarEvento</returns>
    public static ComandoConsultarEvento GetComandoConsultarEvento()
    {
      return new ComandoConsultarEvento();
    }

    /// <summary>
    /// Metodo que retorna el comando ComandoAgregarEvento
    /// </summary>
    /// <returns>El comando ComandoAgregarEvento</returns>
    public static ComandoAgregarEvento GetComandoAgregarEvento()
    {
      return new ComandoAgregarEvento();
    }

    /// <summary>
    /// Metodo que retorna el comando ComandoEliminarEvento
    /// </summary>
    /// <returns>El comando ComandoEliminarEvento</returns>
    public static ComandoEliminarEvento GetComandoEliminarEvento()
    {
      return new ComandoEliminarEvento();
    }

    /// <summary>
    /// Metodo que retorna el comando ComandoConsultarEventosPorCategoria
    /// </summary>
    /// <returns>El comando ComandoConsultarEventosPorCategoria</returns>
    public static ComandoConsultarEventosPorCategoria GetComandoFiltrarEventoPorCategoria()
    {
      return new ComandoConsultarEventosPorCategoria();
    }

    public static ComandoConsultarCategorias GetComandoConsultarCategorias()
    {
      return new ComandoConsultarCategorias();
    }

    public static ComandoModificarCategoria GetComandoModificarCategoria()
    {
      return new ComandoModificarCategoria();
    }
    public static ComandoEstadoCategoria GetComandoEstadoCategoria()
    {
      return new ComandoEstadoCategoria();
    }
    public static ComandoConsultarCategoriaHabilitada GetComandoConsultarCategoriaHabilitada()
    {
      return new ComandoConsultarCategoriaHabilitada();
    }

    public static ComandoConsultarCategoriaSelect GetComandoConsultarCategoriaSelect()
    {
      return new ComandoConsultarCategoriaSelect();
    }

    public static ComandoConsultarCategoriaPorId GetComandoConsultarCategoriaPorId()
    {
      return new ComandoConsultarCategoriaPorId();
    }

    public static ComandoAgregarCategoria GetComandoAgregarCategoria()
    {
      return new ComandoAgregarCategoria();
    }

    public static ComandoConsultarListaCategoria GetComandoConsultarListaCategoria()
    {
      return new ComandoConsultarListaCategoria();
    }

    public static ComandoConsultarLugaresTuristicos GetComandoConsultarLugaresTuristicos()
    {
      return new ComandoConsultarLugaresTuristicos();
    }

    public static ComandoAgregarLugarTuristico GetComandoAgregarLugarTuristico()
    {
      return new ComandoAgregarLugarTuristico();
    }

    public static ComandoAgregarActividad GetComandoAgregarActividad()
    {
      return new ComandoAgregarActividad();
    }

    public static ComandoAgregarHorario GetComandoAgregarHorario()
    {
      return new ComandoAgregarHorario();
    }

        public static ComandoConsultarLugarTuristico GetComandoConsultarLugarTuristico()
        {
            return new ComandoConsultarLugarTuristico();
        }
  }
}
