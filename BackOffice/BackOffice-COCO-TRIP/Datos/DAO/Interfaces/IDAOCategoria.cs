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
        JObject Patch(Categoria data);
        JObject Post(Categoria data);
        JObject Put(Categoria data);
        JObject PutEditarEstado(Categoria data);
        JObject GetCategoriasHabilitadas();
        JObject GetPorId(int id);
  }
}
