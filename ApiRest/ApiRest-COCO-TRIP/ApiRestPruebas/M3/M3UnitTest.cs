using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiRest_COCO_TRIP.Models;
using System.Data;
using System.Data.SqlClient;

namespace ApiRestPruebas.M3
{
  [TestFixture]
  class M3UnitTest
  {

    Usuario usuario1, usuario2;

    [SetUp]
    public void SetUp()
    {
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
      /*
      ConexionBase conexion = new ConexionBase();
      SqlConnection sqlConnection1 = new SqlConnection("Host=localhost;Username=admin_cocotrip;Password=ds1718a;Database=cocotrip");
      SqlCommand cmd = new SqlCommand();
      SqlDataReader reader;

      cmd.CommandText = "INSERT INTO Usuario VALUES( -1 ,'usuarioprueba1', 'Aquiles','pulido', to_date('1963-09-01', 'YYYY-MM-DD') ,'F','usuarioprueba1@gmail.com','123456', null, true);"+
      "INSERT INTO Usuario VALUES( -2 ,'usuarioprueba2', 'Ophy','Nightmare', to_date('1963-09-01', 'YYYY-MM-DD') ,'F','usuarioprueba2@gmail.com','123456', null, true); " +

      "INSERT INTO Usuario VALUES( -3 ,'usuarioprueba3', 'Oalm','Lopez', to_date('1963-09-01', 'YYYY-MM-DD') ,'F','usuarioprueba3@gmail.com','123456', null, true); " +
      "INSERT INTO Usuario VALUES( -5 ,'usuarioprueba4', 'User','user', to_date('1963-09-01', 'YYYY-MM-DD') ,'F','usuarioprueba4@gmail.com','123456', null, true); " +
      "INSERT INTO Usuario VALUES( -4 ,'usuarioprueba5', 'hey','tu', to_date('1963-09-01', 'YYYY-MM-DD') ,'F','usuarioprueba5@gmail.com','123456', null, true); "; 
      cmd.CommandType = CommandType.Text;
      cmd.Connection = sqlConnection1;

      sqlConnection1.Open();

      reader = cmd.ExecuteReader();
      // Data is accessible through the DataReader object here.

      sqlConnection1.Close();*/
      //////////////////////
      ConexionBase conexion = new ConexionBase();
      conexion.Conectar();

      conexion.Comando = conexion.SqlConexion.CreateCommand();
      conexion.Comando.CommandText = "INSERT INTO Usuario VALUES( -1 ,'usuarioprueba1', 'Aquiles','pulido', to_date('1963-09-01', 'YYYY-MM-DD') ,'F','usuarioprueba1@gmail.com','123456', null, true);" +
      "INSERT INTO Usuario VALUES( -2 ,'usuarioprueba2', 'Ophy','Nightmare', to_date('1963-09-01', 'YYYY-MM-DD') ,'F','usuarioprueba2@gmail.com','123456', null, true); " +

      "INSERT INTO Usuario VALUES( -3 ,'usuarioprueba3', 'Oalm','Lopez', to_date('1963-09-01', 'YYYY-MM-DD') ,'F','usuarioprueba3@gmail.com','123456', null, true); " +
      "INSERT INTO Usuario VALUES( -5 ,'usuarioprueba4', 'User','user', to_date('1963-09-01', 'YYYY-MM-DD') ,'F','usuarioprueba4@gmail.com','123456', null, true); " +
      "INSERT INTO Usuario VALUES( -4 ,'usuarioprueba5', 'hey','tu', to_date('1963-09-01', 'YYYY-MM-DD') ,'F','usuarioprueba5@gmail.com','123456', null, true); ";

      conexion.Comando.CommandType = CommandType.Text;
      conexion.Comando.ExecuteReader();
      conexion.Desconectar();

    }

    [Test]
    public void TestAgregarAmigo()
    {
      PeticionAmigoGrupo peticion = new PeticionAmigoGrupo();
      Assert.AreEqual(peticion.AgregarAmigosBD("usuario1", "usuario2"),1);

    }

    [Test]
    public void TestVisualizarPerfilAmigo()
    {
      PeticionAmigoGrupo peticion = new PeticionAmigoGrupo();
      Usuario u = peticion.VisualizarPerfilAmigoBD("usuario1");
      Assert.AreEqual("Aquiles",u.Nombre);
    }

    [Test]
    public void TestSalirGrupo()
    {

    }

    [TearDown]
    public void TearDown()
    {
      
      ConexionBase conexion = new ConexionBase();
      conexion.Conectar();

      conexion.Comando = conexion.SqlConexion.CreateCommand();
      conexion.Comando.CommandText = "Delete from usuario where us_id < 0;";
      conexion.Comando.CommandType = CommandType.Text;
      conexion.Comando.ExecuteReader();
      conexion.Desconectar();
    }

  }
}
