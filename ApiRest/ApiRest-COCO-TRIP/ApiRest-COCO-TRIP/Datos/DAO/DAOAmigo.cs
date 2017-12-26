using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using System.Data;
using Npgsql;
using NpgsqlTypes;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System;
using System.Reflection;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
  /// <summary>
  /// DAO de la entidad Amigo
  /// </summary>
  public class DAOAmigo : DAO
  {
    private NpgsqlParameter parametro;
    private NpgsqlDataReader leerDatos;

    private Amigo amigo;
    private Usuario usuario;

    private List<Entidad> lista;

    public DAOAmigo()
    {
      parametro = new NpgsqlParameter();
      lista = new List<Entidad>();
    }

    public override void Insertar(Entidad objeto)
    {
      try
      {
        amigo = (Amigo) objeto;

        base.Conectar(); //Inicia una sesion con la base de datos

        base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
        base.Comando.CommandText = "AgregarAmigo";
        base.Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
        parametro.Value = amigo.Activo;
        base.Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
        parametro.Value = amigo.Pasivo;
        base.Comando.Parameters.Add(parametro);

        base.Comando.ExecuteNonQuery(); //Ejecuta el comando

        base.Desconectar(); //Culmina la sesion con la base de datos
      }
      catch(NpgsqlException e)
      {
        throw new BaseDeDatosExcepcion(e, "Error de logica de BD en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
      catch(NullReferenceException e)
      {
        throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
    }

    public List<Entidad> BuscarAmigos(Entidad objeto)
    {
      try
      {
        usuario = (Usuario)objeto;

        base.Conectar(); //Inicia una sesion con la base de datos

        base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
        base.Comando.CommandText = "BuscarAmigos";
        base.Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
        parametro.Value = usuario.Id;
        base.Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar; //Ingresa parametros de entrada
        parametro.Value = usuario.Nombre;
        base.Comando.Parameters.Add(parametro);

        leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

        while (leerDatos.Read()) //Lee los resultados
        {
          Usuario fila = FabricaEntidad.CrearEntidadUsuario();

          fila.Nombre = leerDatos.GetString(0);
          fila.NombreUsuario = leerDatos.GetString(1);
          //fila.Foto = leerDatos.GetString(2);

          lista.Add(fila);
        }

        leerDatos.Close(); //Cierra el Data Reader

        base.Desconectar(); //Culmina la sesion con la base de datos

        return lista;
      }
      catch (NpgsqlException e)
      {
        throw new BaseDeDatosExcepcion(e, "Error de logica de BD en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
      catch (NullReferenceException e)
      {
        throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
      catch (InvalidCastException e)
      {
        throw new CasteoInvalidoExcepcion(e, "El nombre de usuario es nulo en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
    }

    public override Entidad ConsultarPorId(Entidad objeto)
    {
      try
      {
        amigo = (Amigo)objeto;

        base.Conectar(); //Inicia una sesion con la base de datos

        base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
        base.Comando.CommandText = "ExisteSolicitud";
        base.Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
        parametro.Value = amigo.Activo;
        base.Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
        parametro.Value = amigo.Pasivo;
        base.Comando.Parameters.Add(parametro);

        leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

        if (leerDatos.Read()) //Lee los resultados
        {
          amigo.Id = leerDatos.GetInt32(0);
        }

        leerDatos.Close(); //Cierra el Data Reader

        base.Desconectar(); //Culmina la sesion con la base de datos

        return amigo;
      }
      catch (NpgsqlException e)
      {
        throw new BaseDeDatosExcepcion(e, "Error de logica de BD en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
      catch (NullReferenceException e)
      {
        throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
    }

    public override List<Entidad> ConsultarLista(Entidad objeto)
    {
      try
      {
        usuario = (Usuario)objeto;

        base.Conectar(); //Inicia una sesion con la base de datos

        base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
        base.Comando.CommandText = "ObtenerListaDeAmigos";
        base.Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
        parametro.Value = usuario.Id;
        base.Comando.Parameters.Add(parametro);

        leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

        while (leerDatos.Read()) //Lee los resultados
        {
          Usuario fila = FabricaEntidad.CrearEntidadUsuario();

          fila.Nombre = leerDatos.GetString(0);
          fila.Apellido = leerDatos.GetString(1);
          fila.NombreUsuario = leerDatos.GetString(2);
          //fila.Foto = leerDatos.GetString(3);

          lista.Add(fila);
        }

        leerDatos.Close(); //Cierra el Data Reader

        base.Desconectar(); //Culmina la sesion con la base de datos

        return lista;
      }
      catch (NpgsqlException e)
      {
        throw new BaseDeDatosExcepcion(e, "Error de logica de BD en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
      catch (NullReferenceException e)
      {
        throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
    }

    public List<Entidad> ConsultarListaNotificaciones(Entidad _usuario)
    {
      try
      {
        usuario = (Usuario)_usuario;

        base.Conectar(); //Inicia una sesion con la base de datos

        base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
        base.Comando.CommandText = "ObtenerListaDeNotificaciones";
        base.Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
        parametro.Value = usuario.Id;
        base.Comando.Parameters.Add(parametro);

        leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

        while (leerDatos.Read()) //Lee los resultados
        {
          Usuario fila = FabricaEntidad.CrearEntidadUsuario();

          fila.Nombre = leerDatos.GetString(0);
          fila.Apellido = leerDatos.GetString(1);
          fila.NombreUsuario = leerDatos.GetString(2);
          //fila.Foto = leerDatos.GetString(3);

          lista.Add(fila);
        }

        leerDatos.Close(); //Cierra el Data Reader

        base.Desconectar(); //Culmina la sesion con la base de datos

        return lista;
      }
      catch (NpgsqlException e)
      {
        throw new BaseDeDatosExcepcion(e, "Error de logica de BD en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
      catch (NullReferenceException e)
      {
        throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
    }

    public void RechazarNotificacion (Entidad _amigo)
    {
      try
      {
        amigo = (Amigo)_amigo;

        base.Conectar(); //Inicia una sesion con la base de datos

        base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
        base.Comando.CommandText = "RechazarNotificacion";
        base.Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
        parametro.Value = amigo.Pasivo;
        base.Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
        parametro.Value = amigo.Activo;
        base.Comando.Parameters.Add(parametro);

        base.Comando.ExecuteNonQuery(); //Ejecuta el comando

        base.Desconectar(); //Culmina la sesion con la base de datos
      }
      catch (NpgsqlException e)
      {
        throw new BaseDeDatosExcepcion(e, "Error de logica de BD en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
      catch (NullReferenceException e)
      {
        throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
    }

    public void AceptarNotificacion(Entidad _amigo)
    {
      try
      {
        amigo = (Amigo)_amigo;

        base.Conectar(); //Inicia una sesion con la base de datos

        base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
        base.Comando.CommandText = "AceptarNotificacion";
        base.Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
        parametro.Value = amigo.Pasivo;
        base.Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
        parametro.Value = amigo.Activo;
        base.Comando.Parameters.Add(parametro);

        base.Comando.ExecuteNonQuery(); //Ejecuta el comando

        base.Desconectar(); //Culmina la sesion con la base de datos
      }
      catch (NpgsqlException e)
      {
        throw new BaseDeDatosExcepcion(e, "Error de logica de BD en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
      catch (NullReferenceException e)
      {
        throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
    }

    public override void Eliminar(Entidad objeto)
    {
      try
      {
        amigo = (Amigo)objeto;

        base.Conectar(); //Inicia una sesion con la base de datos

        base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
        base.Comando.CommandText = "EliminarAmigo";
        base.Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
        parametro.Value = amigo.Activo;
        base.Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
        parametro.Value = amigo.Pasivo;
        base.Comando.Parameters.Add(parametro);

        base.Comando.ExecuteNonQuery(); //Ejecuta el comando

        base.Desconectar(); //Culmina la sesion con la base de datos
      }
      catch (NpgsqlException e)
      {
        throw new BaseDeDatosExcepcion(e, "Error de logica de BD en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
      catch (NullReferenceException e)
      {
        throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en "
        + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
      }
    }

    public override void Actualizar(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }
  }
}
