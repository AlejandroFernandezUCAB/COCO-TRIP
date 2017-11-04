using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models.M1;
using ApiRest_COCO_TRIP.Models;
namespace ApiRest_COCO_TRIP.Controllers
{
  public class  M1_LoginController : ApiController
  {
    // GET api/<controller>
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/<controller>/5
    public string Get(int id)
    {
      return "value";
    }
    // GET api/<controller>/<action>/id
    [HttpGet]
    public Usuario IniciarSesion(int id)
    {
      Usuario usuario = new Usuario();
      usuario.Correo = "prueba@gmail.com";
      usuario.NombreUsuario = "prueba";
      ConexionBase conexion = new ConexionBase();
      conexion.SqlConexion.Open();
      conexion.Comando.Connection = conexion.SqlConexion;
      conexion.Comando.CommandText = "CREATE TABLE PRUEBA (idprueba integer CONSTRAINT firstkey PRIMARY KEY)";
      conexion.Comando.ExecuteNonQuery();
      return usuario;
    }

  
  }
}
