using System;

namespace ApiRest_COCO_TRIP.Datos.Entity
{
  public class Usuario: Entidad
  {
    private string nombreUsuario;
    private string correo;
    private string nombre;
    private string apellido;
    private string genero;
    private DateTime fechaNacimiento;
    private string foto;
    private string clave;
    private Boolean valido;
    //private List<Categoria> preferencias;


    public Usuario()
    {

      //preferencias = new List<Categoria>();

    }

    public string Correo { get => correo; set => correo = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Apellido { get => apellido; set => apellido = value; }
    public string Genero { get => genero; set => genero = value; }
    public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
    public string Clave { get => clave; set => clave = value; }
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    //public List<Categoria> Preferencias { get => preferencias ; set => preferencias = value; }
    public bool Valido { get => valido; set => valido = value; }
    public string Foto { get => foto; set => foto = value; }

    /// <summary>
    /// Metodo que agrega a la lista de categoria (Es decir agrega una preferencia) una nueva categoría
    /// </summary>
    /// <param name="nuevaCategoria"> Categoría nueva a agregar en el objeto </param>
    /// 
    /*public void AgregarPreferencia ( Categoria nuevaCategoria )
    {
      
      preferencias.Add( nuevaCategoria );

    }*/
     


    /*public void EliminarPreferencia( Categoria categoriaAEliminar )
    {

      int elementoAEliminar;
      elementoAEliminar = BusquedaDePreferencia( categoriaAEliminar );
      preferencias.RemoveAt( elementoAEliminar );

    }*/

    /// <summary>
    /// Busqueda de la posicion a borrar
    /// </summary>
    /// <param name="categoriaABuscar">Categoria con que queremos comparar</param>
    /// <returns>La posicion donde se encuertra el elemento a eliminar</returns>
    /*public int BusquedaDePreferencia( Categoria categoriaABuscar )
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

    }*/
    
  }
}
