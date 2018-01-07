using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Comun.Validaciones;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using NUnit.Framework;


namespace ApiRestPruebas.M9
{
    [TestFixture]
    class PruebaValidacionString
    {
        private string _nombreCategoria;
        private string _descripcionCategoria;
        private Entidad _categoria= FabricaEntidad.CrearEntidadCategoria();

        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void PruebaValidarCaracteresEspeciales()
        {
            _nombreCategoria = "abdcjdk-*";
            Assert.AreNotEqual(true, ValidacionString.ValidarCaracteresEspeciales(_nombreCategoria));
        }

        [Test]
        public void PruebaValidarNumeros()
        {
            _nombreCategoria = "ab23dcjdk";
            Assert.AreNotEqual(true, ValidacionString.ValidarCaracteresEspeciales(_nombreCategoria));
        }

        [Test]
        public void PruebaValidarCaracteresExito()
        {
            _nombreCategoria = "Pruebas unitarias, de calidad.";
            Assert.AreEqual(true, ValidacionString.ValidarCaracteresEspeciales(_nombreCategoria));
        }

        [Test]
        public void PruebaValidarEspacios()
        {
            _nombreCategoria = "abcd efg";
            Assert.AreEqual(true, ValidacionString.ValidarCaracteresEspeciales(_nombreCategoria));
        }

        [Test]
        public void PruebaValidarPunto()
        {
            _nombreCategoria = "abcd.";
            Assert.AreEqual(true, ValidacionString.ValidarCaracteresEspeciales(_nombreCategoria));
        }

        [Test]
        public void PruebaValidarComa()
        {
            _nombreCategoria = "abcd,";
            Assert.AreEqual(true, ValidacionString.ValidarCaracteresEspeciales(_nombreCategoria));
        }
        
        [Test]
        public void PruebaValidarLongitudNombreFallo()
        {
            _nombreCategoria = "lamejorcategoriadelmundoentero";
            Assert.AreNotEqual(true, ValidacionString.ValidarLongitudInputNombreCategoria(_nombreCategoria));
        }

        [Test]
        public void PruebaValidarLongitudNombreFallito()
        {
            _nombreCategoria = "lam";
            Assert.AreNotEqual(true, ValidacionString.ValidarLongitudInputNombreCategoria(_nombreCategoria));
        }

        [Test]
        public void PruebaValidarLongitudNombreExito()
        {
            _nombreCategoria = "lamejorcategoria";
            Assert.AreEqual(true, ValidacionString.ValidarLongitudInputNombreCategoria(_nombreCategoria));
        }

        [Test]
        public void PruebaValidarLongitudDescripcionFallo()
        {
            _descripcionCategoria = 
                "lamejorcategnijbvhvugcugoriadeljjddnfkjenrfklenrklergjnekrlgnjeklrjgkelrgjelwkrjgelkrgjeklrg" +
                "jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjmundoentero";
            Assert.AreNotEqual(true, ValidacionString.ValidarLongitudInputDescripcionCategoria(_descripcionCategoria));
        }

        [Test]
        public void PruebaValidarLongitudDescripcionFallito()
        {
            _descripcionCategoria = "lam";
            Assert.AreNotEqual(true, ValidacionString.ValidarLongitudInputDescripcionCategoria(_descripcionCategoria));
        }

        [Test]
        public void PruebaValidarLongitudDescripcionExito()
        {
            _descripcionCategoria = "lamejordescripcionantesinventada";
            Assert.AreEqual(true, ValidacionString.ValidarLongitudInputDescripcionCategoria(_descripcionCategoria));
        }

        [Test]
        public void PruebaValidarCategoriaNombreFalloExcepcion()
        {
            Assert.Catch<ParametrosInvalidosExcepcion>(PruebaExcepcionParametrosInvalidosNOMBRE);
        }

        [Test]
        public void PruebaValidarCategoriaDescripcionFalloExcepcion()
        {
            Assert.Catch<ParametrosInvalidosExcepcion>(PruebaExcepcionParametrosInvalidosDESCRIPCION);
        }

        
        
        
        /// /////////////////////////////////////// METODOS AUXILIARES //////////////////////////////////////////////
        private void PruebaExcepcionParametrosInvalidosNOMBRE()
        {
            ((Categoria)_categoria).Nombre = "La peor categoria!!";
            ((Categoria)_categoria).Descripcion = "Descripcion optima";
            ValidacionString.ValidarCategoria(_categoria);
        }

        private void PruebaExcepcionParametrosInvalidosDESCRIPCION()
        {
            ((Categoria)_categoria).Nombre = "La peor categoria.";
            ((Categoria)_categoria).Descripcion = "Des*";
            ValidacionString.ValidarCategoria(_categoria);
        }


    }
}
