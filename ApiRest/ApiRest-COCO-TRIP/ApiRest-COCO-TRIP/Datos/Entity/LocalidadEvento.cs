using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Datos.Entity
{

    /// <summary>
    /// Clase de la entidad localidadEvento.
    /// </summary>
    public class LocalidadEvento : Entidad
    {
        private string nombre;  // nombre de la localidad
        private string descripcion; // descripcion de la localidad
        private string coordenadas; //coordenadas de la localidad

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get => nombre; set => nombre = value; }
        [JsonProperty(PropertyName = "descripcion")]
        public string Descripcion { get => descripcion; set => descripcion = value; }
        [JsonProperty(PropertyName = "coordenadas")]
        public string Coordenadas { get => coordenadas; set => coordenadas = value; }

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        /// <param name="id">Identificador único de la localidad</param>
        /// <param name="nombre">Nombre de la localidad</param>
        /// <param name="descripcion">Descripcion de la localidad</param>
        /// <param name="coordenadas">Coordenadas de la localidad</param>
        public LocalidadEvento(int id, string nombre, string descripcion, string coordenadas)
        {
            this.Id = id;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.coordenadas = coordenadas;
        }

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        /// <param name="nombre">Nombre de la localidad</param>
        /// <param name="descripcion">Descripcion de la localidad</param>
        /// <param name="coordenadas">Coordenadas de la localidad</param>
        public LocalidadEvento(string nombre, string descripcion, string coordenadas)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.coordenadas = coordenadas;

        }
        public LocalidadEvento() {

        }

    }
}
