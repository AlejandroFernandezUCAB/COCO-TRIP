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
		private IDAOLugarTuristico daoLugarTuristico;
		private IDAOFoto daoFoto;
		private IDAOHorario daoHorario;
		private IDAOActividad daoActividad;
		private IDAOLugarTuristicoCategoria daoCategoria;
		JObject _datos;

		/// <summary>
		/// Creo el comando con la lista de datos ya deseralizada
		/// </summary>
		/// <param name="datos">JSON de Lugar turistico</param>
		public ComandoAgregarLugarTuristico(JObject datos)
		{

			_lugarTuristico = FabricaEntidad.CrearEntidadLugarTuristico();
			_datos = datos;
			_lugarTuristico = _datos.ToObject<LugarTuristico>();

			// Esto se hara con los comandos...

			// ComandoLTAgregarFoto
			_foto = ((LugarTuristico)_lugarTuristico).Foto.ConvertAll(new Converter<Foto, Entidad>(ConvertListFoto));
			// ComandoLTAgregarHorario
			_horario = ((LugarTuristico)_lugarTuristico).Horario.ConvertAll(new Converter<Horario, Entidad>(ConvertListHorario));
			// ComandoLTAgregarActividad
			_actividad = ((LugarTuristico)_lugarTuristico).Actividad.ConvertAll(new Converter<Actividad, Entidad>(ConvertListActividad));
			
			
			_categoria = ((LugarTuristico)_lugarTuristico).Categoria.ConvertAll(new Converter<Categoria, Entidad>(ConvertListCategoria));
			_subCategoria = ((LugarTuristico)_lugarTuristico).SubCategoria.ConvertAll(new Converter<Categoria, Entidad>(ConvertListSubCategoria));

			daoLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();
			//daoCategoria = FabricaDAO.CrearDAOCategoria();
			daoFoto = FabricaDAO.CrearDAOFoto();
			daoHorario = FabricaDAO.CrearDAOHorario();
			daoActividad = FabricaDAO.CrearDAOActividad();

		}

		/// <summary>
		/// Inserta un lugar turistico en la bsae de datos
		/// </summary>
		public override void Ejecutar()
		{
			
			try
			{
				daoLugarTuristico.Insertar(_lugarTuristico);

				//En la siguiente linea se invoca al DAO para que devuelva la lista de todos los lugares turisticos,
				//Luego esta lista pasa a UltimoLugarTuristico y ese id que devuelve se lo pasa al lugar turistico anteriormente insertado.

				_lugarTuristico.Id = UltimoIdLugarTuristico( daoLugarTuristico.ConsultarTodaLaLista() );
					

				for(int i=0; i < _foto.Count; i++)
				{
					daoFoto.Insertar( _foto[i] , _lugarTuristico);
				}

				for (int i = 0; i < _horario.Count; i++)
				{
					daoHorario.Insertar(_horario[i], _lugarTuristico);
				}

				for (int i = 0; i < _actividad.Count; i++)
				{
					daoActividad.Insertar (_actividad[i], _lugarTuristico);
				}
				
				for (int i = 0; i < _categoria.Count; i++)
				{
					daoCategoria.Insertar (_categoria[i], _lugarTuristico);
				}

			}
			catch ( Exception e)
			{
				

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