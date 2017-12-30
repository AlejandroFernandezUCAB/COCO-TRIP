using System;
using System.Collections.Generic;
using System.Data;
//using ApiRest_COCO_TRIP.Models.Excepcion;
using System.Linq;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using Npgsql;
using NpgsqlTypes;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
    public class DAOCategoria : DAO
    {
        private NpgsqlParameter parametro;
        private NpgsqlDataReader leerDatos;
        private NpgsqlParameter AgregarParametro(NpgsqlDbType tipoDeDato, object valor)
        {
            var parametro = new NpgsqlParameter
            {
                NpgsqlDbType = tipoDeDato,
                Value = valor
            };

            return parametro;
        }
        private List<Entidad> lista;
        private Categoria categoria;

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        public DAOCategoria()
        {
            parametro = new NpgsqlParameter();
            lista = new List<Entidad>();
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
        /// Metodo Create, permite insertar una Entidad tipo categoria en la base de datos.
        /// </summary>
        /// <param name="objeto">instancia Catgoria que se desea insertar.</param>
        /// <exception cref="NombreDuplicadoException">Nombre duplicado al momento de insertar.</exception>
        /// <exception cref="BaseDeDatosExcepcion">Error al momento de agregar una categoria.</exception>
        /// <exception cref="Exception">Error inesperado.</exception>
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
                throw new NombreDuplicadoException(ex, this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name, )
                throw new NombreDuplicadoException($"Esta Categoria id:{categoria.Id} No se puede agregar con el nombre:{categoria.Nombre} Porque este nombre ya existe");
            }
            catch (NpgsqlException ex)
            {
                //TODO: utilizar la excepcion "BaseDeDatosExcepcion" que se encuentra en la carpeta Comun, precaucion con las  pruebas de Michel.
                BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
                {
                    DatosAsociados = $"ID : {categoria.Id}, ESTATUS: {categoria.Estatus}",
                    Mensaje = $"Error al momento de agregar la catgoria {categoria.Id}"
                };
                throw bdException;
            }
            catch (Exception ex)
            {
                throw ex;
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
        /// <exception cref="NpgsqlException">Error al actualizar la categoria</exception>
        /// <exception cref="HijoConDePendenciaException">La categoria que intenta actualizar tiene dependencias.</exception>
        /// <exception cref="ArgumentNullException">Ocurre en el momento de utlizar el metodo .ToList()</exception>
        /// <exception cref="Exception">Error inesperado.</exception>
        public override void Actualizar(Entidad objeto)
        {
            categoria = (Categoria)objeto;
            int exitoso = 0;
            try
            {
                StoredProcedure("m9_ModificarCategoria");
                DAOCategoria daoc = FabricaDAO.CrearDAOCategoria();
                IList<Categoria> Lcategoria = daoc.ObtenerCategoriaPorId(categoria);

                if (Lcategoria.First<Categoria>().Nivel == categoria.Nivel)
                {
                    ParametrosModificar(categoria);
                }
                else
                {
                    //categories = listaCategorias.Where(s => s.Id == id).First();        <------- Borrar? TODO
                    IList<Categoria> Listacategoria = daoc.ObtenerTodasLasCategorias();
                    //TODO: Una variable con "var" revisar.
                    var hijos = Listacategoria.Where(item => item.CategoriaSuperior == categoria.Id).ToList();
                    if (hijos.Count == 0)
                    {
                        ParametrosModificar(categoria);
                    }
                    else
                    {
                        //TODO: Cambiar "hijos" por "dependecias".
                        //precaucion con las pruebas de Michel usa estos mensajes. 
                        //Revisar constructor, no recibe una Excepción"
                        throw new HijoConDePendenciaException($"Esta categoria id:{categoria.Id} nombre:{categoria.Nombre} tiene hijos");
                    }
                }
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
                throw new NombreDuplicadoException($"Esta Categoria id:{categoria.Id} No se puede agregar con el nombre:{categoria.Nombre} Porque este nombre ya existe");
            }
            catch (NpgsqlException ex)
            {
                //TODO: utilizar la excepcion "BaseDeDatosExcepcion" que se encuentra en la carpeta Comun, precaucion con las  pruebas de Michel.
                BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
                {
                    DatosAsociados = $" ID : {categoria.Id}, NOMBRE : {categoria.Nombre}, DESCRIPCION : {categoria.Descripcion}, CATEGORIASUPERIOR : {categoria.CategoriaSuperior}, NIVEL : {categoria.Nivel} ",
                    Mensaje = $"Error al momento de actualizar la catgoria {categoria.Id}"
                };
                throw bdException;

            }
            catch (ArgumentNullException ex) //Esto ocurre en el momento de utilizar el metodo .ToList().
            {
                //TODO: Revisar luego, hace conflicto las excepciones de Comun y Models
                //ArgumentoNuloExcepcion ANE = new ArgumentoNuloExcepcion(ex);
                //throw ANE;
                throw ex;
            }
            catch (Exception ex)
            {
                //TODO: Cambiar por la personalizada que se encuentra en comun.
                throw ex;
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
        /// <exception cref="Exception">Error inesperado.</exception>
        public void ActualizarEstado(Entidad objeto)
        {
            categoria = (Categoria)objeto;
            int exitoso = 0; //TODO: Esta variable hace falta?
            try
            {
                StoredProcedure("m9_actualizarEstatusCategoria");
                base.Comando.Parameters.AddWithValue(NpgsqlDbType.Boolean, categoria.Estatus);
                base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Id);

                exitoso = base.Comando.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                //TODO: utilizar la excepcion "BaseDeDatosExcepcion" que se encuentra en la carpeta Comun, precaucion con las  pruebas de Michel.
                BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
                {
                    DatosAsociados = $" ID : {categoria.Id}, ESTATUS: {categoria.Estatus}",
                    Mensaje = $"Error al momento de actualizar la catgoria {categoria.Id}"
                };
                throw bdException;
            }
            catch (Exception ex)
            {
                //TODO: Cambiar por la excepcion personalizada que se encuentra en Comun.
                throw ex;
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

        //Metodos extra
        //TODOS LOS DE CONSULTA ESTAN ACA PORQUE NO CUADRAN CON LOS GENERALES
        /// <summary>
        /// Metodo que lee los datos de la categoria
        /// </summary>
        /// <returns>IList con las categorias que se encuentran en el atributo "leerDatos"</returns>
        private IList<Categoria> SetListaCategoria()
        {
            IList<Categoria> listaCategorias = new List<Categoria>();
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
                listaCategorias[listaCategorias.Count - 1].CategoriaSuperior = Superior;
            }
            return listaCategorias;
        }

        /// <summary>
        /// EndPoint para obtener las categorias hijas a partir de una categoria padre dado un ID,
        /// si el id no viene en la peticion se devuelve las categorias padres absolutas
        /// </summary>
        /// <returns>IList de las categorias habilitadas</returns>
        /// <exception cref="BaseDeDatosExcepcion">Error al momento de realizar la consulta de las categorias</exception>
        /// <exception cref="Exception">Error inesperado</exception>
        public IList<Categoria> ObtenerCategoriasHabilitadas()
        {
            IList<Categoria> listaCategorias;
            try
            {
                StoredProcedure("m9_ConsultarCategoriaHabilitada");
                leerDatos = base.Comando.ExecuteReader();
                listaCategorias = SetListaCategoria();
            }
            catch (NpgsqlException ex)
            {
                BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
                {
                    Mensaje = $"Error al momento de buscar las todas categorias"
                };
                throw bdException;
            }
            catch (Exception ex)
            {
                //TODO: Cambiar por la excepcion personalizada que se encuentra en la carpeta comun.
                throw ex;
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
        /// <exception cref="Exception">Error inesperado</exception>
        public IList<Categoria> ObtenerCategoriaPorId(Entidad entidad)
        {
            categoria = (Categoria)entidad;
            IList<Categoria> listaCategorias;
            try
            {
                StoredProcedure("m9_ObtenerCategoriaPorId");
                base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Id);
                leerDatos = base.Comando.ExecuteReader();
                listaCategorias = SetListaCategoria();
            }
            catch (NpgsqlException ex)
            {
                BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
                {
                    Mensaje = $"Error al momento de buscar las todas categorias"
                };
                throw bdException;
            }
            catch (Exception ex)
            {
                //TODO: Cambiar por la excepcion personalizada que se encuentra en la carpeta comun.
                throw ex;
            }
            finally
            {
                base.Desconectar();
            }
            return listaCategorias;
        }

        /// <summary>
        /// Metodo que obtiene una lista con todas las categorias.
        /// </summary>
        /// <returns>IList con las Categorias.</returns>
        /// <exception cref="BaseDeDatosExcepcion">Error al realizar al consulta de la categoria.</exception>
        /// <exception cref="Exception">Error inesperado</exception>
        public IList<Categoria> ObtenerTodasLasCategorias()
        {
            IList<Categoria> listaCategorias;
            try
            {
                StoredProcedure("m9_devolverTodasCategorias");
                leerDatos = base.Comando.ExecuteReader();
                listaCategorias = SetListaCategoria();
            }
            catch (NpgsqlException ex)
            {
                BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
                {
                    Mensaje = $"Error al momento de buscar las todas categorias"
                };
                throw bdException;
            }
            catch (Exception ex)
            {
                //TODO: Cambiar por la excepcion personalizada que se encuentra en la carpeta comun.
                throw ex;
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
        public Entidad ObtenerIdCategoriaPorNombre(Entidad entidad)
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
                    throw new ItemNoEncontradoException($"No se encontro la categoria con el nombre {categoria.Nombre}");
                }

                categoria.Id = Superior;
            }
            catch (NpgsqlException ex)
            {
                BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
                {
                    Mensaje = $"Error al momento de buscar el id de una categoria a partir del nombre"
                };
                throw bdException;
            }
            catch (Exception ex)
            {
                //TODO: Cambiar por la excepcion personalizada que se encuentra en la carpeta comun.
                throw ex;
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
        public IList<Categoria> ObtenerCategorias(Entidad categoria)
        {
            IList<Categoria> listaCategorias = new List<Categoria>();
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
                BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
                {
                    DatosAsociados = $" ID : {categoria.Id}",
                    Mensaje = $"Error al momento de buscar las categorias"
                };
                throw bdException;
            }
            catch (Exception ex)
            {
                //TODO: Cambiar por la excepcion personalizada que se encuentra en la carpeta comun.
                throw ex;
            }
            finally
            {
                base.Desconectar();
            }
            return listaCategorias;
        }
    }
}
