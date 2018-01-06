using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using System;
using System.Collections;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  public class ComandoActualizarEstadoLugarTuristico : Comando
  {
    private ArrayList objetos = new ArrayList();
    private Entidad lugarTuristico = FabricaEntidad.GetLugarTuristico();
    private DAOLugar_Turistico dao = FabricaDAO.GetDAOLugar_Turistico();

    public override void Execute()
    {

      lugarTuristico.Id = Int32.Parse(objetos[0].ToString());
      ((LugarTuristico)lugarTuristico).Activar = bool.Parse(objetos[1].ToString());
      dao.Put(lugarTuristico);

    }

    public override ArrayList GetResult()
    {
      throw new NotImplementedException();
    }

    public override void SetPropiedad(object propiedad)
    {
      objetos.Add(propiedad);
    }
  }
}
