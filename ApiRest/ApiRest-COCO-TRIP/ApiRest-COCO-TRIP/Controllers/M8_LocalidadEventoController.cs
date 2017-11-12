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
     * <param name="lEvento">Objeto con infomarcion de la localidad evento a agregar</param>
     * <returns>Retorna el ID de la localidad agregada</returns>
     * **/
    [HttpGet]
    public int AgregarLocalidadEvento(LocalidadEvento lEvento)
    {

      PeticionLocalidadEvento peticionLocalidadEvento = new PeticionLocalidadEvento();
      return peticionLocalidadEvento.AgregarLocalidadEvento(lEvento);
    }
    /**
     * <summary>Metodo de controlador para Eliminar una Localidad de Evento de la BBDD</summary>
     * <param name="id">Id de la localidad a eliminar </param>
     * <returns>TRUE si se eliminó exitosamente false si no </returns>
     * **/
    [HttpGet]
    public bool EliminarLocalidadEvento(int id)
    {
      PeticionLocalidadEvento peticionLocalidadEvento = new PeticionLocalidadEvento();
      return peticionLocalidadEvento.EliminarLocalidadEvento(id);
    }
    /**
     * <summary>Metodo de controlador para Consultar una Localidad de evento dado su id todos los datos</summary>
     * <param name="id">Id del evento a consultar</param>
     * <returns>Objeto Localidad Evento </returns>
     * **/
    [HttpGet]
    public LocalidadEvento ConsultarLocalidadEvento(int id)
    {
      PeticionLocalidadEvento peticionLocalidadEvento = new PeticionLocalidadEvento();
      return peticionLocalidadEvento.ConsultarLocalidadEvento(id);
    }
    /**
     * <summary>Metodo de controlador para Listar todos las localidades de los eventos</summary>
     * <param name="id_categoria">id de la categoria a consultar sus eventos</param>
     * <returns>La lista de evento de dicha categoria</returns>
     * **/
    [HttpGet]
    public List<LocalidadEvento> ListaLocalidadEventos(int id_categoria)
    {
      PeticionLocalidadEvento peticionLocalidadEvento = new PeticionLocalidadEvento();
      return peticionLocalidadEvento.ListaLocalidadEventos();
    }

  }
}
