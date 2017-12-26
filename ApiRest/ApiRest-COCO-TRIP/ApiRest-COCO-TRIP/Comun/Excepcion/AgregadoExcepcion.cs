using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
    /// <summary>
    /// Clase que encapsula la información de "AggregateException"
    /// Breve descripcion de cuando se genera.
    /// </summary>
    public class AgregadoExcepcion: AggregateException
    {
        private AggregateException excepcion;
        private DateTime fechaHora;   //Hora y fecha de cuando se genero la excepción.
        private string nombreMetodo;  //Nombre del metodo donde se genero la excepción
        private string mensaje;       //Breve descripción de la excepción genereda con parametro del metodo con la que se ocasiono.

        /// <summary>
        /// Getters y Setters
        /// </summary>
        public SocketException Excepcion { get => excepcion; set => excepcion = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        public string NombreMetodo { get => nombreMetodo; set => nombreMetodo = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "AggregateException"</param>
        public AgregadoExcepcion(AggregateException excepcion) {
            this.excepcion = excepcion;
            this.fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "AggregateException"</param>
        /// <param name="nombreMetodo">Nombre del metodo donde se genero la excepción</param>
        public AgregadoExcepcion(AggregateException excepcion, string nombreMetodo)
        {
            this.excepcion = excepcion;
            this.nombreMetodo = nombreMetodo;
            this.fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "AggregateException"</param>
        /// <param name="mensaje">Breve mensaje referenciando como se genero la excepcion, incluir parametros del metodo</param>
        public AgregadoExcepcion(AggregateException excepcion, string mensaje)
        {
            this.excepcion = excepcion;
            this.mensaje = mensaje;
            this.fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "AggregateException"</param>
        /// <param name="nombreMetodo">Nombre del metodo donde se genero la excepción</param>
        /// <param name="mensaje">Breve mensaje referenciando como se genero la excepcion, incluir parametros del metodo</param>
        public AgregadoExcepcion(AggregateException excepcion, string nombreMetodo, string mensaje)
        {
            this.excepcion = excepcion;
            this.nombreMetodo = nombreMetodo;
            this.mensaje = mensaje;
            this.fechaHora = DateTime.Now;
        }
    }
}
