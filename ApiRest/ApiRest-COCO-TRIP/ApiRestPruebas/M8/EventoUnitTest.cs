using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Controllers;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
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
        private IDAOEvento daoEvento;
        private DAO daoLocalidad;
        private DAO daoCategoria;
        private List<Entidad> lista;
        private Comando comando;
        private M8_EventosController controlador;
        private Dictionary<string, object> esperado = new Dictionary<string, object>();
        private Dictionary<string, object> respuesta = new Dictionary<string, object>();

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
            lista = daoEvento.ConsultarListaPorCategoria(categoria);

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
            Assert.DoesNotThrow(() =>
            {
                daoEvento.Insertar(evento);
            });

            evento.Id += 1;
            daoEvento.Eliminar(evento);
            ((Evento)evento).Nombre = null;
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                daoEvento.Insertar(evento);
            });

            ((Evento)evento).Nombre = "Test";
            ((Evento)evento).Descripcion = null;
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                daoEvento.Insertar(evento);
            });

            ((Evento)evento).Descripcion = "Test";
            ((Evento)evento).Foto = null;
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                daoEvento.Insertar(evento);
            });
            int id = ((Evento)evento).IdCategoria;
            ((Evento)evento).Foto = "/Test";
            ((Evento)evento).IdCategoria = 0;
            Assert.Throws<BaseDeDatosExcepcion>(() =>
            {
                daoEvento.Insertar(evento);
            });

            ((Evento)evento).IdCategoria = id;
            id = ((Evento)evento).IdLocalidad;
            ((Evento)evento).IdLocalidad = 0;

            Assert.Throws<BaseDeDatosExcepcion>(() =>
            {
                daoEvento.Insertar(evento);
            });
        }
        [Test]
        public void TestEliminarEvento()
        {
            Assert.DoesNotThrow(() =>
            {
                daoEvento.Eliminar(evento);
            });

            Assert.DoesNotThrow(() =>
            {
                daoEvento.Eliminar(evento);
            });
            int id = evento.Id + 1;
            daoEvento.Insertar(evento);
            evento = FabricaEntidad.CrearEntidadEvento();

            Assert.DoesNotThrow(() =>
            {
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

            Assert.Throws<OperacionInvalidaExcepcion>(() =>
            {
                daoEvento.ConsultarPorId(evento);
            });


        }

        [Test]
        public void TestConsultarEventos()
        {
            List<Entidad> listaPrueba = new List<Entidad>();
            Assert.DoesNotThrow(() =>
            {
                listaPrueba = daoEvento.ConsultarLista(null);
            });
            foreach (Entidad entidad in listaPrueba)
            {
                if (entidad.Id == evento.Id)
                {
                    Assert.AreEqual(entidad.Id, evento.Id);
                    Assert.AreEqual(((Evento)evento).Nombre, ((Evento)entidad).Nombre);
                    Assert.AreEqual(((Evento)evento).Descripcion, ((Evento)entidad).Descripcion);
                    Assert.AreEqual(((Evento)evento).FechaInicio.Date, ((Evento)entidad).FechaInicio.Date);
                    Assert.AreEqual(((Evento)evento).FechaFin.Date, ((Evento)entidad).FechaFin.Date);
                    Assert.AreEqual(((Evento)evento).Foto, ((Evento)entidad).Foto);
                    Assert.AreEqual(((Evento)evento).HoraInicio.Hour, ((Evento)entidad).HoraInicio.Hour);
                    Assert.AreEqual(((Evento)evento).HoraInicio.Minute, ((Evento)entidad).HoraInicio.Minute);
                    Assert.AreEqual(((Evento)evento).HoraInicio.Second, ((Evento)entidad).HoraInicio.Second);
                    Assert.AreEqual(((Evento)evento).HoraFin.Hour, ((Evento)entidad).HoraFin.Hour);
                    Assert.AreEqual(((Evento)evento).HoraFin.Minute, ((Evento)entidad).HoraFin.Minute);
                    Assert.AreEqual(((Evento)evento).HoraFin.Second, ((Evento)entidad).HoraFin.Second);
                    Assert.AreEqual(((Evento)evento).Precio, ((Evento)entidad).Precio);
                    Assert.AreEqual(((Evento)evento).IdCategoria, ((Evento)entidad).IdCategoria);
                    Assert.AreEqual(((Evento)evento).IdLocalidad, ((Evento)entidad).IdLocalidad);
                }

            }

        }

        [Test]
        public void TestConsultarEventosPorIdCategoria()
        {
            List<Entidad> listaPrueba = new List<Entidad>();
            Assert.DoesNotThrow(() =>
            {
                listaPrueba = daoEvento.ConsultarListaPorCategoria(categoria);
            });
            foreach (Entidad entidad in listaPrueba)
            {
                if (entidad.Id == evento.Id)
                {
                    Assert.AreEqual(entidad.Id, evento.Id);
                    Assert.AreEqual(((Evento)evento).Nombre, ((Evento)entidad).Nombre);
                    Assert.AreEqual(((Evento)evento).Descripcion, ((Evento)entidad).Descripcion);
                    Assert.AreEqual(((Evento)evento).FechaInicio.Date, ((Evento)entidad).FechaInicio.Date);
                    Assert.AreEqual(((Evento)evento).FechaFin.Date, ((Evento)entidad).FechaFin.Date);
                    Assert.AreEqual(((Evento)evento).Foto, ((Evento)entidad).Foto);
                    Assert.AreEqual(((Evento)evento).HoraInicio.Hour, ((Evento)entidad).HoraInicio.Hour);
                    Assert.AreEqual(((Evento)evento).HoraInicio.Minute, ((Evento)entidad).HoraInicio.Minute);
                    Assert.AreEqual(((Evento)evento).HoraInicio.Second, ((Evento)entidad).HoraInicio.Second);
                    Assert.AreEqual(((Evento)evento).HoraFin.Hour, ((Evento)entidad).HoraFin.Hour);
                    Assert.AreEqual(((Evento)evento).HoraFin.Minute, ((Evento)entidad).HoraFin.Minute);
                    Assert.AreEqual(((Evento)evento).HoraFin.Second, ((Evento)entidad).HoraFin.Second);
                    Assert.AreEqual(((Evento)evento).Precio, ((Evento)entidad).Precio);
                    Assert.AreEqual(((Evento)evento).IdLocalidad, ((Evento)entidad).IdLocalidad);
                }

            }
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
            Assert.DoesNotThrow(() =>
            {
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
            Assert.DoesNotThrow(() =>
            {
                daoEvento.Actualizar(evento);
            });

            evento.Id = id;

            ((Evento)evento).Nombre = null;
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                daoEvento.Actualizar(evento);
            });

            ((Evento)evento).Nombre = "test";
            ((Evento)evento).Descripcion = null;
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                daoEvento.Actualizar(evento);
            });

            ((Evento)evento).Descripcion = "test";
            ((Evento)evento).Foto = null;
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                daoEvento.Actualizar(evento);
            });

            ((Evento)evento).Foto = "/Test";
            ((Evento)evento).IdCategoria = 0;
            Assert.Throws<BaseDeDatosExcepcion>(() =>
            {
                daoEvento.Actualizar(evento);
            });

            ((Evento)evento).IdCategoria = id;
            id = ((Evento)evento).IdLocalidad;
            ((Evento)evento).IdLocalidad = 0;

            Assert.Throws<BaseDeDatosExcepcion>(() =>
            {
                daoEvento.Actualizar(evento);
            });

        }

        [Test]
        public void TestComandoAgregarEvento()
        {

            daoEvento.Eliminar(evento);
            comando = FabricaComando.CrearComandoAgregarEvento(evento);

            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });
            evento.Id += 1;
            daoEvento.Eliminar(evento);
            ((Evento)evento).Nombre = null;

            comando = FabricaComando.CrearComandoAgregarEvento(evento);
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                comando.Ejecutar();
            });
            ((Evento)evento).Nombre = "Test";
            ((Evento)evento).Descripcion = null;
            comando = FabricaComando.CrearComandoAgregarEvento(evento);
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                comando.Ejecutar();
            });

            ((Evento)evento).Descripcion = "Test";
            ((Evento)evento).Foto = null;
            comando = FabricaComando.CrearComandoAgregarEvento(evento);
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                comando.Ejecutar();
            });

            evento = FabricaEntidad.CrearEntidadEvento();
            comando = FabricaComando.CrearComandoAgregarEvento(evento);
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                comando.Ejecutar();
            });

        }

        [Test]
        public void TestComandoConsultarEvento()
        {
            comando = FabricaComando.CrearComandoConsultarEvento(evento.Id);

            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });
            Entidad prueba = comando.Retornar();
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

            evento.Id = 0;
            comando = FabricaComando.CrearComandoConsultarEvento(evento.Id);

            Assert.Throws<OperacionInvalidaExcepcion>(() =>
            {
                comando.Ejecutar();
            });

            evento = FabricaEntidad.CrearEntidadEvento();
            comando = FabricaComando.CrearComandoConsultarEvento(evento.Id);
            Assert.Throws<OperacionInvalidaExcepcion>(() =>
            {
                comando.Ejecutar();
            });

        }

        [Test]
        public void TestComandoConsultarEventosPorCategoria()
        {
            Entidad prueba = evento;
            comando = FabricaComando.CrearComandoConsultarEventosPorCategoria(categoria.Id);
            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });
            foreach (Entidad entidad in comando.RetornarLista())
            {
                if (entidad.Id == localidad.Id)
                {
                    Assert.AreEqual(entidad.Id, evento.Id);
                    Assert.AreEqual(((Evento)evento).Nombre, ((Evento)entidad).Nombre);
                    Assert.AreEqual(((Evento)evento).Descripcion, ((Evento)entidad).Descripcion);
                    Assert.AreEqual(((Evento)evento).FechaInicio.Date, ((Evento)entidad).FechaInicio.Date);
                    Assert.AreEqual(((Evento)evento).FechaFin.Date, ((Evento)entidad).FechaFin.Date);
                    Assert.AreEqual(((Evento)evento).Foto, ((Evento)entidad).Foto);
                    Assert.AreEqual(((Evento)evento).HoraInicio.Hour, ((Evento)entidad).HoraInicio.Hour);
                    Assert.AreEqual(((Evento)evento).HoraInicio.Minute, ((Evento)entidad).HoraInicio.Minute);
                    Assert.AreEqual(((Evento)evento).HoraInicio.Second, ((Evento)entidad).HoraInicio.Second);
                    Assert.AreEqual(((Evento)evento).HoraFin.Hour, ((Evento)entidad).HoraFin.Hour);
                    Assert.AreEqual(((Evento)evento).HoraFin.Minute, ((Evento)entidad).HoraFin.Minute);
                    Assert.AreEqual(((Evento)evento).HoraFin.Second, ((Evento)entidad).HoraFin.Second);
                    Assert.AreEqual(((Evento)evento).Precio, ((Evento)entidad).Precio);
                    Assert.AreEqual(((Evento)evento).IdCategoria, ((Evento)entidad).IdCategoria);
                    Assert.AreEqual(((Evento)evento).IdLocalidad, ((Evento)entidad).IdLocalidad);
                }

            }

        }

        [Test]
        public void TestComandoEliminarEvento()
        {

            comando = FabricaComando.CrearComandoEliminarEvento(evento.Id);
            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });

            evento.Id = 0;
            comando = FabricaComando.CrearComandoEliminarEvento(evento.Id);
            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });

            evento = FabricaEntidad.CrearEntidadEvento();
            comando = FabricaComando.CrearComandoEliminarEvento(evento.Id);
            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });

        }

        [Test]
        public void TestComandoActualizarEvento()
        {
            ((Evento)evento).Nombre = "Test2";
            comando = FabricaComando.CrearComandoModificarEvento(evento);
            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });
            Assert.AreEqual(((Evento)evento).Nombre,
              ((Evento)daoEvento.ConsultarPorId(evento)).Nombre);

            ((Evento)evento).Descripcion = "Test2";
            comando = FabricaComando.CrearComandoModificarEvento(evento);
            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });
            Assert.AreEqual(((Evento)evento).Descripcion,
              ((Evento)daoEvento.ConsultarPorId(evento)).Descripcion);

            ((Evento)evento).Foto = "/test2";
            comando = FabricaComando.CrearComandoModificarEvento(evento);
            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });
            Assert.AreEqual(((Evento)evento).Foto,
              ((Evento)daoEvento.ConsultarPorId(evento)).Foto);


            ((Evento)evento).Nombre = null;
            comando = FabricaComando.CrearComandoModificarEvento(evento);
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                comando.Ejecutar();
            });

            ((Evento)evento).Nombre = "Test";
            ((Evento)evento).Descripcion = null;
            comando = FabricaComando.CrearComandoModificarEvento(evento);
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                comando.Ejecutar();
            });

            ((Evento)evento).Descripcion = "Test";
            ((Evento)evento).Foto = null;
            comando = FabricaComando.CrearComandoModificarEvento(evento);
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                comando.Ejecutar();
            });
            ((Evento)evento).Foto = "/Test";
            int id = ((Evento)evento).IdCategoria;
            ((Evento)evento).IdCategoria = 0;
            comando = FabricaComando.CrearComandoModificarEvento(evento);
            Assert.Throws<BaseDeDatosExcepcion>(() =>
            {
                comando.Ejecutar();
            });

            ((Evento)evento).IdCategoria = id;
            id = ((Evento)evento).IdLocalidad;
            ((Evento)evento).IdCategoria = 0;
            comando = FabricaComando.CrearComandoModificarEvento(evento);
            Assert.Throws<BaseDeDatosExcepcion>(() =>
            {
                comando.Ejecutar();
            });
            ((Evento)evento).IdCategoria = id;

        }

        [Test]
        public void TestControladorAgregarEvento()
        {

            daoEvento.Eliminar(evento);
            controlador = new M8_EventosController();
            respuesta = (Dictionary<string, object>)controlador.AgregarEvento((Evento)evento);
            esperado.Add("dato", "Se ha creado un evento");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            evento.Id += 1;

            ((Evento)evento).Nombre = null;

            respuesta = (Dictionary<string, object>)controlador.AgregarEvento((Evento)evento);
            esperado.Add("Error", "Specified cast is not valid.");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            ((Evento)evento).Nombre = "Test";
            ((Evento)evento).Descripcion = null;
            respuesta = (Dictionary<string, object>)controlador.AgregarEvento((Evento)evento);
            esperado.Add("Error", "Specified cast is not valid.");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            ((Evento)evento).Descripcion = "Test";
            ((Evento)evento).Foto = null;
            respuesta = (Dictionary<string, object>)controlador.AgregarEvento((Evento)evento);
            esperado.Add("Error", "Specified cast is not valid.");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            ((Evento)evento).Foto = "/Test";
            int id = ((Evento)evento).IdCategoria;
            ((Evento)evento).IdCategoria = 0;
            respuesta = (Dictionary<string, object>)controlador.AgregarEvento((Evento)evento);
            esperado.Add("Error", "External component has thrown an exception.");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            ((Evento)evento).IdCategoria = id;
            id = ((Evento)evento).IdLocalidad;
            ((Evento)evento).IdLocalidad = 0;

            respuesta = (Dictionary<string, object>)controlador.AgregarEvento((Evento)evento);
            esperado.Add("Error", "External component has thrown an exception.");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();


            evento = FabricaEntidad.CrearEntidadEvento();
            respuesta = (Dictionary<string, object>)controlador.AgregarEvento((Evento)evento);
            esperado.Add("Error", "Specified cast is not valid.");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();


        }

        [Test]
        public void TestControladorConsultarEvento()
        {
            Object prueba;
            Entidad entidad;
            controlador = new M8_EventosController();
            respuesta = (Dictionary<string, object>)controlador.ConsultarEvento(evento.Id);
            respuesta.TryGetValue("dato", out prueba);
            entidad = (Evento)prueba;
            Assert.AreEqual(evento.Id, entidad.Id);
            Assert.AreEqual(((Evento)evento).Nombre, ((Evento)entidad).Nombre);
            Assert.AreEqual(((Evento)evento).Descripcion, ((Evento)entidad).Descripcion);
            Assert.AreEqual(((Evento)evento).FechaInicio.Date, ((Evento)entidad).FechaInicio.Date);
            Assert.AreEqual(((Evento)evento).FechaFin.Date, ((Evento)entidad).FechaFin.Date);
            Assert.AreEqual(((Evento)evento).Foto, ((Evento)entidad).Foto);
            Assert.AreEqual(((Evento)evento).HoraInicio.Hour, ((Evento)entidad).HoraInicio.Hour);
            Assert.AreEqual(((Evento)evento).HoraInicio.Minute, ((Evento)entidad).HoraInicio.Minute);
            Assert.AreEqual(((Evento)evento).HoraInicio.Second, ((Evento)entidad).HoraInicio.Second);
            Assert.AreEqual(((Evento)evento).HoraFin.Hour, ((Evento)entidad).HoraFin.Hour);
            Assert.AreEqual(((Evento)evento).HoraFin.Minute, ((Evento)entidad).HoraFin.Minute);
            Assert.AreEqual(((Evento)evento).HoraFin.Second, ((Evento)entidad).HoraFin.Second);
            Assert.AreEqual(((Evento)evento).Precio, ((Evento)entidad).Precio);
            Assert.AreEqual(((Evento)evento).IdCategoria, ((Evento)entidad).IdCategoria);
            Assert.AreEqual(((Evento)evento).IdLocalidad, ((Evento)entidad).IdLocalidad);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            int id = evento.Id;
            evento.Id = 0;

            respuesta = (Dictionary<string, object>)controlador.ConsultarEvento(evento.Id);
            esperado.Add("Error", "Operation is not valid due to the current state of the object.");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            evento = FabricaEntidad.CrearEntidadEvento();

            respuesta = (Dictionary<string, object>)controlador.ConsultarEvento(evento.Id);
            esperado.Add("Error", "Operation is not valid due to the current state of the object.");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            evento.Id = id;
        }

        [Test]
        public void TestControladorConsultarEventosPorCategoria()
        {
            Object prueba;
            List<Entidad> entidades;
            controlador = new M8_EventosController();
            respuesta = (Dictionary<string, object>)controlador.ListaEventosPorCategoria(categoria.Id);
            respuesta.TryGetValue("dato", out prueba);
            entidades = (List<Entidad>)prueba;
            foreach (Entidad entidad in entidades)
            {
                if (entidad.Id == localidad.Id)
                {
                    Assert.AreEqual(entidad.Id, evento.Id);
                    Assert.AreEqual(((Evento)evento).Nombre, ((Evento)entidad).Nombre);
                    Assert.AreEqual(((Evento)evento).Descripcion, ((Evento)entidad).Descripcion);
                    Assert.AreEqual(((Evento)evento).FechaInicio.Date, ((Evento)entidad).FechaInicio.Date);
                    Assert.AreEqual(((Evento)evento).FechaFin.Date, ((Evento)entidad).FechaFin.Date);
                    Assert.AreEqual(((Evento)evento).Foto, ((Evento)entidad).Foto);
                    Assert.AreEqual(((Evento)evento).HoraInicio.Hour, ((Evento)entidad).HoraInicio.Hour);
                    Assert.AreEqual(((Evento)evento).HoraInicio.Minute, ((Evento)entidad).HoraInicio.Minute);
                    Assert.AreEqual(((Evento)evento).HoraInicio.Second, ((Evento)entidad).HoraInicio.Second);
                    Assert.AreEqual(((Evento)evento).HoraFin.Hour, ((Evento)entidad).HoraFin.Hour);
                    Assert.AreEqual(((Evento)evento).HoraFin.Minute, ((Evento)entidad).HoraFin.Minute);
                    Assert.AreEqual(((Evento)evento).HoraFin.Second, ((Evento)entidad).HoraFin.Second);
                    Assert.AreEqual(((Evento)evento).Precio, ((Evento)entidad).Precio);
                    Assert.AreEqual(((Evento)evento).IdCategoria, ((Evento)entidad).IdCategoria);
                    Assert.AreEqual(((Evento)evento).IdLocalidad, ((Evento)entidad).IdLocalidad);
                }

            }

        }

        [Test]
        public void TestControladorEliminarEvento()
        {

            controlador = new M8_EventosController();
            respuesta = (Dictionary<string, object>)controlador.EliminarEvento(evento.Id);
            esperado.Add("dato", "Se ha eliminado un evento");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            evento.Id = 0;

            respuesta = (Dictionary<string, object>)controlador.EliminarEvento(evento.Id);
            esperado.Add("dato", "Se ha eliminado un evento");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            evento = FabricaEntidad.CrearEntidadEvento();
            respuesta = (Dictionary<string, object>)controlador.EliminarEvento(evento.Id);
            esperado.Add("dato", "Se ha eliminado un evento");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

        }

        [Test]
        public void TestControladorActualizarEvento()
        {
            ((Evento)evento).Nombre = "Test2";
            controlador = new M8_EventosController();
            respuesta = (Dictionary<string, object>)controlador.ActualizarEvento((Evento)evento);
            esperado.Add("dato", "Se ha modificado un evento");
            Assert.AreEqual(respuesta, esperado);
            Assert.AreEqual(((Evento)evento).Nombre,
            ((Evento)daoEvento.ConsultarPorId(evento)).Nombre);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            ((Evento)evento).Descripcion = "Test2";
            respuesta = (Dictionary<string, object>)controlador.ActualizarEvento((Evento)evento);
            esperado.Add("dato", "Se ha modificado un evento");
            Assert.AreEqual(respuesta, esperado);
            Assert.AreEqual(((Evento)evento).Descripcion,
            ((Evento)daoEvento.ConsultarPorId(evento)).Descripcion);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();


            ((Evento)evento).Foto = "/test2";
            respuesta = (Dictionary<string, object>)controlador.ActualizarEvento((Evento)evento);
            esperado.Add("dato", "Se ha modificado un evento");
            Assert.AreEqual(respuesta, esperado);
            Assert.AreEqual(((Evento)evento).Foto,
            ((Evento)daoEvento.ConsultarPorId(evento)).Foto);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            ((Evento)evento).Nombre = null;
            respuesta = (Dictionary<string, object>)controlador.ActualizarEvento((Evento)evento);
            esperado.Add("Error", "Specified cast is not valid.");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            ((Evento)evento).Nombre = "Test";
            ((Evento)evento).Descripcion = null;
            respuesta = (Dictionary<string, object>)controlador.ActualizarEvento((Evento)evento);
            esperado.Add("Error", "Specified cast is not valid.");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            ((Evento)evento).Descripcion = "Test";
            ((Evento)evento).Foto = null;
            respuesta = (Dictionary<string, object>)controlador.ActualizarEvento((Evento)evento);
            esperado.Add("Error", "Specified cast is not valid.");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            ((Evento)evento).Foto = "/Test";
            int id = ((Evento)evento).IdCategoria;
            ((Evento)evento).IdCategoria = 0;
            respuesta = (Dictionary<string, object>)controlador.ActualizarEvento((Evento)evento);
            esperado.Add("Error", "External component has thrown an exception.");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            ((Evento)evento).IdCategoria = id;
            id = ((Evento)evento).IdLocalidad;
            ((Evento)evento).IdLocalidad = 0;

            respuesta = (Dictionary<string, object>)controlador.ActualizarEvento((Evento)evento);
            esperado.Add("Error", "External component has thrown an exception.");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

            evento = FabricaEntidad.CrearEntidadEvento();
            respuesta = (Dictionary<string, object>)controlador.AgregarEvento((Evento)evento);
            esperado.Add("Error", "Specified cast is not valid.");
            Assert.AreEqual(respuesta, esperado);
            esperado = new Dictionary<string, object>();
            controlador = new M8_EventosController();

        }

        [TearDown]
        public void TearDownEvento()
        {
            daoLocalidad.Eliminar(localidad);
            daoEvento.Eliminar(evento);

            daoCategoria.Conectar();
            daoCategoria.Comando = daoCategoria.SqlConexion.CreateCommand();
            daoCategoria.Comando.CommandText = "DELETE FROM categoria where ca_id =" + categoria.Id;
            daoCategoria.Comando.ExecuteNonQuery();
            daoCategoria.Desconectar();
        }

    }
}
