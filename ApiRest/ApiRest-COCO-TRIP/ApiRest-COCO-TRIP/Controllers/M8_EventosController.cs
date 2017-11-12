using System.Net.Http;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using Npgsql;
using System.Data;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Web.Http.Cors;
using System.Collections.Generic;

namespace ApiRest_COCO_TRIP.Controllers
{

  /**
   * <summary>Clase encargada de recibir todas las peticiones relacionadas a Eventos</summary>
   * **/
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M8_EventosController : ApiController
  {
    
    /**
     * <summary>Metodo de controlador para Agregar un Evento a la BBDD</summary>
     * **/

      /**
    [HttpPut]
    public bool AgregarEvento(Evento evento)
    {
      PeticionEvento peticionEvento = new PeticionEvento();
      return peticionEvento.AgregarEvento(evento);
    }
    /**
     * <summary>Metodo de controlador para Eliminar un Evento de la BBDD</summary>
     * **/

      /**
    [HttpPut]
    public bool EliminarEvento(int id)
    {
      return PeticionEvento.EliminarEvento(id);
    }
    /**
     * <summary>Metodo de controlador para Consultar un evento dado su id todos los datos</summary>
     * **/
     /**
    [HttpGet]
    public Evento ConsultarEvento(int id)
    {
      return PeticionEvento.ConsultarEvento(id);
    }
    /**
     * <summary>Metodo de controlador para Listar todos los eventos de una Categoria</summary>
     * **/
     /**
    [HttpGet]
    public List<Evento> ListaEventosPorCategoria(int id_categoria)
    {
      return PeticionEvento.ListaEventosPorCategoria(id_categoria);
    }
    **/
  }
}
