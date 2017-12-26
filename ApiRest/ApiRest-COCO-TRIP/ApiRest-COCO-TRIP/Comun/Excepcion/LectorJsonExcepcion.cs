using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
    /// <summary>
    /// Clase que encapsula la información de "JsonReaderException"
    /// Breve descripcion de cuando se genera.
    /// </summary>
    public class LectorJsonExcepcion : JsonReaderException
    {
        private JsonReaderException excepcion;
        private DateTime fechaHora;   //Hora y fecha de cuando se genero la excepción.
        private string nombreMetodo;  //Nombre del metodo donde se genero la excepción
        private string mensaje;       //Breve descripción de la excepción genereda con parametro del metodo con la que se ocasiono.

        /// <summary>
        /// Getters y Setters
        /// </summary>
        public JsonReaderException Excepcion { get => excepcion; set => excepcion = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        public string NombreMetodo { get => nombreMetodo; set => nombreMetodo = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "JsonReaderException"</param>
        public LectorJsonExcepcion(JsonReaderException excepcion) {
            this.excepcion = excepcion;
            this.fechaHora = DateTime.Now;
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "JsonReaderException"</param>
        /// <param name="mensaje">Breve mensaje referenciando como se genero la excepcion, incluir parametros del metodo</param>
        public LectorJsonExcepcion(JsonReaderException excepcion, string mensaje)
        {
            this.excepcion = excepcion;
            this.mensaje = mensaje;
            this.fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "JsonReaderException"</param>
        /// <param name="nombreMetodo">Nombre del metodo donde se genero la excepción</param>
        /// <param name="mensaje">Breve mensaje referenciando como se genero la excepcion, incluir parametros del metodo</param>
        public LectorJsonExcepcion(JsonReaderException excepcion, string nombreMetodo, string mensaje)
        {
            this.excepcion = excepcion;
            this.nombreMetodo = nombreMetodo;
            this.mensaje = mensaje;
            this.fechaHora = DateTime.Now;
        }
    }
}
