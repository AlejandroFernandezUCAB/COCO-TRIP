using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Singleton;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Comando
{
  /// <summary>
  /// Consulta la lista de grupo del usuario
  /// </summary>
  public class ComandoConsultarListaGrupos : Comando
  {
    private Usuario usuario;
    private List<Entidad> listaGrupos;

    private DAOGrupo datos;
    private Archivo archivo;

    public ComandoConsultarListaGrupos(int id)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      usuario.Id = id;
    }

    public override void Ejecutar()
    {
      archivo = Archivo.ObtenerInstancia();
      datos = FabricaDAO.CrearDAOGrupo();
      listaGrupos = datos.ConsultarLista(usuario);
      
      foreach(Grupo elemento in listaGrupos)
      {
        if(archivo.ExisteArchivo(Archivo.FotoGrupo + elemento.Id + Archivo.Extension))
        {
          elemento.RutaFoto = Archivo.Ruta + Archivo.FotoGrupo + elemento.Id + Archivo.Extension;
        }
      }
    }

    public override Entidad Retornar()
    {
      throw new System.NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      return listaGrupos;
    }
  }
}
