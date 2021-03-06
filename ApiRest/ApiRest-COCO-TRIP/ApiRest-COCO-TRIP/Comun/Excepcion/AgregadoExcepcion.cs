using System;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
    /// <summary>
    /// Clase que encapsula la información de "AggregateException"
    /// representa uno o mas errores que se producen durante la ejecucion del programa.
    /// </summary>
    public class AgregadoExcepcion: AggregateException
    {
        private AggregateException excepcion;
        private DateTime fechaHora;   //Hora y fecha de cuando se genero la excepción.
        private string mensaje;       //Breve descripción de la excepción genereda con parametro del metodo con la que se ocasiono.

        /// <summary>
        /// Getters y Setters del atributo "excepcion".
        /// </summary>
        public AggregateException Excepcion { get => excepcion; set => excepcion = value; }

        /// <summary>
        /// Getters y Setters del atributo "fechaHora".
        /// </summary>
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }

        /// <summary>
        /// Getters y Setters del atributo "mensaje".
        /// </summary>
        public string Mensaje { get => mensaje; set => mensaje = value; }

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "AggregateException"</param>
        public AgregadoExcepcion(AggregateException excepcion) {
            this.excepcion = excepcion;
            this.fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "AggregateException"</param>
        /// <param name="mensaje">Breve mensaje referenciando como se genero la excepcion, incluir parametros del metodo</param>
        public AgregadoExcepcion(AggregateException excepcion, string mensaje)
        {
            this.excepcion = excepcion;
            this.mensaje = mensaje;
            fechaHora = DateTime.Now;
        }
    }
}
