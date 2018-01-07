using System.Collections.Generic;
using ApiRest_COCO_TRIP.Comun.Validaciones;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///     Pruebas unitarias de ValidacionesWS.
/// </summary>
namespace ApiRestPruebas.M9
{
    /// <summary>
    /// Pruebas unitarias de ValidacionesWS. 
    /// </summary>
    [TestFixture]
    class PruebaValidacionWS
    {
        private JObject parametrosRequest;

        /// <summary>
        /// SetUp de las pruebas unitarias, inicializando instancias.
        /// </summary>
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

        /// <summary>
        /// TearDown de las pruebas unitarias, limpiando instancias.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            parametrosRequest = null;
        }

        /// <summary>
        /// Prueba de excepcion con parametros nulos.
        /// </summary>
        [Test]
        public void ProbarExceptionConParametroNulo()
        {

            Assert.Catch<ParametrosNullExcepcion>(
              () => ValidacionWS.ValidarParametrosNotNull(
                parametrosRequest,
                new List<string> {
                    "param1",
                    "param2",
                    "param3",
                    "param4" //parametro null
                }));
        }

        /// <summary>
        /// Prueba de excepcion con parametros inexistente.
        /// </summary>
        [Test]
        public void ProbarExceptionConParametroNoExistente()
        {
            Assert.Catch<ParametrosNullExcepcion>(
              () => ValidacionWS.ValidarParametrosNotNull(
                parametrosRequest,
                new List<string> {
                    "param1",
                    "param2",
                    "param3",
                    "param5" //parametro no existente
                }));
        }

        /// <summary>
        /// Prueba para comprobar una validacion exitosa.
        /// </summary>
        [Test]
        public void ProbarValidacionExitosa()
        {

            Assert.DoesNotThrow(() => ValidacionWS.ValidarParametrosNotNull(
                parametrosRequest,
                new List<string> {
                    "param1",
                    "param2",
                    "param3"
                }));

        }
    }
}
