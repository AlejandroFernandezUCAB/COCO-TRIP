using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Npgsql;
using NpgsqlTypes;
using ApiRest_COCO_TRIP.Models.Excepcion;
using ApiRest_COCO_TRIP.Models.Dato;

namespace ApiRest_COCO_TRIP.Models.Base
{
  /// <summary>
  /// Clase que gestiona la conexion entre la base de datos y la logica de negocios del modulo 7
  /// </summary>
  public class ConexionLugarTuristico
  {
    private ConexionBase conexion; //Conexion con la base de datos PostgreSQL
    private NpgsqlCommand comando; //Instruccion SQL a ejecutar
    private NpgsqlDataReader leerDatos; //Lee la respuesta de la instruccion SQL ejecutada

    /// <summary>
    /// Constructor de la clase
    /// </summary>
    public ConexionLugarTuristico()
    {
      conexion = new ConexionBase();
      comando = new NpgsqlCommand();
    }

    /// <summary>
    /// Inicia una conexion con la base de datos
    /// </summary>
    public void Conectar()
    {
      try
      {
        conexion.Conectar();
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Fallo de conexion con la base de datos";

        throw excepcion;
      }
    }

    /// <summary>
    /// Finaliza la conexion con la base de datos
    /// </summary>
    public void Desconectar()
    {
      try
      {
        conexion.Desconectar();
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Fallo de desconexion con la base de datos";

        throw excepcion;
      }
    }

    //Insertar

