using System.Reflection;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Models.Exceptions;
using ApiRest_COCO_TRIP.Models.M7.Dato;

namespace ApiRest_COCO_TRIP.Models.M7.Base
{
  /// <summary>
  /// Clase que realiza peticiones a la base de datos a traves de la clase Conexion
  /// </summary>
  public class Peticion
  {
    private Conexion conexion;

    /// <summary>
    /// El constructor instancia la clase Conexion introduciendo las credenciales 
    /// de la base de datos para conectarse a ella posteriormente
    /// </summary>
    public Peticion()
    {
      conexion = new Conexion();
    }

    /// <summary>
    /// Inserta toda la data asociada a un lugar turistico en la base de datos.
    /// </summary>
    /// <param name="lugarTuristico">Objeto lugar turistico con todos los campos obligatorios llenos</param>
    /// <returns>(int) ID del Lugar Turistico</returns>
    /// Falta agregar excepciones y robustecer el metodo
    public int InsertarLugarTuristico(LugarTuristico lugarTuristico)
    {
      try
      {
        conexion.Conectar();

        lugarTuristico.Id = conexion.InsertarLugarTuristico(lugarTuristico);

        foreach (Horario elemento in lugarTuristico.Horario)
        {
          conexion.InsertarHorario(elemento, lugarTuristico.Id);
        }

        foreach (Foto elemento in lugarTuristico.Foto)
        {
          conexion.InsertarFoto(elemento, lugarTuristico.Id);
        }

        if (lugarTuristico.Actividad != null)
        {
          foreach (Actividad elemento in lugarTuristico.Actividad)
          {
            conexion.InsertarActividad(elemento, lugarTuristico.Id);
          }
        }

        conexion.Desconectar();

        return lugarTuristico.Id;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      catch (CasteoInvalidoExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    /// <summary>
    /// Inserta una actividad asociada a un lugar turistico en la base de datos
    /// </summary>
    /// <param name="actividad">Objeto Actividad</param>
    /// <param name="idLugarTuristico">ID del lugar turistico</param>
    /// <returns>ID de la actividad insertada</returns>
    public int InsertarActividad (Actividad actividad, int idLugarTuristico)
    {
      try
      {
        conexion.Conectar();

        actividad.Id = conexion.InsertarActividad(actividad, idLugarTuristico);

        conexion.Desconectar();

        return actividad.Id;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    /// <summary>
    /// Inserta un horario asociado a un lugar turistico en la base de datos
    /// </summary>
    /// <param name="horario">Objeto Horario</param>
    /// <param name="idLugarTuristico">ID del horario </param>
    /// <returns>ID del horario insertado</returns>
    public int InsertarHorario (Horario horario, int idLugarTuristico)
    {
      try
      {
        conexion.Conectar();

        horario.Id = conexion.InsertarHorario(horario, idLugarTuristico);

        conexion.Desconectar();

        return horario.Id;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    /// <summary>
    /// Inserta una foto asociada a un lugar turistico en la base de datos
    /// </summary>
    /// <param name="foto">Objeto Foto</param>
    /// <param name="idLugarTuristico">ID del horario </param>
    /// <returns>ID de la foto insertada</returns>
    public int InsertarFoto(Foto foto, int idLugarTuristico)
    {
      try
      {
        conexion.Conectar();

        foto.Id = conexion.InsertarFoto(foto, idLugarTuristico);

        conexion.Desconectar();

        return foto.Id;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    //Falta activar/desactivar lugar turistico, activar/desactivar actividad.

    /// <summary>
    /// Actualiza toda la data asociada a un lugar turistico en la base de datos.
    /// </summary>
    /// <param name="lugarTuristico">Objeto lugar turistico con todos los campos obligatorios llenos
    /// y, el ID de cada lugar turistico, horario, foto y actividad si aplica</param>
    /// Falta agregar excepciones y robustecer el metodo
    public void ActualizarLugarTuristico(LugarTuristico lugarTuristico)
    {
      try
      {
        conexion.Conectar();

        conexion.ActualizarLugarTuristico(lugarTuristico);

        foreach (Horario elemento in lugarTuristico.Horario)
        {
          conexion.ActualizarHorario(elemento);
        }

        foreach (Foto elemento in lugarTuristico.Foto)
        {
          conexion.ActualizarFoto(elemento);
        }

        if (lugarTuristico.Actividad != null)
        {
          foreach (Actividad elemento in lugarTuristico.Actividad)
          {
            conexion.ActualizarActividad(elemento);
          }
        }

        conexion.Desconectar();
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      catch (CasteoInvalidoExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    /// <summary>
    /// Elimina una actividad de la base de datos
    /// </summary>
    /// <param name="id">ID de la actividad</param>
    /// Falta agregar excepciones y robustecer el metodo. ¿El ID es 0 o no existe?
    public void EliminarActividad(int id)
    {
      try
      {
        conexion.Conectar();
        conexion.EliminarActividad(id);
        conexion.Desconectar();
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    /// <summary>
    /// Elimina una foto de la base de datos
    /// </summary>
    /// <param name="id">ID de la foto</param>
    /// Falta agregar excepciones y robustecer el metodo. ¿El ID es 0 o no existe?
    public void EliminarFoto(int id)
    {
      try
      {
        conexion.Conectar();
        conexion.EliminarFoto(id);
        conexion.Desconectar();
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    /// <summary>
    /// Elimina un horario de la base de datos
    /// </summary>
    /// <param name="id">ID del horario</param>
    /// Falta agregar excepciones y robustecer el metodo. ¿El ID es 0 o no existe?
    public void EliminarHorario(int id)
    {
      try
      {
        conexion.Conectar();
        conexion.EliminarHorario(id);
        conexion.Desconectar();
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    /// <summary>
    /// Recibe de la base de datos la lista de lugares turisticos dentro del 
    /// rango establecido
    /// </summary>
    /// <param name="desde">limite inferior</param>
    /// <param name="hasta">limite superior</param>
    /// <returns>(List<LugarTuristico>) Lista de lugares turisticos con ID, nombre, costo, descripcion y estado 
    /// de cada lugar turistico</returns>
    public List<LugarTuristico> ConsultarListaLugarTuristico(int desde, int hasta)
    {
      try
      {
        conexion.Conectar();
        var listaLugarTuristico = conexion.ConsultarListaLugarTuristico(desde, hasta);
        conexion.Desconectar();

        return listaLugarTuristico;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    /// <summary>
    /// Recibe de la base de datos los datos del lugar turistico a excepcion
    /// de los detalles de las actividades
    /// </summary>
    /// <param name="id">ID del lugar turistico</param>
    /// <returns>Objeto Lugar Turistico con todos los campos obligatorios y los nombres de las actividades</returns>
    /// /// Faltan excepciones y robustecer el metodo. ¿Si no hay actividades?
    public LugarTuristico ConsultarLugarTuristico(int id)
    {
      try
      {
        conexion.Conectar();
        var lugarTuristico = conexion.ConsultarLugarTuristico(id);
        lugarTuristico.Actividad = conexion.ConsultarNombreActividades(lugarTuristico.Id);
        conexion.Desconectar();

        return lugarTuristico;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    /// <summary>
    /// Recibe de la base de datos los datos del lugar turistico
    /// con el detalle de las actividades
    /// </summary>
    /// <param name="id">ID del lugar turistico</param>
    /// <returns>Objeto Lugar Turistico con todos los campos obligatorios y campos de actividades</returns>
    /// Faltan excepciones y robustecer el metodo. ¿Si no hay actividades?
    public LugarTuristico ConsultarLugarTuristicoConActividades(int id)
    {
      try
      {
        conexion.Conectar();
        var lugarTuristico = conexion.ConsultarLugarTuristico(id);
        lugarTuristico.Actividad = conexion.ConsultarActividades(lugarTuristico.Id);
        conexion.Desconectar();

        return lugarTuristico;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    /// <summary>
    /// Consulta el detalle de las actividades de un lugar turistico
    /// </summary>
    /// <param name="idLugarTuristico">ID del lugar turistico</param>
    /// <returns>(List<Actividad>) Lista de actividades</returns>
    /// Faltan excepciones y robustecer el metodo. ¿Si no hay actividades?
    public List<Actividad> ConsultarActividades(int idLugarTuristico)
    {
      try
      {
        conexion.Conectar();
        var listaActividades = conexion.ConsultarActividades(idLugarTuristico);
        conexion.Desconectar();

        return listaActividades;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }
  }
}
