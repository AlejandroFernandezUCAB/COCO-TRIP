using System.Web.Http;
using ApiRest_COCO_TRIP.Datos.Entity;
using System.Web.Http.Cors;
using System.Collections.Generic;
using System.Web.Http.Description;
using System.Collections;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using System;

namespace ApiRest_COCO_TRIP.Controllers
{

    ///<summary>
    ///Clase encargada de recibir todas las peticiones relacionadas a Eventos.
    ///</summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class M8_EventosController : ApiController
    {
        private IDictionary respuesta = new Dictionary<string, object>();
        private Comando comando;

        ///<summary>
        ///Método encargado de agregar un evento.
        ///</summary>
        ///<param name="data"> Evento a agregar</param>
        [ResponseType(typeof(IDictionary))]
        [ActionName("agregarEvento")]
        [HttpPost]
        public IDictionary AgregarEvento([FromBody] Evento data)
        {
            try
            {
                comando = FabricaComando.CrearComandoAgregarEvento(data);
                comando.Ejecutar();
                respuesta.Add("dato", "Se ha creado un evento");
            }
            catch (BaseDeDatosExcepcion e)
            {
                respuesta.Add("Error", e.Message);
            }
            catch (CasteoInvalidoExcepcion e)
            {
                respuesta.Add("Error", e.Message);
            }
            catch (Exception e)
            {
                respuesta.Add("Error", e.Message);
            }
            return respuesta;
        }



        ///<summary>
        ///Método encargado de eliminar un evento.
        ///</summary>
        ///<param name="id"> id del evento a eliminar</param>
        [ResponseType(typeof(IDictionary))]
        [ActionName("EliminarEventoPorId")]
        [HttpDelete]
        public IDictionary EliminarEvento(int id)
        {
            try
            {
                comando = FabricaComando.CrearComandoEliminarEvento(id);
                comando.Ejecutar();
                respuesta.Add("dato", "Se ha eliminado un evento");
            }
            catch (BaseDeDatosExcepcion e)
            {
                respuesta.Add("Error", e.Message);
            }
            catch (CasteoInvalidoExcepcion e)
            {
                respuesta.Add("Error", e.Message);
            }
            return respuesta;
        }



        ///<summary>
        ///Método encargado de consultar un evento.
        ///</summary>
        ///<param name="id"> id del evento a consultar</param>
        [ResponseType(typeof(IDictionary))]
        [ActionName("ConsultarEventoPorId")]
        [HttpGet]
        public IDictionary ConsultarEvento(int id)
        {
            try
            {
                comando = FabricaComando.CrearComandoConsultarEvento(id);
                comando.Ejecutar();
                respuesta.Add("dato", comando.Retornar());
            }
            catch (BaseDeDatosExcepcion e)
            {
                respuesta.Add("Error", e.Message);
            }
            catch (CasteoInvalidoExcepcion e)
            {
                respuesta.Add("Error", e.Message);
            }
            catch (OperacionInvalidaExcepcion e)
            {
                respuesta.Add("Error", e.Message);
            }
            return respuesta;
        }

        ///<summary>
        ///Método encargado de consultar todos los eventos .
        ///</summary>
        [ResponseType(typeof(IDictionary))]
        [ActionName("ListarEventos")]
        [HttpGet]
        public IDictionary ListarEventos()
        {
            return null;
        }

        ///<summary>
        ///Método encargado de consultar una lista de eventos dado un id de categoría.
        ///</summary>
        ///<param name="id"> id de la categoria para consultar una lista de eventos asociados
        ///a la categoría</param>
        [ResponseType(typeof(IDictionary))]
        [ActionName("ListarEventosPorCategoria")]
        [HttpGet]
        public IDictionary ListaEventosPorCategoria(int id)
        {
            try
            {
                comando = FabricaComando.CrearComandoConsultarEventosPorCategoria(id);
                comando.Ejecutar();
                respuesta.Add("dato", comando.RetornarLista());
            }
            catch (BaseDeDatosExcepcion e)
            {
                respuesta.Add("Error", e.Message);
            }
            catch (CasteoInvalidoExcepcion e)
            {
                respuesta.Add("Error", e.Message);
            }
            return respuesta;
        }

        ///<summary>
        ///Método encargado de actualizar un evento.
        ///</summary>
        ///<param name="data"> Evento a actualizar</param>
        [ResponseType(typeof(IDictionary))]
        [ActionName("actualizarEvento")]
        [HttpPut]
        public IDictionary ActualizarEvento([FromBody] Evento data)
        {
            try
            {
                comando = FabricaComando.CrearComandoModificarEvento(data);
                comando.Ejecutar();
                respuesta.Add("dato", "Se ha modificado un evento");
            }
            catch (BaseDeDatosExcepcion e)
            {
                respuesta.Add("Error", e.Message);
            }
            catch (CasteoInvalidoExcepcion e)
            {
                respuesta.Add("Error", e.Message);
            }
            return respuesta;

        }

    }
}