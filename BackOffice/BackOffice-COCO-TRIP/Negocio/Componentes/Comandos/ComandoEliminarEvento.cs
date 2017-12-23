using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoEliminarEvento : Comando
  {

    private int id;
    private ArrayList resultado = new ArrayList();
    public override void Execute()
    {
      IDAOEvento peticion = FabricaDAO.GetDAOEvento();
      JObject respuesta = peticion.Delete(id);
      if (respuesta.Property("dato") == null)
      {
        resultado.Add("Ocurrio un error durante la comunicacion, revise su conexion a internet");
      }
      else
      {
        resultado.Add("Se hizo con exito");
      }
    }

    public override ArrayList GetResult()
    {
      return resultado;
    }

    public override void SetPropiedad(object propiedad)
    {
      this.id = (int)propiedad;
    }
  }
}
