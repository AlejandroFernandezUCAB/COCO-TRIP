using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System;
using System.Collections.Generic;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    public class ComandoAgregarCategoria : Comando
    {
        DAO dao = FabricaDAO.CrearDAOCategoria();
        private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        /// <param name="entidad">instacia Categoria que se desea insertar.</param>
        public ComandoAgregarCategoria(Entidad entidad)
        {
            this.entidad = entidad;
        }

        /// <summary>
        /// Metodo que ejecuta la accion del comando.
        /// </summary>
        public override void Ejecutar()
        {
            try
            {
                ((DAOCategoria)dao).Insertar(entidad);
                dao.Insertar(entidad);
            }
            //catch (NombreDuplicadoExcepcion ex)
            //{
            //
            //}
            catch (Exception e)
            {
                //sql exception
                //null reference exception
                //TERMINAR
                throw e;
            }
        }

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
