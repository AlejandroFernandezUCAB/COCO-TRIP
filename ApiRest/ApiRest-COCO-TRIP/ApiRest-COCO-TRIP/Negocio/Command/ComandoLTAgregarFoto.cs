using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Comando que permite agregar una lista de fotos y asociarlas a un lugar
    /// </summary>
    public class ComandoLTAgregarFoto : Comando
    {
        private IDAOFoto iDAOFoto;
        private DAOFoto _dao;
        private List<Entidad> _foto;
        private LugarTuristico _lugarTuristico;

        public ComandoLTAgregarFoto(Entidad lugarTuristico)
        {
            iDAOFoto = FabricaDAO.CrearDAOFoto();
            _foto = ((LugarTuristico)_lugarTuristico).Foto.ConvertAll(new Converter<Foto, Entidad>(ConvertListFoto));
            _lugarTuristico = (LugarTuristico)lugarTuristico;
        }

        public override void Ejecutar()
        {
            try
            {
                // Insercion y asociacion de las fotos
				for(int i=0; i <=_foto.Count; i++)
				{
					iDAOFoto.Insertar( _foto[i] , _lugarTuristico );
				}
            }
            catch (System.Exception)
            {
                
                throw;
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