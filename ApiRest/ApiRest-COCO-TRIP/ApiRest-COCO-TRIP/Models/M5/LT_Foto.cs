using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Models.M5
{
  public class LT_Foto
  {
    int fo_id;
    int fo_idLugarTuristico;
    string fo_byte;

    public int Fo_id { get => fo_id; set => fo_id = value; }
    public int Fo_idLugarTuristico { get => fo_idLugarTuristico; set => fo_idLugarTuristico = value; }
    public string Fo_byte { get => fo_byte; set => fo_byte = value; }
  }
}
