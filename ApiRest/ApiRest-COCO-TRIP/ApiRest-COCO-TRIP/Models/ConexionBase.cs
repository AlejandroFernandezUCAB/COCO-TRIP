using Npgsql;
using System.Reflection;
using ApiRest_COCO_TRIP.Models.Excepcion;

namespace ApiRest_COCO_TRIP.Models
{
  /// <summary>
  /// Clase que gestiona la conexion entre la base de datos
  /// </summary>
  public class ConexionBase
  {
    private const string Configuracion = "Host=localhost;Port=5433;Username=admin_cocotrip;Password=ds1718a;Database=cocotrip";
    private const string Configuracion = "Host=localhost;Port=5432;Username=admin_cocotrip;Password=ds1718a;Database=cocotrip";
    private NpgsqlConnection sqlConexion; //Conexion con la base de datos PostgreSQL
    private NpgsqlCommand comando; //Instrucion SQL a ejecutar

    /// <summary>
    /// Getters y Setters del atributo SqlConexion
    /// </summary>
    public NpgsqlConnection SqlConexion { get => sqlConexion; set => sqlConexion = value; }

    /// <summary>
    /// Getters y Setters del atributo Comando
    /// </summary>
    public NpgsqlCommand Comando { get => comando; set => comando = value; }

    /// <summary>
    /// Constructor de la clase
    /// </summary>
    public ConexionBase()
    {
      SqlConexion = new NpgsqlConnection(Configuracion);
      comando = new NpgsqlCommand();
    }

    /// <summary>
    /// Inicia la conexion con la base de datos
    /// </summary>
    public void Conectar()
    {
      try
      {
        if(SqlConexion.State != System.Data.ConnectionState.Open)
        {
          sqlConexion.Open();
        }
          
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
        sqlConexion.Close();
      }
      catch (NpgsqlException e)
      {
        var excepcion = new BaseDeDatosExcepcion(e);
        excepcion.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        excepcion.DatosAsociados = "Fallo de desconexion con la base de datos";

        throw excepcion;
      }
    }
  }
}
