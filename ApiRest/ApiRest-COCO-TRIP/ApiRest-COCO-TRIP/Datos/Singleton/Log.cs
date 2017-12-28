using ApiRest_COCO_TRIP.Comun.Excepcion;
using log4net;
using System;
using System.IO;
using System.Reflection;

namespace ApiRest_COCO_TRIP.Datos.Singleton
{
    /// <summary>
    /// Singleton del LOG
    /// </summary>
    public class Log
    {
        private static Log instancia;
        private static ILog log;

        /// <summary>
        /// Constructor
        /// </summary>
        private Log()
        {
            log = log4net.LogManager.GetLogger(typeof(Log));
        }

        /// <summary>
        /// Retorna la instancia del Singleton
        /// </summary>
        /// <returns>Correo</returns>
        public static Log ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new Log();
            }

            return instancia;
        }

        /// <summary>
        /// Escribe un error en el log
        /// </summary>
        /// <param name="mensaje">Error a escribir</param>
        public void ApiRestError(String instancia,String mensaje)
        {
            try
            {
                log.Error(instancia + ": " + mensaje);
            }
            catch (IOException e)
            {
                throw new IOExcepcion(e, "Error escribiendo en el Log");
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentoNuloExcepcion(e, "Argumento nulo en LogError" + e.Message);
            }
        }

        /// <summary>
        /// Escribe informacion en el log
        /// </summary>
        /// <param name="mensaje">Mensaje a escribir</param>
        public void ApiRestInfo(String instancia, String mensaje)
        {
            try
            {
                log.Info(instancia+": "+mensaje);
            }
            catch (IOException e)
            {
                throw new IOExcepcion(e, "Error escribiendo en el Log");
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentoNuloExcepcion(e, "Argumento nulo en LogError" + e.Message);
            }
        }

    }

}
