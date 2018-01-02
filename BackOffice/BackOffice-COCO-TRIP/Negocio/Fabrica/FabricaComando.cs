using BackOffice_COCO_TRIP.Negocio.Comandos;


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

        public static ComandoModificarLocalidad GetComandoEditarLocalidad()
        {
            return new ComandoModificarLocalidad();
        }

        public static ComandoModificarEvento GetComandoEditarEvento()
        {
            return new ComandoModificarEvento();
        }

    public static ComandoEliminarLocalidad GetComandoEliminarLocalidad()
        {
            return new ComandoEliminarLocalidad();
        }

        public static ComandoAgregarLocalidad GetComandoInsertarLocalidad()
        {
            return new ComandoAgregarLocalidad();
        }
        public static ComandoConsultarEventos GetComandoConsultarEventos()
        {
            return new ComandoConsultarEventos();
        }

        public static ComandoConsultarEvento GetComandoConsultarEvento()
        {
            return new ComandoConsultarEvento();
        }

    public static ComandoAgregarEvento GetComandoInsertarEvento()
        {
            return new ComandoAgregarEvento();
        }

        public static ComandoEliminarEvento GetComandoEliminarEvento()
        {
            return new ComandoEliminarEvento();
        }

        public static ComandoFiltrarEventoPorCategoria GetComandoFiltrarEventoPorCategoria()
        {
            return new ComandoFiltrarEventoPorCategoria();
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

  }
}
