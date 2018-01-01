using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using NLog;
using ApiRest_COCO_TRIP.Comun.Excepcion;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Comando que tiene como funcion obtener una categoria dado el Id en la base de datos.
    /// </summary>
    public class ComandoObtenerCategoriaPorId:Comando
    {
        DAO dao = FabricaDAO.CrearDAOCategoria();
        private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();
        private List<Entidad> resultado = new List<Entidad>();
        private string datosCategoria;
        private static Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        /// <param name="entidad">Entidad con la que se desea proceder</param>
        public ComandoObtenerCategoriaPorId(Entidad entidad)
        {
            this.entidad = entidad;
        }

        /// <summary>
        /// Ejecuta el comando al cual fue designado, obtiene una categoria dado el Id.
        /// </summary>
        /// <exception cref="BaseDeDatosExcepcion">Error al realizar la operacion a la base de datos.</exception>
        /// <exception cref="Excepcion">Error inesperado</exception>
        public override void Ejecutar()
        {
            try
            {
                datosCategoria = " ID: " + ((Categoria)entidad).Id;
                resultado = ((DAOCategoria)dao).ObtenerCategoriaPorId(entidad);
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
