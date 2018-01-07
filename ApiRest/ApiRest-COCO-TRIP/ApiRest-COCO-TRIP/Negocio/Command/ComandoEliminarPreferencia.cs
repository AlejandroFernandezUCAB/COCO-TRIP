using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    public class ComandoEliminarPreferencia : Comando
    {

        private Entidad usuario;
        private Entidad categoria;
        private List<Categoria> listaCategoria;
        private DAO dao;

        public ComandoEliminarPreferencia(Entidad usuario, Entidad categoria)
        {
            this.usuario = usuario;
            this.categoria = categoria;
        }

        public override void Ejecutar()
        {
            
            try
            {
                dao = FabricaDAO.CrearDAOUsuario();
                ((DAOUsuario)dao).EliminarPreferencia(usuario, categoria.Id);
                listaCategoria = ((DAOUsuario)dao).BuscarPreferencias(usuario);
            }
            catch (Exception e)
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