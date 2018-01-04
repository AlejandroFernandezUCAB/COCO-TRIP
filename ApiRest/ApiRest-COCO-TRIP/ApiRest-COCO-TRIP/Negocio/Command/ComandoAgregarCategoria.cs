using System.Collections.Generic;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Comun.Validaciones;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using NLog;

/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///     Comando para insertar una categorias.
/// </summary>
namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Comando que tiene como funcion insertar una categora a la base de datos.
    /// </summary>
    public class ComandoAgregarCategoria : Comando
    {
        private IDAOCategoria dao = FabricaDAO.CrearDAOCategoria();
        private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();
        private string datosCategoria;
        private static Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        /// <param name="entidad">instacia Categoria que se desea insertar.</param>
        public ComandoAgregarCategoria(Entidad entidad)
        {
            this.entidad = entidad;
        }

        /// <summary>
        /// Metodo que ejecuta la accion del comando, agrega una nueva Categoria.
        /// </summary>
        /// <exception cref="NombreDuplicadoExcepcion">Nombre duplicado al momento de insertar.</exception>
        /// <exception cref="BaseDeDatosExcepcion">Error al realizar la operacion a la base de datos.</exception>
        /// <exception cref="Excepcion">Error inesperado</exception>
        public override void Ejecutar()
        {
            try
            {
                ValidacionString.ValidarCategoria(entidad);
                datosCategoria = " Nombre: " + ((Categoria)entidad).Nombre + " - NivelCategoria: " + ((Categoria)entidad).Nivel;
                ((DAOCategoria)dao).Insertar(entidad);
                log.Info("Categoria nueva agregada con exito: " + datosCategoria);
            }
            catch (ParametrosInvalidosExcepcion e)
            {
                log.Error(e.Mensaje);
                throw e;
            }
            catch (NombreDuplicadoExcepcion e)
            {
                e.DatosAsociados = datosCategoria;
                log.Error(e.Mensaje + " || " + e.DatosAsociados);
                throw e;
            }
            catch (BaseDeDatosExcepcion e)
            {
                e.DatosAsociados = datosCategoria;
                log.Error(e.Mensaje + " || " + e.DatosAsociados);
                throw e;
            }
            catch (Excepcion e)
            {
                e.DatosAsociados = datosCategoria;
                log.Error(e.Mensaje + " || " + e.DatosAsociados);
                throw e;
            }
        }

        /// <summary>
        /// Metodo que retorna la respuesta del metodo ejecutar().
        /// </summary>
        /// <returns>Una instacia categoria</returns>
        /// <exception cref="System.NotImplementedException">Metodo No implementado</exception>
        public override Entidad Retornar()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Metodo que retorna una lista de la respuesta del metodo ejecutar().
        /// </summary>
        /// <returns>Una lista de categoria</returns>
        /// <exception cref="System.NotImplementedException">Metodo No implementado</exception>
        public override List<Entidad> RetornarLista()
        {
            throw new System.NotImplementedException();
        }

    }
}
