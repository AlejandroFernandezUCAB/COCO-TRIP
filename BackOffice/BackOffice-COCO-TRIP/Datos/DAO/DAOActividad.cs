using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;

namespace BackOffice_COCO_TRIP.Datos.DAO
{
  public class DAOActividad : DAO<JObject, Entidad>
  {
    public override JObject Delete(int id)
    {
      throw new NotImplementedException();
    }

    public override JObject Get(int id)
    {
      throw new NotImplementedException();
    }

    public override JObject Patch(Entidad data)
    {
      throw new NotImplementedException();
    }

    public override JObject Post(Entidad data)
    {
      throw new NotImplementedException();
    }

    public override JObject Put(Entidad data)
    {
      throw new NotImplementedException();
    }
  }
}
