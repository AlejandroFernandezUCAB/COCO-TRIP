using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  public class ComandoModificarLugarTuristico : ComandoAgregarLugarTuristico
  {
    IDAOLugar_Turistico daoLugarTuristico = FabricaDAO.GetDAOLugar_Turistico();

    public override void Execute()
    {
      //Agregando la foto al objeto
      ((Foto)foto).Ruta = "No implementado";
      ((Foto)foto).Contenido = ExtraerFoto(datosDeLaPresentacion[0]);
      ((LugarTuristico)lugarTuristico).Foto.Add((Foto)foto);

      //Agregando el nombre al objeto
      ((LugarTuristico)lugarTuristico).Nombre = datosDeLaPresentacion[1].ToString();

      //Agregando el costo al objeto
      ((LugarTuristico)lugarTuristico).Costo = Double.Parse(datosDeLaPresentacion[2].ToString());

      //Agregando el status
      ((LugarTuristico)lugarTuristico).Activar = ExtraerStatus(datosDeLaPresentacion[3]);

      //Agregando Direccion
      ((LugarTuristico)lugarTuristico).Direccion = datosDeLaPresentacion[49].ToString();

      //Agregando Correo
      ((LugarTuristico)lugarTuristico).Correo = datosDeLaPresentacion[50].ToString();

      //Agregando telefono
      ((LugarTuristico)lugarTuristico).Telefono = long.Parse(datosDeLaPresentacion[51].ToString());

      //Agregando descripcion
      ((LugarTuristico)lugarTuristico).Descripcion = datosDeLaPresentacion[52].ToString();

      //Agregando Latitud
      ((LugarTuristico)lugarTuristico).Latitud = double.Parse(datosDeLaPresentacion[53].ToString());

      //Agregando Longitud
      ((LugarTuristico)lugarTuristico).Longitud = double.Parse(datosDeLaPresentacion[54].ToString());

      //Llamando al DAO
      respuesta = daoLugarTuristico.PutLugarActualizar(lugarTuristico);
    }
  }
}
