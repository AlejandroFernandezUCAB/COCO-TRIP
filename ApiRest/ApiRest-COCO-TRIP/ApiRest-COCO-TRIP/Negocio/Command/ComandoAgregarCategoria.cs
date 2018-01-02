using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System;
using System.Collections.Generic;
using NLog;
using ApiRest_COCO_TRIP.Comun.Validaciones;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    public class ComandoAgregarCategoria : Comando
    {
        DAO dao = FabricaDAO.CrearDAOCategoria();
        private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();
        private string datosCategoria;
        private static Logger log = LogManager.GetCurrentClassLogger();
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
        /// <exception cref="NombreDuplicadoExcepcion"></exception>
        /// <exception cref="BaseDeDatosExcepcion"></exception>
        /// <exception cref="Excepcion"></exception>
        public override void Ejecutar()
        {
            try
            {
                ValidacionString.ValidarCategoria(entidad);
                datosCategoria = " Nombre: " + ((Categoria)entidad).Nombre + " - NivelCategoria: " + ((Categoria)entidad).Nivel;
                ((DAOCategoria)dao).Insertar(entidad);
                log.Info("Categoria nueva agregada con exito: " + datosCategoria);
            }
            catch (ParametrosInvalidosExcepcion e)
            {
                log.Error(e.Mensaje);
                throw e;
            }
            catch (NombreDuplicadoExcepcion e)
            {
                e.DatosAsociados = datosCategoria;
                log.Error(e.Mensaje + " || " + e.DatosAsociados);
                throw e;
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
