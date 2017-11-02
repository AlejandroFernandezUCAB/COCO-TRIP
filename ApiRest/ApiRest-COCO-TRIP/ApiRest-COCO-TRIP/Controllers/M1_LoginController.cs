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
      conexion.Sqlconexion.Open();
      conexion.Cmd.Connection = conexion.Sqlconexion;
      conexion.Cmd.CommandText = "CREATE TABLE PRUEBA (idprueba integer CONSTRAINT firstkey PRIMARY KEY)";
      conexion.Cmd.ExecuteNonQuery();
      return usuario;
    }

    // POST api/<controller>
    public void Post([FromBody]string value)
    {
    }

    // PUT api/<controller>/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/<controller>/5
    public void Delete(int id)
    {
    }
  }
}
