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
using ApiRest_COCO_TRIP.Models.Dato;
using ApiRest_COCO_TRIP.Models.BaseDeDatos;

namespace ApiRestPruebas.M1
{
  [TestFixture]
  public class PruebasLogin
  {
    private Usuario usuario;
    private Usuario usuariof;
    private EventoPreferencia evento1;
    private PeticionLogin peticion = new PeticionLogin();
    private PeticionPerfil peticionPerfil = new PeticionPerfil();
    private PeticionLugarTuristico peticionLugarTuristico = new PeticionLugarTuristico();
    private ConexionLugarTuristico conexionLugarTuristico = new ConexionLugarTuristico();
    private PeticionEvento peticionEvento = new PeticionEvento();
    private M1_LoginController controlador = new M1_LoginController();
    private DateTime fechaPrueba;
    private int global;
    private int globalf;
    private LugarTuristico lt = new LugarTuristico();
    private LugarTuristicoPreferencia lugarTuristico1;
    private Evento eve = new Evento();
    private LocalidadEvento localidad = new LocalidadEvento();
    private Categoria categoria = new Categoria();
    private PeticionLocalidadEvento peticionLocalidadEvento = new PeticionLocalidadEvento();

    [SetUp]
    public void SetPrueba()
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
        Foto = ""
      };

      usuariof = new Usuario
      {
        Nombre = "pedro",
        Apellido = "garcia",
        Correo = "quinzzy26@gmail.com",
        Foto = ""
      };


      evento1 = new EventoPreferencia
      {
        NombreEvento = "predespachill",
        FechaInicio = new DateTime (2019, 12, 12, 0, 0, 0),
        FechaFin = new DateTime(2019, 12, 13, 0, 0, 0),
        HoraInicio = new TimeSpan(20, 0, 0),
        HoraFin = new TimeSpan(23, 0, 0),
        Precio = 5000,
        Descripcion = "pre despacho antes de beber en holic",
        NombreLocal = "Holic",
        LocalFotoRuta = "predespachill.jpg",
        NombreCategoria = "bar"

      };

