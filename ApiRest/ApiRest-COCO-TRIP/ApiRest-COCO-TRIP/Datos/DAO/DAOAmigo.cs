using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
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
    private Usuario usuario;

    private List<Entidad> lista;

    public DAOAmigo()
    {
      parametro = new NpgsqlParameter();
      lista = new List<Entidad>();
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

    public List<Entidad> BuscarAmigos(Entidad objeto)
    {
      usuario = (Usuario)objeto;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "BuscarAmigos";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = usuario.Id;
      base.Comando.Parameters.Add(parametro);

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

    public Entidad ConsultarId(Entidad objeto)
    {
      amigo = (Amigo)objeto;

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

    public override List<Entidad> ConsultarLista(Entidad objeto)
    {
      usuario = (Usuario) objeto;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "ObtenerListaDeAmigos";
      base.Comando.CommandType = CommandType.StoredProcedure;

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

    public List<Entidad> ConsultarListaNotificaciones(Entidad _usuario)
    {
      usuario = (Usuario) _usuario;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "ObtenerListaDeNotificaciones";
      base.Comando.CommandType = CommandType.StoredProcedure;

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

    public void RechazarNotificacion (Entidad _amigo)
    {
      amigo = (Amigo) _amigo;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "RechazarNotificacion";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = amigo.Pasivo;
      base.Comando.Parameters.Add(parametro);

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = amigo.Activo;
      base.Comando.Parameters.Add(parametro);

      base.Comando.ExecuteNonQuery(); //Ejecuta el comando

      leerDatos.Close(); //Cierra el Data Reader

      base.Desconectar(); //Culmina la sesion con la base de datos
    }

    public void AceptarNotificacion(Entidad _amigo)
    {
      amigo = (Amigo) _amigo;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "AceptarNotificacion";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = amigo.Pasivo;
      base.Comando.Parameters.Add(parametro);

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = amigo.Activo;
      base.Comando.Parameters.Add(parametro);

      base.Comando.ExecuteNonQuery(); //Ejecuta el comando

      leerDatos.Close(); //Cierra el Data Reader

      base.Desconectar(); //Culmina la sesion con la base de datos
    }

    public override void Eliminar(Entidad objeto)
    {
      amigo = (Amigo) objeto;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "EliminarAmigo";
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

    public override void Actualizar(Entidad objeto)
    {
      throw new System.NotImplementedException();
    }
  }
}
