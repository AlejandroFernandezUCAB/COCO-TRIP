using System.Web.Http;
using System.Web.Http.Cors;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using ApiRest_COCO_TRIP.Models;

namespace ApiRest_COCO_TRIP.Controllers
{

  /**
   * <summary>Clase encargada de recibir todas las peticiones relacionadas a Localidades de los eventos</summary>
   * **/
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M8_LocalidadEventoController : ApiController
  {
    private IDictionary respuesta = new Dictionary<string, object>();
    private Comando comando;


    /**
     * <summary>Metodo de controlador para Agregar un Localidad de Evento a la BBDD</summary>
     * <param name="lEvento">Objeto con infomarcion de la localidad evento a agregar</param>
     * <exception cref="BaseDeDatosExcepcion"></exception>
     * <exception cref="ParametrosNullException"></exception>
     * <returns>Retorna el ID de la localidad agregada</returns>
     * **/
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


    /**
     * <summary>Metodo de controlador para Eliminar una Localidad de Evento de la BBDD</summary>
     * <param name="id">Id de la localidad a eliminar </param>
     * <returns>TRUE si se eliminó exitosamente false si no </returns>
     * **/
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


    /**
     * <summary>Metodo de controlador para Consultar una Localidad de evento dado su id todos los datos</summary>
     * <param name="id">Id del evento a consultar</param>
     * <returns>Objeto Localidad Evento </returns>
     * **/
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

    /**
     * <summary>Metodo de controlador para Listar todos las localidades de los eventos</summary>
     * <param name="id_categoria">id de la categoria a consultar sus eventos</param>
     * <returns>La lista de evento de dicha categoria</returns>
     * **/
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


    /**
     * <summary>Metodo de controlador para Agregar un Localidad de Evento a la BBDD</summary>
     * <param name="lEvento">Objeto con infomarcion de la localidad evento a agregar</param>
     * <exception cref="BaseDeDatosExcepcion"></exception>
     * <exception cref="ParametrosNullException"></exception>
     * <returns>Retorna el ID de la localidad agregada</returns>
     * **/
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
