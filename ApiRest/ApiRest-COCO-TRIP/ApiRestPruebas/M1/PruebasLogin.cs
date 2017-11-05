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

    [SetUp]
    public void setUsuario() {
      usuario = new Usuario();
      usuario.Correo = "kilordpepo@gmail.com";
      usuario.Clave = "prueba";

    }
    [Test]
    [Category("Consultar")]
    public void TestConsultarUsuario() {
      PeticionLogin peticion = new PeticionLogin();
    Assert.AreEqual(1,peticion.ConsultarUsuarioCorreo(usuario));
    }
    }
}
