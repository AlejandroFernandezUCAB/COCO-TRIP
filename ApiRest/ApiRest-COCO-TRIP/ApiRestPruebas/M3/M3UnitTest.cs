using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiRest_COCO_TRIP.Models;

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

  }
}
