using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
  public class HijoConDePendenciaException : Exception
  {

    

    private string mensaje;

    public string Mensaje
    {
      get { return mensaje; }
      set { mensaje = value; }
    }


    public HijoConDePendenciaException(string parametro)
    {
      mensaje = $"Falta el hijo {parametro} posee dependencia";
    }


  }
}
