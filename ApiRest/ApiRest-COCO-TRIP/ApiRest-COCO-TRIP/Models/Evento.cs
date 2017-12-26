using System;

namespace ApiRest_COCO_TRIP.Models { 
    /// <summary>
    /// Clase que define la entidad evento de la aplicación.
    /// </summary>
    public class Evento
    {
        int id;
        string nombre;
        string descripcion;
        double precio;
        DateTime fechaInicio;
        DateTime fechaFin;
        DateTime horaInicio;
        DateTime horaFin;
        string foto;
        int idLocalidad;
        int idCategoria;

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
        public int Id { get => id; set => id = value; }

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
        /// <param name="id">Identificador unico del evento.</param>
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
        /// <param name="horaFin">hora en la que termina el evento.</param>
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




    /// <summary>
    /// Compara si dos objetos del tipo "Evento" son iguales.
    /// </summary>
    /// <param name="obj">Objeto del tipo evento con el que se desea comparar.</param>
    /// <returns>En caso de ser iguales devuelve "true", en caso contrario "false"</returns>
    public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Castea el objeto del Evento, en un string.
        /// </summary>
        /// <returns>Retorna en forma de una cadena de caracteres la informacion del evento.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
