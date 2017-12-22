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
    private Categoria categoria;

        DAO<JObject, Categoria> dao = FabricaDAO.GetDAOCategoria();
        private ArrayList resultado = new ArrayList();

        public override void Execute()
        {
            try
            {
                JObject respuesta = ((DAOCategoria)dao).PutEditarEstado(categoria);
                resultado.Add(respuesta);
            }
            catch (Exception e)
            {
                //TERMINAR
                throw e;
            }


        }

    public override ArrayList GetResult()
    {
      throw new NotImplementedException();
    }

    public override void SetPropiedad(object propiedad)
    {
      throw new NotImplementedException();
    }
  }
}
