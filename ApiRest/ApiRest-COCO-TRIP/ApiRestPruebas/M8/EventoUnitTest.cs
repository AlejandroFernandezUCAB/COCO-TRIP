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
    public void SetUp()
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
    public void prueba() {
    }
  
    [TearDown]
    public void TearDown()
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
