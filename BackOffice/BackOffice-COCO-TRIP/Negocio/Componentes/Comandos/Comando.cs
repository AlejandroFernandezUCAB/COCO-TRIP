using System;


namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public abstract class Comando
  {

    public abstract void  Execute();
    public abstract Object GetResult();
  }
}
