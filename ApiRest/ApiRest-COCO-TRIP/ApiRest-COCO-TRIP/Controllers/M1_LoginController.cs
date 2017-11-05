using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using Npgsql;
using System.Data;
using Newtonsoft.Json;

namespace ApiRest_COCO_TRIP.Controllers
{
  public class M1_LoginController : ApiController
  {



    // GET api/<controller>/<action>/id
    [HttpGet]
    public int IniciarSesionCorreo(String datos)
    {
      Usuario usuario = JsonConvert.DeserializeObject<Usuario>(datos);
        PeticionLogin peticion = new PeticionLogin();
        return peticion.ConsultarUsuarioCorreo(usuario);
     }


  }
}
