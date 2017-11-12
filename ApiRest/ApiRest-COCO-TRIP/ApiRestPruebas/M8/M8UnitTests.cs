using ApiRest_COCO_TRIP.Controllers;
using ApiRest_COCO_TRIP.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestPruebas.M8
{ /**
  <summary>Clase de pruebas para el controlador de localidades de eventos</summary>
  **/
  [TestFixture]
  class M8UnitTests
  {
    private M8_LocalidadEventoController controlador;
    [SetUp]
    public void Init()
    {
      controlador = new M8_LocalidadEventoController();
    }

    /**
     * <summary>Metodo que prueba si la localidad del evento fue agregada exitosamente
     *devuelve el ID de la localidad si se realizo correctamente
     * </summary>
    **/
    [Test]
    public void PruebaAgregarLocalidadEvento()
    {
      LocalidadEvento localidadEvento = new LocalidadEvento("Sambil","Lugar familiar y pasarla bien con amigos y pareja se hacen conciertos y eventos infantiles",
        "Chacao, Venezuela");
      Assert.AreEqual(controlador.AgregarLocalidadEvento(localidadEvento), 5);
    }
    /**
     * <summary>Casos de borde cuando falta algun parametro o todos los parametros
     *para agregar una localidad
     * </summary>
     * <exception cref="InvalidCastException"></exception>
     * **/
    [Test]
    public void PruebaAgregarLocalidadNula()
    {
      Assert.Catch<InvalidCastException>(casoBorde1Agregar);
      Assert.Catch<InvalidCastException>(casoBorde2Agregar);
      Assert.Catch<InvalidCastException>(casoBorde3Agregar);
      Assert.Catch<InvalidCastException>(casoBorde4Agregar);
    }

    public void casoBorde1Agregar()
    {
      LocalidadEvento localidadEvento = new LocalidadEvento();
      controlador.AgregarLocalidadEvento(localidadEvento);
    }

    public void casoBorde2Agregar()
    {
      LocalidadEvento localidadEvento = new LocalidadEvento();
      localidadEvento.Nombre = "prueba1";
      controlador.AgregarLocalidadEvento(localidadEvento);
    }

    public void casoBorde3Agregar()
    {
      LocalidadEvento localidadEvento = new LocalidadEvento();
      localidadEvento.Descripcion = "prueba1";
      controlador.AgregarLocalidadEvento(localidadEvento);
    }

    public void casoBorde4Agregar()
    {
      LocalidadEvento localidadEvento = new LocalidadEvento();
      localidadEvento.Coordenadas = "prueba1";
      controlador.AgregarLocalidadEvento(localidadEvento);
    }
    /**[Test]
    public void PruebaFalloBasedeDatosAgregar()
    {
      Assert.Catch<Npgsql.NpgsqlException>(FalloBasedeDatosAgregar);
    }
    public void FalloBasedeDatosAgregar()
    {
      controlador.AgregarLocalidadEvento(new LocalidadEvento("prueba", "prueba",
        "pruena"));
    }**/

    //Pruebas de ELIMINAR LOCALIDAD
  }
}
