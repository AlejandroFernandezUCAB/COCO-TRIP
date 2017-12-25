using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Web.Http;
using System.Net;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Metodo para obtener el usuario lider
  /// </summary>
  public class ComandoConsultarLider : Comando
  {
    private Usuario usuario;
    private Grupo grupo;

    private DAOGrupo datos;

    public ComandoConsultarLider(int idGrupo)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      grupo = FabricaEntidad.CrearEntidadGrupo();

      grupo.Id = idGrupo;
    }

    public override void Ejecutar()
    {
      try
      {
        datos = FabricaDAO.CrearDAOGrupo();
        usuario = (Usuario)datos.ConsultarLider(grupo);
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = "IdGrupo:" + grupo.Id;
        e.NombreMetodos = this.GetType().FullName;
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
    }

    public override Entidad Retornar()
    {
      return usuario;
    }

    public override List<Entidad> RetornarLista()
    {
      throw new System.NotImplementedException();
    }
  }

}
