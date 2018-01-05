using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.Singleton;
using Npgsql;
using NpgsqlTypes;


/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///     Data Access Object de la entidad Categoria. En esta clase se encapsula el acceso a la fuente de datos.
/// </summary>
namespace ApiRest_COCO_TRIP.Datos.DAO
{
    public class DAOCategoria : DAO, IDAOCategoria
    {
        private NpgsqlParameter parametro;
        private NpgsqlDataReader leerDatos;
        private List<Entidad> lista;
        private Categoria categoria;
        private static MensajeResultadoOperacion mensajeRegistry = MensajeResultadoOperacion.ObtenerInstancia();
        private string mensaje;

        public DAOCategoria()
        {
            parametro = new NpgsqlParameter();
            lista = new List<Entidad>();
        }

        /////////////////////////////////////// METODOS AUXILIARES DE DAO ///////////////////////////////////////
        #region metodosAuxiliares
        private NpgsqlParameter AgregarParametro(NpgsqlDbType tipoDeDato, object valor)
        {
            var parametro = new NpgsqlParameter
            {
                NpgsqlDbType = tipoDeDato,
                Value = valor
            };

            return parametro;
        }


        /// <summary>
        /// Metodo que permite preparar un StoredProcedure para su ejecución.
        /// </summary>
        /// <param name="sp">Nombre del Stored Procedure que se desea utilizar.</param>
        private void StoredProcedure(string sp)
        {
            base.Conectar();
            base.Comando = base.SqlConexion.CreateCommand();
            base.Comando.CommandType = CommandType.StoredProcedure;
            base.Comando.CommandText = sp;
        }

