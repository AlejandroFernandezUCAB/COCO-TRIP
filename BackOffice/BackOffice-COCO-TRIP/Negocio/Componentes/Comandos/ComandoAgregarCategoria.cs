using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoAgregarCategoria : Comando
  {
        private Entidad categoria = FabricaEntidad.GetCategoria();
        DAO<JObject, Categoria> dao = FabricaDAO.GetDAOCategoria();
        private ArrayList resultado = new ArrayList();

        public override void Execute()
        {
            try
            {
                JObject respuesta = ((DAOCategoria)dao).Post((Categoria)categoria);
                resultado.Add(respuesta);
            }
            catch (Exception e)
            {
                //Agregar excepciones personalizadas que haga horacio
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
