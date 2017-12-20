using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using System.Collections.Generic;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoConsultarCategoriaSelect:Comando
  {
    private ArrayList resultado = new ArrayList();
    private Comando com = FabricaComando.GetComandoConsultarCategoriaHabilitada();

    /// <summary>
    /// Metodo que nos permite obtener la lista de las categorias habilitadas mediante una consulta
    /// </summary>
    public override void Execute()
    {
      try
      {
        IList<Categoria> listCategories = null;
        com.Execute();
        JObject respuesta = (JObject)com.GetResult()[0];
        if (respuesta.Property("data") != null)
        {
          listCategories = respuesta["data"].ToObject<IList<Categoria>>();

        }

        else
        {
          listCategories = null;

        }
        resultado.Add(listCategories);
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
