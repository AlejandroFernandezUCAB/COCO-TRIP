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

    Usuario usuario1, usuario2;
    Grupo grupo;
    PeticionAmigoGrupo peticion;
    ConexionBase conexion;

    [SetUp]
    public void SetUp()
    {
      conexion = new ConexionBase();
      conexion.Conectar();
      conexion.Comando = conexion.SqlConexion.CreateCommand();
      conexion.Comando.CommandText = "INSERT INTO Usuario VALUES (-1 ,'usuario55', 'Aquiles','pulido',to_date('1963-09-01', 'YYYY-MM-DD') ,'F','pulidito@gmail.com','123456', null, true)";
      conexion.Comando.CommandType = CommandType.Text;
      conexion.Comando.ExecuteReader();
      conexion.Desconectar();
      peticion = new PeticionAmigoGrupo();
      usuario1 = new Usuario
      {
        Nombre = "Oswaldo",
        Apellido = "Lopez",
        Genero = "M",
        NombreUsuario = "oswalm",
        FechaNacimiento = new DateTime(2017, 03, 09),
        Correo = "oswaldo@gmail.com",
        Clave = "pruebaclave",
        Foto = new byte[28480]
      };

      usuario2 = new Usuario
      {
        Nombre = "Aquiles",
        Apellido = "Pulido",
        Genero = "M",
        NombreUsuario = "aquipu",
        FechaNacimiento = new DateTime(2017, 03, 09),
        Correo = "aqui@gmail.com",
        Clave = "pruebaclave",
        Foto = new byte[28480]
      };
    }
    [TearDown]
    public void TearDown()
    {
      conexion = new ConexionBase();
      conexion.Conectar();
      conexion.Comando = conexion.SqlConexion.CreateCommand();
      conexion.Comando.CommandText = "INSERT INTO Usuario VALUES (-1 ,'usuario55', 'Aquiles','pulido',to_date('1963-09-01', 'YYYY-MM-DD') ,'F','pulidito@gmail.com','123456', null, true)";
      conexion.Comando.CommandType = CommandType.Text;
      conexion.Comando.ExecuteReader();
      conexion.Desconectar();
    }

    [Test]
    public void TestAgregarAmigo()
    {
      PeticionAmigoGrupo peticion = new PeticionAmigoGrupo();
      Assert.AreEqual(peticion.AgregarAmigosBD("usuario1", "usuario2"),13);

    }

    [Test]
    public void TestVisualizarPerfilAmigo()
    {

    }

    [Test]
    public void TestSalirGrupo()
    {

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
