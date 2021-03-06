using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BackOffice_COCO_TRIP.Datos.Entidades
{
  /// <summary>
  /// Clase que contiene los datos de lugar turistico
  /// </summary>
  public class LugarTuristico: Entidad
  {
    
    private string nombre; //Nombre del lugar turistico
    private double costo; //Costo del lugar turistico
    private string descripcion; //Descripcion del lugar turistico
    private string direccion; //Ubicacion del lugar turistico
    private string correo; //Correo electronico del lugar turistico
    private long telefono; //Numero de telefono del lugar turistico
    private double latitud; //Coordenada Google Maps
    private double longitud; //Coordenada Google Maps
    private bool activar; //Activar o desactivar lugar turistico

    private List<Foto> foto; //Fotos del lugar turistico
    private List<Horario> horario; //Horarios del lugar turistico
    private List<Actividad> actividad; //Actividades del lugar turistico
    private List<Categoria> categoria; //Categorias del lugar turistico 
    private List<Categoria> subCategoria; //Subcategorias del lugar turistico

    /// <summary>
    /// Constructor que inicializa los atributos de la clase
    /// </summary>
    public LugarTuristico()
    {
      // Mi propuesta era pedirle las entidades a los dao.
      // o a un comando. Por eso cree los dao, ver nota.

      foto = new List<Foto>();
      horario = new List<Horario>();
      actividad = new List<Actividad>();

      categoria = new List<Categoria>();
      subCategoria = new List<Categoria>();

      // Nota: el apirest devuelve todo junto como un json
      // y ya tiene aplicado patrones al parecer...
      // para no hacer mas daos sin necesidad, y evitar hacer mas llamadas
      // (porque ya todo lo que necesitamos lo envia el api en el json)
      // lo que se hara:
      // eliminar los dao de actividad, foto, horario y hacer que el dao Lugar_turistico
      // extraiga la data, cree las entidades
      // (foto, horario, actividad, categoria y subCategoria)
      // y las pase a esta entidad (crear costructor), para luego devolverla al cliente.





      //ejemplo json del apirest
//[
//    {
//        "Id": 1,
//        "Nombre": "Parque Generalisimo de Miranda",
//        "Costo": 0,
//        "Descripcion": "Lugar al aire libre",
//        "Direccion": null,
//        "Correo": null,
//        "Telefono": 0,
//        "Latitud": 0,
//        "Longitud": 0,
//        "Activar": true,
//        "Foto": [
//            {
//                "Id": 1,
//                "Ruta": "Ruta LT1.jpg",
//                "Contenido": null
//            }
//        ],
//        "Horario": [
//            {
//                "Id": 0,
//                "DiaSemana": 0,
//                "HoraApertura": "08:00:00",
//                "HoraCierre": "18:00:00"
//            }
//        ],
//        "Actividad": [],
//        "Categoria": [],
//        "SubCategoria": []
//    }
//]





    }


    /// <summary>
    /// Getters y Setters del atributo Nombre
    /// </summary>
    [Required(ErrorMessage = "Debe llenar este campo")]
    [MaxLength(400)]
    public string Nombre
    {
      get { return nombre; }
      set { nombre = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Costo
    /// </summary>
    [Required(ErrorMessage = "Debe llenar este campo")]
    [RegularExpression("[0-9]*", ErrorMessage = "El costo debe ser positivo")]
    public double Costo
    {
      get { return costo; }
      set { costo = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Descripcion
    /// </summary>
    [Required(ErrorMessage = "Debe llenar este campo")]
    [MaxLength(2000)]
    public string Descripcion
    {
      get { return descripcion; }
      set { descripcion = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Direccion
    /// </summary>
    [Required(ErrorMessage = "Debe llenar este campo")]
    [MaxLength(2000)]
    public string Direccion
    {
      get { return direccion; }
      set { direccion = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Correo
    /// </summary>
    [Required(ErrorMessage = "Debe llenar este campo")]
    [MaxLength(320)]
    public string Correo
    {
      get { return correo; }
      set { correo = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Telefono
    /// </summary>
    [Required]
    [RegularExpression("[0-9]*", ErrorMessage = "Solo se aceptan numeros")]
    public long Telefono
    {
      get { return telefono; }
      set { telefono = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Latitud
    /// </summary>
    [Required(ErrorMessage = "Debe seleccionar una ubicacion en el mapa")]
    public double Latitud
    {
      get { return latitud; }
      set { latitud = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Longitud
    /// </summary>
    [Required(ErrorMessage = "Debe seleccionar una ubicacion en el mapa")]
    public double Longitud
    {
      get { return longitud; }
      set { longitud = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Activar
    /// </summary>
    [Required(ErrorMessage = "Debe seleccionar una opcion")]
    public bool Activar
    {
      get { return activar; }
      set { activar = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Foto
    /// </summary>
    [Required(ErrorMessage = "Debe agregar al menos una foto")]
    public List<Foto> Foto
    {
      get { return foto; }
      set { foto = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Horario
    /// </summary>
    [Required(ErrorMessage = "Debe agregar al menos un horario")]
    public List<Horario> Horario
    {
      get { return horario; }
      set { horario = value; }
    }

    /// <summary>
    /// Getters y Setters del atributo Horario
    /// </summary>
    [Required(ErrorMessage = "Debe agregar al menos una actividad")]
    public List<Actividad> Actividad
    {
      get { return actividad; }
      set { actividad = value; }

    }

    /// <summary>
    /// Getters y Setters del atributo Categoria
    /// </summary>
    [Required(ErrorMessage = "Debe seleccionar al menos una categoria")]
    public List<Categoria> Categoria
    {
      get { return categoria; }
      set { categoria = value; }

    }

    /// <summary>
    /// Getters y Setters del atributo SubCategoria
    /// </summary>
    [Required(ErrorMessage = "Debe seleccionar al menos una subcategoria")]
    public List<Categoria> SubCategoria
    {
      get { return subCategoria; }
      set { subCategoria = value; }

    }

    /// <summary>
    /// Compara si dos objetos de tipo LugarTuristico son iguales
    /// </summary>
    /// <param name="obj">LugarTuristico</param>
    /// <returns>(bool) Si son iguales retorna true</returns>
    public override bool Equals(object obj)
    {
      if (obj != null && obj is LugarTuristico)
      {
        var objeto = obj as LugarTuristico;

        if (nombre != objeto.nombre || costo != objeto.costo || descripcion != objeto.descripcion
            || direccion != objeto.direccion || correo != objeto.correo || telefono != objeto.telefono || latitud != objeto.latitud
            || longitud != objeto.longitud || activar != objeto.activar || !foto.SequenceEqual<Foto>(objeto.foto)
            || !horario.SequenceEqual<Horario>(objeto.horario) || !actividad.SequenceEqual<Actividad>(objeto.actividad)
          /*|| !categoria.SequenceEqual<Categoria>(objeto.Categoria) || !subCategoria.SequenceEqual<Categoria>(objeto.SubCategoria)*/)
        {

          return (false);
        }
        else
        {
          return (true);
        }
      }
      else
      {
        return (false);
      }
    }

    /// <summary>
    /// Sobreescritura recomendada del metodo GetHashCode
    /// </summary>
    /// <returns>ID del objeto</returns>
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }

}
