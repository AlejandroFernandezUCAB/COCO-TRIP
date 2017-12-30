using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoConsultarCategorias : Comando
  {
    private ArrayList resultado = new ArrayList();
    IDAOCategoria dao = FabricaDAO.GetDAOCategoria();
    public override void Execute()
    {
      try
      {       
        JObject respuesta = dao.Get(-1);
        if (respuesta.Property("data") != null)
        {
          resultado.Add(respuesta["data"].ToObject<List<Categoria>>());
          resultado.Add("Exito");
        }
        else
        {
          resultado.Add(new List<Categoria>());
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
