using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.Entity;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
namespace ApiRest_COCO_TRIP.Comun.Validaciones
{
    public class ValidacionWS
    {

        public static void ValidarParametrosNotNull(JObject parametros, IList<string> obligatorios)
        {
            foreach (string item in obligatorios)
            {
                if (parametros.Property(item) == null || (string)parametros[item] == null)
                {
                    throw new ParametrosNullExcepcion(item);
                }
            }
        }

        public static void ValidarParametrosNotNullCategoria(Entidad categoria)
        {
                if(((Categoria)categoria).Id==0)
                {
                  throw new ParametrosNullExcepcion("ID");
                }
        }

    }
}
