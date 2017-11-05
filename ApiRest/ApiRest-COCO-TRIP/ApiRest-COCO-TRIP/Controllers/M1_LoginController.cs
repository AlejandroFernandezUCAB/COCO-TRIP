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
    private Usuario usuario;
    private PeticionLogin peticion;

    // GET api/<controller>/<action>/id
    [HttpGet]
    public int IniciarSesionCorreo(String datos)
    {
        usuario = JsonConvert.DeserializeObject<Usuario>(datos);
        peticion = new PeticionLogin();
        return peticion.ConsultarUsuarioCorreo(usuario);
     }

    public int IniciarSesionUsuario(String datos)
    {
        usuario = JsonConvert.DeserializeObject<Usuario>(datos);
        peticion = new PeticionLogin();
        return peticion.ConsultarUsuarioNombre(usuario);
    }

    public int IniciarSesionSocial(String datos)
    {
      usuario = JsonConvert.DeserializeObject<Usuario>(datos);
      peticion = new PeticionLogin();
      return peticion.ConsultarUsuarioSocial(usuario);

    }

    public int RegistrarUsuarioFacebook(string datos)
    {
      usuario = JsonConvert.DeserializeObject<Usuario>(datos);
      peticion = new PeticionLogin();
      return peticion.InsertarUsuarioFacebook(usuario);

    }


  }
}
