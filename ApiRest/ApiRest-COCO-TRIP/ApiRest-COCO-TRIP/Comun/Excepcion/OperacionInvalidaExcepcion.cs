using System;
using System.Collections.Generic;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
    /// <summary>
    /// Clase que encapsula la informaci�n de "InvalidOperationException"
    /// se genera cuando no se pudo realizar la operacion debido a que se viola una regla en la base de datos.
    /// </summary>
    public class OperacionInvalidaExcepcion : InvalidOperationException
    {
        private InvalidOperationException excepcion;
        private DateTime fechaHora;
        private List<String> nombreMetodos; //Enlista los metodos que atrapan la excepcion antes de manejarla
        private string datosAsociados; //Datos asociados a la excepcion generada
        private string mensaje; //Mensaje asociado al error

        /// <summary>
        /// Getters y Setters del atributo NombreMetodos
        /// </summary>
        public InvalidOperationException Excepcion
        {
            get { return excepcion; }
            set { excepcion = value; }
        }

        /// <summary>
        /// Getters y Setters del atributo FechaHora
        /// </summary>
        public DateTime FechaHora
        {
            get { return fechaHora; }
            set { fechaHora = value; }
        }

        /// <summary>
        /// Getters y Setters del atributo NombreMetodos
        /// </summary>
        public List<String> NombreMetodos
        {
            get { return nombreMetodos; }
            set { nombreMetodos = value; }
        }

        /// <summary>
        /// Getters y Setters del atributo DatosAsociados
        /// </summary>
        public string DatosAsociados
        {
            get { return datosAsociados; }
            set { datosAsociados = value; }
        }


        /// <summary>
        /// Getters y setters del atributo mensaje
        /// </summary>
        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }

        /// <summary>
        /// Constructor que recibe la excepcion, instacia los metodos y, registra la hora y fecha de la incidencia
        /// </summary>
        /// <param name="e">Excepcion de la base de datos</param>
        public OperacionInvalidaExcepcion(InvalidOperationException e)
        {
            excepcion = e;

            fechaHora = DateTime.Now;
            nombreMetodos = new List<String>();
        }
    }
}
