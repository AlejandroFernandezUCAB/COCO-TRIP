using ApiRest_COCO_TRIP.Datos.Entity;
using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
    /// <summary>
    /// Superclase DAO
    /// </summary>
    public abstract class DAO
    {
        private const string Credenciales =
        "Host = localhost; Port = 5432; " +
        "Username = admin_cocotrip; " +
        "Password = ds1718a; " +
        "Database = cocotrip";

        private NpgsqlConnection sqlConexion;   //Conexion con la base de datos PostgreSQL.
        private NpgsqlCommand comando;          //Instrucion SQL a ejecutar.

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        public DAO()
        {
            SqlConexion = new NpgsqlConnection(Credenciales);
            comando = new NpgsqlCommand();
        }

        /// <summary>
        /// Getters y Setters del atributo SqlConexion.
        /// </summary>
        public NpgsqlConnection SqlConexion
        {
            get { return sqlConexion; }
            set { sqlConexion = value; }
        }

        /// <summary>
        /// Getters y Setters del atributo Comando.
        /// </summary>
        public NpgsqlCommand Comando
        {
            get { return comando; }
            set { comando = value; }
        }

        /// <summary>
        /// Inicia la conexion con la base de datos.
        /// </summary>
        public void Conectar()
        {
            if (sqlConexion.State != ConnectionState.Open)
            {
                sqlConexion.Open();
            }
        }

        /// <summary>
        /// Finaliza la conexion con la base de datos.
        /// </summary>
        public void Desconectar()
        {
            if (sqlConexion.State != ConnectionState.Closed)
            {
                sqlConexion.Close();
            }
        }

        /// <summary>
        /// Metodo Create.
        /// </summary>
        /// <param name="objeto">Instacia tipo Entidad que se desea guardar.</param>
        public abstract void Insertar(Entidad objeto);

        /// <summary>
        /// Metodo Read, consulta mediante un Id.
        /// </summary>
        /// <param name="objeto">Instacia tipo Entidad con Id con el que se desea consultar.</param>
        /// <returns>Entidad asociada al Id colocado por parametro.</returns>
        public abstract Entidad ConsultarPorId(Entidad objeto);

        /// <summary>
        /// Metodo Read.
        /// </summary>
        /// <param name="objeto">Instacia tipo Entidad que se desea consultar.</param>
        /// <returns>Lista de Entidades referenciadas a la consulta</returns>
        public abstract List<Entidad> ConsultarLista(Entidad objeto);

        /// <summary>
        /// Metodo Update, actualiza una Entidad enviada por parametro.
        /// </summary>
        /// <param name="objeto">Instancia tipo Entidad que se desea actualizar/modificar</param>
        public abstract void Actualizar(Entidad objeto);

        /// <summary>
        /// Metodo Delete, elimina una Entidad enviada por parametro.
        /// </summary>
        /// <param name="objeto">Instancia tipo Entidad que se desea eliminar</param>
        public abstract void Eliminar(Entidad objeto);
    }
}
