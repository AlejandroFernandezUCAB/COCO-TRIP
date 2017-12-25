using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using NUnit.Framework;
using System.Collections.Generic;


namespace ApiRestPruebas.M8
{
  [TestFixture]
  class LocalidadUnitTest
  {
    private Entidad localidad;
    private DAO dao;
    private List<Entidad> lista;
    private Comando comando;

    [SetUp]
    public void SetUp()
    {
      localidad = FabricaEntidad.CrearEntidadLocalidad();
      ((LocalidadEvento)localidad).Nombre = "Test";
      ((LocalidadEvento)localidad).Descripcion = "Test Localidad";
      ((LocalidadEvento)localidad).Coordenadas = "0.2 , 0.1";
      dao = FabricaDAO.CrearDAOLocalidad();
      dao.Insertar(localidad);
      lista = dao.ConsultarLista(null);
      foreach (Entidad entidad in lista) {
        if (((LocalidadEvento)entidad).Nombre.Equals(((LocalidadEvento)localidad).Nombre))
          localidad.Id = entidad.Id;
      }

    }

    [Test]
    public void TestInsertar()
    {
      dao.Eliminar(localidad);
      Assert.DoesNotThrow(() => {
        dao.Insertar(localidad);
      });

      localidad.Id += 1;
      dao.Eliminar(localidad);
      ((LocalidadEvento)localidad).Nombre = null;
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        dao.Insertar(localidad);
      });

