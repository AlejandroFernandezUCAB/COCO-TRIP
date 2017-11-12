using System.Reflection;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Models.Excepcion;
using ApiRest_COCO_TRIP.Models.Dato;
using System;

namespace ApiRest_COCO_TRIP.Models.BaseDeDatos
{
  /// <summary>
  /// Clase que realiza peticiones a la base de datos a traves de la clase Conexion
  /// </summary>
  public class PeticionLugarTuristico
  {
    private ConexionLugarTuristico conexion;

    /// <summary>
    /// El constructor instancia la clase Conexion introduciendo las credenciales 
    /// de la base de datos para conectarse a ella posteriormente
    /// </summary>
    public PeticionLugarTuristico()
    {
      conexion = new ConexionLugarTuristico();
    }

    /// <summary>
    /// Inserta toda la data asociada a un lugar turistico en la base de datos.
    /// </summary>
    /// <param name="lugarTuristico">Objeto lugar turistico con todos los campos obligatorios llenos</param>
    /// <returns>(int) ID del Lugar Turistico</returns>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    /// <exception cref="CasteoInvalidoExcepcion"></exception>
    /// <exception cref="ReferenciaNulaExcepcion"></exception>
    /// <exception cref="ArchivoExcepcion"></exception>
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

        foreach (Actividad elemento in lugarTuristico.Actividad)
        {
          conexion.InsertarActividad(elemento, lugarTuristico.Id);
        }

        foreach(Categoria elemento in lugarTuristico.Categoria)
        {
          conexion.InsertarCategoriaLugarTuristico(lugarTuristico.Id, elemento.Id);
        }

