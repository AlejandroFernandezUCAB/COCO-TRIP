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
   * <summary>Clase encargada de recibir todas las peticiones relacionadas a Localidades de los eventos</summary>
   * **/
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M8_LocalidadEventoController : ApiController
  {
    /**
     * <summary>Metodo de controlador para Agregar un Localidad de Evento a la BBDD</summary>
     * **/
    [HttpPut]
    public bool AgregarLocalidadEvento(LocalidadEvento lEvento)
    {
      return PeticionLocalidadEvento.AgregarLocalidadEvento(lEvento);
    }
    /**
     * <summary>Metodo de controlador para Eliminar una Localidad de Evento de la BBDD</summary>
     * **/
    [HttpPut]
    public bool EliminarLocalidadEvento(int id)
    {
      return PeticionLocalidadEvento.EliminarLocalidadEvento(id);
    }
    /**
     * <summary>Metodo de controlador para Consultar una Localidad de evento dado su id todos los datos</summary>
     * **/
    [HttpGet]
    public LocalidadEvento ConsultarLocalidadEvento(int id)
    {
      return PeticionLocalidadEvento.ConsultarLocalidadEvento(id);
    }
    /**
     * <summary>Metodo de controlador para Listar todos las localidades de los eventos</summary>
     * **/
    [HttpGet]
    public List<LocalidadEvento> ListaLocalidadEventos(int id_categoria)
    {
      return PeticionLocalidadEvento.ListaLocalidadEventos();
    }

  }
}
