using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice_COCO_TRIP.Negocio.Entidades
{
  public abstract class Entidad
  {
    private  long id;
    [JsonProperty(PropertyName = "id")]
    public  long Id { get => id; set => id = value; }
  }


}
