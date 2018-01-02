using ApiRest_COCO_TRIP.Models.Excepcion;
using ApiRest_COCO_TRIP.Comun.Validaciones;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ApiRestPruebas.M9
{
  [TestFixture]
  class PruebaValidacionWS
  {

    private JObject parametrosRequest;

    [SetUp]
    public void SetUp()
    {
      parametrosRequest = new JObject()
      {
        {"param1", "probando" },
        {"param2", "probando" },
        {"param3", "probando" },
        {"param4", null }

      };
    }

    [TearDown]
    public void TearDown()
    {
      parametrosRequest = null;
    }

    [Test]
    public void ProbarExceptionConParametroNulo()
    {

      Assert.Catch<ParametrosNullException>(
        () => ValidacionWS.validarParametrosNotNull(
          parametrosRequest,
          new List<string> {
            "param1",
            "param2",
            "param3",
            "param4" //parametro null
          }));
    }

 
    [Test]
    public void ProbarExceptionConParametroNoExistente()
    {
      Assert.Catch<ParametrosNullException>(
        () => ValidacionWS.validarParametrosNotNull(
          parametrosRequest,
          new List<string> {
            "param1",
            "param2",
            "param3",
            "param5" //parametro no existente
          }));
    }

    
    [Test]
    public void ProbarValidacionExitosa()
    {
      
      Assert.DoesNotThrow(() => ValidacionWS.validarParametrosNotNull(
          parametrosRequest,
          new List<string> {
            "param1",
            "param2",
            "param3"
          }));

    }
  }
}
