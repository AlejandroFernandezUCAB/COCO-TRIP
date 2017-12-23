using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Singleton;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Procedimiento para visualizar el perfil del grupo
  /// </summary>
  public class ComandoConsultarPerfilGrupo : Comando
  {
    private Grupo grupo;
    private DAOGrupo datos;
    private Archivo archivo;

    public ComandoConsultarPerfilGrupo(int id)
    {
      grupo = FabricaEntidad.CrearEntidadGrupo();
      grupo.Id = id;
    }

    public override void Ejecutar()
    {
      datos = FabricaDAO.CrearDAOGrupo();
      archivo = Archivo.ObtenerInstancia();
      grupo = (Grupo) datos.ConsultarPorId(grupo);

      if (archivo.ExisteArchivo(Archivo.FotoGrupo + grupo.Id + Archivo.Extension))
      {
        grupo.RutaFoto = Archivo.Ruta + Archivo.FotoGrupo + grupo.Id + Archivo.Extension;
      }
    }

    public override Entidad Retornar()
    {
      return grupo;
    }

    public override List<Entidad> RetornarLista()
    {
      throw new System.NotImplementedException();
    }
  }

}
