using System;
using ApiRest_COCO_TRIP.Models.Dato;
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Controllers;
using NUnit.Framework;
namespace ApiRestPruebas
{
  [TestFixture]
  class M5UnitTests
  {
    [TestCase]
    public void Prueba_AgregarItinerario()
    {
      DateTime fechaini = new DateTime(2017, 05, 28);
      DateTime fechafin = new DateTime(2019, 05, 28);
      Itinerario itinerario = new Itinerario("Michel", fechaini, fechafin, 1);
      M5Controller controller = new M5Controller();
      itinerario = controller.AgregarItinerario(itinerario);
      Assert.AreEqual(50, itinerario.Id);//siempre poner el numero del id que se va a agregar para esta pruba
    }

    [TestCase]
    public void Prueba_EliminarItinerario()
    {
      Itinerario itinerario = new Itinerario(10);
      M5Controller controller = new M5Controller();
      Boolean x = controller.EliminarItinerario(itinerario);
      Assert.AreEqual(true, x);
    }

    [TestCase]
    public void Prueba_ModificarItinerario()
    {
      DateTime fechaini = new DateTime(2021, 05, 28);
      DateTime fechafin = new DateTime(2030, 05, 28);
      Itinerario itinerario = new Itinerario(10, "Michel", fechaini, fechafin, 1);
      M5Controller controller = new M5Controller();
      Boolean x = controller.ModificarItinerario(itinerario);
      Assert.AreEqual(true, x);
    }

 /*   [TestCase]
    public void Prueba_AgregarEvento_It()
    {
      Itinerario itinerario = new Itinerario(9);
      Evento ev = new Evento(3);
      M5Controller controller = new M5Controller();
      Boolean x = controller.AgregarEvento_It(itinerario, ev);
      Assert.AreEqual(true, x);
    }*/

    [TestCase]
    public void Prueba_AgregarActividad_It()
    {
      Itinerario itinerario = new Itinerario(9);
      Actividad ac = new Actividad
      {
        Id = 1
      };
      M5Controller controller = new M5Controller();
      Boolean x = controller.AgregarActividad_It(itinerario, ac);
      Assert.AreEqual(true, x);
    }

    [TestCase]
    public void Prueba_AgregarLugar_It()
    {
      Itinerario itinerario = new Itinerario(9);
      LugarTuristico lt = new LugarTuristico
      {
        Id = 1
      };
      M5Controller controller = new M5Controller();
      Boolean x = controller.AgregarLugar_It(itinerario, lt);
      Assert.AreEqual(true, x);
    }

  }
}
