using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Models;
using BackOffice_COCO_TRIP.Models.Peticion;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoConsultarEventos : Comando
  {
    private int id;
    private ArrayList resultado = new ArrayList();
    public override void Execute()
    {
      try
      {
        DAO<JObject, Categoria> peticionCategoria = FabricaDAO.GetDAOCategoria();
        
        IDAOLocalidad peticionLocalidad = FabricaDAO.GetDAOLocalidad();
        JObject respuestaCategoria = ((DAOCategoria)peticionCategoria).GetCategoriasHabilitadas();
        JObject respuestaLocalidad = peticionLocalidad.GetAll();
        if (respuestaCategoria.Property("data") != null)
        {
          resultado.Add(respuestaCategoria["data"].ToObject<List<Categoria>>());
          resultado.Add("Exito en Categoria");
        }

        else
        {
          resultado.Add(new List<Categoria>());
          resultado.Add("Error en la comunicacion o No existen Categorias");
        }

        if (respuestaLocalidad.Property("dato") != null)
        {
          resultado.Add(respuestaLocalidad["dato"].ToObject<List<Localidad>>());
          resultado.Add("Exito en Localidad");
        }

        else
        {
          resultado.Add(new List<Localidad>());
          resultado.Add(" Error en la comunicacion o No existen localidades");
        }
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
      id = (int)propiedad;
    }
  }
}
