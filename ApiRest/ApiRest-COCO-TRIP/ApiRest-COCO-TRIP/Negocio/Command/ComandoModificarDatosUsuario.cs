using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    public class ComandoModificarDatosUsuario : Comando
    {
        private Entidad entidad;
        private DAO dao;
        private static Logger log = LogManager.GetCurrentClassLogger();

        public ComandoModificarDatosUsuario(Entidad entidad)
        {
            this.entidad = entidad;
        }

        public override void Ejecutar()
        {
            dao = FabricaDAO.CrearDAOUsuario();
            entidad.Id = ((DAOUsuario)dao).ConsultarPorNombre(entidad).Id;

            if (entidad.Id <= 0)
            {
                entidad = null;
                log.Error("No se encontro el usuario ");
                return;
            }
            else
            {
                dao.Actualizar(entidad);
                log.Info("Se pudo actualizar los datos del usuario");
            }
        }

        public override Entidad Retornar()
        {
            return entidad;
        }

        public override List<Entidad> RetornarLista()
        {
            throw new NotImplementedException();
        }
    }
}