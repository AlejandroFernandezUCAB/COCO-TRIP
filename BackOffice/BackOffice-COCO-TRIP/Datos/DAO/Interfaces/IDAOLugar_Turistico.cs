using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;

namespace BackOffice_COCO_TRIP.Datos.DAO.Interfaces
{
  interface IDAOLugar_Turistico
  {

    JObject Get(int id);
    JObject Post(Entidad data);
    JObject Put(Entidad data);
    JObject Delete(int id);
    JObject Patch(Entidad data);
    JObject GetAll();
    JObject PutLugarActualizar(Entidad data);

  }
}
