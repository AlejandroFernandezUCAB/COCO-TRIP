using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice_COCO_TRIP.Datos.DAO.Interfaces
{
  interface IDAOEvento
  {
    JObject Get(int id);
    JObject Post(Entidad data);
    JObject Put(Entidad data);
    JObject Delete(int id);
    JObject Patch(Entidad data);
    JObject GetEvento(int id);

  }
}
