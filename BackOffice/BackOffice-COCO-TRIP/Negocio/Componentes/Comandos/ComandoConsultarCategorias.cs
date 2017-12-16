using BackOffice_COCO_TRIP.Models;
using BackOffice_COCO_TRIP.Models.Peticion;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;


namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoConsultarCategorias : Comando
  {
    private ArrayList resultado = new ArrayList();
    public override void Execute()
    {
      try
      {
        PeticionCategoria peticionCategoria = new PeticionCategoria();
        // DAO peticionCategoria = FabricaDAO.GetDAOCategoria();
        JObject respuesta = peticionCategoria.Get(-1);
        if (respuesta.Property("data") != null)
        {
          resultado.Add(respuesta["data"].ToObject<List<Categories>>());
          resultado.Add("Exito");
        }

        else
        {
          resultado.Add(new List<Categories>());
          resultado.Add("Ocurrio un error durante la comunicacion, revise su conexion a internet");
        }

      
      }
      catch (Exception e)
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
      throw new NotImplementedException();
    }
  }
}