        foreach(Categoria elemento in lugarTuristico.SubCategoria)
        {
          conexion.InsertarCategoriaLugarTuristico(lugarTuristico.Id, elemento.Id);
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
      catch (ReferenciaNulaExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      catch (ArchivoExcepcion e)
      {
        throw e;
      }
    }

    /// <summary>
    /// Inserta una actividad asociada a un lugar turistico en la base de datos
    /// </summary>
    /// <param name="actividad">Objeto Actividad</param>
    /// <param name="idLugarTuristico">ID del lugar turistico</param>
    /// <returns>ID de la actividad insertada</returns>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    /// <exception cref="CasteoInvalidoExcepcion"></exception>
    /// <exception cref="ReferenciaNulaExcepcion"></exception>
    /// <exception cref="ArchivoExcepcion"></exception>
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
      catch (CasteoInvalidoExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      catch (ReferenciaNulaExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      catch (ArchivoExcepcion e)
      {
        throw e;
      }
    }

    /// <summary>
    /// Inserta un horario asociado a un lugar turistico en la base de datos
    /// </summary>
    /// <param name="horario">Objeto Horario</param>
    /// <param name="idLugarTuristico">ID del horario </param>
    /// <returns>ID del horario insertado</returns>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    /// <exception cref="CasteoInvalidoExcepcion"></exception>
    /// <exception cref="ReferenciaNulaExcepcion"></exception>
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
      catch (CasteoInvalidoExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      catch (ReferenciaNulaExcepcion e)
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
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    /// <exception cref="CasteoInvalidoExcepcion"></exception>
    /// <exception cref="ReferenciaNulaExcepcion"></exception>
    /// <exception cref="ArchivoExcepcion"></exception>
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
      catch (CasteoInvalidoExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      catch (ReferenciaNulaExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      catch (ArchivoExcepcion e)
      {
        throw e;
      }
    }

    /// <summary>
    /// Inserta una categoria en el lugar turistico especificado
    /// </summary>
    /// <param name="idLugarTuristico">ID lugar turistico</param>
    /// <param name="idCategoria">ID categoria</param>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    public void InsertarCategoria (int idLugarTuristico, int idCategoria)
    {
      try
      {
        conexion.Conectar();

        conexion.InsertarCategoriaLugarTuristico(idLugarTuristico, idCategoria);

        conexion.Desconectar();
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    /// <summary>
    /// Actualiza toda la data asociada a un lugar turistico en la base de datos.
    /// </summary>
    /// <param name="lugarTuristico">Objeto lugar turistico con todos los campos obligatorios llenos
    /// y, el ID de cada lugar turistico, horario, foto y actividad si aplica</param>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    /// <exception cref="CasteoInvalidoExcepcion"></exception>
    /// <exception cref="ReferenciaNulaExcepcion"></exception>
    /// <exception cref="ArchivoExcepcion"></exception>
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

        //Categorias y
        // sub-categorias de las categorias

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
      catch (ReferenciaNulaExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
      catch (ArchivoExcepcion e)
      {
        throw e;
      }
    }

    /// <summary>
    /// Activa o desactiva un lugar turistico
    /// </summary>
    /// <param name="id">ID del lugar turistico</param>
    /// <param name="activar">true para activar, false para desactivar</param>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    public void ActivarLugarTuristico (int id, bool activar)
    {
      try
      {
        conexion.Conectar();

        conexion.ActivarLugarTuristico(id, activar);

        conexion.Desconectar();
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    /// <summary>
    /// Activa o desactiva una actividad
    /// </summary>
    /// <param name="id">ID de la actividad</param>
    /// <param name="activar">true para activar, false para desactivar</param>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    public void ActivarActividad(int id, bool activar)
    {
      try
      {
        conexion.Conectar();

        conexion.ActivarActividad(id, activar);

        conexion.Desconectar();
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    /// <summary>
    /// Elimina una actividad de la base de datos
    /// </summary>
    /// <param name="id">ID de la actividad</param>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    /// <exception cref="ArchivoExcepcion"></exception>
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
      catch (ArchivoExcepcion e)
      {
        throw e;
      }
    }

    /// <summary>
    /// Elimina una foto de la base de datos
    /// </summary>
    /// <param name="id">ID de la foto</param>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    /// <exception cref="ArchivoExcepcion"></exception>
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
      catch (ArchivoExcepcion e)
      {
        throw e;
      }
    }

    /// <summary>
    /// Elimina un horario de la base de datos
    /// </summary>
    /// <param name="id">ID del horario</param>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
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
    /// Elimina una categoria del lugar turistico
    /// </summary>
    /// <param name="idLugarTuristico">ID lugar turistico</param>
    /// <param name="idCategoria">ID categoria</param>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    public void EliminarCategoria(int idLugarTuristico, int idCategoria)
    {
      try
      {
        conexion.Conectar();
        conexion.EliminarCategoriaLugarTuristico(idLugarTuristico, idCategoria);
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
    /// <returns>(List<LugarTuristico>) Lista de lugares turisticos con ID, nombre, costo, descripcion, estado, el horario del dia actual
    /// y las fotos</returns>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    public List<LugarTuristico> ConsultarListaLugarTuristico(int desde, int hasta)
    {
      try
      {
        conexion.Conectar();

        var listaLugarTuristico = conexion.ConsultarListaLugarTuristico(desde, hasta);

        foreach(LugarTuristico elemento in listaLugarTuristico)
        {
          elemento.Horario.Add(conexion.ConsultarDiaHorario(elemento.Id, (int) DateTime.Now.DayOfWeek));
        }

        foreach (LugarTuristico elemento in listaLugarTuristico)
        {
          elemento.Foto = conexion.ConsultarFotos(elemento.Id);
        }

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
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    public LugarTuristico ConsultarLugarTuristico(int id)
    {
      try
      {
        conexion.Conectar();

        var lugarTuristico = conexion.ConsultarLugarTuristico(id);

        lugarTuristico.Actividad = conexion.ConsultarNombreActividades(lugarTuristico.Id);
        lugarTuristico.Horario = conexion.ConsultarHorarios(lugarTuristico.Id);
        lugarTuristico.Foto = conexion.ConsultarFotos(lugarTuristico.Id);

        var listaCategorias = conexion.ConsultarCategoriaLugarTuristico(lugarTuristico.Id);

        foreach (Categoria elemento in listaCategorias)
        {
          if(elemento.CategoriaSuperior != 0)
          {
            lugarTuristico.SubCategoria.Add(elemento);
          }
          else
          {
            lugarTuristico.Categoria.Add(elemento);
          }
        }

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
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    public LugarTuristico ConsultarLugarTuristicoConActividades(int id)
    {
      try
      {
        conexion.Conectar();

        var lugarTuristico = conexion.ConsultarLugarTuristico(id);

        lugarTuristico.Actividad = conexion.ConsultarActividades(lugarTuristico.Id);
        lugarTuristico.Horario = conexion.ConsultarHorarios(lugarTuristico.Id);
        lugarTuristico.Foto = conexion.ConsultarFotos(lugarTuristico.Id);

        var listaCategorias = conexion.ConsultarCategoriaLugarTuristico(lugarTuristico.Id);

        foreach (Categoria elemento in listaCategorias)
        {
          if (elemento.CategoriaSuperior != 0)
          {
            lugarTuristico.SubCategoria.Add(elemento);
          }
          else
          {
            lugarTuristico.Categoria.Add(elemento);
          }
        }

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
    /// <exception cref="BaseDeDatosExcepcion"></exception>
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

    /// <summary>
    /// Consulta el detalle de la actividad
    /// </summary>
    /// <param name="id">ID de la actividad</param>
    /// <returns>Objeto Actividad</returns>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    public Actividad ConsultarActividad(int id)
    {
      try
      {
        conexion.Conectar();

        var actividad = conexion.ConsultarActividad(id);

        conexion.Desconectar();

        return actividad;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    // Trabajo del M9

    /// <summary>
    /// Consulta las categorias de la base de datos
    /// </summary>
    /// <returns>Lista de categorias de la base de datos</returns>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    public List<Categoria> ConsultarCategoria ()
    {
      try
      {
        conexion.Conectar();

        var listaCategorias = conexion.ConsultarCategoria();

        conexion.Desconectar();

        return listaCategorias;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }

    /// <summary>
    /// Consulta las subcategorias de una categoria
    /// </summary>
    /// <returns>Lista de subcategorias de una categoria</returns>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    public List<Categoria> ConsultarSubCategoria (int idCategoria)
    {
      try
      {
        conexion.Conectar();

        var listaCategorias = conexion.ConsultarSubCategoria(idCategoria);

        conexion.Desconectar();

        return listaCategorias;
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw e;
      }
    }
  }
}
