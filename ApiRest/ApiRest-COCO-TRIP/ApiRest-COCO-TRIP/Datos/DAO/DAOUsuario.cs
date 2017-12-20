using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using System.Data;
using Npgsql;
using NpgsqlTypes;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
  public class DAOUsuario : DAO
  {
    private NpgsqlParameter parametro;
    private NpgsqlDataReader leerDatos;

    private Usuario usuario;

    public DAOUsuario ()
    {
      parametro = new NpgsqlParameter();
    }

    public override void Actualizar(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }

    public override Entidad Consultar(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }

    public override Entidad ConsultarId(Entidad objeto)
    {
      usuario = (Usuario) objeto;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "ConseguirIdUsuario";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Varchar; //Ingresa parametros de entrada
      parametro.Value = usuario.NombreUsuario;
      base.Comando.Parameters.Add(parametro);

      leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

      if(leerDatos.Read()) //Lee los resultados
      {
        usuario.Id = leerDatos.GetInt32(0);
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
  }
}
