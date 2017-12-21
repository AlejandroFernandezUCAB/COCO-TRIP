using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoConsultarLocalidades : Comando
  {
    private ArrayList resultado = new ArrayList();
   
    public override void Execute()
    {
      IDAOLocalidad peticion = FabricaDAO.GetDAOLocalidad();
      JObject respuesta = peticion.GetAll();

      if (respuesta.Property("dato") != null)
      {
        resultado.Add(respuesta["dato"].ToObject<List<Localidad>>());
      }

      else
      {
        resultado.Add(new List<Localidad>());
      }
    }

    public override ArrayList GetResult()
    {
      return resultado;
    }

    public override void SetPropiedad(object propiedad)
    {
      throw new NotImplementedException();
    }
  }
}
