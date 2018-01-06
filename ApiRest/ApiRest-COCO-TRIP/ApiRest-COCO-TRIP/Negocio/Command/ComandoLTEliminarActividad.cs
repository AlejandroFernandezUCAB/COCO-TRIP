using System;
using System.Collections.Generic;
using System.Reflection;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
	/// Integrantes : Pedro Fernandez
	///				GianFranco Verrocchi
	public class ComandoLTEliminarActividad : Comando
    {
        Actividad _actividad;
        IDAOActividad _iDAOActividad;
        private static Logger log = LogManager.GetCurrentClassLogger();

        public ComandoLTEliminarActividad(Entidad actividad)
        {
            _actividad = (Actividad)actividad;
            _iDAOActividad = FabricaDAO.CrearDAOActividad();
        }

        public override void Ejecutar()
        {
            try
            {
                _iDAOActividad.Eliminar(_actividad);

            }
            catch (ReferenciaNulaExcepcion e)
            {
                log.Error(e.Mensaje);
                throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en: "
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
            }
            catch (CasteoInvalidoExcepcion e)
            {

                log.Error("Casteo invalido en:"
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
                throw new CasteoInvalidoExcepcion(e, "Casteo invalido en: "
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

            }
            catch (BaseDeDatosExcepcion e)
            {

                log.Error("Ocurrio un error en la base de datos en: "
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
                throw new BaseDeDatosExcepcion(e, "Ocurrio un error en la base de datos en: "
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

            }
            catch (Excepcion e)
            {

                log.Error("Ocurrio un error desconocido: "
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
                throw new Excepcion(e, "Ocurrio un error desconocido en: "
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

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

    }
}