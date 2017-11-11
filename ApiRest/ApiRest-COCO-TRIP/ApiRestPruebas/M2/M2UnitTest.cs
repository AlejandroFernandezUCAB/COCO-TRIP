using NUnit.Framework;
using ApiRest_COCO_TRIP.Models;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Controllers;
using Npgsql;

namespace ApiRestPruebas.M2
{
  [TestFixture]
  class M2UnitTest
  {

    private Usuario usuario;
    private Categoria categoria, categoria2;
    private int posicionDelElemento;
    private M2_PerfilPreferenciasController apiRest;
    NpgsqlDataReader pgread;

    [SetUp]
    public void SetUp() {
      
      //Creando los objetos vacios
      usuario = new Usuario();
      categoria = new Categoria();
      categoria2 = new Categoria();
      apiRest = new M2_PerfilPreferenciasController();
     
      //Inicializando categoria
      categoria.Id = 1;
      categoria.Nombre = "Deportes";
      categoria.Descripcion = "Los deportes son geniales";
      categoria.Nivel = 1;
      categoria.Estatus = true;

      categoria2.Id = 2;
      categoria2.Nombre = "Deportes";
      categoria2.Descripcion = "Los deportes son geniales";
      categoria2.Nivel = 2;
      categoria2.Estatus = true;

      usuario.NombreUsuario = "Hola";

    }

    [Test]
    [Category("Objeto")]
    public void AgregandoObjetoCategoriaAlList()
    {

      usuario.AgregarPreferencia( categoria  );
      usuario.AgregarPreferencia( categoria2 );

      Assert.IsNotNull(  usuario              );
      Assert.IsNotNull(  categoria            );
      Assert.IsNotNull(  categoria2           );
      Assert.IsNotNull(  usuario.Preferencias );

      Assert.IsNotEmpty( usuario.Preferencias               );
      Assert.AreEqual  ( categoria  , usuario.Preferencias[0] );
      Assert.AreEqual  ( categoria2 , usuario.Preferencias[1] );

    }

    [Test]
    [Category("Objeto")]
    public void BusquedaObjetoCategoriaDelListEncontrado()
    {

      usuario.AgregarPreferencia( categoria  );
      usuario.AgregarPreferencia( categoria2 );
      posicionDelElemento = usuario.BusquedaDePreferencia( categoria );

      Assert.IsNotNull( usuario               );
      Assert.IsNotNull( categoria             );
      Assert.IsNotNull( usuario.Preferencias  );

      Assert.IsNotEmpty( usuario.Preferencias    );
      Assert.AreEqual  ( 0 , posicionDelElemento );

    }

    [Test]
    [Category("Objeto")]
    public void BusquedaObjetoCategoriaDelListNoEncontrado()
    {

      usuario.AgregarPreferencia(categoria);
      posicionDelElemento = usuario.BusquedaDePreferencia(categoria2);

      Assert.IsNotNull( usuario              );
      Assert.IsNotNull( categoria            );
      Assert.IsNotNull( usuario.Preferencias );

      Assert.IsNotEmpty( usuario.Preferencias     );
      Assert.AreEqual  ( -1, posicionDelElemento  );

    }

    [Test]
    [Category("Insertar")]
    public void AgregarPreferencia()
    {

      int count = 0;
      ConexionBase conexion = new ConexionBase();
      List<Categoria> lista = new List<Categoria>();

      lista = apiRest.AgregarPreferencias("conexion", "Deporte");
      conexion.Conectar();
      NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) cantidad " +
                    "FROM preferencia where pr_usuario = 1 and pr_categoria = 1", conexion.SqlConexion);
      pgread = command.ExecuteReader();

      while(pgread.Read())
        count = pgread.GetInt32(0);

      conexion.Desconectar();
      Assert.AreEqual( lista.Count , count );
      
    }

    [Test]
    [Category("Eliminar")]
    public void EliminarPrefencia()
    {

      int count = 0;
      ConexionBase conexion = new ConexionBase();
      List<Categoria> lista = new List<Categoria>();

      lista = apiRest.EliminarPreferencias("conexion", "Deporte");
      conexion.Conectar();
      NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) cantidad " +
                    "FROM preferencia where pr_usuario = 1 and pr_categoria = 1", conexion.SqlConexion);
      pgread = command.ExecuteReader();

      while (pgread.Read())
        count = pgread.GetInt32(0);

      conexion.Desconectar();
      Assert.AreEqual(lista.Count, count);

    }

    [TearDown]
    public void TearDown() {

      usuario    = null;
      categoria  = null;
      categoria2 = null;
      pgread = null;

    }

  }
}
