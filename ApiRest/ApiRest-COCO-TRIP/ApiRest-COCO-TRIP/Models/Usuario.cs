using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    //private List<Categoria> preferencias;


    public Usuario()
    {
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
    //public List<Categoria> Preferencias { get => preferencias ; set => preferencias = value; }

    /*
    public List<Categoria> agregarPreferencia ( Categoria nuevaCategoria ){
      
      preferencias.add( nuevaCategoria );
    }
     */

    /*

    public List<Categoria> eliminarPreferencia(){

    }
     */
  }
}
