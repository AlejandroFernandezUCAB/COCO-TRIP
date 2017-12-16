using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using BackOffice_COCO_TRIP.Datos.DAO;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoModificarCategoria : Comando
  {
    private Categoria categoria;
    DAO<JObject, Categoria> dao = FabricaDAO.GetDAOCategoria();
    
    public override void Execute()
    {
      try
      {
        DAO<JObject, Categoria> dao = FabricaDAO.GetDAOCategoria();
        DAOCategoria daoc = (DAOCategoria)dao;
        JObject respuesta = daoc.PutEditarCategoria(categoria);
/*       if (respuesta.Property("data") != null)
        {
          resultado.Add(respuesta["data"].ToObject<List<Categories>>());
          resultado.Add("Exito");
        }

        else
        {
          resultado.Add(new List<Categories>());
          resultado.Add("Ocurrio un error durante la comunicacion, revise su conexion a internet");
        }*/
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
      categoria = (Categoria)propiedad;
    }
  }
}
