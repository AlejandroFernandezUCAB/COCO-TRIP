using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using Npgsql;
using System.Data;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
  public class DAONotificacion : DAO
  {
    private Itinerario itinerario;
    private NpgsqlParameter parmetro;
    private NpgsqlDataReader respuesta;
    private NpgsqlCommand comando;
    public override void Actualizar(Entidad objeto)
    {
      Notificacion notificacion = (Notificacion)objeto;
      try
      {
        base.Conectar();
        comando = new NpgsqlCommand("modificar_notificacion", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, notificacion.IdUsuario);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Boolean, notificacion.Correo);
        //La siguiente linea determina si se desea recibir notificaciones push, en caso de implementarlo.
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Boolean, notificacion.Push);
        respuesta = comando.ExecuteReader();
        base.Desconectar();
      }
      catch (NpgsqlException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (InvalidCastException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        base.Desconectar();
        throw e;
      }
    }

    public override List<Entidad> ConsultarLista(Entidad objeto)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Consulta si se desea recibir notificaciones por correo.
    /// </summary>
    /// <param name="objeto">Con el Id del usuario al que se desea verificar si desea notificaciones por correo..</param>
    /// <returns>Retorna True en caso que se desee notificaciones y False en caso contrario</returns>

    public override Entidad ConsultarPorId(Entidad objeto)
    {
      Notificacion notificacion = (Notificacion)objeto;
      try
      {
       base.Conectar();
        comando = new NpgsqlCommand("consultar_notificaciones", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, notificacion.IdUsuario);
        respuesta = comando.ExecuteReader();

        if (respuesta.Read())
        {
          notificacion.Push = respuesta.GetBoolean(0);
          return notificacion;
        }
        base.Desconectar();
        notificacion.Push = false;
        return notificacion;
      }
      catch (NpgsqlException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (System.InvalidOperationException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        base.Desconectar();
        throw e;
      }
    }

    /// <summary>
    /// Metodo que elimina el registro de la base de datos que determina si el usuario quiere notificaciones por correo
    /// </summary>
    /// <param name="objeto">Con el Id del usuario al que se desea eliminar el registro</param>
    /// <returns>true si elimina existosamente, false en caso de error</returns>
    public override void Eliminar(Entidad objeto)
    {
      Notificacion notificacion = (Notificacion)objeto;
      try
      {
        base.Conectar();
        comando = new NpgsqlCommand("eliminar_notificacion", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, notificacion.IdUsuario);
        respuesta = comando.ExecuteReader();
        respuesta.Read();
        Boolean resp = respuesta.GetBoolean(0);
        base.Desconectar();
      }
      catch (NpgsqlException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        base.Desconectar();
        throw e;
      }
    }
    /// <summary>
    /// Metodo que agrega en la base de datos si el usuario desea recibir notificaciones por correo
    /// </summary>
    /// <param name="objeto">Con el Id del usuario a que se le agregara el registro</param>
    /// <returns>true si agrega existosamente, false en caso de error</>
    public override void Insertar(Entidad objeto)
    {
      bool rs;
      Notificacion notificacion = (Notificacion)objeto;
      try
      {
        base.Conectar();
        comando = new NpgsqlCommand("agregar_notificacion", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, notificacion.IdUsuario);
        respuesta = comando.ExecuteReader();
        respuesta.Read();
        rs = respuesta.GetBoolean(0);
        base.Desconectar();
        
      }
      catch (NpgsqlException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (InvalidCastException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (NullReferenceException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        base.Desconectar();
        throw e;
      }
    }
  }
}
