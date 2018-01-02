using System.Web.Http;
using System.Web.Http.Cors;
using System.Collections.Generic;
using System.Collections;
using System.Web.Http.Description;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;

namespace ApiRest_COCO_TRIP.Controllers
{

    ///<summary>
    ///Clase encargada de recibir todas las peticiones relacionadas a Localidades.
    ///</summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class M8_LocalidadEventoController : ApiController
    {
        private IDictionary respuesta = new Dictionary<string, object>();
        private Comando comando;


        ///<summary>
        ///Método encargado de agregar una localidad.
        ///</summary>
        ///<param name="data"> Localidad a agregar</param>
        [ResponseType(typeof(IDictionary))]
        [ActionName("agregarLocalidadEvento")]
        [HttpPost]
        public IDictionary AgregarLocalidadEvento([FromBody] LocalidadEvento data)
        {
            try
            {
                comando = FabricaComando.CrearComandoAgregarLocalidad(data);
                comando.Ejecutar();
                respuesta.Add("dato", "Se ha creado una localidad");
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
        ///Método encargado de eliminar una localidad.
        ///</summary>
        ///<param name="id"> id de la localidad a eliminar</param>
        [ResponseType(typeof(IDictionary))]
        [ActionName("EliminarLocalidadEventoPorId")]
        [HttpDelete]
        public IDictionary EliminarLocalidadEvento(int id)
        {
            try
            {
                comando = FabricaComando.CrearComandoEliminarLocalidad(id);
                comando.Ejecutar();
                respuesta.Add("dato", "Se ha eliminado una localidad");
            }
            catch (ItemNoEncontradoException e)
            {
                respuesta.Add("Error", e.Message);
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
        ///Método encargado de consultar una localidad.
        ///</summary>
        ///<param name="id"> id de la localidad a consultar</param>
        [ResponseType(typeof(IDictionary))]
        [ActionName("ConsultarLocalidadPorId")]
        [HttpGet]
        public IDictionary ConsultarLocalidadEvento(int id)
        {
            try
            {
                comando = FabricaComando.CrearComandoConsultarLocalidad(id);
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

            catch (OperacionInvalidaException e)
            {
                respuesta.Add("Error", e.Message);
            }
            return respuesta;
        }

        ///<summary>
        ///Método encargado de consultar todas las localidades.
        ///</summary>
        [ResponseType(typeof(IDictionary))]
        [ActionName("ListarLocalidades")]
        [HttpGet]
        public IDictionary ListaLocalidadEventos()
        {
            try
            {
                comando = FabricaComando.CrearComandoConsultarLocalidades();
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

            catch (OperacionInvalidaException e)
            {
                respuesta.Add("Error", e.Message);
            }
            return respuesta;
        }


        ///<summary>
        ///Método encargado de actualizar una localidad.
        ///</summary>
        ///<param name="data"> Localidad a actualizar</param>
        [ResponseType(typeof(IDictionary))]
        [ActionName("actualizarLocalidadEvento")]
        [HttpPut]
        public IDictionary ActualizarLocalidadEvento([FromBody] LocalidadEvento data)
        {
            try
            {
                comando = FabricaComando.CrearComandoModificarLocalidad(data);
                comando.Ejecutar();
                respuesta.Add("dato", "Se ha modificado una localidad");
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
