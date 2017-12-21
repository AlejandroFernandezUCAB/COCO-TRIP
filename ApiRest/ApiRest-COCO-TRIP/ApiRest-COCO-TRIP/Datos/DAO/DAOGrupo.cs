using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using System.Data;
using NpgsqlTypes;
using Npgsql;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
  /// <summary>
  /// DAO de la entidad Grupo
  /// </summary>
  public class DAOGrupo : DAO
  {
    private NpgsqlParameter parametro;
    private NpgsqlDataReader leerDatos;

    private Usuario usuario;
    private Grupo grupo;

    public DAOGrupo()
    {
      parametro = new NpgsqlParameter();
    }

    public Entidad ConsultarLider(Entidad _grupo)
    {
      grupo = (Grupo) _grupo;
      usuario = FabricaEntidad.CrearEntidadUsuario();

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "ObtenerLider";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = grupo.Id;
      base.Comando.Parameters.Add(parametro);

      leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

      if (leerDatos.Read()) //Lee los resultados
      {
        usuario.Id = leerDatos.GetInt32(0);
        usuario.Nombre = leerDatos.GetString(1);
        usuario.Apellido = leerDatos.GetString(2);
        usuario.NombreUsuario = leerDatos.GetString(3);
        usuario.Foto = leerDatos.GetString(4);
      }

      leerDatos.Close(); //Cierra el Data Reader

      base.Desconectar(); //Culmina la sesion con la base de datos

      return usuario;
    }

    public override void Eliminar (Entidad objeto)
    {
      grupo = (Grupo) objeto;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "EliminarGrupo";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = grupo.Id;
      base.Comando.Parameters.Add(parametro);

      base.Comando.ExecuteNonQuery(); //Ejecuta el comando

      base.Desconectar(); //Culmina la sesion con la base de datos
    }

    public void AbandonarGrupo (Entidad _grupo, Entidad _usuario)
    {
      grupo = (Grupo) _grupo;
      usuario = (Usuario) _usuario;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "SalirDeGrupo";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = grupo.Id;
      base.Comando.Parameters.Add(parametro);

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = usuario.Id;
      base.Comando.Parameters.Add(parametro);

      base.Comando.ExecuteNonQuery(); //Ejecuta el comando

      base.Desconectar(); //Culmina la sesion con la base de datos
    }

    public override void Actualizar(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }

    public override Entidad ConsultarPorId(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }

    public override List<Entidad> ConsultarLista(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }

    public override void Insertar(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }
  }

}
