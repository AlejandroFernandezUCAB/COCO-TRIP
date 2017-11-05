using System;
using System.Collections.Generic;
using Npgsql;
using System.Data;

namespace ApiRest_COCO_TRIP.Models
{
  public class Usuario
  {
    private int id;
    private string nombreUsuario;
    private string correo;
    private string nombre;
    private string apellido;
    private string genero;
    private DateTime fechaNacimiento;
    private byte[] foto;
    private string clave;
    private List<Categoria> preferencias;


    public Usuario()
    {

      preferencias = new List<Categoria>();

    }



    public string Correo { get => correo; set => correo = value; }
    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Apellido { get => apellido; set => apellido = value; }
    public string Genero { get => genero; set => genero = value; }
    public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
    public byte[] Foto { get => foto; set => foto = value; }
    public string Clave { get => clave; set => clave = value; }
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    public List<Categoria> Preferencias { get => preferencias ; set => preferencias = value; }

    /// <summary>
    /// Metodo que agrega a la lista de categoria (Es decir agrega una preferencia) una nueva categoría
    /// </summary>
    /// <param name="nuevaCategoria"> Categoría nueva a agregar en el objeto </param>
    /// 
    public void AgregarPreferencia ( Categoria nuevaCategoria )
    {
      
      preferencias.Add( nuevaCategoria );
      /*Esto va en la clase de peticion
      //A partir de aquí se agregará en base de datos
      ConexionBase conexion = new ConexionBase();
      conexion.Conectar();
      ConexionBase con = new ConexionBase();
      con.Conectar();
      NpgsqlCommand comm = new NpgsqlCommand("consultarusuariosolonombre", con.SqlConexion);
      comm.CommandType = CommandType.StoredProcedure;
      comm.Parameters.AddWithValue( NpgsqlTypes.NpgsqlDbType.Varchar , nombreUsuario );
      NpgsqlDataReader pgread = comm.ExecuteReader();
      while (pgread.Read()) {
        Id = pgread.GetInt32(0);
      }
      conexion.Desconectar();
      //Fin de metodos de Bdd
      */
    }
     


    public void EliminarPreferencia( Categoria categoriaAEliminar )
    {

      int elementoAEliminar;
      elementoAEliminar = BusquedaDePreferencia( categoriaAEliminar );
      preferencias.RemoveAt( elementoAEliminar );

    }

    /// <summary>
    /// Busqueda de la posicion a borrar
    /// </summary>
    /// <param name="categoriaABuscar">Categoria con que queremos comparar</param>
    /// <returns>La posicion donde se encuertra el elemento a eliminar</returns>
    public int BusquedaDePreferencia( Categoria categoriaABuscar )
    {

      int retorno;
      retorno = preferencias.FindIndex(
        o =>
        o.Id == categoriaABuscar.Id &&
        o.Nombre == categoriaABuscar.Nombre &&
        o.Descripcion == categoriaABuscar.Descripcion &&
        o.Estatus == categoriaABuscar.Estatus &&
        o.Nivel == categoriaABuscar.Nivel
        );

      if (retorno == null)
        return retorno = -1;
      else
        return retorno ;

    }
    
  }
}
