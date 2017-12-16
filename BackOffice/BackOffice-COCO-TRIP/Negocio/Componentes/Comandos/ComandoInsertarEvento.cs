using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using BackOffice_COCO_TRIP.Datos.DAO;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoInsertarEvento : Comando
  {
    private Evento evento;
    private ArrayList resultado = new ArrayList();
    public override void Execute()
    {
      try {
        DAO<JObject, Evento> peticionEvento = FabricaDAO.GetDAOEvento();
        JObject respuesta = peticionEvento.Post(evento);
      if (respuesta.Property("dato") == null)
      {
        resultado.Add( "Ocurrio un error durante la comunicacion, revise su conexion a internet");
      }
      else
      {
        resultado.Add("Se hizo con exito");
      }
    }
      catch (NullReferenceException e)
      {
        //TERMINAR
        throw e;
      }
}

    public override ArrayList GetResult()
    {
      return resultado;
    }

    public override void SetPropiedad(object propiedad)
    {
      evento = (Evento)propiedad;
    }
  }
}
