using System.Net.Http;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using Npgsql;
using System.Data;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Web.Http.Cors;
using System.Collections.Generic;
using System;

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

    [HttpPut]
    public int AgregarEvento(Evento evento)
    {
      PeticionEvento peticionEvento = new PeticionEvento();
      return peticionEvento.AgregarEvento(evento);
    }
    
    /**
     * <summary>Metodo de controlador para Eliminar un Evento de la BBDD</summary>
     * **/

    [HttpPut]
    public bool EliminarEvento(int id)
    {
      PeticionEvento peticionEvento = new PeticionEvento();
      return peticionEvento.EliminarEvento(id);
    }
    
    /**
     * <summary>Metodo de controlador para Consultar un evento dado su id todos los datos</summary>
     * <param name="id">Id del evento en la base de datos</param>
     * **/

    [HttpGet]
    public Evento ConsultarEvento(int id)
    {
      PeticionEvento peticionEvento = new PeticionEvento();
      return peticionEvento.ConsultarEvento(id);
    }
    
    /**
     * <summary>Metodo de controlador para Listar todos los eventos de una Categoria</summary>
     * <param name="id_categoria">Id de la categoria en la base de datos</param>
     * **/

    [HttpGet]
    public List<Evento> ListaEventosPorCategoria(int id_categoria)
    {
      PeticionEvento peticionEvento = new PeticionEvento();
      return peticionEvento.ListaEventosPorCategoria(id_categoria);
    }
    /**
     * <summary>Metodo de controlador para listar todas las categorias desde la fecha danda</summary>
     * <param name="date">Fecha desde donde se buscaran los eventos</param>
     * */
    [HttpGet]
    public List<Evento> ListarEventosPorFecha(DateTime date)
    {
      PeticionEvento peticionEvento = new PeticionEvento();
      return peticionEvento.ListaEventosPorFecha(date);
    }

  }
}
