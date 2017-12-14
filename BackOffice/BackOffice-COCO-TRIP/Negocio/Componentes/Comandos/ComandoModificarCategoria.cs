using System;
using BackOffice_COCO_TRIP.Models.Peticion;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoModificarCategoria : Comando
  {
    PeticionCategoria peti = new PeticionCategoria();
    public override void Execute()
    {
    }
    public override Object GetResult()
    {
      throw new NotImplementedException();
    }
  }
}
