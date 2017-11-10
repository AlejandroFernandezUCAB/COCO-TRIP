using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Controllers;
using System.Data;


namespace ApiRestPruebas.M3
{
  [TestFixture]
  class M3UnitTest
  {

    Usuario usuario1, usuario2, usuario3;
    Grupo grupo;
    PeticionAmigoGrupo peticion;
    ConexionBase conexion;

    [SetUp]
    public void SetUp()
    {
      conexion = new ConexionBase();
      conexion.Conectar();
      conexion.Comando = conexion.SqlConexion.CreateCommand();
      conexion.Comando.CommandText = "INSERT INTO Usuario VALUES (-1 ,'usuariopruebas1', 'Aquiles','pulido',to_date('1963-09-01', 'YYYY-MM-DD') ,'F','usuariopruebas1@gmail.com','123456', null, true);" +
        "INSERT INTO Usuario VALUES (-2 ,'usuariopruebas2', 'Aquiles','pulido',to_date('1963-09-01', 'YYYY-MM-DD') ,'F','usuariopruebas2@gmail.com','123456', null, true);"+
        "INSERT INTO Grupo VALUES (-1,'Grupoprueba1',null,-1);" +
        "INSERT INTO Grupo VALUES (-2,'Grupoprueba2',null,-2);" +
        "INSERT INTO Miembro VALUES (-1,-1,-1);" +
"INSERT INTO Miembro VALUES (-2,-1,-2);";
      conexion.Comando.CommandType = CommandType.Text;
      conexion.Comando.ExecuteReader();
      conexion.Desconectar();
      peticion = new PeticionAmigoGrupo();
      
    }

    [TearDown]
    public void TearDown()
    {
      conexion = new ConexionBase();
      conexion.Conectar();
      conexion.Comando = conexion.SqlConexion.CreateCommand();
      conexion.Comando.CommandText = "Delete from miembro where mi_id < 0;" +
        "Delete from Grupo where gr_id < 0;" +
        "Delete from amigo where fk_usuario_conoce <0 or fk_usuario_posee <0;" +
        "Delete from usuario where us_id < 0;";
      conexion.Comando.CommandType = CommandType.Text;
      conexion.Comando.ExecuteReader();
      conexion.Desconectar();
    }


    [Test]
    public void TestAgregarAmigo()
    {
      PeticionAmigoGrupo peticion = new PeticionAmigoGrupo();
      Assert.AreEqual(peticion.AgregarAmigosBD("usuariopruebas1", "usuariopruebas2"),1);
    }

    [Test]
    public void TestVisualizarPerfilAmigo()
    {
      peticion = new PeticionAmigoGrupo();
      Usuario u = peticion.VisualizarPerfilAmigoBD("usuariopruebas1");
      Assert.AreEqual("Aquiles",u.Nombre);
    }

    [Test]
    public void TestSalirGrupo()
    {
      peticion = new PeticionAmigoGrupo();
      Assert.IsTrue(peticion.SalirGrupoBD(-1,"usuariopruebas1"));
    }
    /// <summary>
    /// Prueba para eliminar un amigo
    /// </summary>
    [Test]
    public void EliminarAmigoTest() {
      peticion.AgregarAmigosBD("usuario1", "usuario2");
      Assert.AreEqual(1, peticion.EliminarAmigoBD("usuario1", "usuario2"));
    }

    /// <summary>
    /// Prueba para eliminar un grupo
    /// </summary>
    [Test]
    public void EliminarGrupoTest() {
      peticion.AgregarGrupoBD("Grupo", "usuario1");
      Assert.AreEqual(1, peticion.EliminarGrupoBD("usuario1", 1));
    }

    /// <summary>
    /// Prueba para visualizar lista de grupos
    /// </summary>
    [Test]
    public void VisualizarListaAmigos() {
      peticion.AgregarAmigosBD("usuario1", "usuario2");
      peticion.AgregarAmigosBD("usuario1", "usuario3");
      List<Usuario> lista = new List<Usuario>();
      lista.Add(usuario1);
      lista.Add(usuario2);
      Assert.AreEqual(lista, peticion.VisualizarListaAmigoBD("usuario1"));
    }

    /// <summary>
    /// Prueba para modificar los atributos del grupo
    /// </summary>
    [Test]
    public void ModificarGrupoTest() {
      peticion.AgregarGrupoBD("Grupo", "usuario1");
      Assert.AreEqual(1, peticion.ModificarGrupoBD("GrupoTest","usuario1",1));
    }

    /// <summary>
    /// Prueba para eliminar los integrantes de un grupo al modificar
    /// </summary>
    [Test]
    public void EliminarIntegranteModificarTest() {
      peticion.AgregarGrupoBD("Grupo", "usuario1");
      peticion.AgregarIntegranteModificarBD(1, "usuario2");
      Assert.AreEqual(1, peticion.EliminarIntegranteModificarBD("usuario2",1));

    }

    /// <summary>
    /// Prueba para agregar un integrante a un grupo al modificar
    /// </summary>
    [Test]
    public void AgregarIntegranteModificarTest()
    {
      peticion.AgregarGrupoBD("Grupo", "usuario1");
      Assert.AreEqual(1, peticion.AgregarIntegranteModificarBD(1, "usuario2"));

    }

    /// <summary>
    /// Prueba para obtener el id del usuario dado el nombre de usuario
    /// </summary>
    [Test]
    public void ObtenerIdUsuarioTest()
    {
      Assert.AreEqual(1, peticion.ObtenerIdUsuario("usuario1"));
    }

    [Test]
    public void TestInsertarGrupo()
    {
      Assert.AreEqual(1, peticion.AgregarGrupoBD("aaaaa","usuario55"));
    }

    [Test]
    public void TestPerfilGrupo()
    {
      grupo = peticion.ConsultarPerfilGrupo(3);
      Assert.AreEqual("El MEGAGRUPO", grupo.Nombre);
    }

    [Test]
    public void TestListaGrupo()
    {
      List<Grupo> lista = new List<Grupo>();
      grupo.Nombre = "holaa";
      //lista = controlador.ConsultarListaGrupos("1");
      //grupo.Nombre = "";
      //grupo.Foto =new byte[0];

     // Assert.AreEqual(true, controlador.ConsultarListaGrupos("3").Contains(grupo));
    }
    
  }
}
