using System;

namespace ApiRest_COCO_TRIP.Datos.Entity
{
    /// <summary>
    /// Clase de la entidad Evento
    /// </summary>
    public class Evento : Entidad
    {

        private string nombre; // Nombre del evento
        private string descripcion; // Descripción del evento
        private double precio; // Precio del evento
        private DateTime fechaInicio; // Fecha en la que el evento comienza
        private DateTime fechaFin; // Fecha en la que el evento termina
        private DateTime horaInicio; // Hora de inicio del evento
        private DateTime horaFin; // Hora donde culmina el evento
        private string foto; // Ubicacion de la foto del evento
        private int idLocalidad; // Id de la localidad asociada al evento
        private int idCategoria; // Id de la categoria asociada al evento

        // Getters y Setters de la clase Evento
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public double Precio { get => precio; set => precio = value; }
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
        public DateTime HoraInicio { get => horaInicio; set => horaInicio = value; }
        public DateTime HoraFin { get => horaFin; set => horaFin = value; }
        public string Foto { get => foto; set => foto = value; }
        public int IdLocalidad { get => idLocalidad; set => idLocalidad = value; }
        public int IdCategoria { get => idCategoria; set => idCategoria = value; }

        /// <summary>
        /// Constructor de la clase Evento.
        /// </summary>
        public Evento()
        {
            this.Id = 0;
            this.nombre = null;
            this.descripcion = null;
            this.precio = 0;
            this.fechaInicio = new DateTime();
            this.fechaFin = new DateTime();
            this.horaInicio = new DateTime();
            this.horaFin = new DateTime();
            this.foto = null;
            this.idLocalidad = 0;
            this.idCategoria = 0;
        }

        /// <summary>
        /// Constructor de la clase Evento.
        /// </summary>
        /// <param name="id">Identificador único del evento.</param>
        /// <param name="nombre">Nombre del evento.</param>
        public Evento(int id, string nombre)
        {
            this.Id = id;
            this.nombre = nombre;
            this.descripcion = null;
            this.precio = 0;
            this.fechaInicio = new DateTime();
            this.fechaFin = new DateTime();
            this.horaInicio = new DateTime();
            this.horaFin = new DateTime();
            this.foto = null;
            this.idLocalidad = 0;
            this.idCategoria = 0;
        }

        /// <summary>
        /// Constructor de la clase evento.
        /// </summary>
        /// <param name="id">Identificador unico del evento.</param>
        /// <param name="nombre">Nombre del evento.</param>
        /// <param name="descripcion">Descripcion del evento.</param>
        /// <param name="precio">Precio del evento.</param>
        /// <param name="fechaInicio">Fecha en la que inicia el evento.</param>
        /// <param name="fechaFin">Fecha en la que termina el evento.</param>
        /// <param name="horaInicio">Hora en la que empieza el evento.</param>
        /// <param name="horaFin">Hora en la que termina el evento.</param>
        /// <param name="foto">Foto referencial del evento.</param>
        /// <param name="idLocalidad">Id unico que representa la localidad del evento.</param>
        /// <param name="idCategoria">Id unico que representa la categoria a la que pertenece el evento.</param>
        public Evento(int id, string nombre, string descripcion, double precio, DateTime fechaInicio, DateTime fechaFin, DateTime horaInicio, DateTime horaFin, string foto, int idLocalidad, int idCategoria)
        {
            this.Id = 0;
            this.nombre = null;
            this.descripcion = null;
            this.precio = 0;
            this.fechaInicio = new DateTime();
            this.fechaFin = new DateTime();
            this.horaInicio = new DateTime();
            this.horaFin = new DateTime();
            this.foto = null;
            this.idLocalidad = 0;
            this.idCategoria = 0;
        }
        /// <summary>
        /// Constructor de la clase evento sin categoria.
        /// </summary>
        /// <param name="id">Identificador unico del evento.</param>
        /// <param name="nombre">Nombre del evento.</param>
        /// <param name="descripcion">Descripcion del evento.</param>
        /// <param name="precio">Precio del evento.</param>
        /// <param name="fechaInicio">Fecha en la que inicia el evento.</param>
        /// <param name="fechaFin">Fecha en la que termina el evento.</param>
        /// <param name="horaInicio">Hora en la que empieza el evento.</param>
        /// <param name="horaFin">hora en la que termina el evento.</param>
        /// <param name="foto">Foto referencial del evento.</param>
        /// <param name="idLocalidad">Id unico que representa la localidad del evento.</param>
        public Evento(int id, string nombre, string descripcion, double precio, DateTime fechaInicio, DateTime fechaFin, DateTime horaInicio, DateTime horaFin, string foto, int idLocalidad) : this(id, nombre)
        {
            this.descripcion = descripcion;
            this.precio = precio;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.horaInicio = horaInicio;
            this.horaFin = horaFin;
            this.foto = foto;
            this.idLocalidad = idLocalidad;
        }

    }
}
