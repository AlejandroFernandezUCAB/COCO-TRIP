using ApiRest_COCO_TRIP.Controllers;
using ApiRest_COCO_TRIP.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestPruebas.M8
{
  [TestFixture]
  class M8EventsUnitTests
  {
    private M8_EventosController controlador;
    private PeticionEvento peticion;
    [SetUp]
    public void init()
    {
      controlador = new M8_EventosController();
      peticion = new PeticionEvento();
    }

    [Test]
    public void PruebaConsultarEventoPorID()
    {
      
      string nombre = "prueba 222";
      Evento evento = new Evento();
      evento.Nombre = nombre;
      evento.Descripcion = "Evento de Tecno";
      evento.Precio = 200;
      evento.FechaInicio = new DateTime(2017, 12,21);
      evento.FechaFin = new DateTime(2017, 12,21);
      evento.HoraInicio.AddHours(7);
      evento.HoraFin.AddHours(8);
      evento.Foto = "link prueba";
      evento.IdLocalidad = 1;
      evento.IdCategoria = 1;
      int id = peticion.AgregarEvento(evento);
      peticion = new PeticionEvento();
      Assert.AreEqual(peticion.ConsultarEvento(id).Nombre, nombre);
    }

    [Test]
    public void PruebaConsultarEventosPorFecha()
    {
      DateTime date = new DateTime(2017,1,20);
      Assert.AreEqual(3,peticion.ListaEventosPorFecha(date).Count);
    }
    [Test]
    public void PruebaConsultarEventoPorCategoria()
    {
      int idCategoria = 1;
      Assert.AreEqual(peticion.ListaEventosPorCategoria(idCategoria).Count(), 3);
    }

    [Test]
    public void PruebaConsultarEventoPorCategoriaSinEventos()
    {
      int idCategoria = 3;
      Assert.AreEqual(peticion.ListaEventosPorCategoria(idCategoria).Count(), 0);
    }
    [Test]
    public void PruebaConsultarEventoPorCategoriaNombre()
    {
      string nombreCategoria = "blabla";
      Assert.AreEqual(peticion.ListaEventosPorCategoriaNombre(nombreCategoria).Count(),3 );
    }
    [Test]
    public void PruebaConsultarEventosTotal()
    {
      Assert.AreEqual(peticion.ListaEventos().Count(), 4);
    }
    [Test]
    public void PruebaConsultarEventoNombre()
    {
      string nombreEvento = "prueba 1";
      Assert.AreEqual(peticion.ConsultarEventoNombre(nombreEvento).Id, 3);
    }
    /*    [Test]
        public void PruebaEliminarEventoNombre()
        {
          string nombreEvento = "prueba 1";

        }*/

    /*    [Test]
    public void PruebaEliminarEventoId()
    {
      int id = 1;
      
    }*/

  }
}
