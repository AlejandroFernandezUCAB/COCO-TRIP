using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice_COCO_TRIP.Datos.DAO.Interfaces
{
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
