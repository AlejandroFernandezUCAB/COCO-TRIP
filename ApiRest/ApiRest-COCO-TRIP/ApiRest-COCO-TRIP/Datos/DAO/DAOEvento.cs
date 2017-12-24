using ApiRest_COCO_TRIP.Comun.Excepcion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Reflection;
using ApiRest_COCO_TRIP.Datos.Entity;
using NpgsqlTypes;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
  /**
  * <summary>Clase que recibe todas las peticiones relacionadas a eventos</summary>
  **/


  public class DAOEvento : DAO
  {

    private NpgsqlParameter parametro;
    private NpgsqlDataReader leerDatos;
    private Evento evento;
    private List<Entidad> lista;


    public DAOEvento()
    {
      parametro = new NpgsqlParameter();
      lista = new List<Entidad>();
    }
    public override void Actualizar(Entidad objeto)
    {
      Conectar();
      try
      {

        evento = (Evento)objeto;
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "actualizarEventoPorId";
        Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = evento.Id;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = evento.Nombre;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = evento.Descripcion;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Double;
        parametro.Value = evento.Precio;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Timestamp;
        parametro.Value = evento.FechaInicio;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Timestamp;
        parametro.Value = evento.FechaFin;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Time;
        parametro.Value = evento.HoraInicio;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Time;
        parametro.Value = evento.HoraFin;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = evento.Foto;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = evento.IdLocalidad ;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = evento.IdCategoria;
        Comando.Parameters.Add(parametro);

        leerDatos = Comando.ExecuteReader();
        leerDatos.Close();

      }
      catch (NpgsqlException e)
      {
        BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }
      catch (InvalidCastException e)
      {
        CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }

      catch (InvalidOperationException e)
      {
        OperacionInvalidaException ex = new OperacionInvalidaException(e);
        ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw ex;
      }

      finally
      {
        Desconectar();
      }
    }

    public override List<Entidad> ConsultarLista(Entidad objeto)
    {
      
      lista = new List<Entidad>();

      try
      {
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "ConsultarEventos";
        Comando.CommandType = CommandType.StoredProcedure;

        leerDatos = Comando.ExecuteReader();
        while (leerDatos.Read())
        {
          DateTime horaInicio = new DateTime();
          horaInicio.AddHours(leerDatos.GetTimeSpan(6).Hours);
          horaInicio.AddMinutes(leerDatos.GetTimeSpan(6).Minutes);

          DateTime horaFin = new DateTime();
          horaFin.AddHours(leerDatos.GetTimeSpan(7).Hours);
          horaFin.AddMinutes(leerDatos.GetTimeSpan(7).Minutes);

          evento = new Evento(leerDatos.GetInt32(0), leerDatos.GetString(1), leerDatos.GetString(2),
            leerDatos.GetInt64(3), leerDatos.GetDateTime(4), leerDatos.GetDateTime(5),
            horaInicio, horaFin, leerDatos.GetString(8), leerDatos.GetInt32(10), leerDatos.GetInt32(9));
          lista.Add(evento);
        }
      }
      catch (NpgsqlException e)
      {
        BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }
      catch (InvalidCastException e)
      {
        CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }

      catch (InvalidOperationException e)
      {
        OperacionInvalidaException ex = new OperacionInvalidaException(e);
        ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw ex;
      }
      finally
      {
        Desconectar();
      }
      return lista;
    }

    public override Entidad ConsultarPorId(Entidad objeto)
    {

      try
      {
        evento = (Evento)objeto;
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "ConsultarEventoPorIdEvento";
        Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = evento.Id;
        Comando.Parameters.Add(parametro);

        leerDatos = Comando.ExecuteReader();
        leerDatos.Read();

        evento.Id = leerDatos.GetInt32(0);
        evento.Nombre = leerDatos.GetString(1);
        evento.Descripcion = leerDatos.GetString(2);
        evento.Precio = leerDatos.GetInt64(3);
        evento.FechaInicio = leerDatos.GetDateTime(4);
        evento.FechaFin = leerDatos.GetDateTime(5);
        DateTime horaInicio = new DateTime();
        horaInicio.AddHours(leerDatos.GetTimeSpan(6).Hours);
        horaInicio.AddMinutes(leerDatos.GetTimeSpan(6).Minutes);
        evento.HoraInicio = horaInicio;
        DateTime horaFin = new DateTime();
        horaFin.AddHours(leerDatos.GetTimeSpan(7).Hours);
        horaFin.AddMinutes(leerDatos.GetTimeSpan(7).Minutes);
        evento.HoraFin = horaFin;
        evento.Foto = leerDatos.GetString(8);
        evento.IdLocalidad = leerDatos.GetInt32(9);
        evento.IdCategoria = leerDatos.GetInt32(10);
        leerDatos.Close();
      }
      catch (NpgsqlException e)
      {
        BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }
      catch (InvalidCastException e)
      {
        CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }

      catch (InvalidOperationException e)
      {
        OperacionInvalidaException ex = new OperacionInvalidaException(e);
        ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw ex;
      }
      finally
      {
        Desconectar();
      }
      return evento;
    }

    public override void Eliminar(Entidad objeto)
    {
      Conectar();
      try
      {

        evento = (Evento)objeto;
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "eliminareventoporid";
        Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = evento.Id;
        Comando.Parameters.Add(parametro);
        leerDatos = Comando.ExecuteReader();
        leerDatos.Close();
      }
      catch (NpgsqlException e)
      {
        BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }
      catch (InvalidCastException e)
      {
        CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }

      catch (InvalidOperationException e)
      {
        OperacionInvalidaException ex = new OperacionInvalidaException(e);
        ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw ex;
      }

      finally
      {
        Desconectar();
      }
    }

    public override void Insertar(Entidad objeto)
    {
      Conectar();
      try
      {

        evento = (Evento)objeto;
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "InsertarEvento";
        Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = evento.Nombre;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = evento.Descripcion;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Double;
        parametro.Value = evento.Precio;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Timestamp;
        parametro.Value = evento.FechaInicio;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Timestamp;
        parametro.Value = evento.FechaFin;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Time;
        parametro.Value = evento.HoraInicio;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Time;
        parametro.Value = evento.HoraFin;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = evento.Foto;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = evento.IdLocalidad;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = evento.IdCategoria;
        Comando.Parameters.Add(parametro);

        leerDatos = Comando.ExecuteReader();
        leerDatos.Close();
      }
      catch (NpgsqlException e)
      {
        BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }
      catch (InvalidCastException e)
      {
        CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }

      catch (InvalidOperationException e)
      {
        OperacionInvalidaException ex = new OperacionInvalidaException(e);
        ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw ex;
      }

      finally
      {
       Desconectar();
      }
    }
  }
}
