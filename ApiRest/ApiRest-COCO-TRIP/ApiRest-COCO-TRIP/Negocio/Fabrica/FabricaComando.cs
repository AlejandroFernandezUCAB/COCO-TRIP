using System;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Negocio.Command;
using Newtonsoft.Json.Linq;

namespace ApiRest_COCO_TRIP.Negocio.Fabrica
{
    /// <summary>
    /// Fabrica que instancia los comandos
    /// </summary>
    public class FabricaComando
    {
        /// <summary>
        /// Retorna la instancia de ComandoAgregarAmigo
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <param name="nombre">Nombre de usuario destino</param>
        /// <returns></returns>
        public static ComandoAgregarAmigo CrearComandoAgregarAmigo(int id, string nombre)
        {
            return new ComandoAgregarAmigo(id, nombre);
        }

        internal static Entidad CrearComandoAgregarItinerario()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retorna la instancia de ComandoVisualizarPerfilAmigo
        /// </summary>
        /// <param name="nombre">Nombre de usuario</param>
        /// <returns></returns>
        public static ComandoVisualizarPerfilAmigo CrearComandoVisualizarPerfilAmigo(string nombre)
        {
            return new ComandoVisualizarPerfilAmigo(nombre);
        }

        /// <summary>
        /// Retorna la instancia de ComandoEnviarNotificacionCorreo
        /// </summary>
        /// <param name="correo">Correo electronico de la persona a la que se le va a recomendar la aplicacion</param>
        /// <param name="id">ID del usuario que envia la notificacion</param>
        /// <param name="nombreDestino">Nombre de usuario al que va destinada la notificacion</param>
        /// <returns></returns>
        public static ComandoEnviarNotificacionCorreo CrearComandoEnviarNotificacionCorreo(string correo, int id, string nombreDestino)
        {
            return new ComandoEnviarNotificacionCorreo(correo, id, nombreDestino);
        }

        /// <summary>
        /// Retorna la instancia de ComandoSalirGrupo
        /// </summary>
        /// <param name="idGrupo">Identificador del grupo a eliminar/salir</param>
        /// <param name="idUsuario">Identificador del usuario que quiere eliminar o salir del grupo</param>
        /// <returns></returns>
        public static ComandoSalirGrupo CrearComandoSalirGrupo(int idGrupo, int idUsuario)
        {
            return new ComandoSalirGrupo(idGrupo, idUsuario);
        }

        /// <summary>
        /// Retorna la instancia de ComandoObtenerListaNotificaciones
        /// </summary>
        /// <param name="id">Identificador del usuario</param>
        /// <returns></returns>
        public static ComandoObtenerListaNotificaciones CrearComandoObtenerListaNotificaciones(int id)
        {
            return new ComandoObtenerListaNotificaciones(id);
        }

        /// <summary>
        /// Retorna la instancia de ComandoRechazarNotificacion
        /// </summary>
        /// <param name="id">Identificador del usuario que esta rechazando la notificacion</param>
        /// <param name="nombreRechazado">Nombre del usuario rechazado</param>
        /// <returns></returns>
        public static ComandoRechazarNotificacion CrearComandoRechazarNotificacion(int id, string nombreRechazado)
        {
            return new ComandoRechazarNotificacion(id, nombreRechazado);
        }

        /// <summary>
        /// Retorna la instancia de ComandoAceptarNotificacion
        /// </summary>
        /// <param name="id">Identificador del usuario que esta aceptando la notificacion</param>
        /// <param name="nombreAceptado">Nombre del usuario aceptado</param>
        /// <returns></returns>
        public static ComandoAceptarNotificacion CrearComandoAceptarNotificacion(int id, string nombreAceptado)
        {
            return new ComandoAceptarNotificacion(id, nombreAceptado);
        }

        /// <summary>
        /// Retorna la instancia de ComandoBuscarAmigos
        /// </summary>
        /// <param name="id">Identificador del usuario que esta buscando</param>
        /// <param name="nombre">Nombre del amigo a buscar</param>
        /// <returns></returns>
        public static ComandoBuscarAmigos CrearComandoBuscarAmigos(int id, string nombre)
        {
            return new ComandoBuscarAmigos(id, nombre);
        }


