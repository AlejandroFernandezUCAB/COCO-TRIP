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
  /// Metodo para obtener la lista de amigos que no estan agregados al grupo
  /// </summary>
  public class ComandoConsultarMiembroSinGrupo : Comando
  {
    private Grupo grupo;
    private Usuario usuario;
    private List<Entidad> lista;

    private DAOGrupo datos;

    public ComandoConsultarMiembroSinGrupo(int idGrupo, int idUsuario)
    {
      grupo = FabricaEntidad.CrearEntidadGrupo();
      usuario = FabricaEntidad.CrearEntidadUsuario();

      grupo.Id = idGrupo;
      usuario.Id = idUsuario;
    }

    public override void Ejecutar()
    {
      try
      {
        datos = FabricaDAO.CrearDAOGrupo();
        lista = datos.ConsultarMiembrosSinGrupo(grupo, usuario);
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = "IdGrupo:" + grupo.Id + " IdUsuario:" + usuario.Id;
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
