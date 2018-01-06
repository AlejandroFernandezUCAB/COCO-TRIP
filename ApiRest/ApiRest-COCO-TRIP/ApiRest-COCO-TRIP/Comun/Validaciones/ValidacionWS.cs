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
        /// Metodo que valida que no contenga parametros nulos.
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="obligatorios"></param>
        /// <exception cref="ParametrosNullExcepcion">Los parametros de la instancia no cumple con las condiciones para la informacion de una categoria.</exception>
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
        /// Metodo que valida que una instancia del tipo categoria no contenga parametros nulos.
        /// </summary>
        /// <param name="categoria">Instacia del tipo categoria.</param>
        /// <exception cref="ParametrosNullExcepcion">Los parametros de la instancia no cumple con las condiciones para la informacion de una categoria, id igual a cero.</exception>
        public static void ValidarParametrosNotNullCategoria(Entidad categoria)
        {
                if(((Categoria)categoria).Id==0)
                {
                  throw new ParametrosNullExcepcion("ID");
                }
        }

    }
}
