using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  public class ComandoConsultarLugarTuristico : Comando
  {
    private JObject respuesta;
    private Entidad lugarTuristico;
    private int idLugarTuristico;
    private DAOLugar_Turistico daoLugarTuristico;
    private ArrayList array;
    public override void Execute()
    {
      array = new ArrayList();
      daoLugarTuristico = FabricaDAO.GetDAOLugar_Turistico();
      respuesta = daoLugarTuristico.Get(idLugarTuristico);
      array.Add(respuesta);
    }

    public override ArrayList GetResult()
    {
      return array;
    }

    public override void SetPropiedad(object propiedad)
    {
      idLugarTuristico = Int32.Parse(propiedad.ToString());
    }
  }
}
