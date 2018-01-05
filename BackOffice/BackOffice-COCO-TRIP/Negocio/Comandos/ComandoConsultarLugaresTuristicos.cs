using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;


namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  public class ComandoConsultarLugaresTuristicos : Comando
  {

    private IDAOLugar_Turistico dao = FabricaDAO.GetDAOLugar_Turistico();
    private ArrayList lista = new ArrayList();
    JObject respuesta;

    public override void Execute()
    {
      respuesta = dao.GetAll();
      lista.Add(respuesta);
    }

    public override ArrayList GetResult()
    {
      return lista;
    }

    public override void SetPropiedad(object propiedad)
    {
      throw new NotImplementedException();
    }
  }
}
