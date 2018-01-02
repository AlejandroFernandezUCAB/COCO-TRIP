using System;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using ApiRest_COCO_TRIP.Controllers;
using NUnit.Framework;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Datos.Entity;

namespace ApiRestPruebas
{

  [TestFixture]
  class M5UnitTests
  {
    private M5_ItinerarioController controller;
    private Itinerario itinerario;
    private Itinerario it;
    private Boolean x;
    private DateTime fechaini;
    private DateTime fechafin;
    private int id_usuario;
    private List<ApiRest_COCO_TRIP.Datos.Entity.Entidad> lista;
    private Comando comando;
    private Notificacion notificacion;
    [OneTimeSetUp]
    protected void OTSU()
    {
      controller = new M5_ItinerarioController();
      
    }

    /// <summary>
    /// Prueba de caso exitoso en AgregarItinerario
    /// </summary>
    [Test]
    public void PUAgregarItinerario()
    {
      string nombre = "Pirulin30";
      Comando comando = FabricaComando.CrearComandoAgregarItinerario(2,nombre);
      comando.Ejecutar();
      Itinerario itNew =(Itinerario) comando.Retornar();
      Assert.AreEqual(nombre,itNew.Nombre);
      Assert.AreEqual(30,itNew.Id);
    }

    /// <summary>
    /// Prueba de casos borde(excepciones) en AgregarItinerario
    /// </summary>
    /*[Test]
    public void FalloAgregarItinerario()
    {
      Assert.Catch<HttpResponseException>(Excepcion_Agregar);
      Assert.Catch<HttpResponseException>(Excepcion_Agregar2);
    }*/

    /// <summary>
    /// Metodo utilizados para casos borde(excepciones) de AgregarItinerario
    /// </summary>
    /*public void Excepcion_Agregar()
    {
      itinerario = null;
      controller.AgregarItinerario(itinerario);
    }*/

    /// <summary>
    /// Metodo utilizados para casos borde(excepciones) de AgregarItinerario
    /// </summary>
    /*public void Excepcion_Agregar2()
    {
      itinerario = new Itinerario("Michel", 0);
      controller.AgregarItinerario(itinerario);
    }*/

    /// <summary>
    /// Prueba de caso exitoso en EliminaItinerario
    /// </summary>
    [Test]
    public void PUEliminarItinerario()
    {
      Comando comando = FabricaComando.CrearComandoEliminarItinerario(15);
      comando.Ejecutar();
      Assert.True(true);
    }

    /// <summary>
    /// Prueba de caso borde(fallo) en EliminarItinerario
    /// </summary>
    [Test]
    public void FalloEliminarItinerario()
    {
      x = controller.EliminarItinerario(4);
      Assert.False(x);
    }

    /// <summary>
    /// Prueba de caso exitoso en ModificarItinerario
    /// </summary>
    [Test]
    public void PUModificarItinerario()
    {
      string nombre = "truman";
      DateTime fechaini = new DateTime(2040, 12, 12);
      DateTime fechafin = new DateTime(2044, 12, 12);
      comando = FabricaComando.CrearComandoModificarItinerario(28, nombre, fechaini, fechafin, 2);
      comando.Ejecutar();
      Itinerario itNew = (Itinerario)comando.Retornar();
      Assert.AreEqual(nombre, itNew.Nombre);
    }

    /// <summary>
    /// Prueba de caso exitoso en AgregarEvento_It
    /// </summary>
    [Test]
    public void PUAgregarEvento_It()
    {
      Itinerario itinerario = new Itinerario(19);
      int idEvento = 1;
      fechaini = new DateTime(2017, 11, 15);
      fechafin = new DateTime(2017, 11, 18);
      comando = FabricaComando.CrearComandoAgregarAgenda("Evento",itinerario.Id,
        idEvento,fechaini,fechafin);
      comando.Ejecutar();
      Assert.True(true);
    }

    /// <summary>
    /// Prueba de caso borde(fallo) en AgregarEvento_It
    /// </summary>
    /*[Test]
    public void FalloAgregarEvento_It()
   {
     Itinerario itinerario = new Itinerario(6);
     Evento ev = new Evento
     {
       Id = 1
     };
     fechaini = new DateTime(2017, 11, 15);
     fechafin = new DateTime(2017, 11, 18);
     x = controller.AgregarItem_It("Eventos",itinerario.Id, ev.Id,fechaini, fechafin);
     Assert.False(x);
   }*/

