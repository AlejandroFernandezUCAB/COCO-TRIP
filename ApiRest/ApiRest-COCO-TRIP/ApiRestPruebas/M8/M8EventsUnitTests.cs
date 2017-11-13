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
    [OneTimeSetUp]
    public void init()
    {
      controlador = new M8_EventosController();
    }

    [Test]
    public void PruebaConsultarEventoPorID()
    {
      
      string nombre = "Tomorrowland";
      Evento evento = new Evento();
      evento.Nombre = nombre;
      evento.Descripcion = "Evento de Tecno";
      evento.Precio = 200;
      evento.FechaInicio.AddDays(18);
      evento.FechaInicio.AddMonths(11);
      evento.FechaInicio.AddYears(2017);
      evento.FechaFin.AddDays(17);
      evento.FechaFin.AddMonths(12);
      evento.FechaFin.AddYears(2017);
      evento.HoraInicio.AddHours(7);
      evento.HoraFin.AddHours(8);
      evento.Foto = "link prueba";
      evento.IdLocalidad = 1;
      evento.IdCategoria = 1;
      int id = controlador.AgregarEvento(evento);
      Assert.AreEqual(controlador.ConsultarEvento(id).Nombre, nombre);
    }
  }
}
