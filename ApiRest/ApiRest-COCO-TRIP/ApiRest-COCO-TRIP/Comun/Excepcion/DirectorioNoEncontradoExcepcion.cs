using System;
using System.IO;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
    /// <summary>
    /// Excepcion logica que encapsula la información de "DirectoryNotFoundException"
    /// Se produce cuando no se encuentra parte de un archivo o directorio.
    /// </summary>
    public class DirectorioNoEncontradoExcepcion: DirectoryNotFoundException
    {
        private DirectoryNotFoundException excepcion;
        private DateTime fechaHora;   //Hora y fecha de cuando se genero la excepción.
        private string mensaje;       //Breve descripción de la excepción genereda con parametro del metodo con la que se ocasiono.

        /// <summary>
        /// Getters y Setters del atributo "excepcion".
        /// </summary>
        public DirectoryNotFoundException Excepcion { get => excepcion; set => excepcion = value; }

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
        /// <param name="excepcion">Excepción generada del tipo "DirectoryNotFoundException".</param>
        public DirectorioNoEncontradoExcepcion(DirectoryNotFoundException excepcion) {
            this.excepcion = excepcion;
            this.fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Metodo Constructor
        /// </summary>
        /// <param name="excepcion">Excepción generada del tipo "DirectoryNotFoundException"</param>
        /// <param name="mensaje">Breve mensaje referenciando como se genero la excepcion, incluir parametros del metodo</param>
        public DirectorioNoEncontradoExcepcion(DirectoryNotFoundException excepcion, string mensaje)
        {
            this.excepcion = excepcion;
            this.mensaje = mensaje;
            this.fechaHora = DateTime.Now;
        }
    }
}
