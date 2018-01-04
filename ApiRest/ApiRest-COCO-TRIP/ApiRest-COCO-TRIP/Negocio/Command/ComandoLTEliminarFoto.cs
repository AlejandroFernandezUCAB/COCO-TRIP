using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using NLog;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Comando para eliminar la foto de un lugar turistico
    /// </summary>
    public class ComandoLTEliminarFoto : Comando
    {
        private IDAOFoto iDAOFoto;
        private Entidad _foto;
        private static Logger log = LogManager.GetCurrentClassLogger();

        public ComandoLTEliminarFoto(JObject datos){
            _foto = datos.ToObject<Foto>();
            iDAOFoto = FabricaDAO.CrearDAOFoto();
        }

        public override void Ejecutar()
        {
            try
            {
                iDAOFoto.Eliminar(_foto);
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