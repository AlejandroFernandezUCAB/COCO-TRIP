using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using System.Data;
using Npgsql;
using NpgsqlTypes;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
  /// <summary>
  /// DAO de la entidad Usuario
  /// </summary>
  public class DAOUsuario : DAO
  {
    private NpgsqlParameter parametro;
    private NpgsqlDataReader leerDatos;

    private Usuario usuario;

    public DAOUsuario ()
    {
      parametro = new NpgsqlParameter();
    }

    public override Entidad ConsultarPorId(Entidad objeto)
    {
      usuario = (Usuario) objeto;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "ConsultarUsuarioSoloId";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro = new NpgsqlParameter();
      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = usuario.Id;
      base.Comando.Parameters.Add(parametro);

      leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

      if (leerDatos.Read()) //Lee los resultados
      {
        usuario.NombreUsuario = leerDatos.GetString(0);
        usuario.Correo = leerDatos.GetString(1);
        usuario.Nombre = leerDatos.GetString(2);
        usuario.Apellido = leerDatos.GetString(3);
        usuario.FechaNacimiento = leerDatos.GetDateTime(4);
        usuario.Genero = leerDatos.GetString(5);
        //usuario.Foto = leerDatos.GetString(6);
      }

      leerDatos.Close(); //Cierra el Data Reader

      base.Desconectar(); //Culmina la sesion con la base de datos

      return usuario;
    }

    public Entidad ConsultarPorNombre(Entidad _usuario)
    {
      usuario = (Usuario) _usuario;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "ConsultarUsuarioSoloNombre";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro = new NpgsqlParameter();
      parametro.NpgsqlDbType = NpgsqlDbType.Varchar; //Ingresa parametros de entrada
      parametro.Value = usuario.NombreUsuario;
      base.Comando.Parameters.Add(parametro);

      leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

      if (leerDatos.Read()) //Lee los resultados
      {
        usuario.Id = leerDatos.GetInt32(0);
        usuario.NombreUsuario = leerDatos.GetString(1);
        usuario.Correo = leerDatos.GetString(2);
        usuario.Nombre = leerDatos.GetString(3);
        usuario.Apellido = leerDatos.GetString(4);
        usuario.FechaNacimiento = leerDatos.GetDateTime(5);
        usuario.Genero = leerDatos.GetString(6);
        usuario.Valido = leerDatos.GetBoolean(7);
        //usuario.Foto = leerDatos.GetString(8);
      }

      leerDatos.Close(); //Cierra el Data Reader

      base.Desconectar(); //Culmina la sesion con la base de datos

      return usuario;
    }

    public override List<Entidad> ConsultarLista(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }

    public override void Eliminar(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }

    public override void Insertar(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }

    public override void Actualizar(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }
  }

}
