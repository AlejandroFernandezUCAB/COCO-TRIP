using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    public class ComandoEstadoCategoria : Comando
    {
        DAO dao = FabricaDAO.CrearDAOCategoria();
        private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();
        private string datosCategoria;
        private static Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entidad">Entidad con la que se desea proceder</param>
        public ComandoEstadoCategoria(Entidad entidad)
        {
            this.entidad = entidad;
        }

        /// <summary>
        /// Ejecuta el comando al cual fue designado, actualiza estado de la categoria
        /// </summary>
        /// <exception cref="Exception"></exception>
        public override void Ejecutar()
        {
            try
            {
                datosCategoria = " ID: " + ((Categoria)entidad).Id + " Nombre: " + ((Categoria)entidad).Nombre + " - Estado: " + ((Categoria)entidad).Estatus;
                ((DAOCategoria)dao).ActualizarEstado(entidad);
                log.Info("Estado de Categoria actualizado con exito: " + datosCategoria);
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
        /// No implementado
        /// </summary>
        /// <returns>Entidad que fue utilizada en metodo ejecutar</returns>
        /// <exception cref="NotImplementedException"
        public override Entidad Retornar()
        {
            throw new System.NotImplementedException();
        }

        public override List<Entidad> RetornarLista()
        {
            throw new System.NotImplementedException();
        }


    }
}
