using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Models
{
  public class Categoria
  {

    private int id;

    [JsonProperty(PropertyName = "IdCategoria")]
    public int Id
    {
      get { return id; }
      set { id = value; }
    }

    private string nombre;

    [JsonProperty(PropertyName = "nombre")]
    public string Nombre
    {
      get { return nombre; }
      set { nombre = value; }
    }

    private string descripcion;

    [JsonProperty(PropertyName = "descripcion")]
    public string Descripcion
    {
      get { return descripcion; }
      set { descripcion = value; }
    }

    private bool estatus;

    [JsonProperty(PropertyName = "estatus")]
    public bool Estatus
    {
      get { return estatus; }
      set { estatus = value; }
    }

    private int nivel;

    [JsonProperty(PropertyName = "nivel")]
    public int Nivel
    {
      get { return nivel; }
      set { nivel = value; }
    }

    private int categoriaSuperior;

    [JsonProperty(PropertyName = "categoriaSuperior")]
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
