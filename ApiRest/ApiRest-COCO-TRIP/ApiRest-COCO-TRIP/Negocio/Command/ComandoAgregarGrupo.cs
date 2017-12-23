using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Singleton;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Agrega un grupo
  /// </summary>
  public class ComandoAgregarGrupo : Comando
  {
    private Grupo grupo;

    private Archivo archivo;
    private DAOGrupo datos;

    public ComandoAgregarGrupo (Entidad _grupo)
    {
      grupo = (Grupo) _grupo;
    }

    public override void Ejecutar()
    {
      archivo = Archivo.ObtenerInstancia();
      datos = FabricaDAO.CrearDAOGrupo();

      grupo = (Grupo) datos.InsertarId(grupo);
      
      if(grupo.ContenidoFoto != null) //Valida si el grupo tiene foto
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
