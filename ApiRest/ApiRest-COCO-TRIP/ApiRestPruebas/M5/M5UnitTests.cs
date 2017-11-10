using System;
using ApiRest_COCO_TRIP.Models.Dato;
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Controllers;
using NUnit.Framework;
using Npgsql;
namespace ApiRestPruebas
{

  [TestFixture]
  class M5UnitTests
  {
    private M5Controller controller;
    private Itinerario itinerario;
    private Itinerario it;
    private Boolean x;

    [OneTimeSetUp]
    protected void OTSU()
    {
      controller = new M5Controller();
    }

    /// <summary>
    /// Prueba de caso exitoso en AgregarItinerario
    /// </summary>
    [Test]
    public void Prueba_AgregarItinerario()
    {
      itinerario = new Itinerario("Michel", 1);
      it = controller.AgregarItinerario(itinerario);
      Assert.AreEqual(57, it.Id);//siempre poner el numero del id que se va a agregar para esta prueba
      Assert.AreEqual("Michel", it.Nombre);
    }

    /// <summary>
    /// Prueba de casos borde(excepciones) en AgregarItinerario
    /// </summary>
    [Test]
    public void Prueba_FalloAgregarItinerario()
    {
      Assert.Catch<NullReferenceException>(Excepcion_Agregar);
      Assert.Catch<NpgsqlException>(Excepcion_Agregar2);
    }

    /// <summary>
    /// Metodo utilizados para casos borde(excepciones)
    /// </summary>
    public void Excepcion_Agregar()
    {
      itinerario = null;
      controller.AgregarItinerario(itinerario);
    }

    /// <summary>
    /// Metodo utilizados para casos borde(excepciones)
    /// </summary>
    public void Excepcion_Agregar2()
    {
      itinerario = new Itinerario("Michel", 0);
      controller.AgregarItinerario(itinerario);
    }

    [Test]
    public void Prueba_EliminarItinerario()
    {
      x = controller.EliminarItinerario(47);
      Assert.True(x);
    }

    [Test]
    public void Prueba_FalloEliminarItinerario()
    {
      x = controller.EliminarItinerario(4);
      Assert.False(x);
    }

    [Test]
    public void Prueba_ModificarItinerario()
    {
      DateTime fechaini = new DateTime(2021, 05, 28);
      DateTime fechafin = new DateTime(2030, 05, 28);
      Itinerario itinerario = new Itinerario(51, "Michel", fechaini, fechafin, 1);
      x = controller.ModificarItinerario(itinerario);
      Assert.True(x);
    }

    /*   [Test]
       public void Prueba_AgregarEvento_It()
       {
         Itinerario itinerario = new Itinerario(9);
         Evento ev = new Evento(3);
         M5Controller controller = new M5Controller();
         Boolean x = controller.AgregarEvento_It(itinerario, ev);
         Assert.AreEqual(true, x);
       }*/

    [Test]
    public void Prueba_AgregarActividad_It()
    {
      Itinerario itinerario = new Itinerario(9);
      Actividad ac = new Actividad
      {
        Id = 1
      };
      x = controller.AgregarActividad_It(itinerario, ac);
      Assert.AreEqual(true, x);
    }

    [Test]
    public void Prueba_AgregarLugar_It()
    {
      Itinerario itinerario = new Itinerario(9);
      LugarTuristico lt = new LugarTuristico
      {
        Id = 1
      };
      x = controller.AgregarLugar_It(itinerario, lt);
      Assert.AreEqual(true, x);
    }
   
  
  }
}
