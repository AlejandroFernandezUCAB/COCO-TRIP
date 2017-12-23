using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Singleton;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Procedimiento que se encarga de hacer la peticion para
  /// modificar los datos de un grupo
  /// </summary>
  public class ComandoModificarGrupo : Comando
  {
    private Grupo grupo;

    private Archivo archivo;
    private DAOGrupo datos;

    public ComandoModificarGrupo (Entidad _grupo)
    {
      grupo = (Grupo) _grupo;
    }

    public override void Ejecutar()
    {
      datos = FabricaDAO.CrearDAOGrupo();
      archivo = Archivo.ObtenerInstancia();

      if(grupo.Nombre != null)
      {
        datos.Actualizar(grupo);
      }

      if(grupo.ContenidoFoto != null)
      {
        archivo.EscribirArchivo(grupo.ContenidoFoto, Archivo.FotoGrupo + grupo.Id + Archivo.Extension);
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
