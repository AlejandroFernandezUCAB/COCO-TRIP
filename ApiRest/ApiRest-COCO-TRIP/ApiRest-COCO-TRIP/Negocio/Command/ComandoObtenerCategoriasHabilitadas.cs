using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using NLog;
using ApiRest_COCO_TRIP.Comun.Excepcion;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Comando que tiene como funcion obtener todas las categorias habilitadas en la base de datos.
    /// </summary>
    public class ComandoObtenerCategoriasHabilitadas : Comando
    {
        DAO dao = FabricaDAO.CrearDAOCategoria();
        private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();
        private List<Entidad> resultado = new List<Entidad>();
        private static Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Ejecuta el comando al cual fue designado, Obtiene una lista de todas las categorias habilitadas.
        /// </summary>
        /// <exception cref="BaseDeDatosExcepcion">Error al realizar la operacion a la base de datos.</exception>
        /// <exception cref="Excepcion">Error inesperado</exception>
        public override void Ejecutar()
        {
            try
            {
                resultado = ((DAOCategoria)dao).ObtenerCategoriasHabilitadas();
                log.Info("Categorias habilitadas consultadas exitosamente ");
            }
            catch (BaseDeDatosExcepcion e)
            {
                log.Error(e.Mensaje);
                throw e;
            }
            catch (Excepcion e)
            {
                log.Error(e.Mensaje);
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
        /// Metodo que retorna una lista de categorias de la respuesta del metodo ejecutar().
        /// </summary>
        /// <returns>Una lista de categoria</returns>
        public override List<Entidad> RetornarLista()
        {
            return resultado;
        }
    }
}
