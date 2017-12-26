using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Web.Http;
using System.Net;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Procedimiento que se encarga de recoger los datos de
  /// la base de datos para visualizar la lista de amigos
  /// </summary>
  public class ComandoVisualizarListaAmigos : Comando
  {
    private List<Entidad> lista;
    private Usuario usuario;

    private DAO datos;

    public ComandoVisualizarListaAmigos(int id)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      usuario.Id = id;
    }

    public override void Ejecutar()
    {
      try
      {
        datos = FabricaDAO.CrearDAOAmigo();
        lista = datos.ConsultarLista(usuario);
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = "Id:" + usuario.Id;
        e.NombreMetodos = this.GetType().FullName;
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
    }

    public override Entidad Retornar()
    {
      throw new System.NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      return lista;
    }
  }
}
