using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Reflection;
using NLog;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    public class ComandoLTAgregarCategoria : Comando
    {
		private Entidad _lugarTuristico;
		private List<Entidad> _categorias;
		private IDAOLugarTuristicoCategoria _daoCategoria;
		private static Logger log = LogManager.GetCurrentClassLogger();

		public ComandoLTAgregarCategoria( Entidad lugarTuristico)
		{
			_daoCategoria = FabricaDAO.CrearDAOLugarTuristico_Categoria();
			_lugarTuristico = lugarTuristico;
			_categorias = ((LugarTuristico)_lugarTuristico).Categoria.ConvertAll(new Converter<Categoria, Entidad>(ConvertListCategoria));
		}

        public override void Ejecutar()
        {
			try
			{

				for (int i = 0; i < _categorias.Count; i++)
				{
					_daoCategoria.Insertar(_categorias[i], _lugarTuristico);
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

		/// <summary>
		/// Convierte el objeto Categoria a Entidad
		/// </summary>
		/// <param name="input">Objeto Foto</param>
		/// <returns>Objeto Entidad</returns>
		private Entidad ConvertListCategoria(Categoria input)
		{
			return input;
		}
	}
}