using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.Entity;
using System;
using System.Text.RegularExpressions;

namespace ApiRest_COCO_TRIP.Comun.Validaciones
{
    public class ValidacionString
    {
        public ValidacionString()
        {

        }

        public static bool ValidarLongitudInputNombreCategoria(String input)
        {
            if ((input.Length >= 5) && (input.Length <= 20))
            {
                return true;
            }
            return false;
        }

        public static bool ValidarLongitudInputDescripcionCategoria(String input)
        {
            if ((input.Length >= 5) && (input.Length <= 100))
            {
                return true;
            }
            return false;
        }

        public static bool ValidarCaracteresEspeciales(String input)
        {
            if (Regex.Match(input, @"^[a-zA-Z]+$").Success)
            {
                return true;
            }
            return false;
        }

        public static void ValidarCategoria(Entidad categoria)
        {
            if (!ValidarLongitudInputDescripcionCategoria(((Categoria)categoria).Descripcion) ||  //Validacion de campos de Nombre y Descripcion
            !ValidarCaracteresEspeciales(((Categoria)categoria).Descripcion) ||      //Deben tener al menos 5 caracteres, y solo se permiten letras y espacios
            !ValidarLongitudInputNombreCategoria(((Categoria)categoria).Nombre) ||
            !ValidarCaracteresEspeciales(((Categoria)categoria).Nombre))
            {
                if (!ValidarLongitudInputDescripcionCategoria(((Categoria)categoria).Descripcion) ||
                    !ValidarCaracteresEspeciales(((Categoria)categoria).Descripcion))
                     throw new ParametrosInvalidosExcepcion("Descripcion", 
                        "La descripcion ingresada debe tener al menos 5 caracteres y maximo 100. Solo se permiten letras y espacios");
                else 
                    if (!ValidarLongitudInputNombreCategoria(((Categoria)categoria).Nombre) ||
                        !ValidarCaracteresEspeciales(((Categoria)categoria).Nombre))
                        throw new ParametrosInvalidosExcepcion("Nombre",
                          "El nombre ingresado debe tener al menos 5 caracteres y maximo 20. Solo se permiten letras y espacios");
            }
        }
    }
}