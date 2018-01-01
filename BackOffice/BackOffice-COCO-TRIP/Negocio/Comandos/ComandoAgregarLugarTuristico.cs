using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;


namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  public class ComandoAgregarLugarTuristico : Comando
  {

    private Entidad lugarTuristico = FabricaEntidad.GetLugarTuristico();
    IDAOLugar_Turistico daoLugarTuristico = FabricaDAO.GetDAOLugar_Turistico();
    private ArrayList resultado = new ArrayList();
    JObject respuesta;
    
    /// <summary>
    /// Ejecucion del comando
    /// </summary>
    public override void Execute()
    {

      try
      {
        
        respuesta = daoLugarTuristico.Post( lugarTuristico );
        lugarTuristico = respuesta.ToObject<LugarTuristico>();
        respuesta.Add(lugarTuristico);
        
      }
      catch (Exception e)
      {
        
      }

    }

    /// <summary>
    /// Para la respuesta se necesita algo para agarrar el resultado
    /// </summary>
    /// <returns></returns>
    public override ArrayList GetResult()
    {
      return resultado;
    }

    /// <summary>
    /// Metodo que asigna al lugar turistico los necesario para ejecutarse
    /// </summary>
    /// <param name="propiedad">Objeto lugar</param>
    public override void SetPropiedad(object propiedad)
    {
      lugarTuristico = (LugarTuristico)propiedad;
    }
  }
}
