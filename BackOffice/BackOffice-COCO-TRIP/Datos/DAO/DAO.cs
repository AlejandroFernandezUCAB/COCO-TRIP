using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice_COCO_TRIP.Datos.DAO
{
  /// <summary>
  /// Clase abstracta base para realizar peticiones al servicio web
  /// </summary>
  /// <typeparam name="T1">Parametro que indica el tipo de dato que devolveran los metodos</typeparam>
  /// <typeparam name="T2">Parametro que indica el tipo de dato que recibiran los metodos Post, Put, Patch</typeparam>
  public abstract class DAO<T1, T2>
  {
    protected const string BaseUri = "http://localhost:8082/api";


    public abstract T1 Get(int id);
    public abstract T1 Post(T2 data);
    public abstract T1 Put(T2 data);
    public abstract T1 Delete(int id);
    public abstract T1 Patch(T2 data);

  }
}