        /// <summary>
        /// Retorna la instancia de ComandoAgregarGrupo
        /// </summary>
        /// <param name="grupo">Datos del grupo a agregar</param>
        /// <returns></returns>
        public static ComandoAgregarGrupo CrearComandoAgregarGrupo(Entidad grupo)
        {
            return new ComandoAgregarGrupo(grupo);
        }

        /// <summary>
        /// Retorna la instancia de ComandoVisualizarListaAmigos
        /// </summary>
        /// <param name="id">Identificador del usuario</param>
        /// <returns></returns>
        public static ComandoVisualizarListaAmigos CrearComandoVisualizarListaAmigos(int id)
        {
            return new ComandoVisualizarListaAmigos(id);
        }

        /// <summary>
        /// Retorna la instancia de ComandoEliminarAmigo
        /// </summary>
        /// <param name="id">Identificador del usuario que quiere eliminar</param>
        /// <param name="nombreAmigo">Nombre de usuario del amigo a eliminar</param>
        /// <returns></returns>
        public static ComandoEliminarAmigo CrearComandoEliminarAmigo(int id, string nombreAmigo)
        {
            return new ComandoEliminarAmigo(id, nombreAmigo);
        }

        /// <summary>
        /// Retorna la instancia de ComandoEliminarGrupo
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idGrupo"></param>
        /// <returns></returns>
        public static ComandoEliminarGrupo CrearComandoEliminarGrupo(int idUsuario, int idGrupo)
        {
            return new ComandoEliminarGrupo(idUsuario, idGrupo);
        }

        /// <summary>
        /// Retorna la instancia de ComandoModificarGrupo
        /// </summary>
        /// <param name="grupo">Datos del grupo</param>
        /// <param name="id">Identificador del usuario que quiere modificar los datos del grupo</param>
        /// <returns></returns>
        public static ComandoModificarGrupo CrearComandoModificarGrupo(Entidad grupo, int id)
        {
            return new ComandoModificarGrupo(grupo, id);
        }

        /// <summary>
        /// Retorna la instancia de ComandoConsultarListaGrupos
        /// </summary>
        /// <param name="id">ID del usuario logeado en la aplicacion</param>
        /// <returns></returns>
        public static ComandoConsultarListaGrupos CrearComandoConsultarListaGrupos(int id)
        {
            return new ComandoConsultarListaGrupos(id);
        }

        /// <summary>
        /// Retornar la instancia de ComandoConsultarMiembroGrupo
        /// </summary>
        /// <param name="id">ID del grupo por el cual se devuelven sus integrantes</param>
        /// <returns></returns>
        public static ComandoConsultarMiembroGrupo CrearComandoConsultarMiembroGrupo(int id)
        {
            return new ComandoConsultarMiembroGrupo(id);
        }

        /// <summary>
        /// Retorna la instancia de ComandoConsultarPerfilGrupo
        /// </summary>
        /// <param name="id">ID del grupo a buscar</param>
        /// <returns></returns>
        public static ComandoConsultarPerfilGrupo CrearComandoConsultarPerfilGrupo(int id)
        {
            return new ComandoConsultarPerfilGrupo(id);
        }

        /// <summary>
        /// Retorna la instancia de ComandoAgregarIntegrante
        /// </summary>
        /// <param name="idGrupo">Identificador del grupo</param>
        /// <param name="nombreUsuario">Nombre del usuario a agregar</param>
        /// <returns></returns>
        public static ComandoAgregarIntegrante CrearComandoAgregarIntegrante(int idGrupo, string nombreUsuario)
        {
            return new ComandoAgregarIntegrante(idGrupo, nombreUsuario);
        }

        /// <summary>
        /// Retorna la instancia de ComandoEliminarIntegrante
        /// </summary>
        /// <param name="idGrupo">Identificador del grupo</param>
        /// <param name="nombreUsuario">Nombre del usuario a ser eliminado del grupo</param>
        /// <returns></returns>
        public static ComandoEliminarIntegrante CrearComandoEliminarIntegrante(int idGrupo, string nombreUsuario)
        {
            return new ComandoEliminarIntegrante(idGrupo, nombreUsuario);
        }

