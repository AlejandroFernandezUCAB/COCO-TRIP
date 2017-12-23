using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Comando
{
  public class ComandoConsultarItinerarios : Comando
  {
    Usuario usuario;
    List<Entidad> lista;
    public ComandoConsultarItinerarios(int idUsuario)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      usuario.Id = idUsuario;
    }

    public override void Ejecutar()
    {
      lista = FabricaDAO.CrearDAOItinerario().ConsultarLista(usuario);
    }

    public override Entidad Retornar()
    {
      throw new NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      return lista;
    }
  }
}
