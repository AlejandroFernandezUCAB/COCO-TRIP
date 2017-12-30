using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoAgregarCategoria : Comando
  {
        private Entidad categoria = FabricaEntidad.GetCategoria();
        IDAOCategoria dao = FabricaDAO.GetDAOCategoria();
        private ArrayList resultado = new ArrayList();

        public override void Execute()
        {
            try
            {
                JObject respuesta = dao.Post(categoria);
                resultado.Add(respuesta);
            }
            catch (Exception e)
            {
                //Agregar excepciones personalizadas 
                throw e;
            }
        }

        public override ArrayList GetResult()
        {
          return resultado;
        }


        public override void SetPropiedad(object propiedad)
        {
          categoria = (Categoria)propiedad;
        }
  }
}
