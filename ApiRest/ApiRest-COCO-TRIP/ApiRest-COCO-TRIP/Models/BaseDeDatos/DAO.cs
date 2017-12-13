using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Models.BaseDeDatos
{
  public abstract class DAO
  {
    public abstract Boolean Insertar(object obj);
    public abstract Boolean Modificar(object obj);
    public abstract Boolean Eliminar(int id);
  }
}
