using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Controllers;
using System.Data;
using Npgsql;

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
        "INSERT INTO Usuario VALUES (-2 ,'usuariopruebas2', 'Mariangel','Perez',to_date('1963-09-01', 'YYYY-MM-DD') ,'F','usuariopruebas2@gmail.com','123456', null, true);"+
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

    //PRUEBAS UNITARIAS DE AGREGAR AMIGO
    //CREADO POR: OSWALDO LOPEZ
    /// <summary>
    /// Test para probar el caso de exito del metodo agregar amigo
    /// </summary>
    [Test]
    public void TestAgregarAmigo()
    {
      peticion = new PeticionAmigoGrupo();
      Assert.AreEqual(peticion.AgregarAmigosBD(-1, "usuariopruebas2"),1);
    }

    /// <summary>
    /// Test para probar el caso de falla cuando se ingresa null en los parametros
    /// del metodo agregar amigo
    /// </summary>
    [Test]
    public void TestAgregarAmigoFallidoCast()
    {
      Assert.Catch<InvalidCastException>(ExcepcionAgregarAmigoMalCast);
    }

    public void ExcepcionAgregarAmigoMalCast()
    {
      peticion = new PeticionAmigoGrupo();
      peticion.AgregarAmigosBD(0, null);
    }

    /// <summary>
    /// Test para probar el caso de falla cuando se ingresa nombres de usuarios
    /// que no estan registrados del metodo agregar amigo
    /// </summary>
    [Test]
    public void TestAgregarAmigoFallidoNoExiste()
    {
      Assert.Catch<NpgsqlException>(ExcepcionAgregarAmigoMalNoExiste);
    }

    public void ExcepcionAgregarAmigoMalNoExiste()
    {
      peticion = new PeticionAmigoGrupo();
      peticion.AgregarAmigosBD(-8, "usuarioramdon2");
    }


    //PRUEBAS UNITARIAS DE VISUALIZAR PERFIL AMIGO
    //CREADO POR: OSWALDO LOPEZ
    /// <summary>
    /// Test para probar el caso de exito del metodo Visualizar Perfil amigo
    /// </summary>
    [Test]
    public void TestVisualizarPerfilAmigo()
    {
      peticion = new PeticionAmigoGrupo();
      Usuario u = peticion.VisualizarPerfilAmigoBD("usuariopruebas1");
      Assert.AreEqual("Aquiles",u.Nombre);
    }
    /// <summary>
    /// Test para probar el caso de falla cuando se ingresa null en el metodo Visualizar Perfil amigo
    /// </summary>
    [Test]
    public void TestVisualizarPerfilAmigoFallidoCast()
    {
      Assert.Catch<InvalidCastException>(ExcepcionVisualizarPerfilAmigoMalCast);
    }

    public void ExcepcionVisualizarPerfilAmigoMalCast()
    {
      peticion = new PeticionAmigoGrupo();
      peticion.VisualizarPerfilAmigoBD(null);
    }


    /// <summary>
    /// Test para probar el caso de falla cuando se ingresa nombres de usuarios
    /// que no estan registrados del metodo agregar amigo
    /// </summary>
    ///
    [Test]
    public void TestVisualizarPerfilAmigoFallidoNoExiste()
    {
      peticion = new PeticionAmigoGrupo();
      Assert.IsNull(peticion.VisualizarPerfilAmigoBD("usuarioramdon"));
    }


    //PRUEBAS UNITARIAS DE SALIR DE GRUPO
    //CREADO POR: OSWALDO LOPEZ
    /// <summary>
    /// Test para probar el caso de exito del metodo salir de grupo
    /// </summary>
    [Test]
    public void TestSalirGrupo()
    {
      peticion = new PeticionAmigoGrupo();
      Assert.AreEqual(1,peticion.SalirGrupoBD(-1,-1));
    }

    /// <summary>
    /// Test para probar el caso de falla cuando se ingresa null en los parametros
    /// del metodo agregar amigo
    /// </summary>
    [Test]
    public void TestSalirGrupoFallidoCast()
    {
      Assert.Catch<FormatException>(ExcepcionSalirGrupoMalCast);
    }

    public void ExcepcionSalirGrupoMalCast()
    {
      peticion = new PeticionAmigoGrupo();
      peticion.SalirGrupoBD(2, Convert.ToInt32("sdf"));
    }

    /// <summary>
    /// Test para probar el caso de falla cuando se ingresa nombres de usuarios
    /// que no estan registrados en la tabla miembro del metodo Salir grupo
    /// </summary>
    [Test]
    public void TestSalirGrupoFallidoNoExisteUsuario()
    {
      peticion = new PeticionAmigoGrupo();
      Assert.AreEqual(0,peticion.SalirGrupoBD(1,-88));

    }

    /// <summary>
    /// Test para probar el caso de falla cuando se ingresa id de grupo
    /// que no estan registrados en la tabla miembro del metodo Salir grupo
    /// </summary>
    [Test]
    public void TestSalirGrupoFallidoNoExisteGrupo()
    {
      peticion = new PeticionAmigoGrupo();
      Assert.AreEqual(0,peticion.SalirGrupoBD(-10, -1));

    }








    //PRUEBAS UNITARIAS DE ELIMINAR AMIGO
    //CREADO POR: MARIANGEL PEREZ
    /// <summary>
    /// Prueba para eliminar un amigo
    /// </summary>
    [Test]
    public void EliminarAmigoTest() {
      peticion.AgregarAmigosBD(-1, "usuariopruebas2");
      //Assert.AreEqual(1, peticion.EliminarAmigoBD("usuariopruebas1", "usuariopruebas2"));
    }

    //PRUEBAS UNITARIAS DE ELIMINAR GRUPO
    //CREADO POR: MARIANGEL PEREZ
    /// <summary>
    /// Prueba para eliminar un grupo
    /// </summary>
    [Test]
    public void EliminarGrupoTest() {
     // Assert.AreEqual(1, peticion.EliminarGrupoBD("usuariopruebas1", -1));
    }

    //PRUEBAS UNITARIAS DE VISUALIZAR LISTA DE AMIGOS
    //CREADO POR: MARIANGEL PEREZ
    /// <summary>
    /// Prueba para visualizar lista de amigos
    /// </summary>
    [Test]
    public void VisualizarListaAmigos() {
      peticion.AgregarAmigosBD(-1, "usuariopruebas2");
      List<Usuario> lista = new List<Usuario>();
     // lista = peticion.VisualizarListaAmigoBD("usuariopruebas1");
      Usuario usuario = new Usuario();
      foreach (Usuario u in lista) {
          usuario = u;
      }
      Assert.AreEqual("Mariangel", usuario.Nombre);
      Assert.AreEqual("Perez", usuario.Apellido);
      Assert.AreEqual(null, usuario.Foto);
    }

    //PRUEBAS UNITARIAS DE MODIFICAR GRUPO
    //CREADO POR: MARIANGEL PEREZ
    /// <summary>
    /// Prueba para modificar los atributos del grupo
    /// </summary>
    [Test]
    public void ModificarGrupoTest() {
      //Assert.AreEqual(1, peticion.ModificarGrupoBD("GrupoTest", "usuariopruebas1", -1));
    }

    //PRUEBAS UNITARIAS DE ELIMINAR LOS INTEGRANTES DE UN GRUPO AL MODIFICAR
    //CREADO POR: MARIANGEL PEREZ
    /// <summary>
    /// Prueba para eliminar los integrantes de un grupo al modificar
    /// </summary>
    [Test]
    public void EliminarIntegranteModificarTest() {
      peticion.AgregarIntegranteModificarBD(-2, "usuariopruebas1");
      Assert.AreEqual(1, peticion.EliminarIntegranteModificarBD("usuariopruebas1", -2));

    }

    //PRUEBAS UNITARIAS DE AGREGAR UN INTEGRANTE AL GRUPO AL MODIFICAR
    //CREADO POR: MARIANGEL PEREZ
    /// <summary>
    /// Prueba para agregar un integrante a un grupo al modificar
    /// </summary>
    [Test]
    public void AgregarIntegranteModificarTest()
    {
      Assert.AreEqual(1, peticion.AgregarIntegranteModificarBD(-2, "usuariopruebas1"));
    }

    //PRUEBAS UNITARIAS DE OBTENER ID POR NOMBRE DE USUARIO
    //CREADO POR: MARIANGEL PEREZ
    /// <summary>
    /// Prueba para obtener el id del usuario dado el nombre de usuario
    /// </summary>
    [Test]
    public void ObtenerIdUsuarioTest()
    {
      Assert.AreEqual(-1, peticion.ObtenerIdUsuario("usuariopruebas1"));
    }

    /// <summary>
    /// Prueba para insertar un grupo dado el id del grupo
    /// </summary>
    [Test]
    public void TestInsertarGrupo()
    {
      Assert.AreEqual(2, peticion.AgregarGrupoBD( "usuariopruebas1", -1));
    }

    /// <summary>
    /// Prueba excepcion invalidcast insertando null
    /// </summary>
    [Test]
    public void TestInsertarGrupoMal()
    {
      Assert.Catch<InvalidCastException>(ExcepcionGrupoMal);
    }

    public void ExcepcionGrupoMal()
    {
      peticion.AgregarGrupoBD( null,1);
    }

    /// <summary>
    /// Prueba la excepcion de un grupo fallido
    /// </summary>
    [Test]
    public void TestInsertarGrupoFallidoNoExiste()
    {
      Assert.Catch<NpgsqlException>(ExcepcionAgregarGrupoMalNoExiste);
    }

    public void ExcepcionAgregarGrupoMalNoExiste()
    {
      peticion.AgregarGrupoBD("usuarioramdon1", -50);
    }

    /// <summary>
    /// Prueba que se pueda consultar exitosamente el perfil de grupo
    /// </summary>
    [Test]
    public void TestPerfilGrupo()
    {
      
      List<Grupo> lista = new List<Grupo>();
      lista = peticion.ConsultarPerfilGrupo(-1); 
      Grupo grupo = new Grupo();
      foreach (Grupo g in lista)
      {
        grupo = g;
      }
      Assert.AreEqual("Grupoprueba1", grupo.Nombre);
    }

    [Test]
    public void TestPerfilGrupoNoExiste()
    {
      Assert.Catch<NpgsqlException>(ExcepcionVerperfilGrupoMalNoExiste);
      
    }

    public void ExcepcionVerperfilGrupoMalNoExiste()
    {
      List<Grupo> lista = new List<Grupo>();
      lista = peticion.ConsultarPerfilGrupo(-50);
    }

    [Test]
    public void TestListaGrupo()
    {
      List<Grupo> lista = new List<Grupo>();
      lista = peticion.Listagrupo(-1);
      Grupo grupo = new Grupo();
      foreach (Grupo g in lista)
      {
        grupo = g;
      }
      Assert.AreEqual("Grupoprueba1", grupo.Nombre);
    }

    [Test]
    public void BuscarAmigo()
    {

      List<Usuario> lista = new List<Usuario>();
      lista = peticion.BuscarAmigo("Aquiles",1);
      Usuario usuario = new Usuario();
      foreach (Usuario u in lista)
      {
        usuario = u;
      }
      Assert.AreEqual("Aquiles", usuario.Nombre);

    }

  }
}
