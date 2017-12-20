using ApiRest_COCO_TRIP.Datos.Entity;
using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
  /// <summary>
  /// Superclase DAO
  /// </summary>
  public abstract class DAO
  {
    private const string Credenciales =
    "Host = localhost; Port = 5432; " +
    "Username = admin_cocotrip; " +
    "Password = ds1718a; " +
    "Database = cocotrip";

    private NpgsqlConnection sqlConexion; //Conexion con la base de datos PostgreSQL
    private NpgsqlCommand comando; //Instrucion SQL a ejecutar

    /// <summary>
    /// Constructor de la clase
    /// </summary>
    public DAO()
    {
      SqlConexion = new NpgsqlConnection(Credenciales);
      comando = new NpgsqlCommand();
    }

    /// <summary>
    /// Getters y Setters del atributo SqlConexion
    /// </summary>
    public NpgsqlConnection SqlConexion
    {
      get { return sqlConexion; }
      set { sqlConexion = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Comando
    /// </summary>
    public NpgsqlCommand Comando
    {
      get { return comando; }
      set { comando = value; }
    }

    /// <summary>
    /// Inicia la conexion con la base de datos
    /// </summary>
    public void Conectar()
    {
      if (sqlConexion.State != ConnectionState.Open)
      {
        sqlConexion.Open();
      }
    }

    /// <summary>
    /// Finaliza la conexion con la base de datos
    /// </summary>
    public void Desconectar()
    {
      if (sqlConexion.State != ConnectionState.Closed)
      {
        sqlConexion.Close();
      }
    }

    //Metodos CREATE
    public abstract void Insertar(Entidad objeto);

    //Metodos READ
    public abstract Entidad Consultar (Entidad objeto);
    public abstract Entidad ConsultarId (Entidad objeto);
    public abstract List<Entidad> ConsultarLista (Entidad objeto);

    //Metodos UPDATE
    public abstract void Actualizar (Entidad objeto);

    //Metodos DELETE
    public abstract void Eliminar (Entidad objeto);
  }
}
