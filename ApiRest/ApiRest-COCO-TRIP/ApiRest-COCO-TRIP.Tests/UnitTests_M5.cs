using NUnit.Framework;
using ApiRest_COCO_TRIP.Controllers;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models.M5;
using System;
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

  }
}
