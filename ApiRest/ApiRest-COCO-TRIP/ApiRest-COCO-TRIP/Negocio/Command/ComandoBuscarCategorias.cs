using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{

    public class ComandoBuscarCategorias : Comando
    {

        private Entidad usuario;
        private List<Categoria> listaCategoria;
        private string preferencia;
        private DAO dao;

        public ComandoBuscarCategorias(Entidad usuario, string preferencia)
        {
            this.usuario = usuario;
            this.preferencia = preferencia;
        }
        public override void Ejecutar()
        {
            
            try
            {
                dao = FabricaDAO.CrearDAOUsuario();
                listaCategoria = ((DAOUsuario)dao).ObtenerCategorias(usuario, preferencia);
            }
            catch
            {
                listaCategoria = null;
            }
        }

        public override Entidad Retornar()
        {
            throw new NotImplementedException();
        }

        public override List<Entidad> RetornarLista()
        {
            throw new NotImplementedException();
        }

        public List<Categoria> RetornarListaCategoria()
        {
            return listaCategoria;
        }
    }
}