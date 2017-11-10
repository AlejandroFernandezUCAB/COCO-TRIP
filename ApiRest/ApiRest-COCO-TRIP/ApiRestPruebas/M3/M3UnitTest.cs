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

    Usuario usuario1, usuario2, usuario3;
    PeticionAmigoGrupo peticion;

    [SetUp]
    public void SetUp()
    {
      /*usuario1 = new Usuario
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

      usuario3 = new Usuario
      {
        Nombre = "Mariangel",
        Apellido = "Perez",
        Genero = "F",
        NombreUsuario = "ophy",
        FechaNacimiento = new DateTime(2017, 03, 09),
        Correo = "ophy@gmail.com",
        Clave = "pruebaclave",
        Foto = new byte[28480]
      };*/

      peticion = new PeticionAmigoGrupo();
    }


    [Test]
    public void TestAgregarAmigo()
    {
      PeticionAmigoGrupo peticion = new PeticionAmigoGrupo();
      Assert.AreEqual(peticion.AgregarAmigosBD("usuario1", "usuario2"), 13);

    }

    [Test]
    public void TestVisualizarPerfilAmigo()
    {

    }

    [Test]
    public void TestSalirGrupo()
    {

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
      peticion.AgregarGrupoBD("Grupo", 1);
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
      peticion.AgregarGrupoBD("Grupo", 1);
      Assert.AreEqual(1, peticion.ModificarGrupoBD("GrupoTest","usuario1",1));
    }

    /// <summary>
    /// Prueba para eliminar los integrantes de un grupo al modificar
    /// </summary>
    [Test]
    public void EliminarIntegranteModificarTest() {
      peticion.AgregarGrupoBD("Grupo", 1);
      peticion.AgregarIntegranteModificarBD(1, "usuario2");
      Assert.AreEqual(1, peticion.EliminarIntegranteModificarBD("usuario2",1));

    }

    /// <summary>
    /// Prueba para agregar un integrante a un grupo al modificar
    /// </summary>
    [Test]
    public void AgregarIntegranteModificarTest()
    {
      peticion.AgregarGrupoBD("Grupo", 1);
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

  }
}
