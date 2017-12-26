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
  /// Valida si un usuario es lider o no para eliminar o salir de un grupo
  /// </summary>
  public class ComandoSalirGrupo : Comando
  {
    private Usuario usuario;
    private Usuario lider;
    private Grupo grupo;

    private DAOGrupo datos;

    public ComandoSalirGrupo (int idGrupo, int idUsuario)
    {
      grupo = FabricaEntidad.CrearEntidadGrupo();
      usuario = FabricaEntidad.CrearEntidadUsuario();
      lider = FabricaEntidad.CrearEntidadUsuario();

      grupo.Id = idGrupo;
      usuario.Id = idUsuario; 
    }

    public override void Ejecutar()
    {
      try
      {
        datos = FabricaDAO.CrearDAOGrupo();
        lider = (Usuario)datos.ConsultarLider(grupo);

        if (lider.Id == usuario.Id) //Es el lider?
        {
          datos.Eliminar(grupo);
        }
        else
        {
          datos.AbandonarGrupo(grupo, usuario);
        }
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = "IdGrupo:" + grupo.Id + " IdUsuario: " + usuario.Id;
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
      throw new System.NotImplementedException();
    }
  }

}
