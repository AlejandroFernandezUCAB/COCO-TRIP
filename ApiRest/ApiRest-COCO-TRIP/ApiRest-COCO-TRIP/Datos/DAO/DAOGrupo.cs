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

    private List<Entidad> lista;

    public DAOGrupo()
    {
      parametro = new NpgsqlParameter();
      lista = new List<Entidad>();
    }

    /// <summary>
    /// Inserta el grupo y retorna el identificador del grupo para almacenar la foto en disco
    /// </summary>
    /// <param name="_grupo">Grupo</param>
    /// <returns>Grupo con identificador</returns>
    public Entidad InsertarId (Entidad _grupo)
    {
      grupo = (Grupo) _grupo;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "AgregarGrupo";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Varchar; //Ingresa parametros de entrada
      parametro.Value = grupo.Nombre;
      base.Comando.Parameters.Add(parametro);

      /*parametro.NpgsqlDbType = NpgsqlDbType.Varchar; //Ingresa parametros de entrada
      parametro.Value = grupo.RutaFoto;
      base.Comando.Parameters.Add(parametro);*/

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = grupo.Lider;
      base.Comando.Parameters.Add(parametro);

      leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

      if(leerDatos.Read())
      {
        grupo.Id = leerDatos.GetInt32(0);
      }

      leerDatos.Close(); //Cierra el Data Reader

      base.Desconectar(); //Culmina la sesion con la base de datos

      return grupo;
    }

    public void AgregarIntegrante(Entidad _grupo, Entidad _usuario)
    {
      grupo = (Grupo) _grupo;
      usuario = (Usuario) _usuario;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "AgregarIntegrante";
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

    public override Entidad ConsultarPorId(Entidad objeto)
    {
      grupo = (Grupo) objeto;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "ConsultarPerfilGrupo";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = grupo.Id;
      base.Comando.Parameters.Add(parametro);

      leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

      if (leerDatos.Read())
      {
        grupo.Nombre = leerDatos.GetString(0);
        //grupo.RutaFoto = leerDatos.GetString(1);
        grupo.CantidadIntegrantes = leerDatos.GetInt32(2);
      }

      leerDatos.Close(); //Cierra el Data Reader

      base.Desconectar(); //Culmina la sesion con la base de datos

      return grupo;
    }

    public override List<Entidad> ConsultarLista(Entidad objeto)
    {
      usuario = (Usuario) objeto;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "ConsultarListaGrupos";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = usuario.Id;
      base.Comando.Parameters.Add(parametro);

      leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

      while (leerDatos.Read()) //Lee los resultados
      {
        Grupo fila = FabricaEntidad.CrearEntidadGrupo();

        fila.Id = leerDatos.GetInt32(0);
        fila.Nombre = leerDatos.GetString(1);
        //fila.RutaFoto = leerDatos.GetString(2);

        lista.Add(fila);
      }

      leerDatos.Close(); //Cierra el Data Reader

      base.Desconectar(); //Culmina la sesion con la base de datos

      return lista;
    }

    public Entidad ConsultarUltimoGrupo (Entidad _usuario)
    {
      usuario = (Usuario) _usuario;
      grupo = FabricaEntidad.CrearEntidadGrupo();

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "ConsultarUltimo";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = usuario.Id;
      base.Comando.Parameters.Add(parametro);

      leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

      if (leerDatos.Read())
      {
        grupo.Id = leerDatos.GetInt32(0);
        grupo.Nombre = leerDatos.GetString(1);
        //grupo.RutaFoto = leerDatos.GetString(2);
      }

      leerDatos.Close(); //Cierra el Data Reader

      base.Desconectar(); //Culmina la sesion con la base de datos

      return grupo;
    }

    public List<Entidad> ConsultarMiembros (Entidad _grupo)
    {
      grupo = (Grupo) _grupo;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "VisualizarMiembroGrupo";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = grupo.Id;
      base.Comando.Parameters.Add(parametro);

      leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

      while (leerDatos.Read()) //Lee los resultados
      {
        Usuario fila = FabricaEntidad.CrearEntidadUsuario();

        fila.Id = leerDatos.GetInt32(0);
        fila.Nombre = leerDatos.GetString(1);
        fila.Apellido = leerDatos.GetString(2);
        fila.NombreUsuario = leerDatos.GetString(3);
        //fila.Foto = leerDatos.GetString(4);

        lista.Add(fila);
      }

      leerDatos.Close(); //Cierra el Data Reader

      base.Desconectar(); //Culmina la sesion con la base de datos

      return lista;
    }

    public List<Entidad> ConsultarMiembrosExceptoLider (Entidad _grupo)
    {
      grupo = (Grupo)_grupo;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "VisualizarMiembroSinLider";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = grupo.Id;
      base.Comando.Parameters.Add(parametro);

      leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

      while (leerDatos.Read()) //Lee los resultados
      {
        Usuario fila = FabricaEntidad.CrearEntidadUsuario();

        fila.Id = leerDatos.GetInt32(0);
        fila.Nombre = leerDatos.GetString(1);
        fila.Apellido = leerDatos.GetString(2);
        fila.NombreUsuario = leerDatos.GetString(3);
        //fila.Foto = leerDatos.GetString(4);

        lista.Add(fila);
      }

      leerDatos.Close(); //Cierra el Data Reader

      base.Desconectar(); //Culmina la sesion con la base de datos

      return lista;
    }

    public List<Entidad> ConsultarMiembrosSinGrupo (Entidad _grupo, Entidad _usuario)
    {
      grupo = (Grupo) _grupo;
      usuario = (Usuario) _usuario;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "ListaAmigosSinGrupo";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = grupo.Id;
      base.Comando.Parameters.Add(parametro);

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = usuario.Id;
      base.Comando.Parameters.Add(parametro);

      leerDatos = base.Comando.ExecuteReader(); //Ejecuta el comando

      while (leerDatos.Read()) //Lee los resultados
      {
        Usuario fila = FabricaEntidad.CrearEntidadUsuario();

        fila.Id = leerDatos.GetInt32(0);
        fila.Nombre = leerDatos.GetString(1);
        fila.Apellido = leerDatos.GetString(2);
        fila.NombreUsuario = leerDatos.GetString(3);
        //fila.Foto = leerDatos.GetString(4);

        lista.Add(fila);
      }

      leerDatos.Close(); //Cierra el Data Reader

      base.Desconectar(); //Culmina la sesion con la base de datos

      return lista;
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
        //usuario.Foto = leerDatos.GetString(4);
      }

      leerDatos.Close(); //Cierra el Data Reader

      base.Desconectar(); //Culmina la sesion con la base de datos

      return usuario;
    }

    public override void Eliminar(Entidad objeto)
    {
      grupo = (Grupo)objeto;

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

    public void EliminarIntegrante(Entidad _grupo, Entidad _usuario)
    {
      grupo = (Grupo) _grupo;
      usuario = (Usuario) _usuario;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "EliminarIntegrante";
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
      grupo = (Grupo) objeto;

      base.Conectar(); //Inicia una sesion con la base de datos

      base.Comando = base.SqlConexion.CreateCommand(); //Crea el comando
      base.Comando.CommandText = "ModificarNombreGrupo";
      base.Comando.CommandType = CommandType.StoredProcedure;

      parametro.NpgsqlDbType = NpgsqlDbType.Integer; //Ingresa parametros de entrada
      parametro.Value = grupo.Id;
      base.Comando.Parameters.Add(parametro);

      parametro.NpgsqlDbType = NpgsqlDbType.Varchar; //Ingresa parametros de entrada
      parametro.Value = grupo.Nombre;
      base.Comando.Parameters.Add(parametro);

      base.Comando.ExecuteNonQuery(); //Ejecuta el comando

      base.Desconectar(); //Culmina la sesion con la base de datos
    }

    public override void Insertar (Entidad objeto)
    {
      throw new System.NotImplementedException();
    }
  }
}