      ((LocalidadEvento)localidad).Nombre = "Test";
      ((LocalidadEvento)localidad).Coordenadas = null;
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        dao.Insertar(localidad);
      });

      ((LocalidadEvento)localidad).Coordenadas = "0.2, 0.3";
      ((LocalidadEvento)localidad).Descripcion = null;
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        dao.Insertar(localidad);
      });


    }
    [Test]
    public void TestEliminar()
    {
      Assert.DoesNotThrow(() => {
        dao.Eliminar(localidad);
      });

      Assert.DoesNotThrow(() => {
        dao.Eliminar(localidad);
      });
      int id = localidad.Id + 1;
      dao.Insertar(localidad);
      localidad = FabricaEntidad.CrearEntidadLocalidad();

      Assert.DoesNotThrow(() => {
        dao.Eliminar(localidad);
      });
      localidad.Id = id;
    }

    [Test]
    public void TestConsultar()
    {
      Entidad prueba = FabricaEntidad.CrearEntidadLocalidad();
      prueba.Id = localidad.Id;
      prueba = dao.ConsultarPorId(prueba);

      Assert.AreEqual(prueba.Id, localidad.Id);
      Assert.AreEqual(((LocalidadEvento)prueba).Nombre, ((LocalidadEvento)localidad).Nombre);
      Assert.AreEqual(((LocalidadEvento)prueba).Descripcion, ((LocalidadEvento)localidad).Descripcion);
      Assert.AreEqual(((LocalidadEvento)prueba).Coordenadas, ((LocalidadEvento)localidad).Coordenadas);

      localidad = FabricaEntidad.CrearEntidadLocalidad();

        Assert.Throws<OperacionInvalidaException>(() => {
          dao.ConsultarPorId(localidad);
        });


    }

    [Test]
    public void TestActualizar()
    {
      ((LocalidadEvento)localidad).Nombre = "Test2";
      ((LocalidadEvento)localidad).Descripcion = "Test2";
      ((LocalidadEvento)localidad).Coordenadas = "0,0";
      Assert.DoesNotThrow(() => {
        dao.Actualizar(localidad);
      });

      Entidad prueba = dao.ConsultarPorId(localidad);
      Assert.AreEqual(((LocalidadEvento)localidad).Nombre, ((LocalidadEvento)prueba).Nombre);
      Assert.AreEqual(((LocalidadEvento)localidad).Descripcion, ((LocalidadEvento)prueba).Descripcion);
      Assert.AreEqual(((LocalidadEvento)localidad).Coordenadas, ((LocalidadEvento)prueba).Coordenadas);
      int id = localidad.Id;
      localidad.Id = 0;
      Assert.DoesNotThrow(() => {
        dao.Actualizar(localidad);
      });

      localidad.Id = id;

      ((LocalidadEvento)localidad).Nombre = null;
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        dao.Actualizar(localidad);
      });

      ((LocalidadEvento)localidad).Nombre = "test";
      ((LocalidadEvento)localidad).Descripcion = null;
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        dao.Actualizar(localidad);
      });

      ((LocalidadEvento)localidad).Descripcion = "test";
      ((LocalidadEvento)localidad).Coordenadas = null;
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        dao.Actualizar(localidad);
      });

    }

    [Test]
    public void TestComandoAgregarLocalidad()
    {

      dao.Eliminar(localidad);
      comando = FabricaComando.CrearComandoAgregarLocalidad(localidad);

      Assert.DoesNotThrow(() => {
        comando.Ejecutar();
      });
      localidad.Id += 1;
      dao.Eliminar(localidad);
      ((LocalidadEvento)localidad).Nombre = null;

      comando = FabricaComando.CrearComandoAgregarLocalidad(localidad);
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        comando.Ejecutar();
      });
      ((LocalidadEvento)localidad).Nombre = "Test";
      ((LocalidadEvento)localidad).Descripcion = null;
      comando = FabricaComando.CrearComandoAgregarLocalidad(localidad);
      Assert.DoesNotThrow(() => {
        comando.Ejecutar();
      });

      localidad.Id += 1;
      dao.Eliminar(localidad);
      ((LocalidadEvento)localidad).Descripcion = "Test";
      ((LocalidadEvento)localidad).Coordenadas = null;
      comando = FabricaComando.CrearComandoAgregarLocalidad(localidad);
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        comando.Ejecutar();
      });

      localidad = FabricaEntidad.CrearEntidadLocalidad();
      comando = FabricaComando.CrearComandoAgregarLocalidad(localidad);
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        comando.Ejecutar();
      });

    }

    [Test]
    public void TestComandoConsultarLocalidad()
    {
      comando = FabricaComando.CrearComandoConsultarLocalidad(localidad.Id);

      Assert.DoesNotThrow(() => {
        comando.Ejecutar();
      });
      Entidad prueba = comando.Retornar();
      Assert.AreEqual(((LocalidadEvento)localidad).Nombre, ((LocalidadEvento)prueba).Nombre);
      Assert.AreEqual(((LocalidadEvento)localidad).Descripcion, ((LocalidadEvento)prueba).Descripcion);
      Assert.AreEqual(((LocalidadEvento)localidad).Coordenadas, ((LocalidadEvento)prueba).Coordenadas);

      localidad.Id = 0;
      comando = FabricaComando.CrearComandoConsultarLocalidad(localidad.Id);

      Assert.Throws<OperacionInvalidaException>(() => {
        comando.Ejecutar();
      });

      localidad = FabricaEntidad.CrearEntidadLocalidad();
      comando = FabricaComando.CrearComandoConsultarLocalidad(localidad.Id);
      Assert.Throws<OperacionInvalidaException>(() => {
        comando.Ejecutar();
      });

    }

    [Test]
    public void TestComandoConsultarLocalidades() {
      Entidad prueba = localidad;
      comando = FabricaComando.CrearComandoConsultarLocalidades();
      Assert.DoesNotThrow(() => {
        comando.Ejecutar();
      });
      foreach (Entidad entidad in comando.RetornarLista())
      {
        if (entidad.Id == localidad.Id)
        {
          Assert.AreEqual(((LocalidadEvento)localidad).Nombre, ((LocalidadEvento)entidad).Nombre);
          Assert.AreEqual(((LocalidadEvento)localidad).Descripcion, ((LocalidadEvento)entidad).Descripcion);
          Assert.AreEqual(((LocalidadEvento)localidad).Coordenadas, ((LocalidadEvento)entidad).Coordenadas);
        }
     
      }
     
    }

    [Test]
    public void TestComandoEliminarLocalidad()
    {

      comando = FabricaComando.CrearComandoEliminarLocalidad(localidad.Id);
      Assert.DoesNotThrow(() =>
      {
        comando.Ejecutar();
      });

      localidad.Id = 0;
      comando = FabricaComando.CrearComandoEliminarLocalidad(localidad.Id);
      Assert.DoesNotThrow(() =>
      {
        comando.Ejecutar();
      });

      localidad = FabricaEntidad.CrearEntidadLocalidad();
      comando = FabricaComando.CrearComandoEliminarLocalidad(localidad.Id);
      Assert.DoesNotThrow(() => {
        comando.Ejecutar();
      });

    }

    [Test]
    public void TestComandoActualizarLocalidad() {
      ((LocalidadEvento)localidad).Nombre = "Test2";
      comando = FabricaComando.CrearComandoModificarLocalidad(localidad);
      Assert.DoesNotThrow(() =>
      {
        comando.Ejecutar();
      });
      Assert.AreEqual(((LocalidadEvento)localidad).Nombre,
        ((LocalidadEvento)dao.ConsultarPorId(localidad)).Nombre);

      ((LocalidadEvento)localidad).Descripcion = "Test2";
      comando = FabricaComando.CrearComandoModificarLocalidad(localidad);
      Assert.DoesNotThrow(() =>
      {
        comando.Ejecutar();
      });
      Assert.AreEqual(((LocalidadEvento)localidad).Descripcion,
        ((LocalidadEvento)dao.ConsultarPorId(localidad)).Descripcion);

       ((LocalidadEvento)localidad).Coordenadas = "0.2, 0.02";
      comando = FabricaComando.CrearComandoModificarLocalidad(localidad);
      Assert.DoesNotThrow(() =>
      {
        comando.Ejecutar();
      });
      Assert.AreEqual(((LocalidadEvento)localidad).Coordenadas,
        ((LocalidadEvento)dao.ConsultarPorId(localidad)).Coordenadas);


      ((LocalidadEvento)localidad).Nombre = null;
      comando = FabricaComando.CrearComandoModificarLocalidad(localidad);
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        comando.Ejecutar();
      });

      ((LocalidadEvento)localidad).Nombre = "Test";
      ((LocalidadEvento)localidad).Descripcion = null;
      comando = FabricaComando.CrearComandoModificarLocalidad(localidad);
      Assert.DoesNotThrow(() =>
      {
        comando.Ejecutar();
      });

      ((LocalidadEvento)localidad).Descripcion = "Test";
      ((LocalidadEvento)localidad).Coordenadas = null;
      comando = FabricaComando.CrearComandoModificarLocalidad(localidad);
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        comando.Ejecutar();
      });
    }

    [TearDown]
    public void TearDown()
    {
      dao.Eliminar(localidad);
    }

  }
}
