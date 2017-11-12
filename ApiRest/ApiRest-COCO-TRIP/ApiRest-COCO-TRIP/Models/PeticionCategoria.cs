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
      //conexion.Comando = new NpgsqlCommand();
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

  }
}
