using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.Singleton;
using NLog;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Comando que permite consultar los eventos de cierta categoria.
    /// </summary>
    public class ComandoConsultarEventosPorCategoria : Comando
    {
        private Entidad categoria;
        private IDAOEvento daoEvento;
        private DAO daoCategoria;
        private List<Entidad> eventos;
        private static Logger log;

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        /// <param name="id">id de la categoria con la que se desea consultar los eventos.</param>
        public ComandoConsultarEventosPorCategoria(int id)
        {
            this.categoria = FabricaEntidad.CrearEntidadCategoria();
            this.categoria.Id = id;
            daoEvento = FabricaDAO.CrearDAOEvento();
            daoCategoria = FabricaDAO.CrearDAOCategoria();
            log = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Metodo Ejecutar, realiza la logica del negocio para consultar los eventos de cierta categoría.
        /// </summary>
        public override void Ejecutar()
        {
            try
            {
                eventos = daoEvento.ConsultarListaPorCategoria(categoria);
                List<Categoria> categorias = RetornarHijos(categoria);
                foreach (Categoria cate in categorias)
                {
                    foreach (Evento ev in daoEvento.ConsultarListaPorCategoria(cate))
                    {
                        eventos.Add(ev);
                    }
                }
                log.Info("Ejecutado el comando");
            }

            catch (BaseDeDatosExcepcion e)
            {
                log.Error(e.Message);
                throw e;
            }
            catch (CasteoInvalidoExcepcion e)
            {
                log.Error(e.Message);
                throw e;
            }
               catch (OperacionInvalidaExcepcion e)
            {
                log.Error(e.Message);
                throw e;
            }
            catch (Exception e)

            {
                log.Error(e.Message);
                throw e;
            }
        }

        /// <summary>
        /// Metodo Retornar, obtiene una entidad como resultado de la ejecucion del comando.
        /// </summary>
        public override Entidad Retornar()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metodo Retornar Lista, obtiene una lista de entidades como resultado de la ejecucion del comando.
        /// </summary>
        public override List<Entidad> RetornarLista()
        {
            return eventos;
        }
        /// <summary>
        /// Metodo RetornarHijos, retorna los hijos de una categoria.
        /// </summary>
        /// <param name="papa"> Entidad(categoria) a obtener sus hijos </param>
        private List<Categoria> RetornarHijos(Entidad papa)
        {
            List<Categoria> hijos = new List<Categoria>();
            foreach (Categoria hijo in ((DAOCategoria)daoCategoria).ObtenerCategorias(papa))
            {
                hijos.Add(hijo);
                hijos.AddRange(RetornarHijos(hijo));
            }
            return hijos;
        }
    }
}
