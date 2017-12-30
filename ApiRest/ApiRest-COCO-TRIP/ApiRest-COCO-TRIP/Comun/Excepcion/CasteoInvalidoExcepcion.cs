using System;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
    /// <summary>
    /// Excepcion logica que se produce por un casteo invalido entre un tipo de dato del lenguaje C Sharp y
    /// un tipo de dato de la base de datos.
    /// Es bastante comun cuando se pasa por parametro un valor nulo
    /// </summary>
    public class CasteoInvalidoExcepcion : InvalidCastException
    {
        private InvalidCastException excepcion;
        private DateTime fechaHora;
        private string nombreMetodo; //Metodos que atrapan la excepcion antes de manejarla
        private string datoAsociado; //Datos asociados a la excepcion generada
        private string mensaje; //Mensaje asociado al error

        /// <summary>
        /// Getters y Setters del atributo Excepcion
        /// </summary>
        public InvalidCastException Excepcion
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
        public string DatosAsociados
        {
            get { return datoAsociado; }
            set { datoAsociado = value; }
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
        public CasteoInvalidoExcepcion(InvalidCastException e)
        {
            excepcion = e;

            fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="e">Excepcion generica</param>
        /// <param name="_mensaje">Mensaje asociado al error</param>
        public CasteoInvalidoExcepcion(InvalidCastException e, string _mensaje)
        {
            excepcion = e;
            mensaje = _mensaje;

            fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Metodo Constructor
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "InvalidCastException"</param>
        /// <param name="nombreMetodo">Nombre del metodo donde se genero la excepción</param>
        /// <param name="mensaje">Breve mensaje referenciando como se genero la excepcion, incluir parametros del metodo</param>
        public CasteoInvalidoExcepcion(InvalidCastException excepcion, string nombreMetodo, string mensaje)
        {
            this.excepcion = excepcion;
            this.nombreMetodo = nombreMetodo;
            this.mensaje = mensaje;
            fechaHora = DateTime.Now;
        }
    }
}