        /// <summary>
        /// Retorna la instancia de ComandoVerificarLider
        /// </summary>
        /// <param name="idGrupo">Identificador del grupo</param>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns></returns>
        public static ComandoVerificarLider CrearComandoVerificarLider(int idGrupo, int idUsuario)
        {
            return new ComandoVerificarLider(idGrupo, idUsuario);
        }

        /// <summary>
        /// Retorna la instancia de ComandoConsultarLider
        /// </summary>
        /// <param name="idGrupo">Identificador del grupo</param>
        /// <returns></returns>
        public static ComandoConsultarLider CrearComandoConsultarLider(int idGrupo)
        {
            return new ComandoConsultarLider(idGrupo);
        }

        /// <summary>
        /// Retornar la instancia de ComandoConsultarMiembroSinLider
        /// </summary>
        /// <param name="id">ID del grupo por el cual se devuelven sus integrantes</param>
        /// <returns></returns>
        public static ComandoConsultarMiembroSinLider CrearComandoConsultarMiembroSinLider(int id)
        {
            return new ComandoConsultarMiembroSinLider(id);
        }

        /// <summary>
        /// Retorna la instancia de ComandoConsultarMiembroSinGrupo
        /// </summary>
        /// <param name="idGrupo">Identificador del grupo</param>
        /// <param name="idUsuario">Identificador del usuario lider</param>
        /// <returns></returns>
        public static ComandoConsultarMiembroSinGrupo CrearComandoConsultarMiembroSinGrupo(int idGrupo, int idUsuario)
        {
            return new ComandoConsultarMiembroSinGrupo(idGrupo, idUsuario);
        }

        /// <summary>
        /// Retorna la instancia de ComandoConsultarUltimoGrupo
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns></returns>
        public static ComandoConsultarUltimoGrupo CrearComandoConsultarUltimoGrupo(int idUsuario)
        {
            return new ComandoConsultarUltimoGrupo(idUsuario);
        }

        /// <summary>
        /// Retorna la instancia de ComandoAgregarItinerario
        /// </summary>
        /// <param name ="idUsuario">Id del usuario</param>
        /// <param name ="nombreItinerario">Nombre del itinerario</param> 
        /// <returns></returns>
        public static ComandoAgregarItinerario CrearComandoAgregarItinerario(int idUsuario, string nombreItinerario)
        {
            return new ComandoAgregarItinerario(idUsuario, nombreItinerario);
        }

        /// <summary>
        /// Retorna la instancia de ComandoConsultarItinerario
        /// </summary>
        /// <param name ="idUsuario">Id del usuario</param>
        /// <returns></returns>
        public static ComandoConsultarItinerarios CrearComandoConsultarItinerarios(int idUsuario)
        {
            return new ComandoConsultarItinerarios(idUsuario);
        }

        /// <summary>
        /// Retorna la instancia de ComandoModificarItinerario
        /// </summary>
        /// <param name ="idUsuario">Id del usuario</param>
        /// <returns></returns>
        public static ComandoModificarItinerario CrearComandoModificarItinerario(int idItinerario, string nombre,
          DateTime fechaInicio, DateTime fechaFin, int idUsuario)
        {
            return new ComandoModificarItinerario(idItinerario, nombre, fechaInicio, fechaFin, idUsuario);
        }

        /// <summary>
        /// Retorna la instancia de ComandoEliminarItinerario
        /// </summary>
        /// <param name ="idUsuario">Id del usuario</param>
        /// <returns></returns>
        public static ComandoEliminarItinerario CrearComandoEliminarItinerario(int id)
        {
            return new ComandoEliminarItinerario(id);
        }
        /// Retorna la instancia de ComandoModificarCategoria
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns></returns>
        public static ComandoModificarCategoria CrearComandoModificarCategoria(Entidad entidad)
        {
            return new ComandoModificarCategoria(entidad);
        }


        /// Retorna la instancia de ComandoAgregarCategoria
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns></returns>
        public static ComandoAgregarCategoria CrearComandoAgregarCategoria(Entidad entidad)
        {
            return new ComandoAgregarCategoria(entidad);
        }

