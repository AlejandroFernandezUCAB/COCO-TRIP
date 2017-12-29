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
		private Entidad _horario;
		private List<Entidad> _foto;
		private Entidad _actividad;
		private IDAOLugarTuristico iDAOLugarTuristico;
		private IDAOFoto iDAOFoto;
		private IDAOHorario iDAOHorario;
		private IDAOActividad iDAOActividad;
		
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
			//Esta podria ser otra solucion, pero NOSE :'(
			_foto = ((LugarTuristico)_lugarTuristico).Foto;

			iDAOLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();
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

		/// <summary>
		/// Metodo para insertar en una lista, sacandolo por indice. Funciona para todos los list
		/// </summary>
		/// <param name="objeto">Este parametro hay que parsearlo antes de todo, para que sea un objeto 
		/// diferente cuando entre en el if</param>
		public void InsertarObjetoEnLista(Object objeto)
		{
			//Verifico que tipo de objeto es primero para que segun sea se vaya llenando
			if( Object.ReferenceEquals( objeto.GetType(), new List<Foto>()) )
			{
				Console.WriteLine("Hola");
			}
			
		}
	}
}