      lugarTuristico1 = new LugarTuristicoPreferencia
      {
        NombreLT = "Playa Pelua",
        Costo = 0,
        Descripcion = "Farandu Playa",
        Direccion = "la guaira",
        LugarFotoRuta = "pelua.jpg",
        NombreCategoria= "bar"

      };
      eve = new Evento {
        Nombre = "predespachill",
        Descripcion= "pre despacho antes de beber en holic",
        Precio= 5000,
        FechaInicio = new DateTime(2019, 12, 12, 0, 0, 0),
        FechaFin = new DateTime(2019, 12, 13, 0, 0, 0),
        HoraInicio= new DateTime().AddHours(20),
        HoraFin = new DateTime().AddHours(23),
        Foto = "predespachill.jpg",
        IdLocalidad = 1,
        IdCategoria = 1
      };
      categoria = new Categoria {
        Nombre = "bar",
        Descripcion = "Lugar para beber",
        Estatus = true,
        CategoriaSuperior =0,
        Nivel =0,

      };
      localidad = new LocalidadEvento {
        Nombre="Holic",
        Descripcion="Bar y discoteca",
        Coordenadas = "5.5"
      };
      lt = new LugarTuristico
      {
        Nombre = "Plata Pelua",
        Costo = 0,
        Descripcion = "Farandu Playa",
        Direccion = "la guaira",
        Correo = "correo@msn.com",
        Telefono = 2893517,
        Latitud = 5.3,
        Longitud = 5.4,
        Activar = true,
        Categoria = new List<Categoria> {
          categoria
        }

    };
      globalf = peticion.InsertarUsuarioFacebook(usuariof);
      global = peticion.InsertarUsuario(usuario);
     /* peticion.InsertarCategoria(categoria);
      peticionPerfil.AgregarPreferencia(usuario.Id, categoria.Id);
      peticionLocalidadEvento.AgregarLocalidadEvento(localidad);
      peticionEvento.AgregarEvento(eve);
      */
    }

    [TearDown]
    public void ResetPrueba() {

      peticion.EliminarUsuario(global);
      peticion.EliminarUsuario(globalf);
     /* peticionPerfil.EliminarPreferencia(usuario.Id, categoria.Id);
      peticionEvento.EliminarEvento(eve.Id);
      peticionLocalidadEvento.EliminarLocalidadEvento(localidad.Id);
      peticion.EliminarCategoria(categoria.Id);
      */
    }
    [Test]
    [Category("Insertar")]
    public void TestInsertarUsuarioFacebook()
    {
      Assert.AreNotEqual(0, globalf);
      Assert.Throws<PostgresException>(() => {
        peticion.InsertarUsuarioFacebook(usuariof);
      });
      Assert.Throws<InvalidCastException>(() => {
        usuariof.Nombre = null;
        peticion.InsertarUsuarioFacebook(usuariof);
      });

    }
    [Test]
    [Category("Insertar")]
    public void TestInsertarUsuario()
    {
      Assert.AreNotEqual(0, global);
      Assert.Throws<PostgresException>(() => {
        peticion.InsertarUsuario(usuario);
      });
      Assert.Throws<InvalidCastException>(() => {
        usuario.Nombre = null;
        peticion.InsertarUsuario(usuario);
      });
    }

    // TERMINAR, FALTA VER COMO SABER QUE DIO ERROR TRATANDO DE VALIDAR AL USUARIO
    [Test]
    [Category("Actualizar")]
    public void TestActualizarUsuario()
    {
      usuariof.FechaNacimiento = DateTime.Now;
      usuariof.Clave = "clavef";
      usuariof.Genero = "M";
      usuariof.NombreUsuario = "facebookuser";
      Assert.DoesNotThrow(() => {
        peticion.ActualizarUsuario(usuariof);
      });

      Assert.Throws<InvalidCastException>(() => {
        usuariof.Genero = null;
        peticion.ActualizarUsuario(usuariof);
      });
    }
    [Test]
    [Category("Actualizar")]
    public void TestActualizarValidacionUsuario()
    {
      Assert.DoesNotThrow(() => {
        usuario.Id = global;
        peticion.ValidarUsuario(usuario);
        usuariof.Id = globalf;
        peticion.ValidarUsuario(usuariof);
      });

      Assert.Throws<InvalidCastException>(() => {
        usuario.Correo = null;
        peticion.ValidarUsuario(usuario);
      });
    }

    //TERMINAR
    [Test]
    [Category("Consultar")]
    public void TestConsultarUsuarioFacebook()
    {
      
      Assert.AreEqual(global, peticion.ConsultarUsuarioSocial(usuario));
      usuario.Correo = "cualquierotro@gmail.com";
      Assert.AreEqual(0, peticion.ConsultarUsuarioSocial(usuario));
      Assert.AreEqual(globalf, peticion.ConsultarUsuarioSocial(usuariof));
      usuariof.Correo = "cualquierotro@gmail.com";
      Assert.AreEqual(0, peticion.ConsultarUsuarioSocial(usuariof));

      usuario.Correo = null;
      Assert.Throws<InvalidCastException>(() => {
        peticion.ConsultarUsuarioSocial(usuario);
      });
    }

  
    [Test]
    [Category("Consultar")]
    public void TestConsultarUsuarioCorreo()
    {
      usuariof.FechaNacimiento = DateTime.Now;
      usuariof.Clave = "clavef";
      usuariof.Genero = "M";
      usuariof.NombreUsuario = "facebookuser";
      peticion.ActualizarUsuario(usuariof);
      usuario.Id = global;
      peticion.ValidarUsuario(usuario);
      usuariof.Id = globalf;
      peticion.ValidarUsuario(usuariof);

      Assert.AreEqual(global, peticion.ConsultarUsuarioCorreo(usuario));
      usuario.Correo = "cualquierotro@gmail.com";
      Assert.AreEqual(0, peticion.ConsultarUsuarioCorreo(usuario));

      usuariof.Clave = "clavef";
      Assert.AreEqual(globalf, peticion.ConsultarUsuarioCorreo(usuariof));
      usuariof.Correo = "cualquierotro@gmail.com";
      Assert.AreEqual(0, peticion.ConsultarUsuarioCorreo(usuariof));

      usuario.Correo = null;
      Assert.Throws<InvalidCastException>(() => {
        peticion.ConsultarUsuarioCorreo(usuario);
      });
    }

    [Test]
    [Category("Consultar")]
    public void TestConsultarUsuarioNombre()
    {
      usuariof.FechaNacimiento = DateTime.Now;
      usuariof.Clave = "clavef";
      usuariof.Genero = "M";
      usuariof.NombreUsuario = "facebookuser";
      peticion.ActualizarUsuario(usuariof);
      usuario.Id = global;
      peticion.ValidarUsuario(usuario);
      usuariof.Id = globalf;
      peticion.ValidarUsuario(usuariof);

      Assert.AreEqual(global, peticion.ConsultarUsuarioNombre(usuario));
      usuario.NombreUsuario = "cualquierotro";
      Assert.AreEqual(0, peticion.ConsultarUsuarioNombre(usuario));

      usuariof.Clave = "clavef";
      usuariof.NombreUsuario = "facebookuser";
      Assert.AreEqual(globalf, peticion.ConsultarUsuarioNombre(usuariof));
      usuariof.NombreUsuario = "cualquierotro";
      Assert.AreEqual(0, peticion.ConsultarUsuarioNombre(usuariof));

      usuario.NombreUsuario = null;
      Assert.Throws<InvalidCastException>(() => {
        peticion.ConsultarUsuarioNombre(usuario);
      });

    }
    [Test]
    [Category("Consultar")]
    public void TestRecuperarContrasena() {

      usuariof.FechaNacimiento = DateTime.Now;
      usuariof.Clave = "clavef";
      usuariof.Genero = "M";
      usuariof.NombreUsuario = "facebookuser";
      peticion.ActualizarUsuario(usuariof);
      usuario.Id = global;
      peticion.ValidarUsuario(usuario);
      usuariof.Id = globalf;
      peticion.ValidarUsuario(usuariof);

      Assert.AreEqual("pruebaclave", peticion.RecuperarContrasena(usuario));
      usuario.Correo = "cualquierotro@gmail.com";
      Assert.AreEqual("", peticion.RecuperarContrasena(usuario));

      Assert.AreEqual("clavef", peticion.RecuperarContrasena(usuariof));
      usuariof.Correo = "cualquierotro@gmail.com";
      Assert.AreEqual("", peticion.RecuperarContrasena(usuariof));

      usuario.Correo = null;
      Assert.Throws<InvalidCastException>(() => {
        peticion.RecuperarContrasena(usuario);
      });

    }

    [Test]
    [Category("Consultar")]
    public void TestConsultarUsuarioSoloNombre()
    {
      usuariof.FechaNacimiento = DateTime.Now;
      usuariof.Clave = "clavef";
      usuariof.Genero = "M";
      usuariof.NombreUsuario = "facebookuser";
      peticion.ActualizarUsuario(usuariof);
      usuario.Id = global;
      peticion.ValidarUsuario(usuario);
      usuariof.Id = globalf;
      peticion.ValidarUsuario(usuariof);

      Assert.AreEqual(global, peticion.ConsultarUsuarioSoloNombre(usuario));
      usuario.NombreUsuario = "cualquierotro";
      Assert.AreEqual(0, peticion.ConsultarUsuarioSoloNombre(usuario));

      usuariof.NombreUsuario = "facebookuser";
      Assert.AreEqual(globalf, peticion.ConsultarUsuarioSoloNombre(usuariof));
      usuariof.NombreUsuario = "cualquierotro";
      Assert.AreEqual(0, peticion.ConsultarUsuarioSoloNombre(usuariof));

      usuario.NombreUsuario = null;
      Assert.Throws<InvalidCastException>(() => {
        peticion.ConsultarUsuarioSoloNombre(usuario);
      });
    }

    [Test]
    [Category("Controlador")]
    public void TestRegistrarUsuario()
    {
      peticion.EliminarUsuario(globalf);
      peticion.EliminarUsuario(global);
      globalf += 3;
      global += 1;
      usuario.Correo = "hdms26@gmail.com";
      Assert.AreEqual(global, controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario)));

      Assert.AreEqual(-4, controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario)));

      controlador.IniciarSesionSocial(JsonConvert.SerializeObject(usuariof));
      usuario.Correo = usuariof.Correo;
      Assert.AreEqual(-3, controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario)));

      usuario.NombreUsuario = "pedriviris";
      Assert.AreEqual(globalf, controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario)));//prueba unitaria de actualizar 
      usuario.NombreUsuario = "pepo";

      usuario.Correo = "hdms26@gmail.com";
      controlador.ValidarUsuario(usuario.Correo, global);

      usuario.NombreUsuario = "homero";
      Assert.AreEqual(-2, controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario)));

      usuario.Correo = "homero_dms@hotmail.com";
      usuario.NombreUsuario = "pepo";
      Assert.AreEqual(-3, controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario)));

      usuario.Correo = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario));
      });
      usuario.Correo = "hdms@gmail.com";

      usuario.NombreUsuario = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario));
      });
      usuario.NombreUsuario = "pepo2";

      usuario.Nombre = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario));
      });
      usuario.Nombre = "Carlos";

      usuario.Apellido = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario));
      });
      usuario.Apellido = "Valero";

      usuario.Genero = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario));
      });
      usuario.Genero = "M";

      usuario.Clave = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario));
      });
      usuario.Clave = "pruebaclave";

      usuario.Foto = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.RegistrarUsuario(JsonConvert.SerializeObject(usuario));
      });
      usuario.Foto = "";
    }

    [Test]
    [Category("Controlador")]
    public void TestValidarUsuario()
    {
      usuario.Id = global;
      Assert.AreEqual("Usuario validado", controlador.ValidarUsuario(usuario.Correo, usuario.Id));
    }
    //TERMINAR, FALTA PROBAR NpgsqlException
    [Test]
    [Category("Controlador")]
    public void TestIniciarSesionCorreo()
    {
      usuario.Id = global;
      peticion.ValidarUsuario(usuario);

      Assert.AreEqual(global, controlador.IniciarSesionCorreo(JsonConvert.SerializeObject(usuario)));
      usuario.Correo = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.IniciarSesionCorreo(JsonConvert.SerializeObject(usuario));
        controlador.IniciarSesionCorreo(JsonConvert.SerializeObject(usuariof));
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
      usuario.Id = global;
      peticion.ValidarUsuario(usuario);

      Assert.AreEqual(global, controlador.IniciarSesionUsuario(JsonConvert.SerializeObject(usuario)));
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
      Assert.AreEqual(global, controlador.IniciarSesionSocial(JsonConvert.SerializeObject(usuario)));

      usuario.Correo = null;
      Assert.Throws<HttpResponseException>(() => {
        controlador.IniciarSesionSocial(JsonConvert.SerializeObject(usuario));
      });

    }
    [Test]
    [Category("Controlador")]
    public void TestCorreoRecuperar()
    {
      usuario.Id = global;
      peticion.ValidarUsuario(usuario);
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

    

  
    /// <summary>
    /// Prueba de caso exitoso en ConsultarEventosSegunPreferencias
    /// que se encuentra en el modelo  PeticionLogin.cs
    /// </summary>
 
   /* [Test]
    [Category("Consultar")]
    public void TestEventosSegunPreferenciasControler()
    {
      List<EventoPreferencia> listaEventoprueba = new List<EventoPreferencia>();
      listaEventoprueba = controlador.EventoSegunPreferencias(1);

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
    }

    /// <summary>
    /// Prueba de caso exitoso en LugarTuristicoSegunPreferencias
    /// que se encuentra en el controlador M1_LoginController.cs
    /// </summary>
    [Test]
    [Category("Consultar")]
    public void TestLugarTuristicoSegunPreferencias()
    {
      List<LugarTuristicoPreferencia> listaLT = new List<LugarTuristicoPreferencia>();
      listaLT = controlador.LugarTuristicoSegunPreferencias(1);
      Assert.AreEqual(lugarTuristico1.NombreLT, listaLT[0].NombreLT);
      Assert.AreEqual(lugarTuristico1.Costo, listaLT[0].Costo);
      Assert.AreEqual(lugarTuristico1.Descripcion, listaLT[0].Descripcion);
      Assert.AreEqual(lugarTuristico1.Direccion, listaLT[0].Direccion);
      Assert.AreEqual(lugarTuristico1.LugarFotoRuta, listaLT[0].LugarFotoRuta);
      Assert.AreEqual(lugarTuristico1.NombreCategoria, listaLT[0].NombreCategoria);
    }*/


  }

}
