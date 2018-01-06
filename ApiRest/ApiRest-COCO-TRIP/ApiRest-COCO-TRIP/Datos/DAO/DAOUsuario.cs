using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using System.Data;
using Npgsql;
using NpgsqlTypes;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Reflection;
using System;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
    /// <summary>
    /// DAO de la entidad Usuario
    /// </summary>
    public class DAOUsuario : DAO
    {
        private NpgsqlParameter parametro;
        private NpgsqlDataReader leerDatos;

        private Usuario usuario;

        public DAOUsuario()
        {
            parametro = new NpgsqlParameter();
        }

        public override Entidad ConsultarPorId(Entidad objeto)
        {
            try
            {
                usuario = (Usuario)objeto;

                base.Conectar(); //Inicia una sesion con la base de datos

                base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
                base.Comando.CommandText = "ConsultarUsuarioSoloId";
                base.Comando.CommandType = CommandType.StoredProcedure;

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
                parametro.Value = usuario.Id;
                base.Comando.Parameters.Add(parametro);

                leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

                if (leerDatos.Read()) //Lee los resultados
                {
                    usuario.NombreUsuario = leerDatos.GetString(0);
                    usuario.Correo = leerDatos.GetString(1);
                    usuario.Nombre = leerDatos.GetString(2);
                    usuario.Apellido = leerDatos.GetString(3);
                    usuario.FechaNacimiento = leerDatos.GetDateTime(4);
                    usuario.Genero = leerDatos.GetString(5);
                    //usuario.Foto = leerDatos.GetString(6);
                }

                leerDatos.Close(); //Cierra el Data Reader

                base.Desconectar(); //Culmina la sesion con la base de datos

                return usuario;
            }
            catch (NpgsqlException e)
            {
                throw new BaseDeDatosExcepcion(e, "Error de logica de BD en "
                + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
            }
            catch (NullReferenceException e)
            {
                throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en "
                + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
            }
        }

        public Entidad ConsultarPorNombre(Entidad _usuario)
        {
            try
            {
                usuario = (Usuario)_usuario;

                base.Conectar(); //Inicia una sesion con la base de datos

                base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
                base.Comando.CommandText = "ConsultarUsuarioSoloNombre";
                base.Comando.CommandType = CommandType.StoredProcedure;

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Varchar; //Ingresa parametros de entrada
                parametro.Value = usuario.NombreUsuario;
                base.Comando.Parameters.Add(parametro);

                leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

                if (leerDatos.Read()) //Lee los resultados
                {
                    usuario.Id = leerDatos.GetInt32(0);
                    usuario.NombreUsuario = leerDatos.GetString(1);
                    usuario.Correo = leerDatos.GetString(2);
                    usuario.Nombre = leerDatos.GetString(3);
                    usuario.Apellido = leerDatos.GetString(4);
                    usuario.FechaNacimiento = leerDatos.GetDateTime(5);
                    usuario.Genero = leerDatos.GetString(6);
                    usuario.Valido = leerDatos.GetBoolean(7);
                    //usuario.Foto = leerDatos.GetString(8);
                }

                leerDatos.Close(); //Cierra el Data Reader

                base.Desconectar(); //Culmina la sesion con la base de datos

                return usuario;
            }
            catch (NpgsqlException e)
            {
                throw new BaseDeDatosExcepcion(e, "Error de logica de BD en "
                + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
            }
            catch (NullReferenceException e)
            {
                throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en "
                + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
            }
            catch (InvalidCastException e)
            {
                throw new CasteoInvalidoExcepcion(e, "El nombre del usuario es nulo en "
                + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
            }
        }

        public override List<Entidad> ConsultarLista(Entidad objeto)
        {
            throw new System.NotImplementedException();
        }

        public override void Eliminar(Entidad objeto)
        {
            throw new System.NotImplementedException();
        }

        public override void Insertar(Entidad objeto)
        {
            throw new System.NotImplementedException();
        }

        public override void Actualizar(Entidad objeto)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Se obtiene de la base de datos el password actual del usuario
        /// </summary>
        /// <param name="entidad">Entidad como instancia de usuario</param>
        public Entidad ObtenerPassword(Entidad entidad)
        {

            try
            {
                usuario = (Usuario)entidad;
                base.Conectar();

                base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
                base.Comando.CommandText = "ConsultarContrasena";
                base.Comando.CommandType = CommandType.StoredProcedure;

                parametro = new NpgsqlParameter();
                parametro.NpgsqlDbType = NpgsqlDbType.Varchar; //Ingresa parametros de entrada
                parametro.Value = usuario.NombreUsuario;
                base.Comando.Parameters.Add(parametro);

                leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

                if (leerDatos.Read())
                {
                    usuario.Clave = leerDatos.GetString(0);
                }

                leerDatos.Close(); //Cierra el Data Reader

                base.Desconectar(); //Culmina la sesion con la base de datos


                return usuario;

            }
            catch (NpgsqlException e)
            {

                throw new BaseDeDatosExcepcion(e);

            }
            catch (Exception e)
            {
                throw e;
            }

        }



        /// <summary>
        /// Se modifica la contraseña del usuario en la base de datos
        /// </summary>
        /// <param name="entidad">Entidad como instancia de usuario</param>
        public void CambiarPassword(Entidad entidad)
        {
            usuario = (Usuario)entidad;
            try
            {

                base.Conectar();

                base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
                base.Comando.CommandText = "ModificarPass";
                base.Comando.CommandType = CommandType.StoredProcedure;
                base.Comando.Parameters.Add(new NpgsqlParameter
                {
                    NpgsqlDbType = NpgsqlDbType.Integer,
                    Value = usuario.Id
                });
                base.Comando.Parameters.Add(new NpgsqlParameter
                {
                    NpgsqlDbType = NpgsqlDbType.Varchar,
                    Value = usuario.Clave
                });

                base.Comando.ExecuteReader(); //Ejecuta el comando
                

            }
            catch (NpgsqlException e)
            {

                throw new BaseDeDatosExcepcion(e);

            }
           
            finally
            {
                base.Desconectar(); //Culmina la sesion con la base de datos
            }
        }


        /// <summary>
        /// Se modifican los datos del usuario en la base de datos
        /// </summary>
        /// <param name="entidad">Entidad como instancia de usuario</param>
        public void ModificarDatos(Entidad entidad)
        {

            usuario = (Usuario)entidad;

            try
            {
                base.Conectar();

                base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
                base.Comando.CommandText = "ModificarDatosUsuario";
                base.Comando.CommandType = CommandType.StoredProcedure;

                base.Comando.Parameters.Add(new NpgsqlParameter
                {
                    NpgsqlDbType = NpgsqlDbType.Integer,
                    Value = usuario.Id
                });
                base.Comando.Parameters.Add(new NpgsqlParameter
                {
                    NpgsqlDbType = NpgsqlDbType.Varchar,
                    Value = usuario.Nombre
                });
                base.Comando.Parameters.Add(new NpgsqlParameter
                {
                    NpgsqlDbType = NpgsqlDbType.Varchar,
                    Value = usuario.Apellido
                });
                base.Comando.Parameters.Add(new NpgsqlParameter
                {
                    NpgsqlDbType = NpgsqlDbType.Date,
                    Value = usuario.FechaNacimiento
                });
                base.Comando.Parameters.Add(new NpgsqlParameter
                {
                    NpgsqlDbType = NpgsqlDbType.Char,
                    Value = usuario.Genero[0]
                });


                base.Comando.ExecuteReader(); //Ejecuta el comando

                
            }
            catch (NpgsqlException e)
            {

                throw new BaseDeDatosExcepcion(e);

            }
            catch (Exception e)
            {

                throw e;

            }
            finally
            {

                base.Desconectar(); //Culmina la sesion con la base de datos

            }

        }

        /// <summary>
        /// Se borra al usuario de la base de datos
        /// </summary>
        /// <param name="entidad">Entidad como instancia de usuario</param>
        public void BorrarUsuario(Entidad entidad)
        {

            usuario = (Usuario)entidad;

            try
            {

                base.Conectar();

                base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
                base.Comando.CommandText = "BorrarUsuario";
                base.Comando.CommandType = CommandType.StoredProcedure;

                base.Comando.Parameters.Add(new NpgsqlParameter
                {
                    NpgsqlDbType = NpgsqlDbType.Integer,
                    Value = usuario.Id
                });
                base.Comando.Parameters.Add(new NpgsqlParameter
                {
                    NpgsqlDbType = NpgsqlDbType.Varchar,
                    Value = usuario.Clave
                });

                base.Comando.ExecuteReader(); //Ejecuta el comando


             

            }
            catch (NpgsqlException e)
            {

                throw new BaseDeDatosExcepcion(e);

            }
            catch (Exception e)
            {

                throw e;

            }
            finally
            {

                base.Desconectar(); //Culmina la sesion con la base de datos

            }

        }

    }

}
