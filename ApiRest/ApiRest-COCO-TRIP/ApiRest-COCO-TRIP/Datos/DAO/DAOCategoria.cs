using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using System.Data;
using Npgsql;
using NpgsqlTypes;
using System;
using ApiRest_COCO_TRIP.Models.Excepcion;
using System.Linq;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
  public class DAOCategoria : DAO
  {
    private NpgsqlParameter parametro;
    private NpgsqlDataReader leerDatos;

    private NpgsqlParameter AgregarParametro(NpgsqlDbType tipoDeDato, object valor)
    {
      var parametro = new NpgsqlParameter
      {
        NpgsqlDbType = tipoDeDato,
        Value = valor
      };

      return parametro;
    }
    private List<Entidad> lista;
    private Categoria categoria;
 
    public DAOCategoria()
    {
      parametro = new NpgsqlParameter();
      lista = new List<Entidad>();
    }

    private void StoredProcedure(string sp)
    {
      base.Conectar();
      base.Comando = base.SqlConexion.CreateCommand();
      base.Comando.CommandType = CommandType.StoredProcedure;
      base.Comando.CommandText = sp;
    }

    private void ParametrosModificar(Categoria categoria)
    {
      base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Id);
      base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, categoria.Nombre);
      base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, categoria.Descripcion);
      base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Nivel);
    }

    //Metodos CREATE
    public override void Insertar(Entidad objeto)
    { 
      categoria = (Categoria)objeto;
      try
      {
        int exitoso = 0;
        StoredProcedure("m9_agregarsubcategoria");
        base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, categoria.Nombre); //Nombre de la categoria
        base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, categoria.Descripcion); //descripcion de la categoría
        base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Nivel); //nivel de la categoria
        base.Comando.Parameters.AddWithValue(NpgsqlDbType.Boolean, true); // status de la categoria, en true.


        if (categoria.CategoriaSuperior == 0)
        {
          base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, DBNull.Value);

        }
        else
        {
          base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.CategoriaSuperior);
        }

        exitoso = base.Comando.ExecuteNonQuery();


      }

      catch (PostgresException ex)
      {


        throw new NombreDuplicadoException($"Esta Categoria id:{categoria.Id} No se puede agregar con el nombre:{categoria.Nombre} Porque este nombre ya existe");

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
        base.Desconectar();

      }
    }

    //Metodos READ
    public override Entidad ConsultarPorId(Entidad objeto)
    {
      throw new NotImplementedException();
    }

    public override List<Entidad> ConsultarLista(Entidad objeto)
    {
      throw new NotImplementedException();
    }

    //Metodos UPDATE
    public override void Actualizar(Entidad objeto)
    {
      categoria = (Categoria)objeto;
      int exitoso = 0;
      try
      {

        StoredProcedure("m9_ModificarCategoria");
        DAOCategoria daoc = FabricaDAO.CrearDAOCategoria();
        IList<Categoria> Lcategoria = daoc.ObtenerCategoriaPorId(categoria);

        if (Lcategoria.First<Categoria>().Nivel == categoria.Nivel)
        {
          ParametrosModificar(categoria);
        }
        else
        {
          //categories = listaCategorias.Where(s => s.Id == id).First();
          IList<Categoria> Listacategoria = daoc.ObtenerTodasLasCategorias();
          var hijos = Listacategoria.Where(item => item.CategoriaSuperior == categoria.Id).ToList();
          if (hijos.Count == 0)
          {
            ParametrosModificar(categoria);
          }
          else
          {

            throw new HijoConDePendenciaException($"Esta categoria id:{categoria.Id} nombre:{categoria.Nombre} tiene hijos");
          }
        }
        if (categoria.CategoriaSuperior == 0)
        {
          base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, DBNull.Value);
        }
        else
        {          
          base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.CategoriaSuperior);
        }
        exitoso = base.Comando.ExecuteNonQuery();

      }

      catch (PostgresException ex)
      {
        throw new NombreDuplicadoException($"Esta Categoria id:{categoria.Id} No se puede agregar con el nombre:{categoria.Nombre} Porque este nombre ya existe");
      }

      catch (NpgsqlException ex)
      {
        BaseDeDatosExcepcion bdException = new BaseDeDatosExcepcion(ex)
        {
          DatosAsociados = $" ID : {categoria.Id}, NOMBRE : {categoria.Nombre}, DESCRIPCION : {categoria.Descripcion}, CATEGORIASUPERIOR : {categoria.CategoriaSuperior}, NIVEL : {categoria.Nivel} ",
          Mensaje = $"Error al momento de actualizar la catgoria {categoria.Id}"
        };
        throw bdException;

      }
      finally
      {
        base.Desconectar();

      }
    }
    public void ActualizarEstado(Entidad objeto)
    {
      categoria = (Categoria)objeto;
      int exitoso = 0;
      try
      {
        StoredProcedure("m9_actualizarEstatusCategoria");
        base.Comando.Parameters.AddWithValue(NpgsqlDbType.Boolean, categoria.Estatus);
        base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Id);

        exitoso = base.Comando.ExecuteNonQuery();


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
        base.Desconectar();

      }
    }
    //Metodos DELETE
    public override void Eliminar(Entidad objeto)
    {
      throw new NotImplementedException();
    }

    //Metodos extra
    //TODOS LOS DE CONSULTA ESTAN ACA PORQUE NO CUADRAN CON LOS GENERALES
    /// <summary>
    ///Lee los datos de la categoria
    /// </summary>
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

    /// <summary>
    /// EndPoint para obtener las categorias hijas a partir de una categoria padre dado un ID,
    /// si el id no viene en la peticion se devuelve las categorias padres absolutas
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IList<Categoria> ObtenerCategoriasHabilitadas()
    {
      IList<Categoria> listaCategorias;
      try
      {
        StoredProcedure("m9_ConsultarCategoriaHabilitada");
        leerDatos = base.Comando.ExecuteReader();
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
        base.Desconectar();

      }
      return listaCategorias;

    }

    /// <summary>
    /// Obtener la categorida dado un Id
    /// </summary>
    /// <param name="categoria"></param>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    public IList<Categoria> ObtenerCategoriaPorId(Entidad entidad)
    {
      categoria = (Categoria)entidad;
      IList<Categoria> listaCategorias;
      try
      {
        StoredProcedure("m9_ObtenerCategoriaPorId");
        base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Id);
        leerDatos = base.Comando.ExecuteReader();
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
       base.Desconectar();

      }
      return listaCategorias;

    }

    /// <summary>
    /// Obtener las listas de la categorias
    /// </summary>
    /// <param name="categoria"></param>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    public IList<Categoria> ObtenerTodasLasCategorias()
    {
      IList<Categoria> listaCategorias;
      try
      {
        StoredProcedure("m9_devolverTodasCategorias");
        leerDatos = base.Comando.ExecuteReader();
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
        base.Desconectar();

      }
      return listaCategorias;

    }

    /// <summary>
    /// Obtener el id de una categorida dado el nombre de la misma
    /// </summary>
    /// <param name="categoria"></param>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    public Entidad ObtenerIdCategoriaPorNombre(Entidad entidad)
    {
      categoria = (Categoria)entidad;
      try
      {
        int Superior = 0;
        StoredProcedure("m9_devolverid");
        base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, categoria.Nombre);
        leerDatos = base.Comando.ExecuteReader();
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
        base.Desconectar();

      }

      return categoria;
    }

    /// <summary>
    /// Obtener la lista de la categoria
    /// </summary>
    /// <param name="categoria"></param>
    /// <exception cref="BaseDeDatosExcepcion"></exception>
    public IList<Categoria> ObtenerCategorias(Entidad categoria)
    {
      IList<Categoria> listaCategorias = new List<Categoria>();
      try
      {
        
        if (categoria.Id == -1)
        {
          StoredProcedure("m9_obtenerCategoriaTop");
        }
        else
        {
          StoredProcedure("m9_obtenerCategoriaNoTop");
          base.Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Id);
        }

        leerDatos = base.Comando.ExecuteReader();
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
        base.Desconectar();
      }
      return listaCategorias;
    }

     /// <summary>
     /// Devuelve la lista de Horarios de un lugar turistico especifico
     /// </summary>
     /// <param name="id"></param>
     /// <returns></returns>
     public List<Entidad> ConsultarLista(string id)
     {
         throw new NotImplementedException();
     }

    }
}
