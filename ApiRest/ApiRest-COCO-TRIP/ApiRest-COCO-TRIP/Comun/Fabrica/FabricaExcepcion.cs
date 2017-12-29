using System;
using ApiRest_COCO_TRIP.Comun.Excepcion;

namespace ApiRest_COCO_TRIP.Comun.Fabrica
{
    /// <summary>
    /// Fabrica que instancia las Excepciones.
    /// </summary>
    public static class FabricaExcepcion
    {
        public static AgregadoExcepcion CrearAgregadoExcepcion(AggregateException excepcion, string nombreMetodo, string mensaje)
        {
            return new AgregadoExcepcion(excepcion, nombreMetodo, mensaje);
        }

    }
}
