using System;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
    /// <summary>
    /// Clase que encapsula la información de "JsonReaderException"
    /// se genera cuando ocurre un error leyendo el Json.
    /// </summary>
    public class NombreDuplicadoException : Exception
    {
        private Exception excepcion;
        private DateTime fechaHora;   //Hora y fecha de cuando se genero la excepción.
        private string nombreMetodo;  //Nombre del metodo donde se genero la excepción
        private string mensaje;       //Breve descripción de la excepción genereda con parametro del metodo con la que se ocasiono.

        /// <summary>
        /// Getters y Setters del atributo "excepcion".
        /// </summary>
        public Exception Excepcion { get => excepcion; set => excepcion = value; }

        /// <summary>
        /// Getters y Setters del atributo "fechaHora". 
        /// </summary>
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }

        /// <summary>
        /// Getters y Setters del atributo "nombreMetodo".
        /// </summary>
        public string NombreMetodo { get => nombreMetodo; set => nombreMetodo = value; }

        /// <summary>
        /// Getters y Setters del atributo "mensaje".
        /// </summary>
        public string Mensaje { get => mensaje; set => mensaje = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "Exception"</param>
        public NombreDuplicadoException(Exception excepcion)
        {
            this.excepcion = excepcion;
            fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "Exception"</param>
        /// <param name="nombreMetodo">Nombre del metodo donde se genero la excepción</param>
        /// <param name="mensaje">Breve mensaje referenciando como se genero la excepcion, incluir parametros del metodo</param>
        public NombreDuplicadoException(Exception excepcion, string nombreMetodo, string mensaje)
        {
            this.excepcion = excepcion;
            this.nombreMetodo = nombreMetodo;
            this.mensaje = mensaje;
            fechaHora = DateTime.Now;
        }

        public NombreDuplicadoException(string parametro)
        {
            mensaje = $" {parametro} ";
        } 
    }
}
