using ApiRest_COCO_TRIP.Models.Excepcion;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Validaciones
{
    public class ValidacionWS
    {

        public static void validarParametrosNotNull(JObject parametros, IList<string> obligatorios)
        {
            foreach (string item in obligatorios)
            {
                if (parametros.Property(item) == null || (string)parametros[item] == null)
                {
                    throw new ParametrosNullException(item);
                }
            }
        }

    }
}