    /// <summary>
    /// Inserta en la base de datos los datos del lugar turistico
    /// y retorna el ID del lugar turistico insertado
    /// </summary>
    /// <param name="lugarTuristico">LugarTuristico</param>
    /// <returns>(int) ID del Lugar Turistico</returns>
    public int InsertarLugarTuristico(LugarTuristico lugarTuristico)
    {
      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "InsertarLugarTuristico";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, lugarTuristico.Nombre));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Numeric, lugarTuristico.Costo));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, lugarTuristico.Descripcion));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, lugarTuristico.Direccion));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, lugarTuristico.Correo));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Bigint, lugarTuristico.Telefono));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Numeric, lugarTuristico.Latitud));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Numeric, lugarTuristico.Longitud));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Boolean, lugarTuristico.Activar));

        leerDatos = comando.ExecuteReader();

        if (leerDatos.Read())
        {
          lugarTuristico.Id = leerDatos.GetInt32(0);
          leerDatos.Close();
        }

        return lugarTuristico.Id;

      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorLugarTuristico(lugarTuristico);

        throw excepcion;
      }
      catch (InvalidCastException e)
      {
        var excepcion = new CasteoInvalidoExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorLugarTuristico(lugarTuristico);

        throw excepcion;
      }
      catch (ReferenciaNulaExcepcion e)
      {
        var excepcion = new ReferenciaNulaExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorLugarTuristico(lugarTuristico);

        throw excepcion;
      }
    }

    /// <summary>
    /// Inserta en la base de datos los datos de
    /// la actividad perteneciente a un lugar turistico
    /// y retorna el ID de la actividad insertada
    /// </summary>
    /// <param name="actividad">Actividad</param>
    /// <param name="idLugarTuristico">ID del Lugar Turistico</param>
    /// <returns>(int) ID de la actividad insertada</returns>
    public int InsertarActividad(Actividad actividad, int idLugarTuristico)
    {
      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "InsertarActividad";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Bytea, actividad.Foto.Contenido));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, actividad.Nombre));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Time, actividad.Duracion));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, actividad.Descripcion));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Boolean, actividad.Activar));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idLugarTuristico));

        leerDatos = comando.ExecuteReader();

        if (leerDatos.Read())
        {
          actividad.Id = leerDatos.GetInt32(0);
          leerDatos.Close();
        }

        return actividad.Id;
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorActividad(actividad);
        excepcion.DatosAsociados = " " + "idLugarTuristico " + idLugarTuristico;

        throw excepcion;
      }
      catch (InvalidCastException e)
      {
        var excepcion = new CasteoInvalidoExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorActividad(actividad);
        excepcion.DatosAsociados = " " + "idLugarTuristico " + idLugarTuristico;

        throw excepcion;
      }
      catch (ReferenciaNulaExcepcion e)
      {
        var excepcion = new ReferenciaNulaExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorActividad(actividad);

        throw excepcion;
      }
    }

    /// <summary>
    /// Inserta en la base de datos el horario
    /// perteneciente a un lugar turistico y
    /// retorna el ID del horario insertado
    /// </summary>
    /// <param name="horario">Horario</param>
    /// <param name="idLugarTuristico">ID del Lugar Turistico</param>
    /// <returns>(int) ID del horario insertado</returns>
    public int InsertarHorario(Horario horario, int idLugarTuristico)
    {
      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "InsertarHorario";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, horario.DiaSemana));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Time, horario.HoraApertura));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Time, horario.HoraCierre));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idLugarTuristico));

        leerDatos = comando.ExecuteReader();

        if (leerDatos.Read())
        {
          horario.Id = leerDatos.GetInt32(0);
          leerDatos.Close();
        }

        return horario.Id;
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorHorario(horario);
        excepcion.DatosAsociados = " " + "idLugarturistico " + idLugarTuristico;

        throw excepcion;
      }
      catch (InvalidCastException e)
      {
        var excepcion = new CasteoInvalidoExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorHorario(horario);
        excepcion.DatosAsociados = " " + "idLugarturistico " + idLugarTuristico;

        throw excepcion;
      }
      catch (ReferenciaNulaExcepcion e)
      {
        var excepcion = new ReferenciaNulaExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorHorario(horario);
        excepcion.DatosAsociados = " " + "idLugarturistico " + idLugarTuristico;

        throw excepcion;
      }
    }

    /// <summary>
    /// Inserta en la base de datos la foto
    /// perteneciente a un lugar turistico
    /// y retorna el ID de la foto insertada
    /// </summary>
    /// <param name="foto">Foto</param>
    /// <param name="idLugarTuristico">ID del Lugar Turistico</param>
    /// <returns>(int) ID de la foto insertada</returns>
    public int InsertarFoto(Foto foto, int idLugarTuristico)
    {
      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "InsertarFoto";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Bytea, foto.Contenido));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idLugarTuristico));

        leerDatos = comando.ExecuteReader();

        if (leerDatos.Read())
        {
          foto.Id = leerDatos.GetInt32(0);
          leerDatos.Close();
        }

        return foto.Id;
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "idLugarTuristico " + idLugarTuristico + " ";
        excepcion.DatosAsociados += "Foto (size) " + foto.Contenido.Length;

        throw excepcion;
      }
      catch (InvalidCastException e)
      {
        var excepcion = new CasteoInvalidoExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "idLugarTuristico " + idLugarTuristico + " ";
        excepcion.DatosAsociados += "Foto (size) " + foto.Contenido.Length;

        throw excepcion;
      }
      catch (ReferenciaNulaExcepcion e)
      {
        var excepcion = new ReferenciaNulaExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "idLugarTuristico " + idLugarTuristico + " ";
        excepcion.DatosAsociados += "Foto (size) " + foto.Contenido.Length;

        throw excepcion;
      }

    }

    // Actualizar

    /// <summary>
    /// Actualiza los datos de un lugar turistico
    /// en la base de datos
    /// </summary>
    /// <param name="lugarTuristico">Lugar Turistico</param>
    public void ActualizarLugarTuristico(LugarTuristico lugarTuristico)
    {
      try
      {
        //Inserta los datos propios del lugar turistico
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "ActualizarLugarTuristico";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, lugarTuristico.Id));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, lugarTuristico.Nombre));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Numeric, lugarTuristico.Costo));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, lugarTuristico.Descripcion));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, lugarTuristico.Direccion));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, lugarTuristico.Correo));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Bigint, lugarTuristico.Telefono));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Numeric, lugarTuristico.Latitud));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Numeric, lugarTuristico.Longitud));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Boolean, lugarTuristico.Activar));

        comando.ExecuteNonQuery();
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorLugarTuristico(lugarTuristico);
        excepcion.DatosAsociados = " " + "Id " + lugarTuristico.Id;

        throw excepcion;
      }
      catch (InvalidCastException e)
      {
        var excepcion = new CasteoInvalidoExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorLugarTuristico(lugarTuristico);
        excepcion.DatosAsociados = " " + "Id " + lugarTuristico.Id;

        throw excepcion;
      }
      catch (ReferenciaNulaExcepcion e)
      {
        var excepcion = new ReferenciaNulaExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorLugarTuristico(lugarTuristico);
        excepcion.DatosAsociados = " " + "Id " + lugarTuristico.Id;

        throw excepcion;
      }
    }

    /// <summary>
    /// Actualiza los datos de una actividad asociada a un
    /// lugar turistico
    /// </summary>
    /// <param name="actividad">Actividad</param>
    public void ActualizarActividad(Actividad actividad)
    {
      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "ActualizarActividad";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, actividad.Id));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Bytea, actividad.Foto.Contenido));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, actividad.Nombre));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Time, actividad.Duracion));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, actividad.Descripcion));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Boolean, actividad.Activar));

        comando.ExecuteNonQuery();
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorActividad(actividad);
        excepcion.DatosAsociados += " " + "Id " + actividad.Id;

        throw excepcion;
      }
      catch (InvalidCastException e)
      {
        var excepcion = new CasteoInvalidoExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorActividad(actividad);
        excepcion.DatosAsociados += " " + "Id " + actividad.Id;

        throw excepcion;
      }
      catch (ReferenciaNulaExcepcion e)
      {
        var excepcion = new ReferenciaNulaExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorActividad(actividad);
        excepcion.DatosAsociados += " " + "Id " + actividad.Id;

        throw excepcion;
      }
    }

    /// <summary>
    /// Inserta el horario relacionado a un
    /// lugar turistico
    /// </summary>
    /// <param name="horario">Horario</param>
    public void ActualizarHorario(Horario horario)
    {
      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "ActualizarHorario";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, horario.Id));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, horario.DiaSemana));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Time, horario.HoraApertura));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Time, horario.HoraCierre));

        comando.ExecuteNonQuery();
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorHorario(horario);
        excepcion.DatosAsociados += " " + "Id " + horario.Id;

        throw excepcion;
      }
      catch (InvalidCastException e)
      {
        var excepcion = new CasteoInvalidoExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorHorario(horario);
        excepcion.DatosAsociados += " " + "Id " + horario.Id;

        throw excepcion;
      }
      catch (ReferenciaNulaExcepcion e)
      {
        var excepcion = new ReferenciaNulaExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = DocumentarErrorHorario(horario);
        excepcion.DatosAsociados += " " + "Id " + horario.Id;

        throw excepcion;
      }
    }

    /// <summary>
    /// Actualizar foto asociada a un lugar turistico
    /// </summary>
    /// <param name="foto">Foto</param>
    public void ActualizarFoto(Foto foto)
    {
      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "ActualizarFoto";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, foto.Id));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Bytea, foto.Contenido));

        comando.ExecuteNonQuery();
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "Id " + foto.Id + " ";
        excepcion.DatosAsociados += "Foto (size) " + foto.Contenido.Length;

        throw excepcion;
      }
      catch (InvalidCastException e)
      {
        var excepcion = new CasteoInvalidoExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "Id " + foto.Id + " ";
        excepcion.DatosAsociados += "Foto (size) " + foto.Contenido.Length;

        throw excepcion;
      }
      catch (ReferenciaNulaExcepcion e)
      {
        var excepcion = new ReferenciaNulaExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "Id " + foto.Id + " ";
        excepcion.DatosAsociados += "Foto (size) " + foto.Contenido.Length;

        throw excepcion;
      }

    }

    /// <summary>
    /// Activar o desactivar lugar turistico
    /// </summary>
    /// <param name="id">ID del lugar turistico</param>
    /// <param name="activar">activar (true) o desactivar (false)</param>
    public void ActivarLugarTuristico(int id, bool activar)
    {
      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "ActivarLugarTuristico";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, id));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Boolean, activar));

        comando.ExecuteNonQuery();
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "id " + id + " ";
        excepcion.DatosAsociados += "activar " + activar;

        throw excepcion;
      }
    }

    /// <summary>
    /// Activar o desactivar actividad
    /// </summary>
    /// <param name="id">ID de la actividad</param>
    /// <param name="activar">activar (true) o desactivar (false)</param>
    public void ActivarActividad(int id, bool activar)
    {
      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "ActivarActividad";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, id));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Boolean, activar));

        comando.ExecuteNonQuery();
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "id " + id + " ";
        excepcion.DatosAsociados += "activar " + activar;

        throw excepcion;
      }
    }

    //Eliminar

    /// <summary>
    /// Eliminar actividad asociado al lugar turistico
    /// </summary>
    /// <param name="id">ID de la actividad</param>
    public void EliminarActividad(int id)
    {
      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "EliminarActividad";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, id));

        comando.ExecuteNonQuery();
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "id " + id;

        throw excepcion;
      }
    }

    /// <summary>
    /// Eliminar foto asociada al lugar turistico
    /// </summary>
    /// <param name="id">ID de la foto</param>
    public void EliminarFoto(int id)
    {
      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "EliminarFoto";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, id));

        comando.ExecuteNonQuery();
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "id " + id;

        throw excepcion;
      }
    }

    /// <summary>
    /// Eliminar horario asociada al lugar turistico
    /// </summary>
    /// <param name="id">ID de la foto</param>
    public void EliminarHorario(int id)
    {
      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "EliminarHorario";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, id));

        comando.ExecuteNonQuery();
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "id " + id;

        throw excepcion;
      }
    }

    //Select

    /// <summary>
    /// Consulta en la base de datos la informacion de un lugar turistico
    /// a traves del ID. El metodo retorna todos los datos excepto las actividades,
    /// horarios y fotos asociadas al lugar turistico.
    /// </summary>
    /// <param name="id">ID del Lugar Turistico</param>
    /// <returns>(LugarTuristico) Retorna toda la informacion del lugar turistico</returns>
    public LugarTuristico ConsultarLugarTuristico(int id)
    {
      var lugarTuristico = new LugarTuristico();

      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "ConsultarLugarTuristico";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, id));

        leerDatos = comando.ExecuteReader();

        if (leerDatos.Read())
        {

          lugarTuristico.Id = id;
          lugarTuristico.Nombre = leerDatos.GetString(0);
          lugarTuristico.Costo = leerDatos.GetDouble(1);
          lugarTuristico.Descripcion = leerDatos.GetString(2);
          lugarTuristico.Direccion = leerDatos.GetString(3);
          lugarTuristico.Correo = leerDatos.GetString(4);
          lugarTuristico.Telefono = leerDatos.GetInt64(5);
          lugarTuristico.Latitud = leerDatos.GetDouble(6);
          lugarTuristico.Longitud = leerDatos.GetDouble(7);
          lugarTuristico.Activar = leerDatos.GetBoolean(8);

          leerDatos.Close();
        }

        return lugarTuristico;
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "id " + id;

        throw excepcion;
      }
    }

    /// <summary>
    /// Consulta la informacion minima requerida de los lugares turisticos
    /// dentro de un rango de ID. Retorna el ID, nombre, costo, descripcion y estado
    /// de cada lugar turistico
    /// </summary>
    /// <param name="desde">ID desde el cual se consultan los lugares turisticos</param>
    /// <param name="hasta">ID hasta el cual se consultan los lugares turisticos</param>
    /// <returns>(List<LugarTuristico>) Lista de lugares turisticos con la informacion minima</returns>
    public List<LugarTuristico> ConsultarListaLugarTuristico(int desde, int hasta)
    {
      var listaLugarTuristico = new List<LugarTuristico>();

      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "ConsultarListaLugarTuristico";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, desde));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, hasta));

        leerDatos = comando.ExecuteReader();

        while (leerDatos.Read()) //Recorre las filas retornadas de la base de datos
        {
          var lugarTuristico = new LugarTuristico();

          lugarTuristico.Id = leerDatos.GetInt32(0);
          lugarTuristico.Nombre = leerDatos.GetString(1);
          lugarTuristico.Costo = leerDatos.GetDouble(2);
          lugarTuristico.Descripcion = leerDatos.GetString(3);
          lugarTuristico.Activar = leerDatos.GetBoolean(4);

          listaLugarTuristico.Add(lugarTuristico);
        }

        leerDatos.Close();

        return listaLugarTuristico;
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "desde " + desde + " ";
        excepcion.DatosAsociados += "hasta " + hasta;

        throw excepcion;
      }
    }

    /// <summary>
    /// Consulta en la base de datos la informacion de las actividades
    /// asociadas a un lugar turistico
    /// </summary>
    /// <param name="idLugarTuristico">ID del Lugar Turistico</param>
    /// <returns>(List<Actividad>) Lista de actividades asociadas al lugar turistico</returns>
    public List<Actividad> ConsultarActividades(int idLugarTuristico)
    {
      var listaActividad = new List<Actividad>();

      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "ConsultarActividades";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idLugarTuristico));

        leerDatos = comando.ExecuteReader();

        while (leerDatos.Read()) //Recorre las filas retornadas de la base de datos
        {
          var actividad = new Actividad();

          actividad.Id = leerDatos.GetInt32(0);
          actividad.Foto.Contenido = (byte[])leerDatos[1]; //Simplificacion de GetBytes. TESTEAR!
          actividad.Nombre = leerDatos.GetString(2);
          actividad.Duracion = leerDatos.GetTimeSpan(3);
          actividad.Descripcion = leerDatos.GetString(4);
          actividad.Activar = leerDatos.GetBoolean(5);

          listaActividad.Add(actividad);
        }

        leerDatos.Close();

        return listaActividad;
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "idLugarTuristico " + idLugarTuristico;

        throw excepcion;
      }
    }

    /// <summary>
    /// Consulta en la base de datos la informacion de la actividad
    /// </summary>
    /// <param name="id">ID de la activdad</param>
    /// <returns>Objeto actividad</returns>
    public Actividad ConsultarActividad (int id)
    {
      var actividad = new Actividad ();

      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "ConsultarActividad";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, id));

        leerDatos = comando.ExecuteReader();

        if (leerDatos.Read())
        {
          actividad.Id = id;
          actividad.Foto.Contenido = (byte[])leerDatos[0]; //Simplificacion de GetBytes. TESTEAR!
          actividad.Nombre = leerDatos.GetString(1);
          actividad.Duracion = leerDatos.GetTimeSpan(2);
          actividad.Descripcion = leerDatos.GetString(3);
          actividad.Activar = leerDatos.GetBoolean(4);
        }

        leerDatos.Close();

        return actividad;
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "id " + id;

        throw excepcion;
      }
    }

    /// <summary>
    /// Consulta en la base de datos el nombre de las actividades
    /// asociadas a un lugar turistico
    /// </summary>
    /// <param name="idLugarTuristico">ID del Lugar Turistico</param>
    /// <returns>(List<Actividad>) Lista de actividades asociadas al lugar turistico</returns>
    public List<Actividad> ConsultarNombreActividades(int idLugarTuristico)
    {
      var listaActividad = new List<Actividad>();

      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "ConsultarNombreActividades";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idLugarTuristico));

        leerDatos = comando.ExecuteReader();

        while (leerDatos.Read()) //Recorre las filas retornadas de la base de datos
        {
          var actividad = new Actividad();

          actividad.Nombre = leerDatos.GetString(0);

          listaActividad.Add(actividad);
        }

        leerDatos.Close();

        return listaActividad;
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "idLugarTuristico " + idLugarTuristico;

        throw excepcion;
      }
    }

    /// <summary>
    /// Consulta en la base de datos los horarios asociados
    /// a un lugar turistico
    /// </summary>
    /// <param name="idLugarTuristico">ID de Lugar Turistico</param>
    /// <returns>(List<Horario>) Retorna la lista de horarios</returns>
    public List<Horario> ConsultarHorarios(int idLugarTuristico)
    {
      var listaHorario = new List<Horario>();

      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "ConsultarHorarios";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idLugarTuristico));

        leerDatos = comando.ExecuteReader();

        while (leerDatos.Read()) //Recorre las filas retornadas de la base de datos
        {
          var horario = new Horario();

          horario.Id = leerDatos.GetInt32(0);
          horario.DiaSemana = leerDatos.GetInt32(1);
          horario.HoraApertura = leerDatos.GetTimeSpan(2);
          horario.HoraCierre = leerDatos.GetTimeSpan(3);

          listaHorario.Add(horario);
        }

        leerDatos.Close();

        return listaHorario;
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "idLugarTuristico " + idLugarTuristico;

        throw excepcion;
      }
    }

    /// <summary>
    /// Consulta en la base de datos el horario del dia de la semana
    /// especificado asociado a un lugar turistico
    /// </summary>
    /// <param name="idLugarTuristico">ID de Lugar Turistico</param>
    /// <param name="diaSemana">Dia de la semana</param>
    /// <returns>(Horario) Horario del dia de la semana</returns>
    public Horario ConsultarDiaHorario(int idLugarTuristico, int diaSemana)
    {
      var horario = new Horario();

      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "ConsultarDiaHorario";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idLugarTuristico));
        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, diaSemana));

        leerDatos = comando.ExecuteReader();

        if (leerDatos.Read())
        {
          horario.HoraApertura = leerDatos.GetTimeSpan(0);
          horario.HoraCierre = leerDatos.GetTimeSpan(1);
        }

        leerDatos.Close();

        return horario;
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "idLugarTuristico " + idLugarTuristico + " ";
        excepcion.DatosAsociados += "diaSemana " + diaSemana;

        throw excepcion;
      }
    }

    /// <summary>
    /// Consulta las fotos asociada a un lugar turistico
    /// </summary>
    /// <param name="idLugarTuristico">ID del Lugar Turistico</param>
    /// <returns>(List<Foto>) Lista de fotos del lugar turistico</returns>
    public List<Foto> ConsultarFotos(int idLugarTuristico)
    {
      var listaFoto = new List<Foto>();

      try
      {
        comando = conexion.SqlConexion.CreateCommand();
        comando.CommandText = "ConsultarFotos";
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idLugarTuristico));

        leerDatos = comando.ExecuteReader();

        while (leerDatos.Read()) //Recorre las filas retornadas de la base de datos
        {
          var foto = new Foto();

          foto.Id = leerDatos.GetInt32(0);
          foto.Contenido = (byte[])leerDatos[1];

          listaFoto.Add(foto);
        }

        leerDatos.Close();

        return listaFoto;
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Datos: ";
        excepcion.DatosAsociados += "idLugarTuristico " + idLugarTuristico;

        throw excepcion;
      }
    }

    //Metodos privados

    /// <summary>
    /// Facilita la creacion de parametros
    /// en los comandos a ejecutar en la base de datos
    /// </summary>
    /// <param name="tipoDeDato">Tipo de dato de la base de datos</param>
    /// <param name="valor">Valor asociado al tipo de dato</param>
    /// <returns>Parametro que se adicionara al comando de la base de datos</returns>
    private NpgsqlParameter AgregarParametro(NpgsqlDbType tipoDeDato, object valor)
    {
      var parametro = new NpgsqlParameter();

      parametro.NpgsqlDbType = tipoDeDato;
      parametro.Value = valor;

      return parametro;
    }

    private string DocumentarErrorLugarTuristico(LugarTuristico lugarTuristico)
    {
      return
          (
              "Datos: " +
              "Nombre " + lugarTuristico.Nombre + " " +
              "Costo " + lugarTuristico.Costo + " " +
              "Descripcion " + lugarTuristico.Descripcion + " " +
              "Direccion " + lugarTuristico.Direccion + " " +
              "Correo " + lugarTuristico.Correo + " " +
              "Telefono " + lugarTuristico.Telefono + " " +
              "Latitud " + lugarTuristico.Latitud + " " +
              "Longitud " + lugarTuristico.Longitud + " " +
              "Activar " + lugarTuristico.Activar
          );
    }

    private string DocumentarErrorActividad(Actividad actividad)
    {
      return
          (
              "Datos: " +
              "Foto (size) " + actividad.Foto.Contenido.Length + " " +
              "Nombre " + actividad.Nombre + " " +
              "Duracion " + actividad.Duracion.ToString() + " " +
              "Descripcion " + actividad.Descripcion + " " +
              "Activar " + actividad.Activar
          );
    }

    private string DocumentarErrorHorario(Horario horario)
    {
      return
          (
              "Datos: " +
              "DiaSemana " + horario.DiaSemana + " " +
              "HoraApertura " + horario.HoraApertura + " " +
              "HoraCierre " + horario.HoraCierre
          );
    }
  }
}
