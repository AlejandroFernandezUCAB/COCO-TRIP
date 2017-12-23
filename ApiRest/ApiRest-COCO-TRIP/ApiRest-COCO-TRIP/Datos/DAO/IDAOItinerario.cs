using ApiRest_COCO_TRIP.Datos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
  public interface  IDAOItinerario
  {
    List<Entidad> ConsultarEventos(string busqueda, DateTime fechainicio, DateTime fechafin);
    List<Entidad> ConsultarLugarTuristico(string busqueda);
    List<Entidad> ConsultarActividades(string busqueda);
    Boolean AgregarItem_It(string tipo, int idit, int iditem, DateTime fechaini, DateTime fechafin);
    bool AgregarNotificacion(int id_usuario);
    Entidad Buscar_Usuario(int id_usuario);
    List<Entidad> ConsultarItinerariosCorreo(int id_usuario);
    bool ConsultarNotificacion(int id_usuario);
    Boolean EliminarItem_It(string tipo, int idit, int iditem);
    Boolean EliminarNotificacion(int id_usuario);
    string EnviarCorreo(int id_usuario);
    bool ModificarNotificacion(int id_usuario, bool correo);
    Boolean SetVisible(int idusuario, int iditinerario, Boolean visible);






  }
}
