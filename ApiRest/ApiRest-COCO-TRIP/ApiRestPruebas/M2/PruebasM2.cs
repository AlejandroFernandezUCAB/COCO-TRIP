using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestPruebas.M2
{
    [TestFixture]
    class PruebasM2
    {

        private Entidad usuario;
        private DAO dao;
        private Comando comando;
        private Entidad respuesta;

        [SetUp]
        public void SetUp()
        {
            dao = FabricaDAO.CrearDAOUsuario();
            dao.Conectar();
            dao.Comando = dao.SqlConexion.CreateCommand();
            dao.Comando.CommandText = "INSERT INTO usuario values (500, 'usuario1T', 'usuario1', 'prueba1', '10-10-2000', 'M', 'prueba@test.com', '123456', null, true)";
            dao.Comando.ExecuteNonQuery();
            dao.Desconectar();
            
        }

        [TearDown]
        public void TearDown()
        {
            dao.Conectar();
            dao.Comando = dao.SqlConexion.CreateCommand();
            dao.Comando.CommandText = "DELETE FROM usuario where us_id = 500";
            dao.Comando.ExecuteNonQuery();
            dao.Desconectar();
            usuario = null;
            dao = null;
            comando = null;
            respuesta = null;
        }

        [Test]
        public void ProbarDaoUsuarioConsultarPorId()
        {
            usuario = FabricaEntidad.CrearEntidadUsuario(500);
            respuesta = dao.ConsultarPorId(usuario);
            Assert.AreEqual(usuario.Id, respuesta.Id);

        }

        [Test]
        public void ProbarDaoUsuarioConsultarPorNombre()
        {
            usuario = FabricaEntidad.CrearEntidadUsuario("usuario1T", "123456"); //inicia usuario con su nombreUsuario y clave
            respuesta = ((DAOUsuario)dao).ConsultarPorNombre(usuario);
            Assert.AreEqual(((Usuario)usuario).Nombre, ((Usuario)respuesta).Nombre);

        }

        [Test]
        public void ProbarDaoUsuarioEliminar()
        {
            usuario = FabricaEntidad.CrearEntidadUsuario("usuario1T", "123456"); //inicia usuario con su nombreUsuario y clave
            dao.Eliminar(usuario);
            Assert.DoesNotThrow(() => dao.Eliminar(usuario));
            

        }

        [Test]
        public void ProbarDaoUsuarioActualizar()
        {
            usuario = FabricaEntidad.CrearEntidadUsuario("usuario1C", "prueba1C", "usuario1T", "12-12-2012", "M");
            dao.Actualizar(usuario);
            respuesta = ((DAOUsuario)dao).ConsultarPorNombre(usuario);
            Assert.AreEqual(((Usuario)usuario).Nombre, ((Usuario)respuesta).Nombre);


        }

        [Test]
        public void ProbarDaoUsuarioObtenerPassword()
        {
            usuario = FabricaEntidad.CrearEntidadUsuario("usuario1T", "123456");
            respuesta = ((DAOUsuario)dao).ObtenerPassword(usuario);
            Assert.AreEqual(((Usuario)usuario).Clave, ((Usuario)respuesta).Clave);


        }

        [Test]
        public void ProbarDaoUsuarioCambiarPassword()
        {
            usuario = FabricaEntidad.CrearEntidadUsuario("usuario1T", "1234567");
            usuario.Id = 500;
            Assert.DoesNotThrow(() => ((DAOUsuario)dao).CambiarPassword(usuario));
            respuesta = ((DAOUsuario)dao).ObtenerPassword(usuario);
            Assert.AreEqual(((Usuario)usuario).Clave, ((Usuario)respuesta).Clave);


        }




       


        [Test]
        public void ProbarComandoBorrarUsuario()
        {
            usuario = FabricaEntidad.CrearEntidadUsuario("usuario1T", "123456");
            comando = FabricaComando.CrearComandoBorrarUsuario(usuario);
            comando.Ejecutar();
            respuesta = comando.Retornar();
            Assert.IsNotNull(respuesta);
        }

        [Test]
        public void ProbarComandoBorrarUsuarioPassWordIncorrecta()
        {
            usuario = FabricaEntidad.CrearEntidadUsuario("usuario1T", "1234567");
            comando = FabricaComando.CrearComandoBorrarUsuario(usuario);
            comando.Ejecutar();
            respuesta = comando.Retornar();
            Assert.IsNull(respuesta);
        }

        [Test]
        public void ProbarComandoModificarDatosUsuario()
        {
            usuario = FabricaEntidad.CrearEntidadUsuario("usuario1C", "prueba1C", "usuario1T", "12-12-2012", "M");
            comando = FabricaComando.CrearComandoModificarDatosUsuario(usuario);
            comando.Ejecutar();
            respuesta = comando.Retornar();
            Assert.IsNotNull(respuesta);
            
        }

        [Test]
        public void ProbarComandoModificarDatosUsuarioNotFound()
        {
            usuario = FabricaEntidad.CrearEntidadUsuario("usuario1C", "prueba1C", "usuario2T", "12-12-2012", "M");
            comando = FabricaComando.CrearComandoModificarDatosUsuario(usuario);
            comando.Ejecutar();
            respuesta = comando.Retornar();
            Assert.IsNull(respuesta);

        }

        [Test]
        public void ProbarComandoCambiarPassword()
        {
            usuario = FabricaEntidad.CrearEntidadUsuario("usuario1T", "123456"); //usuario con la contrasena actual
            comando = FabricaComando.CrearComandoCambiarPassword(usuario, "1234567"); //paso el usuario y la nueva contrasena
            comando.Ejecutar();
            respuesta = comando.Retornar();
            Assert.AreEqual(((Usuario)usuario).Clave, ((Usuario)respuesta).Clave);

        }

        [Test]
        public void ProbarComandoObtenerDatosUsuario()
        {
            usuario = FabricaEntidad.CrearEntidadUsuario(500);
            comando = FabricaComando.CrearComandoObtenerDatosUsuario(usuario);
            comando.Ejecutar();
            respuesta = comando.Retornar();
            Assert.AreEqual(usuario.Id, respuesta.Id);
        
        }

        [Test]
        public void ProbarComandoObtenerDatosUsuarioNotFound()
        {
            usuario = FabricaEntidad.CrearEntidadUsuario(5001);//id no existe
            comando = FabricaComando.CrearComandoObtenerDatosUsuario(usuario);
            comando.Ejecutar();
            respuesta = comando.Retornar();
            Assert.IsNull(respuesta);

        }



    }
}
