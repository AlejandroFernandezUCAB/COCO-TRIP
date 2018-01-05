using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using Newtonsoft.Json.Linq;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    public class ComandoLTActualizarInformacion : Comando
    {
        Entidad _lugarTuristico;
        IDAOLugarTuristico iDAOLugar;
        private static Logger log = LogManager.GetCurrentClassLogger();

        public ComandoLTActualizarInformacion(JObject data)
        {
            // Deserializamos el objeto lugar turistico
            _lugarTuristico = data.ToObject<LugarTuristico>();
            // Inicializamos la fabrica dao
            iDAOLugar = FabricaDAO.CrearDAOLugarTuristico();
        }
        public override void Ejecutar()
        {
            try
            { 
                // Ejecutamos el dao para actualizar el lugar
                iDAOLugar.Actualizar(_lugarTuristico);
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