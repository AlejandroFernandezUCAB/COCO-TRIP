using System.Net;
using System.Web.Http;
using System.Collections.Generic;
using System.Web.Http.Cors;
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Models.Dato;
using ApiRest_COCO_TRIP.Models.BaseDeDatos;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using System;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Web.Http.Description;
using System.Collections;
using ApiRest_COCO_TRIP.Datos.Singleton;

namespace ApiRest_COCO_TRIP.Controllers
{
  /// <summary>
  /// Controlador del Modulo 7 de Gestion de Lugares Turisticos y Actividades en Lugares Turisticos
  /// Integrantes : Pedro Fernandez
  ///				GianFranco Verrocchi
  /// </summary>
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M7_LugaresTuristicosController : ApiController
    {
        private PeticionLugarTuristico peticion; //Clase que interactua con la clase Conexion <-- Esto hay que borrarlo luego.
												 //y que permite al controlador consultar/insertar/actualizar/eliminar datos en la base de datos
		private Comando com;
		private IDictionary response;
		private MensajeResultadoOperacion mensaje = MensajeResultadoOperacion.ObtenerInstancia();
		private const String data = "data";
		private const String error = "error";
		//GET

		/// <summary>
		/// Consulta la lista de lugares turisticos dentro del rango establecido
		/// </summary>
		/// <returns>Lista de lugares turisticos con ID, nombre, costo, descripcion y estado 
		/// de cada lugar turistico. Formato JSON</returns>
		/// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
		[HttpGet]
		[ResponseType(typeof(IDictionary))]
		[ActionName("ListaLugaresTuristicos")]
		public IDictionary GetLista ()
        {
			response = new Dictionary<string, object>();

			try
			{
				com = FabricaComando.CrearComandoObtenerLugaresTuristicos();
				com.Ejecutar();
				response.Add( data, ((ComandoObtenerLugaresTuristicos)com).RetornarLista() );
				
			}
			catch (ReferenciaNulaExcepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}
			catch (CasteoInvalidoExcepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}
			catch (BaseDeDatosExcepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}
			catch (Excepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}

			return response;

		}

        /// <summary>
        /// Consulta el detalle del lugar turistico y los nombres de las actividades asociadas
        /// </summary>
        /// <param name="datos">JSON de Lugar turistico</param>
        /// <returns>Datos del lugar turistico y nombre de las actividades. Formato JSON</returns>
        [HttpPost]
		[ResponseType(typeof(IDictionary))]
		[ActionName("ListaLugaresTuristicosDetallados")]
		public IDictionary GetLugar (JObject datos)
        {
			response = new Dictionary<string, object>();
			try
			{
				com = FabricaComando.CrearComandoConsultarLugarTuristicoDetallado(datos);
				com.Ejecutar();
				response.Add(data, com.Retornar() );
			}
			catch (ReferenciaNulaExcepcion)
			{

				response.Add(error, mensaje.ErrorParametrosNull);

			}
			catch (CasteoInvalidoExcepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}
			catch (BaseDeDatosExcepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}
			catch (Excepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}

			return response;
        }

       
		
		/// <summary>
		/// Elimina la foto de un lugar turistico
		/// </summary>
		/// <param name="foto">Objeto Foto</param>
		/// <returns>Mensaje Con el resultado de la operacion</returns>
		/// <exception cref="ReferenciaNulaExcepcion">Existe un elemento nulo en el objeto</exception>
		/// <exception cref="CasteoInvalidoExcepcion">No se pudo pasar de un objeto a otro la variable</exception>
		/// <exception cref="BaseDeDatosExcepcion">Error en base de datos</exception>
		/// <exception cref="Excepcion">Error inesperado</exception>
		[HttpDelete]
    [ResponseType(typeof(IDictionary))]
		[ActionName("EliminarFotoLugarTuristico")]
    public IDictionary DeleteFoto(JObject datos)
    {
      response = new Dictionary<string, object>();
      try
      {
          com = FabricaComando.CrearComandoLTEliminarFoto(datos);
          com.Ejecutar();
          response.Add(data, mensaje.ExitoEliminarFoto);
      }
catch (ReferenciaNulaExcepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}
			catch (CasteoInvalidoExcepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}
			catch (BaseDeDatosExcepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}
			catch (Excepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}

			return response;
    }


		//POST

		/// <summary>
		/// Inserta los datos del lugar turistico
		/// </summary>
		/// <param name="datos">Objeto LugarTuristico</param>
		/// <returns>Mensaje Con el resultado de la operacion</returns>
		/// <exception cref="ReferenciaNulaExcepcion">Existe un elemento nulo en el objeto</exception>
		/// <exception cref="CasteoInvalidoExcepcion">No se pudo pasar de un objeto a otro la variable</exception>
		/// <exception cref="BaseDeDatosExcepcion">Error en base de datos</exception>
		/// <exception cref="Excepcion">Error inesperado</exception>
		[HttpPost]
		[ResponseType(typeof(IDictionary))]
		[ActionName("AgregarLugarTuristico")]
		public IDictionary PostLugar(JObject datos)
        {
			response = new Dictionary<string, object>();

			try
			{			

				com = FabricaComando.CrearComandoLTAgregar(datos);
				com.Ejecutar();
				response.Add(data, mensaje.ExitoInsertar);

			}
			catch (ReferenciaNulaExcepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}
			catch (CasteoInvalidoExcepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}
			catch (BaseDeDatosExcepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}
			catch (Excepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}

			return response;


		}


		/// <summary>
		/// Elimina una actividad del sistema
		/// </summary>
		/// <param name="datos">Objeto Actividad</param>
		/// <returns>Mensaje Con el resultado de la operacion</returns>
		/// <exception cref="ReferenciaNulaExcepcion">Existe un elemento nulo en el objeto</exception>
		/// <exception cref="CasteoInvalidoExcepcion">No se pudo pasar de un objeto a otro la variable</exception>
		/// <exception cref="BaseDeDatosExcepcion">Error en base de datos</exception>
		/// <exception cref="Excepcion">Error inesperado</exception>
		[HttpPost]
        [ResponseType(typeof(IDictionary))]
        [ActionName("EliminarActividad")]
        public IDictionary DeleteActividad(JObject datos)
        {
            response = new Dictionary<string, object>();
            try
            {
                //com = FabricaComando.CrearComandoLTEliminarActividad(datos);
                //com.Ejecutar();
                //response.Add(data, mensaje.ExitoInsertar);
            }
            catch (ReferenciaNulaExcepcion)
            {

                response.Add(error, mensaje.ErrorInesperado);

            }
            catch (CasteoInvalidoExcepcion)
            {

                response.Add(error, mensaje.ErrorInesperado);

            }
            catch (BaseDeDatosExcepcion)
            {

                response.Add(error, mensaje.ErrorInesperado);

            }
            catch (Excepcion)
            {

                response.Add(error, mensaje.ErrorInesperado);

            }

            return response;
        }


		/// <summary>
		/// Actualiza los datos de un lugar turistico
		/// </summary>
		/// <param name="lugar">Objeto LugarTuristico</param>
		/// <returns>Mensaje Con el resultado de la operacion</returns>
		/// <exception cref="ReferenciaNulaExcepcion">Existe un elemento nulo en el objeto</exception>
		/// <exception cref="CasteoInvalidoExcepcion">No se pudo pasar de un objeto a otro la variable</exception>
		/// <exception cref="BaseDeDatosExcepcion">Error en base de datos</exception>
		/// <exception cref="Excepcion">Error inesperado</exception>
		[HttpPut]
        [ResponseType(typeof(IDictionary))]
        [ActionName("ActualizarLugarTuristico")]
        public IDictionary PutLugar(JObject datos)
        {
            response = new Dictionary<string, object>();
            try
            {
                com = FabricaComando.CrearComandoLTActualizarInformacion(datos);
                com.Ejecutar();
                response.Add(data, mensaje.ExitoModificar);
            }
            catch (ReferenciaNulaExcepcion)
            {

                response.Add(error, mensaje.ErrorInesperado);

            }
            catch (CasteoInvalidoExcepcion)
            {

                response.Add(error, mensaje.ErrorInesperado);

            }
            catch (BaseDeDatosExcepcion)
            {

                response.Add(error, mensaje.ErrorInesperado);

            }
            catch (Excepcion)
            {

                response.Add(error, mensaje.ErrorInesperado);

            }

            return response;
        }
       
		/// <summary>
		/// Activa o desactiva el lugar turistico
		/// </summary>
		/// <param name="datos">JSON con los datos a cambiar</param>
		/// <exception cref="ReferenciaNulaExcepcion">Existe un elemento nulo en el objeto</exception>
		/// <exception cref="CasteoInvalidoExcepcion">No se pudo pasar de un objeto a otro la variable</exception>
		/// <exception cref="BaseDeDatosExcepcion">Error en base de datos</exception>
		/// <exception cref="Excepcion">Error inesperado</exception>
		[HttpPut]
		[ResponseType(typeof(IDictionary))]
		[ActionName("ActualizarEstadoLugarTuristico")]
		public void PutActivarLugar(JObject datos)
        {

			response = new Dictionary<string, object>();
			try
			{
				com = FabricaComando.CrearComandoActualizarEstadoLT(datos);
				com.Ejecutar();
				response.Add(data, mensaje.ExitoModificar);

			}
			catch (ReferenciaNulaExcepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}
			catch (CasteoInvalidoExcepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}
			catch (BaseDeDatosExcepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}
			catch (Excepcion)
			{

				response.Add(error, mensaje.ErrorInesperado);

			}

			
		}

       

    }

}