        /// Retorna la instancia de ComandoAgregarCategoria
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns></returns>
        public static ComandoObtenerCategoriaPorNombre CrearComandoObtenerCategoriaPorNombre(Entidad entidad)
        {
            return new ComandoObtenerCategoriaPorNombre(entidad);
        }

        /// <summary>
        /// Retorna la instancia de ComandoObtenerCategorias
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns></returns>
        public static ComandoObtenerCategorias CrearComandoObtenerCategorias(Entidad entidad)
        {
            return new ComandoObtenerCategorias(entidad);
        }

        /// <summary>
        /// Retorna la instancia de ComandoEstadoCategoria
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns></returns>
        public static ComandoEstadoCategoria CrearComandoEstadoCategoria(Entidad entidad)
        {
            return new ComandoEstadoCategoria(entidad);
        }

        /// <summary>
        /// Retorna la instancia de ComandoAgregarLocalidad
        /// </summary>
        /// <returns>ComandoAgregarLocalidad</returns>
        public static ComandoAgregarLocalidad CrearComandoAgregarLocalidad(Entidad localidad)
        {
            return new ComandoAgregarLocalidad(localidad);
        }

        /// <summary>
        /// Retorna la instancia de ComandoConsultarLocalidad
        /// </summary>
        /// <returns>ComandoConsultarLocalidad</returns>
        public static ComandoConsultarLocalidad CrearComandoConsultarLocalidad(int id)
        {
            return new ComandoConsultarLocalidad(id);
        }

        /// <summary>
        /// Retorna la instancia de ComandoConsultarLocalidades
        /// </summary>
        /// <returns>ComandoConsultarLocalidades</returns>
        public static ComandoConsultarLocalidades CrearComandoConsultarLocalidades()
        {
            return new ComandoConsultarLocalidades();
        }

        /// <summary>
        /// Retorna la instancia de ComandoModificarLocalidad
        /// </summary>
        /// <returns>ComandoModificarLocalidad</returns>
        public static ComandoModificarLocalidad CrearComandoModificarLocalidad(Entidad localidad)
        {
            return new ComandoModificarLocalidad(localidad);
        }

        /// <summary>
        /// Retorna la instancia de ComandoEliminarLocalidad
        /// </summary>
        /// <returns>ComandoEliminarLocalidad</returns>
        public static ComandoEliminarLocalidad CrearComandoEliminarLocalidad(int id)
        {
            return new ComandoEliminarLocalidad(id);
        }

        public static ComandoAgregarEvento CrearComandoAgregarEvento(Entidad evento)
        {
            return new ComandoAgregarEvento(evento);
        }

        /// <summary>
        /// Retorna la instancia de ComandoConsultarLocalidad
        /// </summary>
        /// <returns>ComandoConsultarLocalidad</returns>
        public static ComandoConsultarEvento CrearComandoConsultarEvento(int id)
        {
            return new ComandoConsultarEvento(id);
        }

        public static ComandoConsultarEventos CrearComandoConsultarEventos() {
            return new ComandoConsultarEventos();
        }

        /// <summary>
        /// Retorna la instancia de ComandoConsultarLocalidades
        /// </summary>
        /// <returns>ComandoConsultarLocalidades</returns>
        public static ComandoConsultarEventosPorCategoria CrearComandoConsultarEventosPorCategoria(int id)
        {
            return new ComandoConsultarEventosPorCategoria(id);
        }

        /// <summary>
        /// Retorna la instancia de ComandoModificarLocalidad
        /// </summary>
        /// <returns>ComandoModificarLocalidad</returns>
        public static ComandoModificarEvento CrearComandoModificarEvento(Entidad evento)
        {
            return new ComandoModificarEvento(evento);
        }

        /// <summary>
        /// Retorna la instancia de ComandoEliminarLocalidad
        /// </summary>
        /// <returns>ComandoEliminarLocalidad</returns>
        public static ComandoEliminarEvento CrearComandoEliminarEvento(int id)
        {
            return new ComandoEliminarEvento(id);
        }


