using System;
using System.Web.Http;
using Npgsql;
using System.Data;
using ApiRest_COCO_TRIP.Models;
namespace ApiRest_COCO_TRIP.Controllers
{
  public class M2_PreferenciasController : ApiController
  {
  
    [HttpGet]
    public Boolean Prueba()
    {     
      return true;
    }
  }

}