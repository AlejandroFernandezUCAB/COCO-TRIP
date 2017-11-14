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


      }
      catch (NpgsqlException ex)
      {
        BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
        {
          DatosAsociados = $" ID : {categoria.Id}, ESTATUS: {categoria.Estatus}",
          Mensaje = $"Error al momento de actualizar la catgoria {categoria.Id}"
        };
        throw bdException;

      }
      finally
      {
        conexion.Desconectar();

      }
    }

    public IList<Categoria> ObtenerCategorias(Categoria categoria)
    {
      IList<Categoria> listaCategorias = new List<Categoria>();
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
        listaCategorias = SetListaCategoria();

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

    public IList<Categoria> ObtenerTodasLasCategorias()
    {
      IList<Categoria> listaCategorias;
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.CommandText = "m9_devolverTodasCategorias";
        leerDatos = conexion.Comando.ExecuteReader();
        listaCategorias = SetListaCategoria();
      }
      catch (NpgsqlException ex)
      {
        BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
        {
          Mensaje = $"Error al momento de buscar las todas categorias"
        };
        throw bdException;

      }
      finally
      {
        conexion.Desconectar();

      }
      return listaCategorias;

    }

    public Categoria ObtenerIdCategoriaPorNombre(Categoria categoria)
    {
      try
      {
        int Superior = 0;
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.CommandText = "m9_devolverid";
        conexion.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, categoria.Nombre);
        leerDatos = conexion.Comando.ExecuteReader();
        if (leerDatos.Read())
        {
          Int32.TryParse(leerDatos.GetValue(0).ToString(), out Superior);
        }

        if (Superior == 0)
        {
          throw new ItemNoEncontradoException($"No se encontro la categoria con el nombre {categoria.Nombre}");
        }

        categoria.Id = Superior;

      }
      catch (NpgsqlException ex)
      {
        BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
        {
          Mensaje = $"Error al momento de buscar el id de una categoria a partir del nombre"
        };
        throw bdException;

      }
      finally
      {
        conexion.Desconectar();

      }

      return categoria;
    }

    private IList<Categoria> SetListaCategoria()
    {

      IList<Categoria> listaCategorias = new List<Categoria>();
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
        Int32.TryParse(leerDatos.GetValue(5).ToString(), out int Superior);
        listaCategorias[listaCategorias.Count - 1].CategoriaSuperior = Superior;
      }

      return listaCategorias;
    }

    public void ModificarCategoria(Categoria categoria)
    {

      int exitoso = 0;
      try
      {

        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandText = "m9_ModificarCategoria";
        conexion.Comando.CommandType = CommandType.StoredProcedure;

        conexion.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Id);
        conexion.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, categoria.Nombre);
        conexion.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, categoria.Descripcion);

        if (categoria.CategoriaSuperior == 0)
        {
          conexion.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, DBNull.Value);

        }
        else
        {
          conexion.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.CategoriaSuperior);
        }

        

        exitoso = conexion.Comando.ExecuteNonQuery();


      }
      catch (NpgsqlException ex)
      {
        BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
        {
          DatosAsociados = $" ID : {categoria.Id}, NOMBRE : {categoria.Nombre}, DESCRIPCION : {categoria.Descripcion}, CATEGORIASUPERIOR : {categoria.CategoriaSuperior} ",
          Mensaje = $"Error al momento de actualizar la catgoria {categoria.Id}"
        };
        throw bdException;

      }
      finally
      {
        conexion.Desconectar();

      }
    }

    public IList<Categoria> ObtenerCategoriasHabilitadas()
    {
      IList<Categoria> listaCategorias;
      try
      {
        conexion.Conectar();
        conexion.Comando = conexion.SqlConexion.CreateCommand();
        conexion.Comando.CommandType = CommandType.StoredProcedure;
        conexion.Comando.CommandText = "m9_ConsultarCategoriaHabilitada";/* aqui va el stored procedure */
        leerDatos = conexion.Comando.ExecuteReader();
        listaCategorias = SetListaCategoria();
      }
      catch (NpgsqlException ex)
      {
        BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
        {
          Mensaje = $"Error al momento de buscar las todas categorias"
        };
        throw bdException;

      }
      finally
      {
        conexion.Desconectar();

      }
      return listaCategorias;

    }

    public void AgregarCategoria(Categoria categoria)
        {  //Endpoint CREAR UNA NUEVA CATEGORIA
            try
            {
                conexion.Conectar();
                conexion.Comando = conexion.SqlConexion.CreateCommand();
                conexion.Comando.CommandText = "m9_agregarsubcategoria";
                conexion.Comando.CommandType = CommandType.StoredProcedure;

                conexion.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, categoria.nombre); //Nombre de la categoria
                conexion.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, categoria.descripcion); //descripcion de la categoría
                conexion.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.nivel); //nivel de la categoria
                conexion.Comando.Parameters.AddWithValue(NpgsqlDbType.Boolean, true); // status de la categoria, en true.
               

                     if (categoria.CategoriaSuperior == 0)
                {
                    conexion.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, DBNull.Value);

                }
                else
                {
                    conexion.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.CategoriaSuperior);
                }
                exitoso = conexion.Comando.ExecuteNonQuery();


            }
            catch (NpgsqlException ex)
            {
                BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
                {
                    DatosAsociados = $" ID : {categoria.Id}, ESTATUS: {categoria.Estatus}",
                    Mensaje = $"Error al momento de agregar la catgoria {categoria.Id}"
                };

                throw bdException;

            }
            finally
            {
                conexion.Desconectar();

            }
        }


    }



}
