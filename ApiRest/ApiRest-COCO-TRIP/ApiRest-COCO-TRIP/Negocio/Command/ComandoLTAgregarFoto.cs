using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Reflection;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Comando que permite agregar una lista de fotos y asociarlas a un lugar
    /// </summary>
    public class ComandoLTAgregarFoto : Comando
    {
        private IDAOFoto iDAOFoto;
        private List<Entidad> _foto;
        private Entidad _lugarTuristico;
		private static Logger log = LogManager.GetCurrentClassLogger();

		public ComandoLTAgregarFoto(Entidad lugarTuristico)
        {

			_lugarTuristico = lugarTuristico;
            iDAOFoto = FabricaDAO.CrearDAOFoto();
			_lugarTuristico = (LugarTuristico)lugarTuristico;
			_foto = ((LugarTuristico)lugarTuristico).Foto.ConvertAll(new Converter<Foto, Entidad>(ConvertListFoto));

        }

        public override void Ejecutar()
        {
            try
            {
                // Insercion y asociacion de las fotos
				for(int i=0; i < _foto.Count; i++)
				{
					iDAOFoto.Insertar( _foto[i] , _lugarTuristico );
				}
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

        private Entidad ConvertListFoto(Foto input)
		{
			return input;
		}
    }
}