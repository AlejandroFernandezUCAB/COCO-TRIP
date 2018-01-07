using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    public class ComandoBuscarPreferencias : Comando
    {

        private Entidad usuario;
        private List<Categoria> listaCategoria;
        private DAO dao;

        public ComandoBuscarPreferencias(Entidad usuario)
        {
            this.usuario = usuario;
            
        }


        public override void Ejecutar()
        {
            
             try
            {
                dao = FabricaDAO.CrearDAOUsuario();
                listaCategoria = ((DAOUsuario)dao).BuscarPreferencias(usuario);
            } catch
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