using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Datos.Singleton;

namespace ApiRest_COCO_TRIP.Negocio.Command
{	
	/// <summary>
	/// Comando que permite agregar el lugares turistico con su foto, actividad y horario
	/// </summary>
	public class ComandoAgregarLugarTuristico : Comando
	{

		private Entidad _lugarTuristico;
		private List<Entidad> _foto;
		private List<Entidad> _horario; 
		private List<Entidad> _actividad; 
		private List<Entidad> _categoria; 
		private List<Entidad> _subCategoria; 
		private IDAOLugarTuristico iDAOLugarTuristico;
		private IDAOFoto iDAOFoto;
		private IDAOHorario iDAOHorario;
		private IDAOActividad iDAOActividad;
		private IDAOCategoria iDAOCategoria;
		JObject _datos;
		private Log log;

		/// <summary>
		/// Creo el comando con la lista de datos ya deseralizada
		/// </summary>
		/// <param name="datos">JSON de Lugar turistico</param>
		public ComandoAgregarLugarTuristico(JObject datos)
		{

			_lugarTuristico = FabricaEntidad.CrearEntidadLugarTuristico();
			_datos = datos;
			_lugarTuristico = _datos.ToObject<LugarTuristico>();

			_foto = ((LugarTuristico)_lugarTuristico).Foto.ConvertAll(new Converter<Foto, Entidad>(ConvertListFoto));
			_horario = ((LugarTuristico)_lugarTuristico).Horario.ConvertAll(new Converter<Horario, Entidad>(ConvertListHorario));
			_actividad = ((LugarTuristico)_lugarTuristico).Actividad.ConvertAll(new Converter<Actividad, Entidad>(ConvertListActividad));
			_categoria = ((LugarTuristico)_lugarTuristico).Categoria.ConvertAll(new Converter<Categoria, Entidad>(ConvertListCategoria));
			_subCategoria = ((LugarTuristico)_lugarTuristico).SubCategoria.ConvertAll(new Converter<Categoria, Entidad>(ConvertListSubCategoria));

			iDAOLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();
			iDAOCategoria = FabricaDAO.CrearDAOCategoria();
			iDAOFoto = FabricaDAO.CrearDAOFoto();
			iDAOHorario = FabricaDAO.CrearDAOHorario();
			iDAOActividad = FabricaDAO.CrearDAOActividad();

			log = Log.ObtenerInstancia();

		}

		/// <summary>
		/// Inserta un lugar turistico en la bsae de datos
		/// </summary>
		public override void Ejecutar()
		{
			
			try
			{
				iDAOLugarTuristico.Insertar(_lugarTuristico);

				//En la siguiente linea se invoca al DAO para que devuelva la lista de todos los lugares turisticos,
				//Luego esta lista pasa a UltimoLugarTuristico y ese id que devuelve se lo pasa al lugar turistico anteriormente insertado.

				_lugarTuristico.Id = UltimoIdLugarTuristico( iDAOLugarTuristico.ConsultarTodaLaLista() );
					

				for(int i=0; i <=_foto.Count; i++)
				{
					iDAOFoto.Insertar( _foto[i] );
				}

				for (int i = 0; i <= _horario.Count; i++)
				{
					iDAOHorario.Insertar(_horario[i]);
				}

				for (int i = 0; i <= _actividad.Count; i++)
				{
					iDAOActividad.Insertar(_actividad[i]);
				}

				/*
				for (int i = 0; i <= _categoria.Count; i++)
				{
					//iDAOCategoria.Insertar(_horario[i]);
				}

				for (int i = 0; i <= _horario.Count; i++)
				{
					//iDAOHorario.Insertar(_horario[i]);
				}
				*/

			}
			catch ( Exception e)
			{
				log.ApiRestError( e.ToString(), "Error");

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

		private Entidad ConvertListSubCategoria(Categoria input)
		{
			return input;
		}

		private Entidad ConvertListCategoria(Categoria input)
		{
			return input;
		}

		private Entidad ConvertListActividad(Actividad input)
		{
			return input;
		}

		private Entidad ConvertListHorario(Horario input)
		{
			return input;
		}

		/// <summary>
		/// Se busca el ID del ultimo objeto en la lista
		/// </summary>
		/// <param name="lugaresTuristicos">Lista de lugares turisticos</param>
		/// <returns>El ultimo id del ultimo objeto de la lista</returns>
		private int UltimoIdLugarTuristico(List<Entidad> lugaresTuristicos)
		{
			int cantidadDeLugares = lugaresTuristicos.Count - 1;
			return lugaresTuristicos[cantidadDeLugares].Id;
		}
	}
}