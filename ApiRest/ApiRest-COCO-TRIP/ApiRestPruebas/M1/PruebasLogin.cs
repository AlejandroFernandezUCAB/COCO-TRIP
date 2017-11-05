using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ApiRest_COCO_TRIP.Models;

namespace ApiRestPruebas
{
  [TestFixture]
  public class PruebasLogin
  {
    private Usuario usuario;
    private PeticionLogin peticion = new PeticionLogin();

    [SetUp]
    public void setUsuario()
    {
      usuario = new Usuario
      {
        Nombre = "Carlos",
        Apellido = "Valero",
        Genero = "M",
        NombreUsuario = "pepo",
        FechaNacimiento = new DateTime(2017, 03, 09),
        Correo = "pepo@gmail.com",
        Clave = "prueba",
        Foto = new byte[28480]
      };
    }

    [Test]
    [Category("Insertar")]
    public void TestInsertarUsuarioFacebook()
    {
      Assert.AreEqual(1, peticion.InsertarUsuarioFacebook(usuario));
    }
    [Test]
    [Category("Consultar")]
    public void TestConsultarUsuarioFacebook()
    {
      Assert.AreEqual(1, peticion.ConsultarUsuarioSocial(usuario));
    }

    [Test]
    [Category("Actualizar")]
    public void TestActualizarValidacionUsuario()
    {
      Assert.DoesNotThrow(() => {
        usuario.Id = peticion.ConsultarUsuarioSocial(usuario);
        peticion.ValidarUsuario(usuario);

      });
    }

    [Test]
    [Category("Consultar")]
    public void TestConsultarUsuarioCorreo()
    {
      usuario.Clave = "";
      Assert.AreEqual(1, peticion.ConsultarUsuarioCorreo(usuario));
    }

    [Test]
    [Category("Consultar")]
    public void TestConsultarUsuarioNombre()
    {
      usuario.Clave = "";
      Assert.AreEqual(1, peticion.ConsultarUsuarioCorreo(usuario));
    }



  }

}
