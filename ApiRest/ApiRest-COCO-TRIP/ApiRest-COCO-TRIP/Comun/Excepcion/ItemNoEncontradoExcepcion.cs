using System;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
    /// <summary>
    /// Excepcion logica que encapsula la informaci�n de "Exception"
    /// ocurre cuando no se encuentra un Item buscado.
    /// </summary>
    public class ItemNoEncontradoExcepcion : Exception
    {
        private Exception excepcion;
        private DateTime fechaHora;   //Hora y fecha de cuando se genero la excepci�n.
        private string mensaje;       //Breve descripci�n de la excepci�n genereda con parametro del metodo con la que se ocasiono.

        /// <summary>
        /// Getters y Setters del atributo "excepcion".
        /// </summary>
        public Exception Excepcion { get => excepcion; set => excepcion = value; }

        /// <summary>
        /// Getters y Setters del atributo "fechaHora". 
        /// </summary>
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }

        /// <summary>
        /// Getters y Setters del atributo "mensaje".
        /// </summary>
        public string Mensaje { get => mensaje; set => mensaje = value; }

        /// <summary>
        /// Metodo Constructor
        /// </summary>
        /// <param name="_excepcion">Excepci�n generada del tipo "Exception"</param>
        public ItemNoEncontradoExcepcion(Exception excepcion)
        {
            this.excepcion = excepcion;
            fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Metodo Constructor
        /// </summary>
        /// <param name="_excepcion">Excepci�n generada del tipo "Exception"</param>
        /// <param name="mensaje">Breve mensaje referenciando como se genero la excepcion, incluir parametros del metodo</param>
        public ItemNoEncontradoExcepcion(Exception excepcion, string mensaje)
        {
            this.excepcion = excepcion;
            this.mensaje = mensaje;
            fechaHora = DateTime.Now;
        }

        public ItemNoEncontradoExcepcion(string mensajeError)
        {
            mensaje = mensajeError;
        }
    }
}
