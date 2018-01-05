
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  public class ComandoConsultarCategoriaPorId:Comando
  {
    private int Id;
    private ArrayList resultado = new ArrayList();
    IDAOCategoria dao = FabricaDAO.GetDAOCategoria();

    public override void Execute()
    {
      try
      {
        JObject respuesta = dao.GetPorId(Id);
        resultado.Add(respuesta);
      }
      catch (Exception e)
      {
        throw e;
      }


    }
    public override ArrayList GetResult()
    {
      return resultado;
    }


    public override void SetPropiedad(object propiedad)
    {
      Id = (int)propiedad;
    }
  }
}
