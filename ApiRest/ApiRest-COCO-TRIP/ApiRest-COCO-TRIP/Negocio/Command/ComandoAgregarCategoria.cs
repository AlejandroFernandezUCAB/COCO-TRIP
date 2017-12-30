using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System;
using System.Collections.Generic;
using System.Reflection;

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
        /// <exception cref="NombreDuplicadoExcepcion"></exception>
        /// <exception cref="BaseDeDatosExcepcion"></exception>
        /// <exception cref="Excepcion"></exception>
        public override void Ejecutar()
        {
            try
            {
                ((DAOCategoria)dao).Insertar(entidad);
                //dao.Insertar(entidad);
            }
            catch (NombreDuplicadoExcepcion ex)
            {
                string mensaje = "Error en duplicidad de nombre en " + 
                    this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw new NombreDuplicadoExcepcion(ex, mensaje);
            }
            catch (BaseDeDatosExcepcion ex)
            {
                string mensaje = "Error al realizar operacion con la base de datos en " +
                    this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw new BaseDeDatosExcepcion(ex, mensaje);
            }
            catch (Excepcion ex)
            {
                //TODO: Se puede borrar esto?
                //sql exception
                //null reference exception
                //TERMINAR
                string mensaje = "Error inesperado en " +
                    this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
                throw new Excepcion(ex, mensaje);
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
