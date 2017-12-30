using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
  public class ParametrosNullException : Exception
  {

    private string parametroNull;

    public string ParametroNull
    {
      get { return parametroNull; }
      set { parametroNull = value; }
    }

    private string mensaje;

    public string Mensaje
    {
      get { return mensaje; }
      set { mensaje = value; }
    }


    public ParametrosNullException(string parametro)
    {
      parametroNull = parametro;
      mensaje = $"Falta el parametro {parametro} o este es nulo";
    }


  }
}
