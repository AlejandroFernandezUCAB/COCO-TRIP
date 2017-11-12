using ApiRest_COCO_TRIP.Models.Excepcion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Reflection;

namespace ApiRest_COCO_TRIP.Models
{
  public class PeticionEvento
  {
    private  ConexionBase conexion;
    private  NpgsqlDataReader read;
    private  NpgsqlCommand comando;

    public PeticionEvento()
    {
            try
            {
                conexion = new ConexionBase();
                conexion.Conectar();
            }
            catch (BaseDeDatosExcepcion e)
            {
                e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
                throw e;
            }
        }
      /// <summary>
      /// Metodo que agrega eventos y retorna ecenario de exito y fallo
      /// </summary>
      /// <param name="evento"> Objeto del tipo Evento</param>
      /// <returns> Respuesta de Agregar con exito</returns>
    public  int AgregarEvento(Evento evento)
    {
            int respuesta = -1;
            try
            {

                comando = new NpgsqlCommand("InsertarEvento", conexion.SqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                //Aqui registro los valores
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, evento.Nombre);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, evento.Descripcion);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Double, evento.Precio);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, evento.FechaInicio);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, evento.FechaFin);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Timestamp, evento.HoraInicio);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Timestamp, evento.HoraFin);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, evento.Foto);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, evento.IdCategoria);
                comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, evento.IdLocalidad);
                read = comando.ExecuteReader();
                read.Read();
                respuesta = read.GetInt32(0);
                conexion.Desconectar();
            }
            catch (BaseDeDatosExcepcion e)
            {
                e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
                throw e;
            }
            return respuesta;
           
    }

    internal static bool EliminarEvento(int v, int id)
    {
      throw new NotImplementedException();
    }

    internal static Evento ConsultarEvento(int id)
    {
      throw new NotImplementedException();
    }

    internal static bool EliminarEvento(int id)
    {
      throw new NotImplementedException();
    }

    internal static List<Evento> ListaEventosPorCategoria(int id_categoria)
    {
      throw new NotImplementedException();
    }
  }
}
