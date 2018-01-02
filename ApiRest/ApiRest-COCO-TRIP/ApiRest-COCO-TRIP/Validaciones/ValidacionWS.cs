using System.Collections.Generic;
using ApiRest_COCO_TRIP.Models.Excepcion;
//using ApiRest_COCO_TRIP.Comun.Excepcion;
using Newtonsoft.Json.Linq;

/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///     Clase destinada a las validaciones necesarias en el programa.
/// </summary>
namespace ApiRest_COCO_TRIP.Validaciones
{
    public class ValidacionWS
    {
        /// <summary>
        /// Metodo que verifica si cumple con no tener parametros nulos.
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="obligatorios"></param>
        /// <exception cref="ParametrosNullException"></exception>
        public static void ValidarParametrosNotNull(JObject parametros, IList<string> obligatorios)
        {
            foreach (string item in obligatorios)
            {
                if (parametros.Property(item) == null || (string)parametros[item] == null)
                {
                    //TODO: Cambiar a la que esta en Comun, no tengo idea de que mensaje es ese.
                    throw new ParametrosNullException(item);
                }
            }
        }

    }
}
