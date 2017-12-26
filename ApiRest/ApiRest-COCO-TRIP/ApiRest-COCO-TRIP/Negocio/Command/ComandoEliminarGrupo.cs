using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.Singleton;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Web.Http;
using System.Net;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Procedimiento que se encarga de hacer la peticion para
  /// eliminar un grupo de la base de datos
  /// </summary>
  public class ComandoEliminarGrupo : Comando
  {
    private Usuario usuario;
    private Usuario lider;
    private Grupo grupo;

    private DAOGrupo datos;
    private Archivo archivo;

    public ComandoEliminarGrupo(int idUsuario, int idGrupo)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      lider = FabricaEntidad.CrearEntidadUsuario();
      grupo = FabricaEntidad.CrearEntidadGrupo();

      usuario.Id = idUsuario;
      grupo.Id = idGrupo;
    }

    public override void Ejecutar()
    {
      try
      {
        datos = FabricaDAO.CrearDAOGrupo();
        archivo = Archivo.ObtenerInstancia();
        lider = (Usuario)datos.ConsultarLider(grupo);

        if (lider.Id == usuario.Id) //El usuario que quiere eliminar el grupo es el lider?
        {
          datos.Eliminar(grupo);

          if (archivo.ExisteArchivo(Archivo.FotoGrupo + grupo.Id + Archivo.Extension))
          {
            archivo.EliminarArchivo(Archivo.FotoGrupo + grupo.Id + Archivo.Extension);
          }
        }
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = "IdUsuario:" + usuario.Id + " idGrupo:" + grupo.Id;
        e.NombreMetodos = this.GetType().FullName;
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (IOExcepcion e)
      {
        e.DatosAsociados = "IdUsuario:" + usuario.Id + " idGrupo:" + grupo.Id;
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
