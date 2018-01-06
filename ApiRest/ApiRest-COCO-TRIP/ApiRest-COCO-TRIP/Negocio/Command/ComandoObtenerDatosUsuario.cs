using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    public class ComandoObtenerDatosUsuario : Comando

    {
        private Entidad entidad;
        private DAO dao;

        public ComandoObtenerDatosUsuario(Entidad entidad)
        {
            this.entidad = entidad;
        }

        public override void Ejecutar()
        {
            try
            {
                dao = FabricaDAO.CrearDAOUsuario();
                entidad = dao.ConsultarPorId(entidad);


            }
            catch(Exception e)
            {
                entidad = null;
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