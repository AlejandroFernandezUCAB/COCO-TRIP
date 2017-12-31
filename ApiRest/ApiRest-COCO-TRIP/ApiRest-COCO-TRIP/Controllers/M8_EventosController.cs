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

  /**
   * <summary>Clase encargada de recibir todas las peticiones relacionadas a Eventos</summary>
   * **/
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M8_EventosController : ApiController
  {
    private IDictionary respuesta = new Dictionary<string, object>();
    private Comando comando;

    /**
     * <summary>Metodo de controlador para Agregar un Evento a la BBDD</summary>
     * **/

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



    /**
     * <summary>Metodo de controlador para Eliminar un Evento de la BBDD</summary>
     * **/
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



    /**
     * <summary>Metodo de controlador para Consultar un evento dado su id todos los datos</summary>
     * <param name="id">Id del evento en la base de datos</param>
     * **/
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

    /**
     * <summary>Metodo de controlador para Listar todos los eventos de una Categoria</summary>
     * <param name="id_categoria">Id de la categoria en la base de datos</param>
     * **/
    [ResponseType(typeof(IDictionary))]
    [ActionName("ListarEventos")]
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
