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

        public ComandoModificarDatosUsuario(Entidad entidad)
        {
            this.entidad = entidad;
        }

        public override void Ejecutar()
        {
            dao = FabricaDAO.CrearDAOUsuario();
            Entidad usuario = FabricaEntidad.CrearEntidadUsuario(((Usuario)entidad).Nombre, ((Usuario)entidad).Apellido, ((Usuario)entidad).NombreUsuario, ((Usuario)entidad).FechaNacimiento, ((Usuario)entidad).Genero);
            entidad.Id = ((DAOUsuario)dao).ConsultarPorNombre(entidad).Id;

            usuario.Id = entidad.Id;
            if (entidad.Id <= 0)
            {
                entidad = null;
                return;
            }
            else
            {
                dao.Actualizar(usuario);
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