        /// <summary>
        /// Retorna la instancia de ComandoAgregarAgenda
        /// </summary>
        /// <returns>ComandoAgregarAgenda</returns>
        public static ComandoAgregarAgenda CrearComandoAgregarAgenda(string tipo,
          int idItinerario, int idItem, DateTime fechaInicio, DateTime fechaFin)
        {
            return new ComandoAgregarAgenda(tipo, idItinerario, idItem, fechaInicio, fechaFin);
        }

        /// <summary>
        /// Retorna la instancia de ComandoEliminarAgendaItem
        /// </summary>
        /// <returns>ComandoEliminarAgendaItem</returns>
        public static ComandoEliminarAgendaItem CrearComandoEliminarAgendaItem(string tipo,
          int idItinerario, int idItem)
        {
            return new ComandoEliminarAgendaItem(tipo, idItem, idItinerario);
        }

        /// <summary>
        /// Retorna la instancia de ComandoSetVisibleItinerario
        /// </summary>
        /// <returns>ComandoSetVisibleItinerario</returns>
        public static ComandoSetVisibleItinerario CrearComandoSetVisibleItinerario(Boolean visible,
          int idUsuario, int idItinerario)
        {
            return new ComandoSetVisibleItinerario(idItinerario, idUsuario, visible);
        }
        /// <summary>
        /// Retorna la instancia de ComandoAgregarNotificacion
        /// </summary>
        /// <returns>ComandoAgregarNotificacion</returns>
        public static ComandoAgregarNotificacion CrearComandoAgregarNotificacion(int idUsuario)
        {
            return new ComandoAgregarNotificacion(idUsuario);
        }

        /// <summary>
        /// Retorna la instancia de ComandoEliminarNotificacion
        /// </summary>
        /// <returns>ComandoEliminarNotificacion</returns>
        public static ComandoEliminarNotificacion CrearComandoEliminarNotificacion(int idUsuario)
        {
            return new ComandoEliminarNotificacion(idUsuario);
        }

        /// <summary>
        /// Retorna la instancia de ComandoModificarNotificacion
        /// </summary>
        /// <returns>ComandoModificarNotificacion</returns>
        public static ComandoModificarNotificacion CrearComandoModificarNotificacion(int idUsuario, Boolean push, Boolean correo)
        {
            return new ComandoModificarNotificacion(idUsuario, push, correo);
        }
        /// <summary>
        /// Retorna la instancia de ComandoEnviarCorreoItinerario
        /// </summary>
        /// <returns>ComandoEnviarCorreoItinerario</returns>
        public static ComandoEnviarCorreoItinerario CrearComandoEnviarCorreoItinerario(int idUsuario)
        {
            return new ComandoEnviarCorreoItinerario(idUsuario);
        }

        /// <summary>
        /// Retorna la instancia de ComandoListarCoincidenciaLugaresTurisiticos
        /// </summary>
        /// <returns>ComandoListarCoincidenciaLugaresTurisiticos</returns>
        public static ComandoListarCoincidenciaLugaresTurisiticos CrearComandoListarCoincidenciaLugaresTurisiticos(string busqueda)
        {
            return new ComandoListarCoincidenciaLugaresTurisiticos(busqueda);
        }

        /// <summary>
        /// Retorna la instancia de ComandoListarCoincidenciaEventos
        /// </summary>
        /// <returns>ComandoListarCoincidenciaEventos</returns>
        public static ComandoListarCoincidenciaEventos CrearComandoListarCoincidenciaEventos(string busqueda, DateTime fechai,
          DateTime fechaf)
        {
            return new ComandoListarCoincidenciaEventos(busqueda, fechai, fechaf);
        }

        /// <summary>
        /// Retorna la instancia de ComandoListarCoincidenciaActividades
        /// </summary>
        /// <returns>ComandoListarCoincidenciaActividades</returns>
        public static ComandoListarCoincidenciaActividades CrearComandoListarCoincidenciaActividades(string busqueda)
        {
            return new ComandoListarCoincidenciaActividades(busqueda);
        }


        /// <summary>
        /// Retorna la instancia de ComandoConsultarNotificacion
        /// </summary>
        /// <returns>ComandoConsultarNotificacion</returns>
        public static ComandoConsultarNotificacion CrearComandoConsultarNotificacion(int idUsuario)
        {
            return new ComandoConsultarNotificacion(idUsuario);
        }


