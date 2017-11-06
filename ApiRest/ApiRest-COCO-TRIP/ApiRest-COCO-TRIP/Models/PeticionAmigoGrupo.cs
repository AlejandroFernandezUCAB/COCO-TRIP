using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using Npgsql;
using System.Data;
using Newtonsoft.Json;
using NpgsqlTypes;

namespace ApiRest_COCO_TRIP.Models
{
  public class PeticionAmigoGrupo 
  {
    private ConexionBase conexion;
    private NpgsqlDataReader leerDatos;

    public PeticionAmigoGrupo()
    {
      conexion = new ConexionBase();
    }

    private NpgsqlParameter AgregarParametro(NpgsqlDbType tipoDeDato, object valor)
    {
      var parametro = new NpgsqlParameter();

      parametro.NpgsqlDbType = tipoDeDato;
      parametro.Value = valor;

      return parametro;
    }

    public void AgregarAmigosBD(int idUsuario1 , int idUsuario2)
    {
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "AgregarAmigo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario1));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, idUsuario2));
        conexion.Comando.ExecuteReader();
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
    }

    public Usuario VisualizarPerfilAmigoBD(string nombreUsuario)
    {
      Usuario usuario = new Usuario();
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "VisualizarPerfilPublico";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, nombreUsuario));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          usuario.Nombre = leerDatos.GetString(0);
          usuario.Apellido = leerDatos.GetString(1);
          usuario.Correo = leerDatos.GetString(2);
          if (!leerDatos.IsDBNull(3))
          {
            usuario.Foto[0] = leerDatos.GetByte(3);
          }
          //usuario.Foto[0] = leerDatos.GetByte(3);
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

      return usuario;
    }

   public List<Usuario> BuscarAmigo(string dato)
    {
      var listausuarios = new List<Usuario>();
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "BuscarAmigos";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, dato));
        leerDatos = conexion.Comando.ExecuteReader();
        while (leerDatos.Read())
        {
          var usuario = new Usuario();
          usuario.Nombre = leerDatos.GetString(0);
          usuario.NombreUsuario = leerDatos.GetString(1);
          if (!leerDatos.IsDBNull(2))
          {
            usuario.Foto[0] = leerDatos.GetByte(3);
          }

          listausuarios.Add(usuario);
         
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

      return listausuarios;
    }

    public List<Grupo> Listagrupo(int dato)
    {
      var listagrupos = new List<Grupo>();
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "ConsultarListaGrupos";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, dato));
        leerDatos = conexion.Comando.ExecuteReader();
        while (leerDatos.Read())
        {
          var grupo = new Grupo();
          grupo.Nombre = leerDatos.GetString(0);
          if (!leerDatos.IsDBNull(1))
          {
            grupo.Foto[0] = leerDatos.GetByte(1);
          }

          listagrupos.Add(grupo);

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

      return listagrupos;
    }


    public Grupo ConsultarPerfilGrupo(int dato)
    {
      
      var grupo = new Grupo();
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "ConsultarPerfilGrupo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, dato));
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          
          grupo.Nombre = leerDatos.GetString(0);
          if (!leerDatos.IsDBNull(1))
          {
            grupo.Foto[0] = leerDatos.GetByte(3);
          }

         
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

      return grupo;
    }


    public void AgregarGrupoBD(String nombre, byte foto,int usuario)
    {
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "AgregarGrupo";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, nombre));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Bytea, foto));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, usuario));

        conexion.Comando.ExecuteReader();
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
    }

    public void AgregarGrupoBD(String nombre, int usuario)
    {
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "AgregarGrupoSinFoto";
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Varchar, nombre));
        //conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, ));
        conexion.Comando.Parameters.Add(AgregarParametro(NpgsqlDbType.Integer, usuario));

        conexion.Comando.ExecuteReader();
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
    }




  }
}
