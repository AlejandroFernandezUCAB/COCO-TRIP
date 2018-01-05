using System.Collections.Generic;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Comun.Validaciones;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using NLog;

/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///     Comando para modificar una categoria.
/// </summary>
namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Comando que tiene como funcion modificar una categora de la base de datos.
    /// </summary>
    public class ComandoModificarCategoria : Comando
    {
        private IDAOCategoria dao = FabricaDAO.CrearDAOCategoria();
        private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();
        private string datosCategoria;
        private static Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        /// <param name="entidad">instacia Categoria que se desea insertar.</param>
        public ComandoModificarCategoria(Entidad entidad)
        {
            this.entidad = entidad;
        }

        /// <summary>
        /// Metodo que ejecuta la accion del comando, modifica una Categoria.
        /// </summary>        
        /// <exception cref="ParametrosInvalidosExcepcion">Los parametros de la instancia no cumple con las condiciones para la informacion de una categoria</exception>
        /// <exception cref="BaseDeDatosExcepcion">Error al actualizar la categoria</exception>
        /// <exception cref="NombreDuplicadoExcepcion">Error en duplicidad en el nombre de la categoria que intenta actualizar.</exception>
        /// <exception cref="HijoConDePendenciaExcepcion">La categoria que intenta actualizar tiene dependencias.</exception>
        /// <exception cref="ArgumentoNuloExcepcion">Error al utilizar el ToList, para convertir la lista a Categorias.</exception>
        /// <exception cref="Excepcion">Error inesperado.</exception>
        public override void Ejecutar()
        {
            try
            {
                ValidacionString.ValidarCategoria(entidad);
                datosCategoria = " ID: " + ((Categoria)entidad).Id + " Nombre: " + ((Categoria)entidad).Nombre;
                ((DAOCategoria)dao).Actualizar(entidad);
                log.Info("Categoria nueva modificada con exito: " + datosCategoria);
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
            catch (ArgumentoNuloExcepcion e)
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
            catch (HijoConDePendenciaExcepcion e)
            {
                log.Error(e.Mensaje);
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