        /// <summary>
        /// Metodo que permite modificar los parametro del comando que se ejecutara.
        /// </summary>
        /// <param name="categoria">Instancia categoria con la que se desea modificar los parametros del comando.</param>
        private void ParametrosModificar(Categoria categoria)
        {
            base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Id);
            base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, categoria.Nombre);
            base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, categoria.Descripcion);
            base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Nivel);
        }


        /// <summary>
        /// Metodo que lee los datos de la categoria
        /// </summary>
        /// <returns>IList con las categorias que se encuentran en el atributo "leerDatos"</returns>
        private List<Entidad> SetListaCategoria()
        {
            List<Entidad> listaCategorias = new List<Entidad>();
            while (leerDatos.Read())
            {
                listaCategorias.Add(new Categoria()
                {
                    Id = leerDatos.GetInt32(0),
                    Nombre = leerDatos.GetString(1),
                    Descripcion = leerDatos.GetString(2),
                    Estatus = leerDatos.GetBoolean(3),
                    Nivel = leerDatos.GetInt32(4)
                });
                Int32.TryParse(leerDatos.GetValue(5).ToString(), out int Superior);
                ((Categoria)listaCategorias[listaCategorias.Count - 1]).CategoriaSuperior = Superior;
            }
            return listaCategorias;
        }


        #endregion metodosAuxiliares


        /////////////////////////////////////// METODOS HEREDADOS DE DAO ///////////////////////////////////////
        #region metodosHeredados
        /// <summary>
        /// Metodo Create, permite insertar una Entidad tipo categoria en la base de datos.
        /// </summary>
        /// <param name="objeto">instancia Catgoria que se desea insertar.</param>
        /// <exception cref="NombreDuplicadoExcepcion">Nombre duplicado al momento de insertar.</exception>
        /// <exception cref="BaseDeDatosExcepcion">Error al momento de agregar una categoria.</exception>
        /// <exception cref="Excepcion">Error inesperado.</exception>
        public override void Insertar(Entidad objeto)
        {
            categoria = (Categoria)objeto;
            try
            {
                int exitoso = 0;
                StoredProcedure("m9_agregarsubcategoria");
                base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, categoria.Nombre);       //Nombre de la categoria
                base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, categoria.Descripcion);  //descripcion de la categoría
                base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Nivel);        //nivel de la categoria
                base.Comando.Parameters.AddWithValue(NpgsqlDbType.Boolean, true);                   //status de la categoria, en true por defecto

                if (categoria.CategoriaSuperior == 0)
                {
                    base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, DBNull.Value);
                }
                else
                {
                    base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.CategoriaSuperior);
                }
                exitoso = base.Comando.ExecuteNonQuery();
            }
            catch (PostgresException ex)
            {
                mensaje = "Error de duplicidad de nombre en " + this.GetType().FullName + ". " + MethodBase.GetCurrentMethod().Name
                    + ". Este nombre de categoria ya existe. || " + ex.Message;
                throw new NombreDuplicadoExcepcion(ex, mensaje);
            }
            catch (NpgsqlException ex)
            {
                mensaje = "Error al agregar categoria en " + this.GetType().FullName + ". " + MethodBase.GetCurrentMethod().Name
                    + " || " + ex.Message;
                throw new BaseDeDatosExcepcion(ex, mensaje);
            }
            catch (Exception ex)
            {
                throw new Excepcion(ex, mensajeRegistry.ErrorInesperado + " || " + ex.Message);
            }
            finally
            {
                base.Desconectar();
            }
        }



        /// <summary>
        /// Metodo Read, consulta mediante un Id. 
        /// </summary>
        /// <param name="objeto">Instacia tipo categoria con Id con el que se desea consultar.</param>
        /// <returns>Categoria asociada al Id colocado por parametro.</returns>
        /// <exception cref="NotImplementedException">Metodo no implementado</exception>
        public override Entidad ConsultarPorId(Entidad objeto)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Metodo Read.
        /// </summary>
        /// <param name="objeto">Instacia tipo Categoria que se desea consultar.</param>
        /// <returns>Lista de Categorias referenciadas a la consulta</returns>
        /// <exception cref="NotImplementedException">Metodo no implementado</exception>
        public override List<Entidad> ConsultarLista(Entidad objeto)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Metodo Update, actualiza una categoria enviada por parametro.
        /// </summary>
        /// <param name="objeto">Instancia tipo Categoria que se desea actualizar/modificar</param>
        /// <exception cref="BaseDeDatosExcepcion">Error al actualizar la categoria</exception>
        /// <exception cref="NombreDuplicadoExcepcion">Error en duplicidad en el nombre de la categoria que intenta actualizar.</exception>
        /// <exception cref="HijoConDePendenciaExcepcion">La categoria que intenta actualizar tiene dependencias.</exception>
        /// <exception cref="ArgumentoNuloExcepcion">Error al utilizar el ToList, para convertir la lista a Categorias.</exception>
        /// <exception cref="Excepcion">Error inesperado.</exception>
        public override void Actualizar(Entidad objeto)
        {
            categoria = (Categoria)objeto;
            try
            {
                StoredProcedure("m9_ModificarCategoria");
                VerificarExistenciaCategoria();
                if (categoria.CategoriaSuperior == 0)
                {
                    base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, DBNull.Value);
                }
                else
                {
                    base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.CategoriaSuperior);
                }
                base.Comando.ExecuteNonQuery();
            }
            catch (PostgresException ex)
            {
                mensaje = "Error de duplicidad en nombre en " + this.GetType().FullName + ". " +
                    MethodBase.GetCurrentMethod().Name + " donde id: " + categoria.Id +
                    " No se puede modificar al nombre: " + categoria.Nombre +
                    " porque este nombre de categoria ya existe" + " || " + ex.Message;
                throw new NombreDuplicadoExcepcion(ex, mensaje);
            }
            catch (NpgsqlException ex)
            {
                mensaje = "Error al actualizar/modificar el registro en " + this.GetType().FullName + ". " +
                    MethodBase.GetCurrentMethod().Name + " donde ca_id: " + categoria.Id +
                    " y ca_nombre: " + categoria.Nombre + " || " + ex.Message;
                throw new BaseDeDatosExcepcion(ex, mensaje);

            }
            catch (ArgumentNullException ex) 
            {
                string mensaje = "Error interno creando las lista para las categorias " + " || " + ex.Message;
                throw new ArgumentoNuloExcepcion(ex, mensaje);
            }
            catch (HijoConDePendenciaExcepcion ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Excepcion(ex, mensajeRegistry.ErrorInesperado + " || " + ex.Message);
            }
            finally
            {
                base.Desconectar();
            }
        }



        /// <summary>
        /// Metodo Update, actualiza el estado de una categoria enviada por parametro.
        /// </summary>
        /// <param name="objeto">Instancia tipo Categoria que se desea actualizar/modificar</param>
        /// <exception cref="BaseDeDatosExcepcion">Error al momento de actualizar la categoria.</exception>
        /// <exception cref="Excepcion">Error inesperado.</exception>
        public virtual void ActualizarEstado(Entidad objeto)
        {
            categoria = (Categoria)objeto;
            try
            {
                StoredProcedure("m9_actualizarEstatusCategoria");
                base.Comando.Parameters.AddWithValue(NpgsqlDbType.Boolean, categoria.Estatus);
                base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Id);

                base.Comando.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                mensaje = "Error al actualizar el estado de la categoria. || " + this.GetType().FullName
                    + ". En el metodo " + MethodBase.GetCurrentMethod().Name + " || " + ex.Message;
                throw new BaseDeDatosExcepcion(ex, mensaje);
            }
            catch (Exception ex)
            {
                throw new Excepcion(ex, mensajeRegistry.ErrorInesperado + " || " + ex.Message);
            }
            finally
            {
                base.Desconectar();
            }
        }

        /// <summary>
        /// Metodo Delete, elimina una Categoria enviada por parametro.
        /// </summary>
        /// <param name="objeto">Instancia Categoria que se desea eliminar</param>
        /// <exception cref="NotImplementedException">Metodo No implementado</exception>
        public override void Eliminar(Entidad objeto)
        {
            throw new NotImplementedException();
        }

        #endregion metodosHeredados


        /////////////////////////////////////// METODOS ESPECIFICOS DE DAOCATEGORIA ///////////////////////////////////////
        #region metodosEspecificos

        /// <summary>
        /// EndPoint para obtener las categorias hijas a partir de una categoria padre dado un ID,
        /// si el id no viene en la peticion se devuelve las categorias padres absolutas
        /// </summary>
        /// <returns>IList de las categorias habilitadas</returns>
        /// <exception cref="BaseDeDatosExcepcion">Error al momento de realizar la consulta de las categorias</exception>
        /// <exception cref="Excepcion">Error inesperado</exception>
        public virtual List<Entidad> ObtenerCategoriasHabilitadas()
        {
            List<Entidad> listaCategorias;
            try
            {
                StoredProcedure("m9_ConsultarCategoriaHabilitada");
                leerDatos = base.Comando.ExecuteReader();
                listaCategorias = SetListaCategoria();
            }
            catch (NpgsqlException ex)
            {
                mensaje = "Error al realizar la consulta de las categorias habilitadas " +
                    this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + " || " + ex.Message;
                throw new BaseDeDatosExcepcion(ex, mensaje);
            }
            catch (Exception ex)
            {
                throw new Excepcion(ex, mensajeRegistry.ErrorInesperado);
            }
            finally
            {
                base.Desconectar();
            }
            return listaCategorias;
        }



        /// <summary>
        /// Metodo que obtiene la categoria dado un Id.
        /// </summary>
        /// <param name="entidad">Instacia Categoria que contiene el id por el cual se consultara.</param>
        /// <returns>IList con la categoria que se desea cuyo Id se encuentra en la instancia del parametro.</returns>
        /// <exception cref="BaseDeDatosExcepcion">Error al realizar al consulta de la categoria.</exception>
        /// <exception cref="Excepcion">Error inesperado</exception>
        public virtual List<Entidad> ObtenerCategoriaPorId(Entidad entidad)
        {
            categoria = (Categoria)entidad;
            List<Entidad> listaCategorias;
            try
            {
                StoredProcedure("m9_ObtenerCategoriaPorId");
                base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Id);
                leerDatos = base.Comando.ExecuteReader();
                listaCategorias = SetListaCategoria();
            }
            catch (NpgsqlException ex)
            {
                string mensaje = "Error al consultar categoria por id " +
                    this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + " || ca_id = " + categoria.Id + " || " + ex.Message;
                throw new BaseDeDatosExcepcion(ex, mensaje);
            }
            catch (Exception ex)
            {
                throw new Excepcion(ex, mensajeRegistry.ErrorInesperado);
            }
            finally
            {
                base.Desconectar();
            }
            return listaCategorias;
        }



        /// <summary>
        /// Metodo que obtiene una lista con todas las categorias de la base de datos.
        /// </summary>
        /// <returns>IList con las Categorias.</returns>
        /// <exception cref="BaseDeDatosExcepcion">Error al realizar al consulta de la categoria.</exception>
        /// <exception cref="Excepcion">Error inesperado</exception>
        public virtual List<Entidad> ObtenerTodasLasCategorias()
        {
            List<Entidad> listaCategorias;
            try
            {
                StoredProcedure("m9_devolverTodasCategorias");
                leerDatos = base.Comando.ExecuteReader();
                listaCategorias = SetListaCategoria();
            }
            catch (NpgsqlException ex)
            {
                mensaje = "Error al realizar la consulta de todas las categorias en " +
                 this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw new BaseDeDatosExcepcion(ex, mensaje);
            }
            catch (Exception ex)
            {
                throw new Excepcion(ex, mensajeRegistry.ErrorInesperado);
            }
            finally
            {
                base.Desconectar();
            }
            return listaCategorias;
        }



        /// <summary>
        /// Metodo que obtiene el id de una categoria dado el nombre de la misma.
        /// </summary>
        /// <param name="entidad">Instacia Categoria que contiene el nombre por el cual se consultara.</param>
        /// <returns>Categoria asociada al nombre colocado en la instacia por parametro.</returns>
        /// <exception cref="BaseDeDatosExcepcion">Error al realizar al consulta de la categoria.</exception>
        /// <exception cref="Exception">Error inesperado</exception>
        public virtual Entidad ObtenerIdCategoriaPorNombre(Entidad entidad)
        {
            categoria = (Categoria)entidad;
            try
            {
                int Superior = 0;
                StoredProcedure("m9_devolverid");
                base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, categoria.Nombre);
                leerDatos = base.Comando.ExecuteReader();

                if (leerDatos.Read())
                {
                    Int32.TryParse(leerDatos.GetValue(0).ToString(), out Superior);
                }

                if (Superior == 0)
                {
                    throw new ItemNoEncontradoExcepcion($"No se encontro la categoria con el nombre {categoria.Nombre}");
                }

                categoria.Id = Superior;
            }
            catch (NpgsqlException ex)
            {
                mensaje = "Error al realizar la consulta por el nombre de las categorias en " +
                this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + " ca_nombre = " + categoria.Nombre + "  || " + ex.Message;
                throw new BaseDeDatosExcepcion(ex, mensaje);
            }
            catch (Exception ex)
            {
                throw new Excepcion(ex, mensajeRegistry.ErrorInesperado);
            }
            finally
            {
                base.Desconectar();
            }
            return categoria;
        }



        /// <summary>
        /// Metodo que obtiene la lista de la categoria.
        /// </summary>
        /// <param name="categoria">Instacia Categoria que contiene el Id por el cual se consultara.</param>
        /// <exception cref="BaseDeDatosExcepcion">Error al realizar al consulta de la categoria.</exception>
        /// <exception cref="Exception">Error inesperado</exception>
        public virtual List<Entidad> ObtenerCategorias(Entidad categoria)
        {
            List<Entidad> listaCategorias = new List<Entidad>();
            try
            {

                if (categoria.Id == -1)
                {
                    StoredProcedure("m9_obtenerCategoriaTop");
                }
                else
                {
                    StoredProcedure("m9_obtenerCategoriaNoTop");
                    base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Id);
                }

                leerDatos = base.Comando.ExecuteReader();
                listaCategorias = SetListaCategoria();

            }
            catch (NpgsqlException ex)
            {
                mensaje = "Error al realizar la consulta de la categorias en " +
                    this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name +
                    " con el id:" + categoria.Id;
                throw new BaseDeDatosExcepcion(ex, mensaje);
            }
            catch (Exception ex)
            {
                throw new Excepcion(ex, mensajeRegistry.ErrorInesperado);
            }
            finally
            {
                base.Desconectar();
            }
            return listaCategorias;
        }


        #endregion metodosEspecificos

        private void VerificarExistenciaCategoria()
        {
            DAOCategoria daoc = FabricaDAO.CrearDAOCategoria();
            List<Entidad> Lcategoria = daoc.ObtenerCategoriaPorId(categoria);
            if (((Categoria)Lcategoria.First<Entidad>()).Nivel == categoria.Nivel)
            {
                ParametrosModificar(categoria);
            }
            else
            {
                List<Entidad> Listacategoria = daoc.ObtenerTodasLasCategorias();
                List<Entidad> hijos = Listacategoria.Where(item => ((Categoria)item).CategoriaSuperior == categoria.Id).ToList();
                if (hijos.Count == 0)
                {
                    ParametrosModificar(categoria);
                }
                else
                {
                    Exception ex = new Exception("Dependencias asociadas.");
                    mensaje = "Error de dependiencia en " + this.GetType().FullName + ". " +
                        MethodBase.GetCurrentMethod().Name + " donde id: " + categoria.Id +
                        " y nombre: " + categoria.Nombre + " ya que tiene dependencia";
                    throw new HijoConDePendenciaExcepcion(ex, mensaje);
                }
            }
        }
    }
}
