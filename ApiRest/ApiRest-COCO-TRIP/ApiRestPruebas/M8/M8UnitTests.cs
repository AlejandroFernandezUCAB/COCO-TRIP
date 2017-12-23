using ApiRest_COCO_TRIP.Controllers;
using ApiRest_COCO_TRIP.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections;
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
    /*private M8_LocalidadEventoController controlador;
    private PeticionLocalidadEvento peticionLocalidadEvento;
    [SetUp]
    public void Init()
    {
      controlador = new M8_LocalidadEventoController();
      peticionLocalidadEvento = new PeticionLocalidadEvento();
    }*/

    /**
     * <summary>Metodo que prueba si la localidad del evento fue agregada exitosamente
     *devuelve el ID de la localidad si se realizo correctamente
     * </summary>
    **/
    /*[Test]
    public void PruebaAgregarLocalidadEvento()
    {
      LocalidadEvento localidadEvento = new LocalidadEvento("hola", "Lugar familiar y pasarla bien con amigos",
        "Chacao, Venezuela");
      
      Assert.AreEqual(peticionLocalidadEvento.AgregarLocalidadEvento(localidadEvento), 6);
    }*/
    /**
     * <summary>Casos de borde cuando falta algun parametro o todos los parametros
     *para agregar una localidad
     * </summary>
     * <exception cref="InvalidCastException"></exception>
     * **/

    /*[Test]
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
      
      peticionLocalidadEvento.AgregarLocalidadEvento(localidadEvento);
    }

    public void casoBorde2Agregar()
    {
      LocalidadEvento localidadEvento = new LocalidadEvento();
      localidadEvento.Nombre = "prueba1";
      peticionLocalidadEvento.AgregarLocalidadEvento(localidadEvento);
    }

    public void casoBorde3Agregar()
    {
      LocalidadEvento localidadEvento = new LocalidadEvento();
      localidadEvento.Descripcion = "prueba1";
      peticionLocalidadEvento.AgregarLocalidadEvento(localidadEvento);
    }

    public void casoBorde4Agregar()
    {
      LocalidadEvento localidadEvento = new LocalidadEvento();
      localidadEvento.Coordenadas = "prueba1";
      peticionLocalidadEvento.AgregarLocalidadEvento(localidadEvento);
    }*/
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
    /**
     * <summary>Prueba que elimina una localidad exitosamente</summary>
    **/
    /*[Test]
    public void PruebaEliminarLocalidadEvento()
    {
      int idLocalidad = 5;
      Assert.IsTrue(peticionLocalidadEvento.EliminarLocalidadEvento(idLocalidad));
    }
    /**
     * <summary>Prueba de eliminar localidad inexistente</summary>
     * **/
    /*[Test]
    public void PruebaEliminarLocalidadInexistente()
    {
      int idLocalidad = 200;
      Assert.IsFalse(peticionLocalidadEvento.EliminarLocalidadEvento(idLocalidad));
    }*/
    //Pruebas de Consultar Localidad
    /**
     * <summary>Prueba que consulta una Localidad existente</summary>
     * **/
    /*[Test]
    public void PruebaConsultarLocalidadEvento()
    {
      LocalidadEvento localidadEvento = new LocalidadEvento("Suiza", "Europa", "Europa");
      int idLocalidadEvento = peticionLocalidadEvento.AgregarLocalidadEvento(localidadEvento);
      peticionLocalidadEvento = new PeticionLocalidadEvento();
      Assert.AreEqual(peticionLocalidadEvento.ConsultarLocalidadEvento(idLocalidadEvento).Nombre,
        localidadEvento.Nombre);
    }*/
      /**
     * <summary>Prueba de consulta casos de borde id inexitente o invalido</summary>
     * */
    /*[Test]
    public void PruebaConsultarLocalidadNula()
    {
      Assert.Catch<InvalidOperationException>(casoBorde1Consultar);
      Assert.Catch<InvalidOperationException>(casoBorde2Consultar);
    }

    public void casoBorde1Consultar()
    {
      int id = 1111;
      LocalidadEvento localidad = peticionLocalidadEvento.ConsultarLocalidadEvento(id);
    }
    public void casoBorde2Consultar()
    {
      int id = -1;
      LocalidadEvento localidad = peticionLocalidadEvento.ConsultarLocalidadEvento(id);
    }
    */
    /**
     * <summary>Prueba de busqueda de todas las localidades de eventos</summary>
     * */
     /*[Test]
     public void PruebaListaLocalidades()
    {
      
      Assert.AreEqual(peticionLocalidadEvento.ListaLocalidadEventos().Count, 4);

    }
    [Test]
    public void PruebaLocalidadNombre()
    {
      string nombreLo = "hola";
      Assert.AreEqual(peticionLocalidadEvento.ConsultarLocalidadEventoPorNombre(nombreLo).Id,5);

    }*/

  }
}
