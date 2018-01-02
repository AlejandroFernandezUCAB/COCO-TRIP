using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Datos.Singleton
{   
/// <summary>
/// Clase para aplicar el patron Registry y encapsular los mensajes de resultado de una operacion del servicio web. 
/// </summary>
    public class MensajeResultadoOperacion
    {
        private static MensajeResultadoOperacion instancia;


        //Registros para los mensajes de confirmacion de las operaciones de los servicios.
        private const string exitoInsertarCategoria = "Se agrego la categoria de forma exitosa.";
        public string ExitoInsertarCategoria => exitoInsertarCategoria;

        private const string exitoInsertar = "Se agrego exitosamente.";
        public string ExitoInsertar => exitoInsertar;

        private const string exitoModificar = "Se modifico exitosamente.";
        public string ExitoModificar => exitoModificar;

        private const string errorCategoriaDuplicada = "Este nombre de categoria ya existe";
        public string ErrorCategoriaDuplicada => errorCategoriaDuplicada;

        private const string errorInesperado = "Ocurrio un error inesperado";
        public  string ErrorInesperado => errorInesperado;

        private const string errorCategoriaAsociada = "No se puede mover porque tiene categorias asociadas";
        public string ErrorCategoriaAsociada => errorCategoriaAsociada;

        private const string errorFormatoCampoCategoria = 
            "Error de formato de Nombre y/o Descripcion de categoria. Solo se permiten letras y espacios. Al menos 5 caracteres.";
        public string ErrorFormatoCampoCategoria => errorFormatoCampoCategoria;

        private const string errorParametrosNull = "Todos los campos son obligatorios";
        public string ErrorParametrosNull => errorParametrosNull;

        private const string errorInternoServidor = "Error interno del servidor. Intente más tarde.";
        public string ErrorInternoServidor => errorInternoServidor;

        private MensajeResultadoOperacion()
        {

        }


        /// <summary>
        /// Retorna la instancia del Registry para acceder a los mensajes
        /// </summary>
        /// <returns>Correo</returns>
        public static MensajeResultadoOperacion ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new MensajeResultadoOperacion();
            }
            return instancia;
        }

    }
}