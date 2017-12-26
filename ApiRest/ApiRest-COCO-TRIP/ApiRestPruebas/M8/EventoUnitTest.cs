using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Negocio.Command;
using NUnit.Framework;
using System;
using System.Collections.Generic;


namespace ApiRestPruebas.M8
{
  [TestFixture]
  class EventoUnitTest
  {
    private Entidad evento;
    private Entidad categoria;
    private Entidad localidad;
    private DAO daoEvento;
    private DAO daoLocalidad;
    private DAO daoCategoria;
    private List<Entidad> lista;
    private Comando comando;

    [SetUp]
    public void SetUpEvento()
    {
      localidad = FabricaEntidad.CrearEntidadLocalidad();
      ((LocalidadEvento)localidad).Nombre = "Test";
      ((LocalidadEvento)localidad).Descripcion = "Test Localidad";
      ((LocalidadEvento)localidad).Coordenadas = "0.2 , 0.1";

      daoLocalidad = FabricaDAO.CrearDAOLocalidad();
      daoLocalidad.Insertar(localidad);
      lista = daoLocalidad.ConsultarLista(null);
      foreach (Entidad entidad in lista)
      {
        if (((LocalidadEvento)entidad).Nombre.Equals(((LocalidadEvento)localidad).Nombre))
          localidad.Id = entidad.Id;
      }

      categoria = FabricaEntidad.CrearEntidadCategoria();

      daoCategoria = FabricaDAO.CrearDAOCategoria();
      ((Categoria)categoria).CategoriaSuperior = 0;
      ((Categoria)categoria).Descripcion = "Test";
      ((Categoria)categoria).Estatus = true;
      ((Categoria)categoria).Nombre = "Test";
      ((Categoria)categoria).Nivel = 1;
      daoCategoria.Insertar(categoria);

      categoria.Id = ((DAOCategoria)daoCategoria).ObtenerIdCategoriaPorNombre((Categoria)categoria).Id;

      evento = FabricaEntidad.CrearEntidadEvento();
      ((Evento)evento).Nombre = "Test";
      ((Evento)evento).Descripcion = "Test Localidad";
      ((Evento)evento).FechaInicio = System.DateTime.Now;
      ((Evento)evento).FechaFin = System.DateTime.Now;
      ((Evento)evento).HoraInicio = System.DateTime.Now;
      ((Evento)evento).HoraFin = System.DateTime.Now;
      ((Evento)evento).Precio = 150.28;
      ((Evento)evento).Foto = "/test.jpg";
      ((Evento)evento).IdCategoria = categoria.Id;
      ((Evento)evento).IdLocalidad = localidad.Id;
      daoEvento = FabricaDAO.CrearDAOEvento();
      daoEvento.Insertar(evento);
      lista = daoEvento.ConsultarLista(categoria);

      foreach (Entidad entidad in lista)
      {
        if (((Evento)entidad).Nombre.Equals(((Evento)evento).Nombre))
          evento.Id = entidad.Id;
      }
    }

    [Test]
    public void TestInsertarEvento()
    {
      daoEvento.Eliminar(evento);
      Assert.DoesNotThrow(() => {
        daoEvento.Insertar(evento);
      });

      evento.Id += 1;
      daoEvento.Eliminar(evento);
      ((Evento)evento).Nombre = null;
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        daoEvento.Insertar(evento);
      });

