using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Comun.Validaciones;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.Singleton;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///     Controlador del Modulo 9, Categorias.
/// </summary>
namespace ApiRest_COCO_TRIP.Controllers
{
    /// <summary>
    /// Controlador del Modulo 9, Categorias.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class M9_CategoriasController : ApiController
    {
        private IDictionary response = new Dictionary<string, object>();
        private const string Response_Data = "data";
        private const string Response_Error = "error";
        private Comando com;
        private MensajeResultadoOperacion mensaje = MensajeResultadoOperacion.ObtenerInstancia();
        private Entidad categoria = FabricaEntidad.CrearEntidadCategoria();

        /// <summary>
        /// EndPoint para actualizar el estatus de una categoria a aprtir de el Id
        /// </summary>
        /// <param name="data">JObject de tipo categoria cuyo status se quiere actualizar.</param>
        /// <returns>Json respuesta a la solicitud de la peticion.</returns>
        /// <exception cref="JsonSerializationException">Error al Serializar el Json.</exception>
        /// <exception cref="ParametrosNullExcepcion">Error al intentar actualizar, existe algun parametro nulo.</exception>
        /// <exception cref="BaseDeDatosExcepcion">Error al realizar operacion a la base de datos.</exception>
        /// <exception cref="Excepcion">Error inesperado.</exception>
        [ResponseType(typeof(IDictionary))]
        [ActionName("actualizarEstatus")]
        [HttpPut]
        public IDictionary ActualizarEstatusCategoria([FromBody] JObject data)
        {
            response = new Dictionary<string, object>();
            try
            {
                ValidacionWS.ValidarParametrosNotNull(data, new List<string> {
                    "id","estatus"});
                categoria = data.ToObject<Categoria>();
                com = FabricaComando.CrearComandoEstadoCategoria(categoria);
                com.Ejecutar();
                response.Add(Response_Data, mensaje.ExitoModificar);
            }
            catch (JsonSerializationException ex)
            {
                response.Add(Response_Error, ex.Message);
            }
            catch (BaseDeDatosExcepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorInternoServidor);
            }
            catch (ParametrosNullExcepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorParametrosNull);
            }
            catch (Excepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorInesperado);
            }
            return response;
        }

        /// <summary>
        /// EndPoint para obtener las categorias hijas a partir de una categoria padre dado un ID,
        /// si el id no viene en la peticion se devuelve las categorias padres absolutas
        /// </summary>
        /// <param name="id">Identificador unico de la categoria a la cual se requiere saber la subcategoria</param>
        /// <returns>Json respuesta a la solicitud de la peticion.</returns>
        /// <exception cref="System.ArgumentNullException">Error al generar la respuesta, argumento nulo.</exception>
        /// <exception cref="System.ArgumentException">Error al generar la respuesta, argumento invalido.</exception>
        /// <exception cref="System.NotSupportedException">Error al generar la respuesta, parametro no soportado.</exception>
        /// <exception cref="BaseDeDatosExcepcion">Error al obtener las categorias.</exception>
        /// <exception cref="Excepcion">Error inesperado.</exception>
        [ResponseType(typeof(IDictionary))]
        [ActionName("listarCategorias")]
        [HttpGet]
        public IDictionary ObtenerCategorias(int id = -1)
        {
            try
            {
                categoria = new Categoria(id);
                com = FabricaComando.CrearComandoObtenerCategorias(categoria);
                com.Ejecutar();
                response.Add(Response_Data, ((ComandoObtenerCategorias)com).RetornarLista());
            }
            catch (System.ArgumentNullException ex)
            {
                response.Add(Response_Error, ex.Message);
            }
            catch (System.ArgumentException ex)
            {
                response.Add(Response_Error, ex.Message);
            }
            catch (System.NotSupportedException ex)
            {
                response.Add(Response_Error, ex.Message);
            }
            catch (BaseDeDatosExcepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorInternoServidor);
            }
            catch (Excepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorInesperado);
            }
            return response;
        }

        /// <summary>
        /// EndPoint para modificar los datos de la categoria.
        /// </summary>
        /// <param name="data">JObject de tipo categoria el cual se quiere modificar.</param>
        /// <returns>Json respuesta a la solicitud de la peticion.</returns>
        /// <exception cref="ParametrosNullExcepcion">Los parametros de la instancia no cumple con las condiciones para la informacion de una categoria.</exception>
        /// <exception cref="JsonSerializationException">Error al Serializar el Json.</exception>        
        /// <exception cref="ParametrosInvalidosExcepcion">Los parametros de la instancia no cumple con las condiciones para la informacion de una categoria</exception>
        /// <exception cref="BaseDeDatosExcepcion">Error al modificar la categoria.</exception>
        /// <exception cref="NombreDuplicadoExcepcion">Error en duplicidad en el nombre de la categoria que intenta actualizar.</exception>
        /// <exception cref="HijoConDePendenciaExcepcion">La categoria que intenta actualizar tiene dependencias.</exception>
        /// <exception cref="ArgumentoNuloExcepcion">Error al utilizar el ToList, para convertir la lista a Categorias.</exception>
        /// <exception cref="Excepcion">Error inesperado.</exception>
        [ResponseType(typeof(IDictionary))]
        [ActionName("ModificarCategoria")]
        [HttpPut]
        public IDictionary ModificarCategorias([FromBody] JObject data)
        {
            try
            {
                ValidacionWS.ValidarParametrosNotNull(data, new List<string> {
                    "nombre","descripcion","categoriaSuperior","nivel"});
                categoria = data.ToObject<Categoria>();
                ValidacionString.ValidarCategoria(categoria);
                com = FabricaComando.CrearComandoModificarCategoria(categoria);
                com.Ejecutar();
                response.Add(Response_Data, mensaje.ExitoModificar);
            }
            catch (ParametrosNullExcepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorParametrosNull);
            }
            catch (ArgumentoNuloExcepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorParametrosNull);
            }
            catch (ParametrosInvalidosExcepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorFormatoCampoCategoria);
            }
            catch (HijoConDePendenciaExcepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorCategoriaAsociada);
            }
            catch (NombreDuplicadoExcepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorCategoriaDuplicada);
            }
            catch (JsonSerializationException ex)
            {
                response.Add(Response_Error, ex.Message);
            }
            catch (BaseDeDatosExcepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorInternoServidor);
            }
            catch (Excepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorInesperado);
            }
            return response;

        }

        /// <summary>
        /// EndPoint para obtener todas las categorias Habilitadas.
        /// </summary>
        /// <returns>Json respuesta a la solicitud de la peticion.</returns>
        /// <exception cref="System.ArgumentNullException">Error al generar la respuesta, argumento nulo.</exception>
        /// <exception cref="System.ArgumentException">Error al generar la respuesta, argumento invalido.</exception>
        /// <exception cref="System.NotSupportedException">Error al generar la respuesta, parametro no soportado.</exception>
        /// <exception cref="BaseDeDatosExcepcion">Error al obtener las categorias.</exception>
        /// <exception cref="Excepcion">Error inesperado.</exception>
        [ResponseType(typeof(IDictionary))]
        [ActionName("CategoriasHabilitadas")]
        [HttpGet]
        public IDictionary ObtenerCategoriasHabilitadas()
        {
            try
            {
                com = FabricaComando.CrearComandoObtenerCategoriasHabilitadas();
                com.Ejecutar();
                response.Add(Response_Data, ((ComandoObtenerCategoriasHabilitadas)com).RetornarLista());
            }
            catch (System.ArgumentNullException ex)
            {
                response.Add(Response_Error, ex.Message);
            }
            catch (System.ArgumentException ex)
            {
                response.Add(Response_Error, ex.Message);
            }
            catch (System.NotSupportedException ex)
            {
                response.Add(Response_Error, ex.Message);
            }
            catch (BaseDeDatosExcepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorInternoServidor);
            }
            catch (Excepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorInesperado);
            }
            return response;
        }

        /// <summary>
        /// EndPoint para agregar categorias.
        /// </summary>
        /// <param name="data">JObject de tipo categoria el cual se quiere agregar.</param>
        /// <returns>Json respuesta a la solicitud de la peticion.</returns>
        /// <exception cref="JsonSerializationException">Error al Serializar el Json.</exception>
        /// <exception cref="ParametrosNullExcepcion">Los parametros de la instancia no cumple con las condiciones para la informacion de una categoria.</exception>
        /// <exception cref="ParametrosInvalidosExcepcion">Los parametros de la instancia no cumple con las condiciones para la informacion de una categoria</exception>
        /// <exception cref="BaseDeDatosExcepcion">Error al agregar la categoria</exception>
        /// <exception cref="NombreDuplicadoExcepcion">Error en duplicidad en el nombre de la categoria que intenta actualizar.</exception>        
        /// <exception cref="Excepcion">Error inesperado.</exception>
        [ResponseType(typeof(IDictionary))]
        [ActionName("AgregarCategoria")]
        [HttpPost]
        public IDictionary AgregarCategoria([FromBody] JObject data)
        {
            try
            {
                ValidacionWS.ValidarParametrosNotNull(data, new List<string> {
                    "nombre","descripcion","categoriaSuperior","nivel"});
                categoria = data.ToObject<Categoria>();
                ValidacionString.ValidarCategoria(categoria);
                com = FabricaComando.CrearComandoAgregarCategoria(categoria);
                com.Ejecutar();
                response.Add(Response_Data, mensaje.ExitoInsertarCategoria);
            }
            catch (ParametrosNullExcepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorParametrosNull);
            }
            catch (JsonSerializationException ex)
            {
                response.Add(Response_Error, ex.Message);
            }
            catch (ParametrosInvalidosExcepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorFormatoCampoCategoria);
            }
            catch (NombreDuplicadoExcepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorCategoriaDuplicada);
            }
            catch (BaseDeDatosExcepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorInternoServidor);
            }
            catch (Excepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorInesperado);
            }
            return response;
        }

        /// <summary>
        /// EndPoint para consultar una categoria por ID
        /// </summary>
        /// <param name="id">Identificar unico de la categoria que se requiere consultar.</param>
        /// <returns>Json respuesta a la solicitud de la peticion.</returns>
        /// <exception cref="System.ArgumentNullException">Error al generar la respuesta, argumento nulo.</exception>
        /// <exception cref="System.ArgumentException">Error al generar la respuesta, argumento invalido.</exception>
        /// <exception cref="System.NotSupportedException">Error al generar la respuesta, parametro no soportado.</exception>
        /// <exception cref="BaseDeDatosExcepcion">Error al obtener la categoria.</exception>
        /// <exception cref="Excepcion">Error inesperado.</exception>
        [ResponseType(typeof(IDictionary))]
        [HttpGet]
        [ActionName("obtenerCategoriasPorId")]
        public IDictionary ObtenerCategoriaPorId(int id)
        {
            try
            {
                categoria.Id = id;
                com = FabricaComando.CrearComandoObtenerCategoriaPorId(categoria);
                com.Ejecutar();
                response.Add(Response_Data, ((ComandoObtenerCategoriaPorId)com).RetornarLista());
            }
            catch (System.ArgumentNullException ex)
            {
                response.Add(Response_Error, ex.Message);
            }
            catch (System.ArgumentException ex)
            {
                response.Add(Response_Error, ex.Message);
            }
            catch (System.NotSupportedException ex)
            {
                response.Add(Response_Error, ex.Message);
            }
            catch (BaseDeDatosExcepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorInternoServidor);
            }
            catch (Excepcion ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                response.Add("MensajeError", mensaje.ErrorInesperado);
            }
            return response;
        }
    }

}
