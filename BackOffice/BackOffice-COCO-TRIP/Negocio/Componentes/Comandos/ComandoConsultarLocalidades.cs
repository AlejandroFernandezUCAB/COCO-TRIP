using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Models.Peticion;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoConsultarLocalidades : Comando
  {
    private IList<LocalidadEvento> listLocalidadEvento;
   
    public override void Execute()
    {
      PeticionM8_Localidad peticion = new PeticionM8_Localidad();
      JObject respuesta = peticion.GetAll();

      if (respuesta.Property("dato") != null)
      {
        listLocalidadEvento = respuesta["dato"].ToObject<List<LocalidadEvento>>();
      }

      else
      {
        listLocalidadEvento = new List<LocalidadEvento>();
      }
    }

    public override object GetResult()
    {
      return listLocalidadEvento;
    }
  }
}
