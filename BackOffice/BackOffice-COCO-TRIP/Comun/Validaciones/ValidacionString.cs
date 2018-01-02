using BackOffice_COCO_TRIP.Datos.Entidades;
using System;
using System.Text.RegularExpressions;

namespace BackOffice_COCO_TRIP.Comun
{
  public class ValidacionString
  {
    public ValidacionString()
    {

    }

    public bool ValidarLongitudInputNombreCategoria(String input)
    {
      if ((input.Length >= 5) && (input.Length <= 20))
      {
        return true;
      }
      return false;
    }

    public bool ValidarLongitudInputDescripcionCategoria(String input)
    {
      if ((input.Length >= 5) && (input.Length <= 100))
      {
        return true;
      }
      return false;
    }

    public bool ValidarCaracteresEspeciales(String input)
    {
      if (Regex.Match(input, @"^[a-zA-Z]+$").Success)
      {
        return true;
      }
      return false;
    }

    public bool ValidarCategoria(Entidad categoria)
    {
      if (ValidarLongitudInputDescripcionCategoria(((Categoria)categoria).Description) &&  //Validacion de campos de Nombre y Descripcion
      ValidarCaracteresEspeciales(((Categoria)categoria).Description) &&      //Deben tener al menos 5 caracteres, y solo se permiten letras y espacios
      ValidarLongitudInputNombreCategoria(((Categoria)categoria).Name) &&
      ValidarCaracteresEspeciales(((Categoria)categoria).Name))
      {
        return true;
      }
      return false;
    }

  }
}