    /// <summary>
    /// Prueba de caso exitoso en AgregarActividad_It
    /// </summary>
    [Test]
    public void PUAgregarActividad_It()
    {
      Itinerario itinerario = new Itinerario(19);
      int idActividad = 4;
      fechaini = new DateTime(2017, 11, 15);
      fechafin = new DateTime(2017, 11, 18);
      comando = FabricaComando.CrearComandoAgregarAgenda("Actividad", itinerario.Id,
        idActividad, fechaini, fechafin);
      comando.Ejecutar();
      Assert.True(true);
    }

    /// <summary>
    /// Prueba de caso borde(fallo) en AgregarActividad_It
    /// </summary>
    [Test]
    public void FalloAgregarActividad_It()
    {
      itinerario = new Itinerario(6);
      Actividad ac = new Actividad
      {
        Id = 2
      };
      fechaini = new DateTime(2017, 11, 15);
      fechafin = new DateTime(2017, 11, 18);
      x = controller.AgregarItem_It("Actividads", itinerario.Id, ac.Id, fechaini, fechafin);
      Assert.False(x);
    }

    /// <summary>
    /// Prueba de caso exitoso en AgregarLugar_It
    /// </summary>
    [Test]
    public void PUAgregarLugar_It()
    {
      Itinerario itinerario = new Itinerario(21);
      int id = 2;
      fechaini = new DateTime(2017, 11, 15);
      fechafin = new DateTime(2017, 11, 18);
      comando = FabricaComando.CrearComandoAgregarAgenda("Lugar Turistico", itinerario.Id,
        id, fechaini, fechafin);
      comando.Ejecutar();
      Assert.True(true);
    }

    /// <summary>
    /// Prueba de caso borde(fallo) en AgregarLugar_It
    /// </summary>
    [Test]
    public void FalloAgregarLugar_It()
    {
      itinerario = new Itinerario(6);
      LugarTuristico lt = new LugarTuristico()
      {
        Id = 1
      };
      fechaini = new DateTime(2017, 11, 15);
      fechafin = new DateTime(2017, 11, 18);
      x = controller.AgregarItem_It("LugarTuristico", itinerario.Id, lt.Id, fechaini, fechafin);
      Assert.False(x);

    }

    /// <summary>
    /// Prueba de caso exitoso en EliminarLugar_It
    /// </summary>
    [Test]
    public void PUEliminarLugar_It()
    {
      itinerario = new Itinerario(21);
      int idLugar = 2;
      comando = FabricaComando.CrearComandoEliminarAgendaItem("Lugar Turistico", itinerario.Id,
        idLugar);
      comando.Ejecutar();
      Assert.True(true);
    }

    /// <summary>
    /// Prueba de caso borde(fallo) en EliminarLugar_It
    /// </summary>
    [Test]
    public void FalloEliminarLugar_It()
    {
      itinerario = new Itinerario(6);
      LugarTuristico lt = new LugarTuristico()
      {
        Id = 1
      };
      x = controller.EliminarItem_It("LugarTuristico", itinerario.Id, lt.Id);
      Assert.False(x);
    }

    /// <summary>
    /// Prueba de caso exitoso en EliminarActividad_It
    /// </summary>
    [Test]
    public void PUEliminarActividad_It()
    {
      itinerario = new Itinerario(19);
      int idActividad = 4;
      comando = FabricaComando.CrearComandoEliminarAgendaItem("Actividad",itinerario.Id,
        idActividad);
      comando.Ejecutar();
      Assert.True(true);
    }

    /// <summary>
    /// Prueba de caso borde(fallo) en EliminarActividad_It
    /// </summary>
    [Test]
    public void FalloEliminarActividad_It()
    {
      itinerario = new Itinerario(6);
      Actividad ac = new Actividad()
      {
        Id = 1
      };
      x = controller.EliminarItem_It("Actividads", itinerario.Id, ac.Id);
      Assert.False(x);
    }

    /// <summary>
    /// Prueba de caso exitoso en EliminarEvento_It
    /// </summary>
    [Test]
    public void PUEliminarEvento_It()
    {
      itinerario = new Itinerario(19);
      int idEvento = 1;
      comando = FabricaComando.CrearComandoEliminarAgendaItem("Evento", itinerario.Id,
        idEvento);
      comando.Ejecutar();
      Assert.True(true);
    }

    /// <summary>
    /// Prueba de caso borde(fallo) en EliminarEvento_It
    /// </summary>
    /*[Test]
    public void FalloEliminarEvento_It()
    {
      itinerario = new Itinerario(6);
      Evento ev = new Evento()
      {
        Id = 1
      };
      x = controller.EliminarItem_It("Eventoss", itinerario.Id, ev.Id);
      Assert.False(x);
    }*/

