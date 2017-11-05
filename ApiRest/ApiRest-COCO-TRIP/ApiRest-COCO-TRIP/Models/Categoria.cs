using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Models
{
  public class Categoria
  {

    private int id;

    public int Id
    {
      get { return id; }
      set { id = value; }
    }

    private string nombre;

    public string Nombre
    {
      get { return nombre; }
      set { nombre = value; }
    }

    private string descripcion;

    public string Descripcion
    {
      get { return descripcion; }
      set { descripcion = value; }
    }

    private bool estatus;

    public bool Estatus
    {
      get { return estatus; }
      set { estatus = value; }
    }

    private int nivel;

    public int Nivel
    {
      get { return nivel; }
      set { nivel = value; }
    }

    private int categoriaSuperior;

    public int CategoriaSupeior
    {
      get { return categoriaSuperior; }
      set { categoriaSuperior = value; }
    }



    public Categoria() { }

    public Categoria(int Id)
    {
      this.id = Id;
    }

    public Categoria(string Nombre, string Descripcion, bool Estatus, int Nivel, Categoria CategoriaSuperior)
    {
     
      this.nombre = Nombre;
      this.descripcion = Descripcion;
      this.estatus = Estatus;
      this.categoriaSuperior = CategoriaSupeior;

    }


    public Categoria(int Id, string Nombre, string Descripcion, bool Estatus, int Nivel, Categoria CategoriaSuperior)
    {
      this.id = Id;
      this.nombre = Nombre;
      this.descripcion = Descripcion;
      this.estatus = Estatus;
      this.categoriaSuperior = CategoriaSupeior;

    }


  }
}