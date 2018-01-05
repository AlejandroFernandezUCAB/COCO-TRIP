using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.Entity;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ApiRest_COCO_TRIP.Comun.Validaciones
{
    /// <summary>
    /// Clase destinada a validaciones necesarias en el programa.
    /// </summary>
    public class ValidacionWS
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="obligatorios"></param>
        /// <exception cref = "ParametrosInvalidosExcepcion">Los parametros de la instancia no cumple con las condiciones.</exception>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <exception cref="ParametrosInvalidosExcepcion">Los parametros de la instancia no cumple con las condiciones para la informacion de una categoria</exception>
        public static void ValidarParametrosNotNullCategoria(Entidad categoria)
        {
                if(((Categoria)categoria).Id==0)
                {
                  throw new ParametrosNullExcepcion("ID");
                }
        }

    }
}
