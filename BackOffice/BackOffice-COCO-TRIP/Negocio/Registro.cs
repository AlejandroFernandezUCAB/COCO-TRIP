using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace BackOffice_COCO_TRIP.Negocio
{
  public class Registro
  {
    public static String ApiRestBaseUri
    {
      get { return ConfigurationManager.AppSettings["ApiRestBaseUri"]; }
    }

  }
}
