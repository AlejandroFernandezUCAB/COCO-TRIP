using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
    /// <summary>
    /// Clase que encapsula la información de "Exception"
    /// Breve descripcion de cuando se genera.
    /// </summary>
    public class Excepcion : Exception
    {
        private Exception excepcion;
        private DateTime fechaHora;   //Hora y fecha de cuando se genero la excepción.
        private string nombreMetodo;  //Nombre del metodo donde se genero la excepción
        private string mensaje;       //Breve descripción de la excepción genereda con parametro del metodo con la que se ocasiono.

        /// <summary>
        /// Getters y Setters
        /// </summary>
        public Exception Excepcion { get => excepcion; set => excepcion = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        public string NombreMetodo { get => nombreMetodo; set => nombreMetodo = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "Exception"</param>
        public Excepcion(Exception excepcion) {
            this.excepcion = excepcion;
            this.fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "Exception"</param>
        /// <param name="nombreMetodo">Nombre del metodo donde se genero la excepción</param>
        public Excepcion(Exception excepcion, string nombreMetodo)
        {
            this.excepcion = excepcion;
            this.nombreMetodo = nombreMetodo;
            this.fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "Exception"</param>
        /// <param name="mensaje">Breve mensaje referenciando como se genero la excepcion, incluir parametros del metodo</param>
        public Excepcion(Exception excepcion, string mensaje)
        {
            this.excepcion = excepcion;
            this.mensaje = mensaje;
            this.fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "Exception"</param>
        /// <param name="nombreMetodo">Nombre del metodo donde se genero la excepción</param>
        /// <param name="mensaje">Breve mensaje referenciando como se genero la excepcion, incluir parametros del metodo</param>
        public Excepcion(Exception excepcion, string nombreMetodo, string mensaje)
        {
            this.excepcion = excepcion;
            this.nombreMetodo = nombreMetodo;
            this.mensaje = mensaje;
            this.fechaHora = DateTime.Now;
        }
    }
}
