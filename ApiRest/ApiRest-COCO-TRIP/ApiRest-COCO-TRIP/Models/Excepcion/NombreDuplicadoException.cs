using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Models.Excepcion
{
  public class NombreDuplicadoException : Exception
  {

    

    private string mensaje;

    public string Mensaje
    {
      get { return mensaje; }
      set { mensaje = value; }
    }


    public NombreDuplicadoException(string parametro)
    {
      mensaje = $" {parametro} ";
    }


  }
}