        /// <summary>
        /// Retorna la instancia de ComandoListarCoincidenciaActividades
        /// </summary>
        /// <returns>ComandoObtenerCategoriaPorId</returns>
        public static ComandoObtenerCategoriaPorId CrearComandoObtenerCategoriaPorId(Entidad entidad)
        {
            return new ComandoObtenerCategoriaPorId(entidad);
        }

        /// <summary>
        /// Retorna la instancia de ComandoObtenerCategoriasHabilitadas
        /// </summary>
        /// <returns>ComandoObtenerCategoriasHabilitadas</returns>
        public static ComandoObtenerCategoriasHabilitadas CrearComandoObtenerCategoriasHabilitadas()
        {
            return new ComandoObtenerCategoriasHabilitadas();

        }


        /// <summary>
        /// Retorna la instancia de  ComandoLTActualizarInformacion
        /// </summary>
        /// <returns>ComandoObtenerCategoriasHabilitadas</returns>
        public static ComandoLTActualizarInformacion CrearComandoLTActualizarInformacion()
        {
            return new ComandoLTActualizarInformacion();
        }

		/// <summary>
		/// Retorna la instancia de ComandoLTModificarUbicacion
		/// </summary>
		/// <returns></returns>
		public static ComandoLTModificarUbicacion CrearComandoLTModificarUbicacion()
        {
            return new ComandoLTModificarUbicacion();
        }

		/// <summary>
		/// Retorna la instancia de ComandoLTAgregarCategoria
		/// </summary>
		/// <returns></returns>
		public static ComandoLTAgregarCategoria CrearComandoLTAgregarCategoria(Entidad lugarTuristico)
        {
            return new ComandoLTAgregarCategoria(lugarTuristico);
        }

		/// <summary>
		/// Retorna la instancia de ComandoLTEliminarCategoria
		/// </summary>
		/// <returns></returns>
		public static ComandoLTEliminarCategoria CrearComandoLTEliminarCategoria()
        {
            return new ComandoLTEliminarCategoria();
        }

		/// <summary>
		/// Retorna la instancia de ComandoLTEliminarActividad
		/// </summary>
		/// <returns></returns>
		public static ComandoLTEliminarActividad CrearComandoLTEliminarActividad()
        {
            return new ComandoLTEliminarActividad();
        }

		/// <summary>
		/// Retorna la instancia de ComandoLTAgregarActividad
		/// </summary>
		/// <returns></returns>
		public static ComandoLTAgregarActividad CrearComandoLTAgregarActividad(Entidad lugarTuristico)
        {
            return new ComandoLTAgregarActividad(lugarTuristico);
        }

		/// <summary>
		/// Retorna la instancia de ComandoLTEliminarFoto
		/// </summary>
		/// <returns></returns>
		public static ComandoLTEliminarFoto CrearComandoLTEliminarFoto(JObject foto)
        {
            return new ComandoLTEliminarFoto(foto);
        }

		/// <summary>
		/// Retorna la instancia de ComandoLTAgregarFoto
		/// </summary>
		/// <returns></returns>
		public static ComandoLTAgregarFoto CrearComandoLTAgregarFoto(Entidad objeto)
        {
            return new ComandoLTAgregarFoto(objeto);
        }

		/// <summary>
		/// Retorna la instancia de ComandoAgregarLT
		/// </summary>
		/// <returns></returns>
		public static ComandoAgregarLT CrearComandoLTAgregar(JObject datos)
		{
			return new ComandoAgregarLT(datos);
		}

		/// <summary>
		/// Retorna la instancia de ComandoLTAgregarHorario
		/// </summary>
		/// <returns></returns>
		public static ComandoLTAgregarHorario CrearComandoLTAgregarHorario(Entidad lugarTuristico)
        {
            return new ComandoLTAgregarHorario(lugarTuristico);
        }

		public static ComandoObtenerLugaresTuristicos CrearComandoObtenerLugaresTuristicos()
		{
			return new ComandoObtenerLugaresTuristicos();
		}

    }
}
