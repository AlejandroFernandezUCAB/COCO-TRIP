using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;

/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///     Interfaz implementada en DAOCategoria.
/// </summary>
namespace BackOffice_COCO_TRIP.Datos.DAO.Interfaces
{
  /// <summary>
  /// Interfaz para DAOCategoria.
  /// </summary>
  interface IDAOCategoria
  {
    JObject Delete(int id);
    JObject Get(int id);
    JObject Patch(Entidad data);
    JObject Post(Entidad data);
    JObject Put(Entidad data);
    JObject PutEditarEstado(Entidad data);
    JObject GetCategoriasHabilitadas();
    JObject GetPorId(int id);
  }
}