      ((Evento)evento).Nombre = "Test";
      ((Evento)evento).Descripcion = null;
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        daoEvento.Insertar(evento);
      });

      ((Evento)evento).Descripcion = "Test";
      ((Evento)evento).Foto = null;
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        daoEvento.Insertar(evento);
      });


    }
    [Test]
    public void TestEliminarEvento()
    {
      Assert.DoesNotThrow(() => {
        daoEvento.Eliminar(evento);
      });

      Assert.DoesNotThrow(() => {
        daoEvento.Eliminar(evento);
      });
      int id = evento.Id + 1;
      daoEvento.Insertar(evento);
      evento = FabricaEntidad.CrearEntidadEvento();

      Assert.DoesNotThrow(() => {
        daoEvento.Eliminar(evento);
      });
      evento.Id = id;
    }

    [Test]
    public void TestConsultarEvento()
    {
      Entidad prueba = FabricaEntidad.CrearEntidadEvento();
      prueba.Id = evento.Id;
      prueba = daoEvento.ConsultarPorId(prueba);

      Assert.AreEqual(prueba.Id, evento.Id);
      Assert.AreEqual(((Evento)evento).Nombre, ((Evento)prueba).Nombre);
      Assert.AreEqual(((Evento)evento).Descripcion, ((Evento)prueba).Descripcion);
      Assert.AreEqual(((Evento)evento).FechaInicio.Date, ((Evento)prueba).FechaInicio.Date);
      Assert.AreEqual(((Evento)evento).FechaFin.Date, ((Evento)prueba).FechaFin.Date);
      Assert.AreEqual(((Evento)evento).Foto, ((Evento)prueba).Foto);
      Assert.AreEqual(((Evento)evento).HoraInicio.Hour, ((Evento)prueba).HoraInicio.Hour);
      Assert.AreEqual(((Evento)evento).HoraInicio.Minute, ((Evento)prueba).HoraInicio.Minute);
      Assert.AreEqual(((Evento)evento).HoraInicio.Second, ((Evento)prueba).HoraInicio.Second);
      Assert.AreEqual(((Evento)evento).HoraFin.Hour, ((Evento)prueba).HoraFin.Hour);
      Assert.AreEqual(((Evento)evento).HoraFin.Minute, ((Evento)prueba).HoraFin.Minute);
      Assert.AreEqual(((Evento)evento).HoraFin.Second, ((Evento)prueba).HoraFin.Second);
      Assert.AreEqual(((Evento)evento).Precio, ((Evento)prueba).Precio);
      Assert.AreEqual(((Evento)evento).IdCategoria, ((Evento)prueba).IdCategoria);
      Assert.AreEqual(((Evento)evento).IdLocalidad, ((Evento)prueba).IdLocalidad);


      evento = FabricaEntidad.CrearEntidadEvento();

      Assert.Throws<OperacionInvalidaException>(() => {
        daoEvento.ConsultarPorId(evento);
      });


    }

    [Test]
    public void TestActualizarEvento()
    {
      ((Evento)evento).Nombre = "Test2";
      ((Evento)evento).Descripcion = "Test2";
      ((Evento)evento).FechaFin = System.DateTime.Now;
      ((Evento)evento).FechaInicio = System.DateTime.Now;
      ((Evento)evento).Foto = "/test2.jpg";
      ((Evento)evento).HoraFin = System.DateTime.Now;
      ((Evento)evento).HoraInicio = System.DateTime.Now;
      ((Evento)evento).Precio = 100.20;
      Assert.DoesNotThrow(() => {
        daoEvento.Actualizar(evento);
      });

      Entidad prueba = daoEvento.ConsultarPorId(evento);
      Assert.AreEqual(prueba.Id, evento.Id);
      Assert.AreEqual(((Evento)evento).Nombre, ((Evento)prueba).Nombre);
      Assert.AreEqual(((Evento)evento).Descripcion, ((Evento)prueba).Descripcion);
      Assert.AreEqual(((Evento)evento).FechaInicio, ((Evento)prueba).FechaInicio);
      Assert.AreEqual(((Evento)evento).FechaFin, ((Evento)prueba).FechaFin);
      Assert.AreEqual(((Evento)evento).Foto, ((Evento)prueba).Foto);
      Assert.AreEqual(((Evento)evento).HoraInicio, ((Evento)prueba).HoraFin);
      Assert.AreEqual(((Evento)evento).Precio, ((Evento)prueba).Precio);
      Assert.AreEqual(((Evento)evento).IdCategoria, ((Evento)prueba).IdCategoria);
      Assert.AreEqual(((Evento)evento).IdLocalidad, ((Evento)prueba).IdLocalidad);
      int id = evento.Id;
      evento.Id = 0;
      Assert.DoesNotThrow(() => {
        daoEvento.Actualizar(evento);
      });

      evento.Id = id;

      ((Evento)evento).Nombre = null;
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        daoEvento.Actualizar(evento);
      });

      ((Evento)evento).Nombre = "test";
      ((Evento)evento).Descripcion = null;
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        daoEvento.Actualizar(evento);
      });

      ((Evento)evento).Descripcion = "test";
      ((Evento)evento).Foto = null;
      Assert.Throws<CasteoInvalidoExcepcion>(() => {
        daoEvento.Actualizar(evento);
      });

    }

    [TearDown]
    public void TearDownEvento()
    {
      daoLocalidad.Eliminar(localidad);
      daoEvento.Eliminar(evento);

      daoCategoria.Conectar();
      daoCategoria.Comando = daoCategoria.SqlConexion.CreateCommand();
      daoCategoria.Comando.CommandText = "DELETE FROM categoria where ca_id ="+categoria.Id;
      daoCategoria.Comando.ExecuteNonQuery();
      daoCategoria.Desconectar();
    }

  }
}