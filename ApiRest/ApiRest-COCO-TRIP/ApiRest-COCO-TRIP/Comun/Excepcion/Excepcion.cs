﻿using System;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
    /// <summary>
    /// Excepcion logica que encapsula la información de "Exception"
    /// representa los errores que se producen durante la ejecucion del programa.
    /// </summary>
    public class Excepcion: Exception
    {
        private Exception _excepcion;
        private DateTime fechaHora;   //Hora y fecha de cuando se genero la excepción.
        private string mensaje;       //Breve descripción de la excepción genereda con parametro del metodo con la que se ocasiono.

        /// <summary>
        /// Getters y Setters del atributo "_excepcion".
        /// </summary>
        public Exception _Excepcion { get => _excepcion; set => _excepcion = value; }

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
        /// <param name="_excepcion">Excepción generada del tipo "Exception"</param>
        public Excepcion(Exception _excepcion)
        {
            this._excepcion = _excepcion;
            fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Metodo Constructor
        /// </summary>
        /// <param name="_excepcion">Excepción generada del tipo "Exception"</param>
        /// <param name="mensaje">Breve mensaje referenciando como se genero la excepcion, incluir parametros del metodo</param>
        public Excepcion(Exception _excepcion, string mensaje)
        {
            this._excepcion = _excepcion;
            this.mensaje = mensaje;
            fechaHora = DateTime.Now;
        }
    }
}