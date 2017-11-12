using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Models.Excepcion;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiRest_COCO_TRIP
{
  public class PeticionCategoria
  {

    private ConexionBase conexion;
    private NpgsqlDataReader leerDatos;

    public PeticionCategoria()
    {
      conexion = new ConexionBase();
    }

    private NpgsqlParameter AgregarParametro(NpgsqlDbType tipoDeDato, object valor)
    {
      var parametro = new NpgsqlParameter
      {
        NpgsqlDbType = tipoDeDato,
        Value = valor
      };

      return parametro;
    }

    public void ActualizarEstatus(Categoria categoria)
    {

      int exitoso = 0;
      try
      {
        
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "m9_actualizarEstatusCategoria";
        conexion.Comando.CommandType = CommandType.StoredProcedure;

        conexion.Comando.Parameters.AddWithValue(NpgsqlDbType.Boolean, categoria.Estatus);
        conexion.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Id);

        exitoso = conexion.Comando.ExecuteNonQuery();
        

      } catch( NpgsqlException ex)
      {
        BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
        {
          DatosAsociados = $" ID : {categoria.Id}, ESTATUS: {categoria.Estatus}",
          Mensaje = $"Error al momento de actualizar la catgoria {categoria.Id}"
        };
        throw bdException;

      } finally
      {
        conexion.Desconectar();

      }  
    }

    public IList<Categoria> ObtenerCategorias(Categoria categoria)
    {
      IList<Categoria> listaCategorias = new List<Categoria>();
      int Superior;
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        if (categoria.Id == -1)
        {
          conexion.Comando.CommandText = "m9_obtenerCategoriaTop";
        }
        else
        {
          conexion.Comando.CommandText = "m9_obtenerCategoriaNoTop";
          conexion.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Id);
        }

        leerDatos = conexion.Comando.ExecuteReader();
        while (leerDatos.Read())
        {
          listaCategorias.Add(new Categoria()
          {
            Id = leerDatos.GetInt32(0),
            Nombre = leerDatos.GetString(1),
            Descripcion = leerDatos.GetString(2),
            Estatus = leerDatos.GetBoolean(3),
            Nivel = leerDatos.GetInt32(4)
          });
          Int32.TryParse(leerDatos.GetValue(5).ToString(), out Superior);
          listaCategorias[listaCategorias.Count - 1].CategoriaSupeior = Superior;
        }
      }
      catch (NpgsqlException ex)
      {
        BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
        {
          DatosAsociados = $" ID : {categoria.Id}",
          Mensaje = $"Error al momento de buscar las categorias"
        };
        throw bdException;

      }
      finally
      {
        conexion.Desconectar();

      }


      return listaCategorias;
      

      
    }

  }
}
