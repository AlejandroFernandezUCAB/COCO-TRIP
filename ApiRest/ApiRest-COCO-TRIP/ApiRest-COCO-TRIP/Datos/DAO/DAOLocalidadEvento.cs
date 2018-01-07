using ApiRest_COCO_TRIP.Comun.Excepcion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using ApiRest_COCO_TRIP.Datos.Entity;
using NpgsqlTypes;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
    /// <summary>
    /// Clase que realiza el CRUD para la entidad LocalidadEvento.
    /// </summary>
    public class DAOLocalidadEvento : DAO
    {
        private NpgsqlParameter parametro;
        private NpgsqlDataReader leerDatos;
        private Entidad localidad;
        private List<Entidad> lista;

        /// <summary>
        /// Método constructor.
        /// </summary>
        public DAOLocalidadEvento()
        {
            parametro = new NpgsqlParameter();
            lista = new List<Entidad>();
        }

        /// <summary>
        /// Método ConsultarLista, consulta todas las localidades y las retorna en una lista.
        /// </summary>
        /// <param name="objeto"> Entidad para utilizar si se desea filtrar la lista</param>
        public override List<Entidad> ConsultarLista(Entidad objeto)
        {
            try
            {
                Conectar();
                Comando = SqlConexion.CreateCommand();
                Comando.CommandText = "consultarlocalidades";
                Comando.CommandType = CommandType.StoredProcedure;

                leerDatos = Comando.ExecuteReader();
                while (leerDatos.Read())
                {
                    localidad = FabricaEntidad.CrearEntidadLocalidad();
                    ((LocalidadEvento)localidad).Id = leerDatos.GetInt32(0);
                    ((LocalidadEvento)localidad).Nombre = leerDatos.GetString(1);
                    ((LocalidadEvento)localidad).Descripcion = leerDatos.GetString(2);
                    ((LocalidadEvento)localidad).Coordenadas = leerDatos.GetString(3);
                    lista.Add(localidad);
                }
                leerDatos.Close();
            }
            catch (NpgsqlException e)
            {
                BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
                ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
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
        /// Método ConsultarPorId, consulta una localidad dado su id y lo retorna como una entidad.
        /// </summary>
        /// <param name="objeto"> Entidad que contiene el id de la localidad a consultar</param>
        public override Entidad ConsultarPorId(Entidad objeto)
        {
            localidad = (LocalidadEvento)objeto;
            try
            {
                Conectar();
                Comando = SqlConexion.CreateCommand();
                Comando.CommandText = "ConsultarLocalidadPorId";
                Comando.CommandType = CommandType.StoredProcedure;

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Integer;
                parametro.Value = localidad.Id;
                Comando.Parameters.Add(parametro);


                leerDatos = Comando.ExecuteReader();
                leerDatos.Read();
                localidad.Id = leerDatos.GetInt32(0);
                ((LocalidadEvento)localidad).Nombre = leerDatos.GetString(1);
                ((LocalidadEvento)localidad).Descripcion = leerDatos.GetString(2);
                ((LocalidadEvento)localidad).Coordenadas = leerDatos.GetString(3);
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

            return localidad;
        }

        /// <summary>
        /// Método Insertar, Inserta una localidad.
        /// </summary>
        /// <param name="objeto"> Entidad(localidad) a insertar</param>
        public override void Insertar(Entidad objeto)
        {
            try
            {
                localidad = (LocalidadEvento)objeto;
                Conectar();
                Comando = SqlConexion.CreateCommand();
                Comando.CommandText = "InsertarLocalidad";
                Comando.CommandType = CommandType.StoredProcedure;

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
                parametro.Value = ((LocalidadEvento)localidad).Nombre;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
                parametro.Value = ((LocalidadEvento)localidad).Descripcion;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
                parametro.Value = ((LocalidadEvento)localidad).Coordenadas;
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
        /// Método Actualizar, Actualiza una localidad.
        /// </summary>
        /// <param name="objeto"> Entidad(localidad) a actualizar</param>
        public override void Actualizar(Entidad objeto)
        {
            try
            {
                localidad = (LocalidadEvento)objeto;
                Conectar();
                Comando = SqlConexion.CreateCommand();
                Comando.CommandText = "actualizarlocalidadporid";
                Comando.CommandType = CommandType.StoredProcedure;

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Integer;
                parametro.Value = localidad.Id;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
                parametro.Value = ((LocalidadEvento)localidad).Nombre;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
                parametro.Value = ((LocalidadEvento)localidad).Descripcion;
                Comando.Parameters.Add(parametro);

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
                parametro.Value = ((LocalidadEvento)localidad).Coordenadas;
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
        /// Método Eliminar, Elimina una localidad.
        /// </summary>
        /// <param name="objeto"> Entidad(localidad) a eliminar</param>
        public override void Eliminar(Entidad objeto)
        {
            try
            {

                localidad = (LocalidadEvento)objeto;
                Conectar();
                Comando = SqlConexion.CreateCommand();
                Comando.CommandText = "EliminarLocalidadporId";
                Comando.CommandType = CommandType.StoredProcedure;

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Integer;
                parametro.Value = localidad.Id;
                Comando.Parameters.Add(parametro);

                leerDatos = Comando.ExecuteReader();
                leerDatos.Read();
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