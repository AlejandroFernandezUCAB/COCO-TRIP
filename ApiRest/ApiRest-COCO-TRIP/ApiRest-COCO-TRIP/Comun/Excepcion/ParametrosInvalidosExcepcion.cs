using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
    public class ParametrosInvalidosExcepcion : ArgumentException
    {
        private ParametrosInvalidosExcepcion excepcion;
        private DateTime fechaHora;
        private string nombreMetodo; //Metodos que atrapan la excepcion antes de manejarla
        private string parametro; //Datos asociados a la excepcion generada
        private string mensaje; //Mensaje asociado al error

        /// <summary>
        /// Getters y Setters del atributo Excepcion
        /// </summary>
        public ParametrosInvalidosExcepcion Excepcion
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
        public string NombreMetodos
        {
            get { return nombreMetodo; }
            set { nombreMetodo = value; }
        }

        /// <summary>
        /// Getters y Setters del atributo DatosAsociados
        /// </summary>
        public string Parametro
        {
            get { return parametro; }
            set { parametro = value; }
        }


        /// <summary>
        /// Getters y setters del atributo Mensaje
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
        public ParametrosInvalidosExcepcion(ParametrosInvalidosExcepcion e)
        {
            excepcion = e;
            fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_parametro">Parametro que no cumple las reglas de validacion</param>
        /// <param name="_mensaje">Mensaje asociado al error</param>
        public ParametrosInvalidosExcepcion(string _parametro, string _mensaje)
        {
            parametro = _parametro;
            mensaje = _mensaje;
            fechaHora = DateTime.Now;
        }
    }
}