using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Singleton;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Web.Http;
using System.Net;
using Newtonsoft.Json;
using System;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Procedimiento que se encarga de hacer la peticion para
  /// modificar los datos de un grupo
  /// </summary>
  public class ComandoModificarGrupo : Comando
  {
    private Grupo grupo;
    private Usuario usuario;
    private Usuario lider;
    private Archivo archivo;
    private DAOGrupo datos;

    public ComandoModificarGrupo (Entidad _grupo, int id)
    {
      grupo = (Grupo) _grupo;
      lider = FabricaEntidad.CrearEntidadUsuario();
      usuario = FabricaEntidad.CrearEntidadUsuario();
      usuario.Id = id;
    }

    public override void Ejecutar()
    {
      try
      {
        datos = FabricaDAO.CrearDAOGrupo();
        archivo = Archivo.ObtenerInstancia();
        lider = (Usuario)datos.ConsultarLider(grupo);

        if (lider.Id == usuario.Id) //El usuario que quiere modificar el grupo es el lider?
        {        
          if (grupo.Nombre != null)
          {
            datos.Actualizar(grupo);
          }

          if (grupo.ContenidoFoto != null)
          {
            archivo.EscribirArchivo(grupo.ContenidoFoto, Archivo.FotoGrupo + grupo.Id + Archivo.Extension);
          }
        }
        else
        {
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = JsonConvert.SerializeObject(grupo);
        e.NombreMetodos = this.GetType().FullName;
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (IOExcepcion e)
      {
        e.DatosAsociados = JsonConvert.SerializeObject(grupo);
        e.NombreMetodos = this.GetType().FullName;
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (NullReferenceException e)
      {
        ReferenciaNulaExcepcion excepcion = new ReferenciaNulaExcepcion(e, "Parametros de entrada nulo");
        excepcion.NombreMetodos = this.GetType().FullName;
        throw new HttpResponseException(HttpStatusCode.BadRequest);
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
