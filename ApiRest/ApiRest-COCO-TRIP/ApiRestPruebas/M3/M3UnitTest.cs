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
/// <summary>
/// Clase de pruebas unitarias del MODULO 3
/// </summary>
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
        "INSERT INTO Usuario VALUES (-2 ,'usuariopruebas2', 'Mariangel','Perez',to_date('1963-09-01', 'YYYY-MM-DD') ,'F','usuariopruebas2@gmail.com','123456', null, true);" +
        "INSERT INTO Grupo VALUES (-1,'Grupoprueba1',null,-1);" +
        "INSERT INTO Grupo VALUES (-2,'Grupoprueba2',null,-2);" +
        "INSERT INTO Grupo VALUES (-3,'Grupoprueba3',null,-1);" +
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

    //--------------------------------------------------------------------------------------------//
    //--------------------------------------CASOS DE PRUEBA OSWALDO-------------------------------//
    //--------------------------------------------------------------------------------------------//

    //PRUEBAS UNITARIAS DE AGREGAR AMIGO
    //CREADO POR: OSWALDO LOPEZ
    /// <summary>
    /// Test para probar el caso de exito del metodo agregar amigo
    /// </summary>
    [Test]
    public void TestAgregarAmigo()
    {
      peticion = new PeticionAmigoGrupo();
      Assert.AreEqual(peticion.AgregarAmigosBD(-1, "usuariopruebas2"), 1);
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
      Assert.AreEqual("Aquiles", u.Nombre);
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
      Assert.AreEqual(3, peticion.SalirGrupoBD(-1, -1));
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
      Assert.AreEqual(2, peticion.SalirGrupoBD(1, -88));

    }

    /// <summary>
    /// Test para probar el caso de falla cuando se ingresa id de grupo
    /// que no estan registrados en la tabla miembro del metodo Salir grupo
    /// </summary>
    [Test]
    public void TestSalirGrupoFallidoNoExisteGrupo()
    {
      peticion = new PeticionAmigoGrupo();
      Assert.AreEqual(2, peticion.SalirGrupoBD(-10, -1));

    }


    //PRUEBAS UNITARIAS DE OBTENERLISTADENOTIFICACIONES
    //CREADO POR: OSWALDO LOPEZ
    /// <summary>
    /// Test para probar el coaso de exito de obtenerListaNotifiaciones
    /// </summary>
    [Test]
    public void TestObtenerListaNotificaciones()
    {
      peticion = new PeticionAmigoGrupo();
      peticion.AgregarAmigosBD(-2,"usuariopruebas1");
      Usuario usuario = new Usuario();
      List<Usuario> lista = peticion.ObtenerListaNotificacionesBD(-1);
      foreach (Usuario u in lista)
      {
        usuario = u;
      }
      Assert.AreEqual("Mariangel", usuario.Nombre);
    }

    /// <summary>
    /// Test para probar el caso de  falla cuando existe un casteo incorrecto
    /// </summary>
    ///
    [Test]
    public void TestObtenerListaNotificacionesFalloCast()
    {
      Assert.Catch<FormatException>(ExcepcionSalirGrupoMalCast);
    }

    public void ExcepcionObtenerListaNotificacionesFalloCast()
    {
      peticion = new PeticionAmigoGrupo();
      peticion.ObtenerListaNotificacionesBD(Convert.ToInt32("5ff"));
    }


    //PRUEBAS UNITARIAS DE ACEPNOTIFICACIONES
    //CREADO POR: OSWALDO LOPEZ
    /// <summary>
    /// Test para probar el caso de exito del metodo AceptarNotificacionesBD
    /// </summary>
    [Test]
    public void TestAceptarNotificaciones()
    {
      peticion = new PeticionAmigoGrupo();
      peticion.AgregarAmigosBD(-2, "usuariopruebas1");
      peticion.AceptarNotificacionBD("usuariopruebas2", -1);
      Usuario usuario = new Usuario();
      List<Usuario> lista = peticion.VisualizarListaAmigoBD(-1);
      foreach (Usuario u in lista)
      {
        usuario = u;
      }
      Assert.AreEqual("Mariangel", usuario.Nombre);
    }

    /// <summary>
    /// Test para probar el caso de  falla cuando existe un casteo incorrecto
    /// </summary>
    ///
    [Test]
    public void TestAceptarNotificacionesFalloCast()
    {
      Assert.Catch<InvalidCastException>(ExcepcionAceptarNotificacionesFalloCast);
    }

    public void ExcepcionAceptarNotificacionesFalloCast()
    {
      peticion = new PeticionAmigoGrupo();
      peticion.AceptarNotificacionBD(null, -1);
    }


    //PRUEBAS UNITARIAS DE RECHAZARNOTIFICACIONES 
    //CREADO POR: OSWALDO LOPEZ 
    /// <summary>
    /// Test para probar el caso de exito del metodo AceptarNotificacionesBD
    /// </summary>
    [Test]
    public void TestRechazarNotificaciones()
    {
      peticion = new PeticionAmigoGrupo();
      peticion.AgregarAmigosBD(-2, "usuariopruebas1");
      peticion.RechazarNotificacionBD("usuariopruebas2", -1);
      Usuario usuario = new Usuario();
      List<Usuario> lista = peticion.VisualizarListaAmigoBD(-1);
      foreach (Usuario u in lista)
      {
        usuario = u;
      }
      Assert.IsNull(usuario.Nombre);
    }

    /// <summary>
    /// Test para probar el caso de  falla cuando existe un casteo incorrecto
    /// </summary>
    ///
    [Test]
    public void TestRechazarNotificacionesFalloCast()
    {
      Assert.Catch<FormatException>(ExcepcionSalirGrupoMalCast);
    }

    public void ExcepcionRechazarNotificacionesFalloCast()
    {
      peticion = new PeticionAmigoGrupo();
      peticion.RechazarNotificacionBD(null, -1);
    }


    //PRUEBAS UNITARIAS DE CONSULTAR USUARIO
    //CREADO POR: OSWALDO LOPEZ
    /// <summary>
    /// Metodo que retorna el nombre del usuario
    /// </summary>
    [Test]
    public void TestObtenerUsuario()
    {
      peticion = new PeticionAmigoGrupo();
      Assert.AreEqual("Aquiles",peticion.ConsultarUsuario(-1));
    }
    /// <summary>
    /// Caso de fallo, cuando el usuario no existe
    /// </summary>
    [Test]
    public void TestObtenerUsuarioFalloNoExiste()
    {
      peticion = new PeticionAmigoGrupo();
      Assert.AreEqual("", peticion.ConsultarUsuario(-11));
    }

    /// <summary>
    /// Verifica que no se puede convertir un nombre de usuario en int
    /// </summary>
    [Test]
    public void TestObtenerUsuarioFalloCast()
    {
      Assert.Catch<FormatException>(ExcepcionTestObtenerUsuarioFalloCast);
    }

    public void ExcepcionTestObtenerUsuarioFalloCast()
    {
      peticion = new PeticionAmigoGrupo();
      peticion.ConsultarUsuario(Convert.ToInt32("asdsda"));
    }


    //PRUEBAS UNITARIAS DE EXISTENOTIFICACION
    //CREADO POR: OSWALDO LOPEZ
    /// <summary>
    /// Test para probar el caso de exito del metodo ExisteNotificacionBD
    /// </summary>
    [Test]
    public void TestExisteNotificacion()
    {
      peticion = new PeticionAmigoGrupo();
      peticion.AgregarAmigosBD(-2, "usuariopruebas1");
      Assert.AreNotEqual(-1, peticion.ExisteSolicitud(-1, "usuariopruebas2"));
    }

    /// <summary>
    /// Test para probar el caso cuando la solicitud/amistad no existe
    /// </summary>
    [Test]
    public void TestExisteNotificacionFalloNoExiste()
    {
      peticion = new PeticionAmigoGrupo();
      Assert.AreEqual(-1, peticion.ExisteSolicitud(-1, "usuariopruebas2"));
    }

    /// <summary>
    /// Test para probar el caso de  falla cuando existe un casteo incorrecto
    /// </summary>
    ///
    [Test]
    public void TestExisteNotificacionFalloCast()
    {
      Assert.Catch<InvalidCastException>(ExcepcionTestExisteNotificacionFalloCast);
    }

    public void ExcepcionTestExisteNotificacionFalloCast()
    {
      peticion = new PeticionAmigoGrupo();
      peticion.ExisteSolicitud(-1, null);
    }
    //--------------------------------------------------------------------------------------------//
    //-------------------------------------CASOS DE PRUEBA MARIANGEL------------------------------//
    //--------------------------------------------------------------------------------------------//


    //PRUEBAS UNITARIAS DE ELIMINAR AMIGO
    //CREADO POR: MARIANGEL PEREZ
    /// <summary>
    /// Prueba para eliminar un amigo
    /// </summary>
    [Test]
    public void EliminarAmigoTest() {
      peticion.AgregarAmigosBD(-1, "usuariopruebas2");
      Assert.AreEqual(1, peticion.EliminarAmigoBD("usuariopruebas2", -1));
    }
    /// <summary>
    /// Caso de prueba cuando se intenta eliminar un amigo que no existe
    /// </summary>
    [Test]
    public void EliminarAmigoNoExisteTest()
    {
      Assert.AreEqual(0, peticion.EliminarAmigoBD("usuariopruebas2", -1));
    }

    /// <summary>
    ///  Caso de prueba cuando se ingresa un dato null
    /// </summary>
    [Test]
    public void EliminarAmigoNull()
    {
      Assert.Catch<InvalidCastException> (AmigoNullCast);
    }

    public void AmigoNullCast()
    {
      peticion.EliminarAmigoBD(null, -1);
    }


    //PRUEBAS UNITARIAS DE ELIMINAR GRUPO
    //CREADO POR: MARIANGEL PEREZ
    /// <summary>
    /// Prueba para eliminar un grupo
    /// </summary>
    [Test]
    public void EliminarGrupoTest() {
      Assert.AreEqual(1, peticion.EliminarGrupoBD(-1, -1));
    }

    /// <summary>
    /// Caso de prueba cuando se intenta eliminar un grupo que no existe
    /// </summary>
    [Test]
    public void EliminarGrupoNoExisteTest()
    {
      Assert.AreEqual(0, peticion.EliminarGrupoBD(-5, -1));
    }

    /// <summary>
    /// Caso de prueba cuando se intenta eliminar un grupo y el usuario no existe
    /// </summary>
    [Test]
    public void EliminarGrupoNoExisteUsuarioTest()
    {
      Assert.AreEqual(0, peticion.EliminarGrupoBD(-2, -10));
    }


    //PRUEBAS UNITARIAS DE VISUALIZAR LISTA DE AMIGOS
    //CREADO POR: MARIANGEL PEREZ
    /// <summary>
    /// Prueba para visualizar lista de amigos
    /// </summary>
    [Test]
    public void VisualizarListaAmigos() {
      peticion.AgregarAmigosBD(-1, "usuariopruebas2");
      peticion.AceptarNotificacionBD("usuariopruebas1", -2);
      List<Usuario> lista = new List<Usuario>();
      lista = peticion.VisualizarListaAmigoBD(-1);
      Usuario usuario = new Usuario();
      foreach (Usuario u in lista) {
        usuario = u;
      }
      Assert.AreEqual("Mariangel", usuario.Nombre);
      Assert.AreEqual("Perez", usuario.Apellido);
      Assert.AreEqual("usuariopruebas2", usuario.NombreUsuario);
      Assert.AreEqual(null, usuario.Foto);
    }

    /// <summary>
    /// Prueba para visualizar que la lista de amigos no existe
    /// </summary>
    [Test]
    public void VisualizarListaAmigosNoExisteTest()
    {
     
      List<Usuario> lista = new List<Usuario>();
      lista = peticion.VisualizarListaAmigoBD(-1);
      Usuario usuario = new Usuario();
      foreach (Usuario u in lista)
      {
        usuario = u;
      }
      Assert.IsEmpty(lista);
    }

    //PRUEBAS UNITARIAS DE MODIFICAR GRUPO
    //CREADO POR: MARIANGEL PEREZ
    /// <summary>
    /// Prueba para modificar los atributos del grupo
    /// </summary>
    [Test]
    public void ModificarGrupoTest() {
      Assert.AreEqual(1, peticion.ModificarGrupoBD("GrupoTest", -1, -1));
    }

    /// <summary>
    /// Prueba de caso de fallo, cuando el usuario que intenta modificar no es el lider
    /// </summary>
    [Test]
    public void ModificarGrupoNoEsElLiderTest()
    {
      Assert.AreEqual(0, peticion.ModificarGrupoBD("GrupoTest", -3, -1));
    }

    /// <summary>
    /// Prueba de caso de fallo, cuando el grupo que se intenta
    /// modificar no existe
    /// </summary>
    [Test]
    public void ModificarGrupoNoEsElGrupoTest()
    {
      Assert.AreEqual(0, peticion.ModificarGrupoBD("GrupoTest", -1, -5));
    }

    /// <summary>
    /// Prueba de caso de fallo, cuando el nombre del grupo se modifica a null
    /// </summary>
    [Test]
    public void ModificarGrupoNombreNullTest()
    {
      Assert.Catch<InvalidCastException>(NombreNullCast);
    }

    public void NombreNullCast()
    {
      peticion.ModificarGrupoBD(null, -1, -1);
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

    /// <summary>
    /// Caso de fallo, cuando no existe el integrante a eliminar
    /// </summary>
    [Test]
    public void EliminarIntegranteModificarFallaTest()
    {
      Assert.AreEqual(0, peticion.EliminarIntegranteModificarBD("usuariopruebas1", -2));
    }
    /// <summary>
    /// Caso de fallo cuando el integrante a eliminar es null
    /// </summary>
    [Test]
    public void EliminarIntegranteModificarNullTest()
    {
      Assert.Catch<InvalidCastException>(EliminarIntegranteNullCast);
    }

    public void EliminarIntegranteNullCast()
    {
      peticion.EliminarIntegranteModificarBD(null, -2);
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

    /// <summary>
    /// Caso de fallo, cuando ingresas un usuario que no existe
    /// </summary>
    [Test]
    public void AgregarIntegranteModificarFallaTest()
    {
      Assert.Catch<Npgsql.PostgresException>(AgregarIntegranteFalla);
    }

    public void AgregarIntegranteFalla()
    {
      peticion.AgregarIntegranteModificarBD(-2, "usuariorandom1");
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
    /// Caso de fallo cuando el usuario no existe
    /// </summary>
    [Test]
    public void ObtenerIdUsuarioFallaTest()
    {
      Assert.AreEqual(0, peticion.ObtenerIdUsuario("usuarioRandom1"));
    }

    //PRUEBAS UNITARIAS DE OBTENER LIDER DEL GRUPO
    //CREADO POR: MARIANGEL PEREZ
    /// <summary>
    /// Prueba para obtener los datos del lider del grupo
    /// </summary>
    [Test]
    public void ObtenerLiderTest()
    {
      List<Usuario> lista = new List<Usuario>();
      lista = peticion.ObtenerLider(-2, -2);
      Usuario usuario = new Usuario();
      foreach (Usuario u in lista)
      {
        usuario = u;
      }
      Assert.AreEqual("Mariangel", usuario.Nombre);
      Assert.AreEqual("Perez", usuario.Apellido);
      Assert.AreEqual("usuariopruebas2", usuario.NombreUsuario);
      Assert.AreEqual(null, usuario.Foto);
    }

    /// <summary>
    /// caso de fallo, cuando el usuario que se ingresa no es el lider
    /// del grupo
    /// </summary>
    [Test]
    public void ObtenerLiderVacioTest()
    {
      List<Usuario> lista = new List<Usuario>();
      lista = peticion.ObtenerLider(-2, -1);
      Usuario usuario = new Usuario();
      foreach (Usuario u in lista)
      {
        usuario = u;
      }
      Assert.IsEmpty(lista);
    }

    /// <summary>
    /// Caso de fallo, cuando no existe el grupo del que se trata
    /// de obtener el lider
    /// </summary>
    [Test]
    public void ObtenerLiderNoEsGrupoTest()
    {
      List<Usuario> lista = new List<Usuario>();
      lista = peticion.ObtenerLider(-8, -2);
      Usuario usuario = new Usuario();
      foreach (Usuario u in lista)
      {
        usuario = u;
      }
      Assert.IsEmpty(lista);
    }
    /// <summary>
    /// Caso de fallo cuando no existe el lider, ni el grupo
    /// </summary>
    [Test]
    public void ObtenerLiderGrupoNoEsLiderNoEsGrupoTest()
    {
      List<Usuario> lista = new List<Usuario>();
      lista = peticion.ObtenerLider(-8, -7);
      Usuario usuario = new Usuario();
      foreach (Usuario u in lista)
      {
        usuario = u;
      }
      Assert.IsEmpty(lista);
    }

    //PRUEBAS UNITARIAS DE OBTENER LOS INTEGRANTES DE UN GRUPO
    //SIN EL LIDER
    //CREADO POR: MARIANGEL PEREZ
    /// <summary>
    /// Prueba para obtener los datos de los integrantes del grupo
    /// Sin el lider
    /// </summary>
    [Test]
    public void ObtenerSinLiderTest()
    {
      List<Usuario> lista = new List<Usuario>();
      lista = peticion.ObtenerSinLider(-1);
      Usuario usuario = new Usuario();
      foreach (Usuario u in lista)
      {
        usuario = u;
      }
      Assert.AreEqual("Mariangel", usuario.Nombre);
      Assert.AreEqual("Perez", usuario.Apellido);
      Assert.AreEqual("usuariopruebas2", usuario.NombreUsuario);
      Assert.AreEqual(null, usuario.Foto);
    }

    /// <summary>
    /// caso de fallo, cuando no existe el grupo
    /// </summary>
    [Test]
    public void ObtenerSinLiderFallaTest()
    {
      List<Usuario> lista = new List<Usuario>();
      lista = peticion.ObtenerSinLider(-4);
      Usuario usuario = new Usuario();
      foreach (Usuario u in lista)
      {
        usuario = u;
      }
      Assert.IsEmpty(lista);
    }

    //PRUEBAS UNITARIAS DE OBTENER LA LISTA DE AMIGOS QUE NO 
    //ESTAN AGREGADOS A UN GRUPO
    //CREADO POR: MARIANGEL PEREZ
    /// <summary>
    ///  Prueba para obtener los datos de los amigos que no estan
    ///  agregados al grupo
    /// </summary>
   [Test]
    public void ObtenerMiembroSinGrupoTest()
    {
      peticion.AgregarAmigosBD(-2, "usuariopruebas1");
      peticion.AceptarNotificacionBD("usuariopruebas2", -1);
      List<Usuario> lista = new List<Usuario>();
      lista = peticion.ObtenerMiembrosSinGrupo(-2,-2);
      Usuario usuario = new Usuario();
      foreach (Usuario u in lista)
      {
        usuario = u;
      }
      Assert.AreEqual("Aquiles", usuario.Nombre);
      Assert.AreEqual("pulido", usuario.Apellido);
      Assert.AreEqual("usuariopruebas1", usuario.NombreUsuario);
      Assert.AreEqual(null, usuario.Foto);
    }


    /// <summary>
    /// Caso de fallo, el usuario lider no existe
    /// </summary>
    [Test]
    public void ObtenerMiembroSinGrupoSinLiderTest()
    {
      peticion.AgregarAmigosBD(-2, "usuariopruebas1");
      peticion.AceptarNotificacionBD("usuariopruebas2", -1);
      List<Usuario> lista = new List<Usuario>();
      lista = peticion.ObtenerMiembrosSinGrupo(-4, -2);
      Usuario usuario = new Usuario();
      foreach (Usuario u in lista)
      {
        usuario = u;
      }
      Assert.IsEmpty(lista);
    }
    /// <summary>
    /// Caso de fallo, no existe lider, no existe el grupo
    /// </summary>
    [Test]
    public void MiembroSinGrupoSinLiderTest()
    {
      peticion.AgregarAmigosBD(-2, "usuariopruebas1");
      peticion.AceptarNotificacionBD("usuariopruebas2", -1);
      List<Usuario> lista = new List<Usuario>();
      lista = peticion.ObtenerMiembrosSinGrupo(-4, -4);
      Usuario usuario = new Usuario();
      foreach (Usuario u in lista)
      {
        usuario = u;
      }
      Assert.IsEmpty(lista);
    }


    //--------------------------------------------------------------------------------------------//
    //--------------------------------------CASOS DE PRUEBA AQUILES-------------------------------//
    //--------------------------------------------------------------------------------------------//



    //PRUEBAS UNITARIAS DE AGREGAR UN GRUPO
    //CREADO POR: AQUILES PULIDO
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


    //PRUEBAS UNITARIAS DE VER DETALLE DE GRUPO
    //CREADO POR: AQUILES PULIDO
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
    /// <summary>
    /// Caso de fallo, cuando el grupo no existe
    /// </summary>
    [Test]
    public void TestVisualizarPerfilGrupoNoExiste()
    {
      Assert.IsEmpty(peticion.ConsultarPerfilGrupo(0));
    }

    //PRUEBAS UNITARIAS DE LISTA DE GRUPOS
    //CREADO POR: AQUILES PULIDO
    /// <summary>
    /// Prueba de visualizar la lista de grupos
    /// </summary>
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

    /// <summary>
    /// Caso de fallo, cuando la lista es vacia
    /// </summary>
    [Test]
    public void TestListaGrupoVacio()
    {
      List<Grupo> lista = new List<Grupo>();
      lista = peticion.Listagrupo(0);
      Grupo grupo = new Grupo();
      foreach (Grupo g in lista)
      {
        grupo = g;
      }
      Assert.IsEmpty(grupo.Nombre);
    }

    //PRUEBAS UNITARIAS DE BUSCAR GRUPO
    //CREADO POR: AQUILES PULIDO
    /// <summary>
    /// Metodo de prueba para buscar un amigo
    /// </summary>
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
    /// <summary>
    /// Caso de fallo, cuando el amigo no existe
    /// </summary>
    [Test]
    public void BuscarAmigoNoExiste()
    {

      List<Usuario> lista = new List<Usuario>();
      lista = peticion.BuscarAmigo("Aquilessss", 1);
      Usuario usuario = new Usuario();
      foreach (Usuario u in lista)
      {
        usuario = u;
      }
      Assert.IsNull(usuario.Nombre);

    }

    //PRUEBAS UNITARIAS DE OBTENER EL ID DEL ULTIMO
    //GRUPO AGREGADO
    //CREADO POR: AQUILES PULIDO
    /// <summary>
    /// Metodo para obtener el ultimo grupo creado por un usuario
    /// </summary>
    [Test]
    public void UltimoGrupo()
    {
      Assert.AreEqual(-1, peticion.ObtenerultimoGrupo(-1));
    }
    /// <summary>
    /// Caso de fallo, cuando el usuario no existe
    /// </summary>
    [Test]
    public void UltimoGrupoFalla()
    {
      Assert.AreEqual(0,peticion.ObtenerultimoGrupo(-55));
    }
  }
}
