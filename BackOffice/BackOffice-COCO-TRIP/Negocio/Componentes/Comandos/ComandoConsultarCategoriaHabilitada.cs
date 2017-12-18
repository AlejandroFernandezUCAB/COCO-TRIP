using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using BackOffice_COCO_TRIP.Datos.DAO;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoConsultarCategoriaHabilitada:Comando
  {
    private Categoria categoria;
    private ArrayList resultado = new ArrayList();
    DAO<JObject, Categoria> dao = FabricaDAO.GetDAOCategoria();

    public override void Execute()
    {
      try
      {
        DAO<JObject, Categoria> dao = FabricaDAO.GetDAOCategoria();
        DAOCategoria daoc = (DAOCategoria)dao;
        JObject respuesta = daoc.GetCategoriasHabilitadas();
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
      return resultado;
    }


    public override void SetPropiedad(object propiedad)
    {
      throw new NotImplementedException();
    }
  }
}
