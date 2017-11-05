using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Models
{
  public class PeticionLogin
  {
    private ConexionBase conexion;
    private NpgsqlDataReader leerDatos;

    public PeticionLogin() {
      conexion = new ConexionBase();
    }

    private NpgsqlParameter AgregarParametro(NpgsqlDbType tipoDeDato, object valor)
    {
      var parametro = new NpgsqlParameter();

      parametro.NpgsqlDbType = tipoDeDato;
      parametro.Value = valor;

      return parametro;
    }
    public int ConsultarUsuarioCorreo(Usuario usuario) {
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "ConsultarUsuarioCorreo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;

        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Correo));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, usuario.Clave));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          usuario.Id = leerDatos.GetInt32(0);
          
        }

        leerDatos.Close();
        conexion.Desconectar();  
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      return usuario.Id;
    }
  }
}
