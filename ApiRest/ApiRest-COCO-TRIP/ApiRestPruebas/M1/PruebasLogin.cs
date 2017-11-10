using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Controllers;
using Npgsql;
using Newtonsoft.Json;
using System.Web.Http;
using System.Net;

namespace ApiRestPruebas
{
  [TestFixture]
  public class PruebasLogin
  {
    private Usuario usuario;
    private PeticionLogin peticion = new PeticionLogin();
    private M1_LoginController controlador = new M1_LoginController();

    [SetUp]
    public void setUsuario()
    {
      usuario = new Usuario
      {
        Nombre = "Carlos",
        Apellido = "Valero",
        Genero = "M",
        NombreUsuario = "pepo",
        FechaNacimiento = new DateTime(2017, 03, 09),
        Correo = "kilordpepo@gmail.com",
        Clave = "pruebaclave",
        Foto = new byte[28480]
      };
    }

    [Test]
    [Category("Insertar")]
    public void TestInsertarUsuarioFacebook()
    {
      Assert.AreEqual(1, peticion.InsertarUsuarioFacebook(usuario));
      Assert.Throws<PostgresException>(() => {
        peticion.InsertarUsuarioFacebook(usuario);
      });
      Assert.Throws<InvalidCastException>(() => {
          usuario.Nombre = null;
          peticion.InsertarUsuarioFacebook(usuario);
        });
     
    }

    [Test]
    [Category("Consultar")]
    public void TestConsultarUsuarioFacebook()
    {
      Assert.AreEqual(1, peticion.ConsultarUsuarioSocial(usuario));
      usuario.Correo = "cualquierotro@gmail.com";
      Assert.AreEqual(0, peticion.ConsultarUsuarioSocial(usuario));

      usuario.Correo = null;
      Assert.Throws<InvalidCastException>(() => {
        peticion.ConsultarUsuarioSocial(usuario);
      });
    }

    // TERMINAR, FALTA VER COMO SABER QUE DIO ERROR TRATANDO DE VALIDAR AL USUARIO
    [Test]
    [Category("Actualizar")]
    public void TestActualizarValidacionUsuario()
    {
      Assert.DoesNotThrow(() => {
        usuario.Id = peticion.ConsultarUsuarioSocial(usuario);
        peticion.ValidarUsuario(usuario);
      });

      Assert.Throws<InvalidCastException>(() => {
        usuario.Correo = null;
        peticion.ValidarUsuario(usuario);
      });
    }

    [Test]
    [Category("Consultar")]
    public void TestConsultarUsuarioCorreo()
    {
      usuario.Clave = "";
      Assert.AreEqual(1, peticion.ConsultarUsuarioCorreo(usuario));
      usuario.Correo = "cualquierotro@gmail.com";
      Assert.AreEqual(0, peticion.ConsultarUsuarioCorreo(usuario));
      usuario.Correo = null;
      Assert.Throws<InvalidCastException>(() => {
        peticion.ConsultarUsuarioCorreo(usuario);
      });
    }

    [Test]
    [Category("Consultar")]
    public void TestConsultarUsuarioNombre()
    {
      usuario.Clave = "";
      Assert.AreEqual(1, peticion.ConsultarUsuarioNombre(usuario));
      usuario.NombreUsuario = "cualquierotro";
      Assert.AreEqual(0, peticion.ConsultarUsuarioNombre(usuario));
      usuario.NombreUsuario = null;
      Assert.Throws<InvalidCastException>(() => {
        peticion.ConsultarUsuarioNombre(usuario);
      });

    }
    [Test]
    [Category("Consultar")]
    public void TestRecuperarContrasena() {
      Assert.AreEqual("pruebaclave", peticion.RecuperarContrasena(usuario));
      usuario.Correo = "cualquierotro@gmail.com";
      Assert.AreEqual("", peticion.RecuperarContrasena(usuario));
      usuario.Correo = null;
      Assert.Throws<InvalidCastException>(() => {
        peticion.RecuperarContrasena(usuario);
      });

    }

    [Test]
    [Category("Consultar")]
    public void TestConsultarUsuarioSoloNombre()
    {
      Assert.AreEqual(1, peticion.ConsultarUsuarioSoloNombre(usuario));
      usuario.NombreUsuario = "cualquierotro";
      Assert.AreEqual(0, peticion.ConsultarUsuarioSoloNombre(usuario));
      usuario.NombreUsuario = null;
      Assert.Throws<InvalidCastException>(() => {
        peticion.ConsultarUsuarioSoloNombre(usuario);
      });
    }
    //TERMINAR, FALTA PROBAR NpgsqlException
    [Test]
    [Category("Controlador")]
    public void TestIniciarSesionCorreo()
    {

      Assert.AreEqual(1, controlador.IniciarSesionCorreo(JsonConvert.SerializeObject(usuario)));
      usuario.Correo = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.IniciarSesionCorreo(JsonConvert.SerializeObject(usuario));
      });
      usuario.Correo = "kilordpepo@gmail.com";
      usuario.Clave = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.IniciarSesionCorreo(JsonConvert.SerializeObject(usuario));
      });
    }

    [Test]
    [Category("Controlador")]
    public void TestIniciarSesionUsuario()
    {

      Assert.AreEqual(1, controlador.IniciarSesionUsuario(JsonConvert.SerializeObject(usuario)));
      usuario.NombreUsuario = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.IniciarSesionUsuario(JsonConvert.SerializeObject(usuario));
      });
      usuario.NombreUsuario = "pepo";
      usuario.Clave = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.IniciarSesionUsuario(JsonConvert.SerializeObject(usuario));
      });
    }

    [Test]
    [Category("Controlador")]
    public void TestIniciarSesionSocial()
    {

      Assert.AreEqual(1, controlador.IniciarSesionSocial(JsonConvert.SerializeObject(usuario)));
      usuario.Correo = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.IniciarSesionSocial(JsonConvert.SerializeObject(usuario));
      });
  
    }
    [Test]
    [Category("Controlador")]
    public void TestCorreoRecuperar()
    {
      Assert.AreEqual(HttpStatusCode.OK, controlador.CorreoRecuperar(JsonConvert.SerializeObject(usuario)));
      usuario.Correo = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.IniciarSesionSocial(JsonConvert.SerializeObject(usuario));
      });
     
    }
    //CORRER SOLO CUANDO NO TIENES CONEXION
    [Test]
    [Category("Controlador")]
    public void TestCorreoRecuperarSinConexion()
    {
      Assert.Throws<HttpResponseException>(() => {
        controlador.IniciarSesionSocial(JsonConvert.SerializeObject(usuario));
      });

      /*Assert.Throws<HttpResponseException>(() => {
        controlador.IniciarSesionSocial(JsonConvert.SerializeObject(usuario));
      });
      */
    }

  }

}
