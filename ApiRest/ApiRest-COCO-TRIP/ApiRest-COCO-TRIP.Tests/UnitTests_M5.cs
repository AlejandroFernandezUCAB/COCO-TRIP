using NUnit.Framework;
using ApiRest_COCO_TRIP.Controllers;
using ApiRest_COCO_TRIP.Models;
using System;
using Moq;
namespace ApiRest_COCO_TRIP.Tests
{
  [TestFixture]
  class UnitTests_M5
  {
    [TestCase]
    public void Prueba_AgregarItinerario()
    {
      DateTime fechaini = new DateTime(2017,05,28);
      DateTime fechafin = new DateTime(2019,05,28);
      Itinerario itinerario = new Itinerario("Michel",fechaini,fechafin,1);
      M5Controller controller = new M5Controller();
      Boolean x = controller.AgregarItinerario(itinerario);
      Assert.AreEqual(true,x);
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
      Itinerario itinerario = new Itinerario(10,"Michel", fechaini, fechafin, 1);
      M5Controller controller = new M5Controller();
      Boolean x = controller.ModificarItinerario(itinerario);
      Assert.AreEqual(true, x);
    }

    [TestCase]
    public void Prueba_AgregarEvento_It()
    { 
      Itinerario itinerario = new Itinerario(9);
      Evento ev = new Evento(3);
      M5Controller controller = new M5Controller();
      Boolean x = controller.AgregarEvento_It(itinerario,ev);
      Assert.AreEqual(true, x);
    }

    [TestCase]
    public void Prueba_AgregarActividad_It()
    {
      Itinerario itinerario = new Itinerario(9);
      Actividad ac = new Actividad(1);
      M5Controller controller = new M5Controller();
      Boolean x = controller.AgregarActividad_It(itinerario, ac);
      Assert.AreEqual(true, x);
    }

    [TestCase]
    public void Prueba_AgregarLugar_It()
    {
      Itinerario itinerario = new Itinerario(9);
      LugarTuristico lt = new LugarTuristico(1);
      M5Controller controller = new M5Controller();
      Boolean x = controller.AgregarLugar_It(itinerario, lt);
      Assert.AreEqual(true, x);
    }

  }
}
