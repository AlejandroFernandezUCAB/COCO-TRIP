using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using System;
using System.Collections;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  public class ComandoConsultarLugarTuristico : Comando
  {
    private Entidad lugarTuristico;
    private int idLugarTuristico;
    private DAOLugar_Turistico daoLugarTuristico;

    public override void Execute()
    {
      daoLugarTuristico = FabricaDAO.GetDAOLugar_Turistico();
			lugarTuristico = daoLugarTuristico.Get(idLugarTuristico);
    }

    public override ArrayList GetResult()
    {
      throw new NotImplementedException();
    }

    public override void SetPropiedad(object propiedad)
    {
      idLugarTuristico = Int32.Parse(propiedad.ToString());
    }
  }
}
