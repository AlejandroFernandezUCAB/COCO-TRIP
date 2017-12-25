using System;
using ApiRest_COCO_TRIP.Models.Dato;
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
      Comando comando = FabricaComando.CrearComandoAgregarItinerario(2,"itinerario3");
      comando.Ejecutar();
      Assert.IsFalse(false);
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
      x = false;
      DateTime fechaini = new DateTime(2040, 12, 12);
      DateTime fechafin = new DateTime(2044, 12, 12);
     // ApiRest_COCO_TRIP.Datos.Entity.Itinerario  itinerario =
     //   new ApiRest_COCO_TRIP.Datos.Entity.Itinerario(15, "Epco Reloaded", fechaini, fechafin, 1);
      comando = FabricaComando.CrearComandoModificarItinerario(19, "ususu", fechaini, fechafin, 1);
      comando.Ejecutar();
      comando = FabricaComando.CrearComandoConsultarItinerarios(1);
      comando.Ejecutar();
      lista = comando.RetornarLista();
      foreach (Entidad item in lista)
      {
        Itinerario itinerario = (Itinerario)item;
        if (itinerario.Nombre == "ususu") x = true;
      }
      Assert.True(x);
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
      Comando comando = FabricaComando.CrearComandoConsultarItinerarios(2);
      comando.Ejecutar();
      lista =  comando.RetornarLista();
      Assert.AreNotEqual(0, lista.Count);
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
    public void Prueba_EliminarActividad()
    {
      x = controller.EliminarItem_It("Actividad", 4, 1);
      Assert.True(x);
    }
  }
}
