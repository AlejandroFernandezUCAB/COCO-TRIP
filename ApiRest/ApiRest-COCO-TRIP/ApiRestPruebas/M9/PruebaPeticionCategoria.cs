using ApiRest_COCO_TRIP;
using ApiRest_COCO_TRIP.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestPruebas.M9
{
  [TestFixture]
  class PruebaPeticionCategoria
  {
    Categoria pCategoria;
    PeticionCategoria pPeticion;
    ConexionBase conn;


    [SetUp]
    public void SetUp()
    {
      conn = new ConexionBase();
      conn.Conectar();
      conn.Comando = conn.SqlConexion.CreateCommand();
      conn.Comando.CommandText = "INSERT INTO categoria values (250, 'prueba', 'descripcion de prueba', true, null, 1)";
      conn.Comando.ExecuteNonQuery();
      conn.Desconectar();
      pPeticion = new PeticionCategoria();
      conn = null;
      pCategoria = new Categoria()
      {
        Id = 250,
        Estatus = false
      };

    }

    [TearDown]
    public void TearDown()
    {
      conn = new ConexionBase();
      conn.Conectar();
      conn.Comando = conn.SqlConexion.CreateCommand();
      conn.Comando.CommandText = "DELETE FROM categoria where ca_id = 250";
      conn.Comando.ExecuteNonQuery();
      conn.Desconectar();
    }

    [Test]
    public void ProbarActualizarEstatus()
    {
      Assert.DoesNotThrow(() => pPeticion.ActualizarEstatus(pCategoria));
    }
   

  }
}
