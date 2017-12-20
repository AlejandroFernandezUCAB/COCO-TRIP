using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using System.Data;
using Npgsql;
using NpgsqlTypes;

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

    public override Entidad ConsultarId(Entidad objeto)
    {
      amigo = (Amigo) objeto;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "ExisteSolicitud";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = amigo.Activo;
      base.Comando.Parameters.Add(parametro);

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

    public override void Insertar(Entidad objeto)
    {
      amigo = (Amigo) objeto;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "AgregarAmigo";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = amigo.Activo;
      base.Comando.Parameters.Add(parametro);

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = amigo.Pasivo;
      base.Comando.Parameters.Add(parametro);

      base.Comando.ExecuteNonQuery(); //Ejecuta el comando

      leerDatos.Close(); //Cierra el Data Reader

      base.Desconectar(); //Culmina la sesion con la base de datos
    }
    
    public override Entidad ConsultarPorId(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }

    public override List<Entidad> ConsultarLista(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }

    public override Entidad ConsultarPorNombre(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }

    public override void Actualizar(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }

    public override void Eliminar(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }
  }
}
