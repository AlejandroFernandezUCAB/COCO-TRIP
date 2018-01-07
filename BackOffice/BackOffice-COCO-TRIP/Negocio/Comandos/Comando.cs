using System;
using System.Collections;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  public abstract class Comando
  {

    public abstract void  Execute();
    public abstract void SetPropiedad(Object propiedad);
    public abstract ArrayList GetResult();
  }
}