     [Test]
    public void PUConsultarItinerarios()
    {
      Comando comando = FabricaComando.CrearComandoConsultarItinerarios(1);
      comando.Ejecutar();
      lista =  comando.RetornarLista();
            Itinerario itinerario = (Itinerario)lista[0];
      Assert.AreEqual(itinerario.Id, lista[0].Id);
    }
    /*
    public void ExcepcionItinerarioNull()
    {
      id_usuario = null;
      controller.ConsultarItinerarios(id_usuario);
    } */

    [Test]
    public void ConsultarItinerarioIsEmpty()
    {
      //id de usuario que no existe
      id_usuario = 50;
      comando = FabricaComando.CrearComandoConsultarItinerarios(id_usuario);
      comando.Ejecutar();
      Assert.IsEmpty(comando.RetornarLista());
    }
    /// <summary>
    /// Prueba de caso exitoso en Cambiar visibilidad a FALSO
    /// </summary>
    [Test]
    public void PUSetVisibleFalse()
    {
      Boolean visible = false;
      id_usuario = 1;
      int idItinerario = 19;
      comando = FabricaComando.CrearComandoSetVisibleItinerario(visible,id_usuario,
        idItinerario);
      comando.Ejecutar();
    }

    /// <summary>
    /// Prueba de caso exitoso en Cambiar visibilidad a TRUE
    /// </summary>
    [Test]
    public void PUSetVisibleTrue()
    {
      Boolean visible = true;
      id_usuario = 1;
      int idItinerario = 19;
      comando = FabricaComando.CrearComandoSetVisibleItinerario(visible, id_usuario,
        idItinerario);
      comando.Ejecutar();
    }

    [Test]
    public void PU_AgregarNotificacion()
    {
      comando = FabricaComando.CrearComandoAgregarNotificacion(1);
      comando.Ejecutar();
      notificacion = (Notificacion)comando.Retornar();
      Assert.True(notificacion.Push);
    }
    
    [Test]
    public void PU_EliminarNotificacion()
    {
      comando = FabricaComando.CrearComandoEliminarNotificacion(1);
      comando.Ejecutar();
      notificacion = (Notificacion)comando.Retornar();
      Assert.False(notificacion.Push);
    }

    [Test]
    public void PU_ModificarNotificacionCorreo()
    {
      comando = FabricaComando.CrearComandoModificarNotificacion(1,false,true);
      comando.Ejecutar();
      notificacion = (Notificacion)comando.Retornar();
      Assert.True(notificacion.Push);
    }

    [Test]
    public void PU_ConsultarNotificacion()
    {
      comando = FabricaComando.CrearComandoConsultarNotificacion(1);
      comando.Ejecutar();
      notificacion = (Notificacion)comando.Retornar();
      Assert.True(notificacion.Correo);
    }

    [Test]
    public void PU_ConsultarNotificacionUsuarioNoExiste()
    {
      comando = FabricaComando.CrearComandoConsultarNotificacion(2);
      comando.Ejecutar();
      notificacion = (Notificacion)comando.Retornar();
      Assert.False(notificacion.Correo);
    }

    [Test]
    public void PU_EnviarCorreo()
    {
      int idUsuario = 1;
      comando = FabricaComando.CrearComandoEnviarCorreoItinerario(1);
      comando.Ejecutar();
    
      Assert.True(true);
    }

    [Test]
    public void PU_ConsultarEventos()
    {
      string busqueda = "jorge";
      fechaini = new DateTime(2017, 12, 30);
      fechafin = new DateTime(2017, 12, 30);
      comando = FabricaComando.CrearComandoListarCoincidenciaEventos(busqueda,fechaini,fechafin);
      comando.Ejecutar();
      lista = comando.RetornarLista();
      Evento evento = (Evento)lista[0];
      Assert.AreEqual(busqueda, evento.Nombre);
      Assert.AreEqual(1, evento.Id);
    }

    [Test]
    public void PU_ConsultarLugares()
    {
      string busqueda = "Parque Generalisimo de Miranda";
      comando = FabricaComando.CrearComandoListarCoincidenciaLugaresTurisiticos(busqueda);
      comando.Ejecutar();
      lista = comando.RetornarLista();
      LugarTuristico lugarTuristico = (LugarTuristico)lista[0];
      Assert.AreEqual(busqueda, lugarTuristico.Nombre);
      Assert.AreEqual(2, lugarTuristico.Id);
    }

    [Test]
    public void PU_ConsultarActividades()
    {
      string busqueda = "Parque Generalisimo de Miranda";
      comando = FabricaComando.CrearComandoListarCoincidenciaActividades(busqueda);
      comando.Ejecutar();
      lista = comando.RetornarLista();
      Actividad actividad = (Actividad)lista[0];
      Assert.AreEqual(busqueda, actividad.Nombre);
      Assert.AreEqual(1, actividad.Id);
    }
  }
}
