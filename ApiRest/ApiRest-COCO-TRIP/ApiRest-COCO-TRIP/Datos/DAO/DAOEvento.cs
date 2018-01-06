using ApiRest_COCO_TRIP.Comun.Excepcion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using ApiRest_COCO_TRIP.Datos.Entity;
using NpgsqlTypes;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
    /// <summary>
    /// Clase que realiza el CRUD para la entidad Evento.
    /// </summary>
    public class DAOEvento : DAO, IDAOEvento
    {

        private NpgsqlParameter parametro;
        private NpgsqlDataReader leerDatos;
        private Entidad evento;
        private List<Entidad> lista;

        /// <summary>
        /// Método constructor.
        /// </summary>
        public DAOEvento()
        {
            parametro = new NpgsqlParameter();
            lista = new List<Entidad>();
        }

        /// <summary>
        /// Método Actualizar, Actualiza un evento.
        /// </summary>
        /// <param name="objeto"> Entidad(evento) a actualizar</param>
        public override void Actualizar(Entidad objeto)
        {
            try
            {
                evento = (Evento)objeto;
                Conectar();
                Comando = SqlConexion.CreateCommand();
                Comando.CommandText = "actualizarEventoPorId";
                Comando.CommandType = CommandType.StoredProcedure;

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Integer;
                parametro.Value = evento.Id;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
                parametro.Value = ((Evento)evento).Nombre;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
                parametro.Value = ((Evento)evento).Descripcion;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Double;
                parametro.Value = ((Evento)evento).Precio;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Timestamp;
                parametro.Value = ((Evento)evento).FechaInicio;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Timestamp;
                parametro.Value = ((Evento)evento).FechaFin;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Time;
                parametro.Value = ((Evento)evento).HoraInicio.Hour + ":" + ((Evento)evento).HoraInicio.Minute + ":00";
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Time;
                parametro.Value = ((Evento)evento).HoraFin.Hour + ":" + ((Evento)evento).HoraFin.Minute + ":00";
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
                parametro.Value = ((Evento)evento).Foto;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Integer;
                parametro.Value = ((Evento)evento).IdLocalidad;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Integer;
                parametro.Value = ((Evento)evento).IdCategoria;
                Comando.Parameters.Add(parametro);

                leerDatos = Comando.ExecuteReader();
                leerDatos.Close();

            }
            catch (NpgsqlException e)
            {
                BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
                ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw ex;
            }
            catch (InvalidCastException e)
            {
                CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
                ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw ex;
            }

            catch (InvalidOperationException e)
            {
                OperacionInvalidaExcepcion ex = new OperacionInvalidaExcepcion(e);
                ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                Desconectar();
            }
        }

        /// <summary>
        /// Metodo ConsultarLista, consulta todos los eventos.
        /// </summary>
        /// <param name="objeto"> Entidad para utilizar en la consulta</param>
        /// <returns></returns>
        public override List<Entidad> ConsultarLista(Entidad objeto)
        {
            lista = new List<Entidad>();
            try
            {
                Conectar();
                Comando = SqlConexion.CreateCommand();
                Comando.CommandText = "ConsultarEventos";
                Comando.CommandType = CommandType.StoredProcedure;

                leerDatos = Comando.ExecuteReader();
                while (leerDatos.Read())
                {
                    evento = FabricaEntidad.CrearEntidadEvento();

                    DateTime horaInicio = new DateTime();

                    horaInicio = horaInicio.AddHours(leerDatos.GetTimeSpan(6).Hours);
                    horaInicio = horaInicio.AddMinutes(leerDatos.GetTimeSpan(6).Minutes);
                    horaInicio = horaInicio.AddSeconds(leerDatos.GetTimeSpan(6).Seconds);

                    DateTime horaFin = new DateTime();

                    horaFin = horaFin.AddHours(leerDatos.GetTimeSpan(7).Hours);
                    horaFin = horaFin.AddMinutes(leerDatos.GetTimeSpan(7).Minutes);
                    horaFin = horaFin.AddSeconds(leerDatos.GetTimeSpan(7).Seconds);

                    ((Evento)evento).Id = leerDatos.GetInt32(0);
                    ((Evento)evento).Nombre = leerDatos.GetString(1);
                    ((Evento)evento).Descripcion = leerDatos.GetString(2);
                    ((Evento)evento).Precio = leerDatos.GetDouble(3);
                    ((Evento)evento).FechaInicio = leerDatos.GetDateTime(4);
                    ((Evento)evento).FechaFin = leerDatos.GetDateTime(5);
                    ((Evento)evento).HoraInicio = horaInicio;
                    ((Evento)evento).HoraFin = horaFin;
                    ((Evento)evento).Foto = leerDatos.GetString(8);
                    ((Evento)evento).IdLocalidad = leerDatos.GetInt32(9);
                    ((Evento)evento).IdCategoria = leerDatos.GetInt32(10);
                    lista.Add(evento);
                }
            }
            catch (NpgsqlException e)
            {
                BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
                ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw ex;
            }
            catch (InvalidCastException e)
            {
                CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
                ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw ex;
            }

            catch (InvalidOperationException e)
            {
                OperacionInvalidaExcepcion ex = new OperacionInvalidaExcepcion(e);
                ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Desconectar();
            }
            return lista;
        }

        /// <summary>
        /// Método ConsultarListaPorCategoria, consulta todos los eventos filtrados por una categoria y 
        /// los retorna en una lista.
        /// </summary>
        /// <param name="objeto"> Entidad(categoria) para filtrar la lista</param>
        public List<Entidad> ConsultarListaPorCategoria(Entidad objeto)
        {
            lista = new List<Entidad>();
            Entidad categoria = (Categoria)objeto;
            try
            {
                Conectar();
                Comando = SqlConexion.CreateCommand();
                Comando.CommandText = "ConsultarEventosPorIdCategoria";
                Comando.CommandType = CommandType.StoredProcedure;

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Integer;
                parametro.Value = categoria.Id;
                Comando.Parameters.Add(parametro);
                leerDatos = Comando.ExecuteReader();
                while (leerDatos.Read())
                {
                    evento = FabricaEntidad.CrearEntidadEvento();

                    DateTime horaInicio = new DateTime();

                    horaInicio = horaInicio.AddHours(leerDatos.GetTimeSpan(6).Hours);
                    horaInicio = horaInicio.AddMinutes(leerDatos.GetTimeSpan(6).Minutes);
                    horaInicio = horaInicio.AddSeconds(leerDatos.GetTimeSpan(6).Seconds);

                    DateTime horaFin = new DateTime();

                    horaFin = horaFin.AddHours(leerDatos.GetTimeSpan(7).Hours);
                    horaFin = horaFin.AddMinutes(leerDatos.GetTimeSpan(7).Minutes);
                    horaFin = horaFin.AddSeconds(leerDatos.GetTimeSpan(7).Seconds);

                    ((Evento)evento).Id = leerDatos.GetInt32(0);
                    ((Evento)evento).Nombre = leerDatos.GetString(1);
                    ((Evento)evento).Descripcion = leerDatos.GetString(2);
                    ((Evento)evento).Precio = leerDatos.GetDouble(3);
                    ((Evento)evento).FechaInicio = leerDatos.GetDateTime(4);
                    ((Evento)evento).FechaFin = leerDatos.GetDateTime(5);
                    ((Evento)evento).HoraInicio = horaInicio;
                    ((Evento)evento).HoraFin = horaFin;
                    ((Evento)evento).Foto = leerDatos.GetString(8);
                    ((Evento)evento).IdLocalidad = leerDatos.GetInt32(9);
                    ((Evento)evento).IdCategoria = categoria.Id;
                    lista.Add(evento);
                }
            }
            catch (NpgsqlException e)
            {
                BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
                ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw ex;
            }
            catch (InvalidCastException e)
            {
                CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
                ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw ex;
            }

            catch (InvalidOperationException e)
            {
                OperacionInvalidaExcepcion ex = new OperacionInvalidaExcepcion(e);
                ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Desconectar();
            }
            return lista;
        }


        /// <summary>
        /// Método ConsultarPorId, consulta un evento dado su id y lo retorna como una entidad.
        /// </summary>
        /// <param name="objeto"> Entidad que contiene el id del evento a consultar</param>
        public override Entidad ConsultarPorId(Entidad objeto)
        {

            try
            {
                evento = (Evento)objeto;
                Conectar();
                Comando = SqlConexion.CreateCommand();
                Comando.CommandText = "ConsultarEventoPorIdEvento";
                Comando.CommandType = CommandType.StoredProcedure;

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Integer;
                parametro.Value = evento.Id;
                Comando.Parameters.Add(parametro);

                leerDatos = Comando.ExecuteReader();
                leerDatos.Read();

                DateTime horaInicio = new DateTime();
                horaInicio = horaInicio.AddHours(leerDatos.GetTimeSpan(6).Hours);
                horaInicio = horaInicio.AddMinutes(leerDatos.GetTimeSpan(6).Minutes);
                horaInicio = horaInicio.AddSeconds(leerDatos.GetTimeSpan(6).Seconds);
                DateTime horaFin = new DateTime();
                horaFin = horaFin.AddHours(leerDatos.GetTimeSpan(7).Hours);
                horaFin = horaFin.AddMinutes(leerDatos.GetTimeSpan(7).Minutes);
                horaFin = horaFin.AddSeconds(leerDatos.GetTimeSpan(7).Seconds);

                evento.Id = leerDatos.GetInt32(0);
                ((Evento)evento).Nombre = leerDatos.GetString(1);
                ((Evento)evento).Descripcion = leerDatos.GetString(2);
                ((Evento)evento).Precio = leerDatos.GetDouble(3);
                ((Evento)evento).FechaInicio = leerDatos.GetDateTime(4);
                ((Evento)evento).FechaFin = leerDatos.GetDateTime(5);
                ((Evento)evento).HoraInicio = horaInicio;
                ((Evento)evento).HoraFin = horaFin;
                ((Evento)evento).Foto = leerDatos.GetString(8);
                ((Evento)evento).IdLocalidad = leerDatos.GetInt32(9);
                ((Evento)evento).IdCategoria = leerDatos.GetInt32(10);
                leerDatos.Close();
            }
            catch (NpgsqlException e)
            {
                BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
                ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw ex;
            }
            catch (InvalidCastException e)
            {
                CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
                ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw ex;
            }

            catch (InvalidOperationException e)
            {
                OperacionInvalidaExcepcion ex = new OperacionInvalidaExcepcion(e);
                ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Desconectar();
            }
            return evento;
        }

        /// <summary>
        /// Método Eliminar, Elimina un evento.
        /// </summary>
        /// <param name="objeto"> Entidad(evento) a eliminar</param>
        public override void Eliminar(Entidad objeto)
        {
            try
            {

                evento = (Evento)objeto;
                Conectar();
                Comando = SqlConexion.CreateCommand();
                Comando.CommandText = "eliminareventoporid";
                Comando.CommandType = CommandType.StoredProcedure;

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Integer;
                parametro.Value = evento.Id;
                Comando.Parameters.Add(parametro);
                leerDatos = Comando.ExecuteReader();
                leerDatos.Close();
            }
            catch (NpgsqlException e)
            {
                BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
                ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw ex;
            }
            catch (InvalidCastException e)
            {
                CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
                ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw ex;
            }

            catch (InvalidOperationException e)
            {
                OperacionInvalidaExcepcion ex = new OperacionInvalidaExcepcion(e);
                ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Desconectar();
            }
        }

        /// <summary>
        /// Método Insertar, Inserta un evento.
        /// </summary>
        /// <param name="objeto"> Entidad(evento) a insertar</param>
        public override void Insertar(Entidad objeto)
        {
            try
            {

                evento = (Evento)objeto;
                Conectar();
                Comando = SqlConexion.CreateCommand();
                Comando.CommandText = "InsertarEvento";
                Comando.CommandType = CommandType.StoredProcedure;

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
                parametro.Value = ((Evento)evento).Nombre;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
                parametro.Value = ((Evento)evento).Descripcion;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Double;
                parametro.Value = ((Evento)evento).Precio;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Timestamp;
                parametro.Value = ((Evento)evento).FechaInicio;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Timestamp;
                parametro.Value = ((Evento)evento).FechaFin;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Time;
                parametro.Value = ((Evento)evento).HoraInicio.Hour + ":" + ((Evento)evento).HoraInicio.Minute + ":"
                  + ((Evento)evento).HoraInicio.Second;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Time;
                parametro.Value = ((Evento)evento).HoraFin.Hour + ":" + ((Evento)evento).HoraFin.Minute + ":"
                  + ((Evento)evento).HoraFin.Second;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
                parametro.Value = ((Evento)evento).Foto;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Integer;
                parametro.Value = ((Evento)evento).IdLocalidad;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Integer;
                parametro.Value = ((Evento)evento).IdCategoria;
                Comando.Parameters.Add(parametro);

                leerDatos = Comando.ExecuteReader();
                leerDatos.Close();
            }
            catch (NpgsqlException e)
            {
                BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
                ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw ex;
            }
            catch (InvalidCastException e)
            {
                CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
                ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw ex;
            }

            catch (InvalidOperationException e)
            {
                OperacionInvalidaExcepcion ex = new OperacionInvalidaExcepcion(e);
                ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Desconectar();
            }
        }
    }
}