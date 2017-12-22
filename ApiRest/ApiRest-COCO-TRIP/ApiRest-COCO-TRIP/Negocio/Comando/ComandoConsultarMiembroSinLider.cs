using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Comando
{
  /// <summary>
  /// Metodo que devuelve los integrantes de un grupo sin el integrante lider
  /// </summary>
  public class ComandoConsultarMiembroSinLider : Comando
  {
    private Grupo grupo;
    private List<Entidad> lista;

    private DAOGrupo datos;

    public ComandoConsultarMiembroSinLider(int id)
    {
      grupo = FabricaEntidad.CrearEntidadGrupo();
      grupo.Id = id;
    }

    public override void Ejecutar()
    {
      datos = FabricaDAO.CrearDAOGrupo();
      lista = datos.ConsultarMiembrosExceptoLider(grupo);
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
