using System.Collections.Generic;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using NLog;

/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///     Comando para obtener las categorias dados un nombre.
/// </summary>
namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Comando que tiene como funcion obtener una categoria dado el nombre en la base de datos.
    /// </summary>
    public class ComandoObtenerCategoriaPorNombre : Comando
    {
        private DAO dao = FabricaDAO.CrearDAOCategoria();
        private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();
        private Entidad resultado;
        private string datosCategoria;
        private static Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        /// <param name="entidad">Entidad con la que se desea proceder</param>
        public ComandoObtenerCategoriaPorNombre(Entidad entidad)
        {
            this.entidad = entidad;
        }

        /// <summary>
        /// Ejecuta el comando al cual fue designado, actualiza estado de la categoria.
        /// </summary>
        /// <exception cref="BaseDeDatosExcepcion">Error al realizar la operacion a la base de datos.</exception>
        /// <exception cref="Excepcion">Error inesperado</exception>
        public override void Ejecutar()
        {
            try
            {
                datosCategoria = " Nombre: " + ((Categoria)entidad).Nombre;
                resultado = ((DAOCategoria)dao).ObtenerIdCategoriaPorNombre(entidad);
                log.Info("Categoria consultada con exito: " + datosCategoria);
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
        public override Entidad Retornar()
        {
            return resultado;
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
