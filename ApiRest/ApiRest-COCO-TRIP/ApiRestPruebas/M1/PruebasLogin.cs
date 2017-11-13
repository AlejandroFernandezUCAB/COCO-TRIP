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
    private EventoPreferencia evento1;
    private EventoPreferencia evento2;
    private PeticionLogin peticion = new PeticionLogin();
    private M1_LoginController controlador = new M1_LoginController();
    private DateTime fechaPrueba;
    private LugarTuristicoPreferencia lugarTuristico1;
    private LugarTuristicoPreferencia lugarTuristico2;
    private LugarTuristicoPreferencia lugarTuristico3;
    private LugarTuristicoPreferencia lugarTuristico4;
    private LugarTuristicoPreferencia lugarTuristico5;

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
        Correo = "hdms26@gmail.com",
        Clave = "pruebaclave",
        Foto = ""
      };

      evento1 = new EventoPreferencia
      {
        NombreEvento = "predespachil",
        FechaInicio = new DateTime (2019, 12, 12, 0, 0, 0),
        FechaFin = new DateTime(2019, 12, 13, 0, 0, 0),
        HoraInicio = new TimeSpan(20, 0, 0),
        HoraFin = new TimeSpan(23, 0, 0),
        Precio = 5000,
        Descripcion = "pre despacho antes de beber en holic",
        NombreLocal = "Holic",
        LocalFotoRuta = "C:\\Users\\pedro\\OneDrive\\Documentos\\GitKraken\\COCO-TRIP\\FrontOffice\\src\\assets\\images\\predespachill.jpg",
        NombreCategoria = "bar"

      };
      evento2 = new EventoPreferencia
      {
        NombreEvento = "birrazo",
        FechaInicio = new DateTime(2018, 12, 12, 0, 0, 0),
        FechaFin = new DateTime(2018, 12, 13, 0, 0, 0),
        HoraInicio = new TimeSpan(20, 0, 0),
        HoraFin = new TimeSpan(23, 0, 0),
        Precio = 5000,
        Descripcion = "tomar birras para recaudar fondos para la ucab",
        NombreLocal = "Birras Bistro",
        LocalFotoRuta = "C:\\Users\\pedro\\OneDrive\\Documentos\\GitKraken\\COCO-TRIP\\FrontOffice\\src\\assets\\images\\rc.jpg",
        NombreCategoria = "bar"

      };
      lugarTuristico1 = new LugarTuristicoPreferencia
      {
        NombreLT = "Playa Pelua",
        Costo = 0,
        Descripcion = "Farandu lLaya",
        Direccion = "la guaria",
        LugarFotoRuta = "C:\\Users\\pedro\\OneDrive\\Documentos\\GitKraken\\COCO-TRIP\\FrontOffice\\src\\assets\\images\\pelua.jpg",
        NombreCategoria= "bar"

      };

    }

    [Test]
    [Category("Insertar")]
    public void TestInsertarUsuarioFacebook()
    {
      Assert.AreEqual(20, peticion.InsertarUsuarioFacebook(usuario));
      Assert.Throws<PostgresException>(() => {
        peticion.InsertarUsuarioFacebook(usuario);
      });
      Assert.Throws<InvalidCastException>(() => {
        usuario.Nombre = null;
        peticion.InsertarUsuarioFacebook(usuario);
      });

    }
    [Test]
    [Category("Insertar")]
    public void TestInsertarUsuario()
    {
      Assert.AreEqual(13, peticion.InsertarUsuario(usuario));
      Assert.Throws<PostgresException>(() => {
        peticion.InsertarUsuario(usuario);
      });
      Assert.Throws<InvalidCastException>(() => {
        usuario.Nombre = null;
        peticion.InsertarUsuario(usuario);
      });
    }

    [Test]
    [Category("Consultar")]
    public void TestConsultarUsuarioFacebook()
    {
      usuario.Apellido = null;
      usuario.Nombre = null;
      usuario.FechaNacimiento = DateTime.Now;
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
      usuario.Correo = "carlospepo@msn.com";
      Assert.AreEqual(31, controlador.IniciarSesionSocial(JsonConvert.SerializeObject(usuario)));

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

    [Test]
    [Category("Controlador")]
    public void TestRegistrarUsuario()
    {
      Assert.AreEqual(1, controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario)));

      usuario.NombreUsuario = "homero";
      Assert.AreEqual(-2, controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario)));

      usuario.Correo = "homero_dms@hotmail.com";
      Assert.AreEqual(-3, controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario)));

      usuario.Correo = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario));
      });
    }

    [Test]
    [Category("Controlador")]
    public void TestValidarUsuario()
    {
      usuario.Id = 1;
      Assert.AreEqual("Usuario validado", controlador.ValidarUsuario(usuario.Correo, usuario.Id));
    }
    /// <summary>
    /// Prueba de caso exitoso en ConsultarEventosSegunPreferencias
    /// que se encuentra en el modelo  PeticionLogin.cs
    /// </summary>
    [Test]
    [Category("Consultar")]
    public void TestEventosSegunPreferenciasMod() {
      List<EventoPreferencia> listaEventoprueba = new List<EventoPreferencia>();
      listaEventoprueba = peticion.ConsultarEventosSegunPreferencias(1, fechaPrueba);

      fechaPrueba = new DateTime(2017, 03, 09);
      Assert.AreEqual(evento1.NombreEvento, listaEventoprueba[0].NombreEvento);
      Assert.AreEqual(evento1.FechaInicio, listaEventoprueba[0].FechaInicio);
      Assert.AreEqual(evento1.FechaFin, listaEventoprueba[0].FechaFin);
      Assert.AreEqual(evento1.HoraInicio, listaEventoprueba[0].HoraInicio);
      Assert.AreEqual(evento1.HoraFin, listaEventoprueba[0].HoraFin);
      Assert.AreEqual(evento1.Precio, listaEventoprueba[0].Precio);
      Assert.AreEqual(evento1.Descripcion, listaEventoprueba[0].Descripcion);
      Assert.AreEqual(evento1.NombreLocal, listaEventoprueba[0].NombreLocal);
      Assert.AreEqual(evento1.LocalFotoRuta, listaEventoprueba[0].LocalFotoRuta);
      Assert.AreEqual(evento1.NombreCategoria, listaEventoprueba[0].NombreCategoria);

      Assert.AreEqual(evento2.NombreEvento, listaEventoprueba[1].NombreEvento);
      Assert.AreEqual(evento2.FechaInicio, listaEventoprueba[1].FechaInicio);
      Assert.AreEqual(evento2.FechaFin, listaEventoprueba[1].FechaFin);
      Assert.AreEqual(evento2.HoraInicio, listaEventoprueba[1].HoraInicio);
      Assert.AreEqual(evento2.HoraFin, listaEventoprueba[1].HoraFin);
      Assert.AreEqual(evento2.Precio, listaEventoprueba[1].Precio);
      Assert.AreEqual(evento2.Descripcion, listaEventoprueba[1].Descripcion);
      Assert.AreEqual(evento2.NombreLocal, listaEventoprueba[1].NombreLocal);
      Assert.AreEqual(evento2.LocalFotoRuta, listaEventoprueba[1].LocalFotoRuta);
      Assert.AreEqual(evento2.NombreCategoria, listaEventoprueba[1].NombreCategoria);
    }
    /// <summary>
    /// Prueba de caso exitoso en EventoSegunPreferenciass
    /// que se encuentra en el controllador  M1_LoginController.cs
    /// </summary>
    [Test]
    [Category("Consultar")]
    public void TestEventosSegunPreferenciasControler()
    {
      List<EventoPreferencia> listaEventoprueba = new List<EventoPreferencia>();
      listaEventoprueba = controlador.EventoSegunPreferencias(1, fechaPrueba);

      fechaPrueba = new DateTime(2017, 03, 09);
      Assert.AreEqual(evento1.NombreEvento, listaEventoprueba[0].NombreEvento);
      Assert.AreEqual(evento1.FechaInicio, listaEventoprueba[0].FechaInicio);
      Assert.AreEqual(evento1.FechaFin, listaEventoprueba[0].FechaFin);
      Assert.AreEqual(evento1.HoraInicio, listaEventoprueba[0].HoraInicio);
      Assert.AreEqual(evento1.HoraFin, listaEventoprueba[0].HoraFin);
      Assert.AreEqual(evento1.Precio, listaEventoprueba[0].Precio);
      Assert.AreEqual(evento1.Descripcion, listaEventoprueba[0].Descripcion);
      Assert.AreEqual(evento1.NombreLocal, listaEventoprueba[0].NombreLocal);
      Assert.AreEqual(evento1.LocalFotoRuta, listaEventoprueba[0].LocalFotoRuta);
      Assert.AreEqual(evento1.NombreCategoria, listaEventoprueba[0].NombreCategoria);

      Assert.AreEqual(evento2.NombreEvento, listaEventoprueba[1].NombreEvento);
      Assert.AreEqual(evento2.FechaInicio, listaEventoprueba[1].FechaInicio);
      Assert.AreEqual(evento2.FechaFin, listaEventoprueba[1].FechaFin);
      Assert.AreEqual(evento2.HoraInicio, listaEventoprueba[1].HoraInicio);
      Assert.AreEqual(evento2.HoraFin, listaEventoprueba[1].HoraFin);
      Assert.AreEqual(evento2.Precio, listaEventoprueba[1].Precio);
      Assert.AreEqual(evento2.Descripcion, listaEventoprueba[1].Descripcion);
      Assert.AreEqual(evento2.NombreLocal, listaEventoprueba[1].NombreLocal);
      Assert.AreEqual(evento2.LocalFotoRuta, listaEventoprueba[1].LocalFotoRuta);
      Assert.AreEqual(evento2.NombreCategoria, listaEventoprueba[1].NombreCategoria);
    }


  }

}
