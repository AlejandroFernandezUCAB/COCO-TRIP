using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  public class ComandoAgregarLugarTuristico : Comando
  {

    private Entidad lugarTuristico = FabricaEntidad.GetLugarTuristico();
    private Entidad foto = FabricaEntidad.GetFoto();
    private Entidad horario = FabricaEntidad.GetHorario();
    private Entidad actividad = FabricaEntidad.GetActividad();
    IDAOLugar_Turistico daoLugarTuristico = FabricaDAO.GetDAOLugar_Turistico();
    private ArrayList resultado = new ArrayList();
    private ArrayList datosDeLaPresentacion = new ArrayList();
    JObject respuesta;
    
    /// <summary>
    /// Ejecucion del comando
    /// </summary>
    public override void Execute()
    {

      try
      {
        //Agregando la foto al objeto
        ((Foto)foto).Contenido = ExtraerFoto(datosDeLaPresentacion[0]);
        ((LugarTuristico)lugarTuristico).Foto.Add((Foto)foto);

        //Agregando el nombre al objeto
        ((LugarTuristico)lugarTuristico).Nombre = datosDeLaPresentacion[1].ToString();

        //Agregando el costo al objeto
        ((LugarTuristico)lugarTuristico).Costo = Double.Parse(datosDeLaPresentacion[2].ToString());

        //Agregando el status
        ((LugarTuristico)lugarTuristico).Activar = ExtraerStatus(datosDeLaPresentacion[3]);

        //Agregando las categorias
        ((LugarTuristico)lugarTuristico).Categoria = ExtraerCategorias();//i hasta 7

        //Agregando los horarios
        ((LugarTuristico)lugarTuristico).Horario = ExtraerHorario();//i Hasta 28

        //Agregando las actividades
        ((LugarTuristico)lugarTuristico).Actividad = ExtraerActividad(); // i hasta 48

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
        ((LugarTuristico)lugarTuristico).Longitud = double.Parse( datosDeLaPresentacion[54].ToString());

        //Llamando al DAO
        respuesta = daoLugarTuristico.Post( lugarTuristico );
        lugarTuristico = respuesta.ToObject<LugarTuristico>();
        respuesta.Add(lugarTuristico);
        
      }
      catch (Exception e)
      {
        
      }

    }

    private List<Actividad> ExtraerActividad()
    {
      List<Actividad> actividades = new List<Actividad>();
      Actividad actividad;
      String activar;
      int i = 29;

      while(i<=48)
      {
        actividad = FabricaEntidad.GetActividad();

        if ( datosDeLaPresentacion[i] != null)
        {
          i++;
          actividad.Foto.Contenido = ExtraerFoto(datosDeLaPresentacion[i]);
          i++;
          actividad.Nombre = datosDeLaPresentacion[i].ToString();
          i++;
          actividad.Descripcion = datosDeLaPresentacion[i].ToString();
          i++;
          actividad.Duracion = ExtraerTimeSpan(datosDeLaPresentacion[i].ToString());
          i++;
          actividades.Add(actividad);
        }
        else
        {
          i = i + 5;
        }

      }
      return actividades;
    }

    /// <summary>
    /// Metodo que extrae de datosDeLaPresentacion los horarios
    /// </summary>
    /// <returns>Lista de horarios</returns>
    private List<Horario> ExtraerHorario()
    {
      List<Horario> horarios = new List<Horario>();
      Horario horario;
      int i = 8;

      while(  i<=28 )
      {

        horario = FabricaEntidad.GetHorario();
        horario.DiaSemana = ExtraerDiaSemana(datosDeLaPresentacion[i].ToString());
        i++;
        horario.HoraApertura = ExtraerTimeSpan(datosDeLaPresentacion[i].ToString());
        i++;
        horario.HoraCierre = ExtraerTimeSpan(datosDeLaPresentacion[i].ToString());
        horarios.Add(horario);
        i++;

      }

      return horarios;

    }

    /// <summary>
    /// Metodo para extraer las categorias de datosDeLaPresentacion
    /// </summary>
    /// <returns></returns>
    private List<Categoria> ExtraerCategorias()
    {
      List<Categoria> categorias = new List<Categoria>();
      Categoria categoria;

      for (int i = 4; i<=7; i++)
      {
        categoria = FabricaEntidad.GetCategoria();
        categoria.Name = datosDeLaPresentacion[i].ToString();
        categorias.Add(categoria);
      }

      return categorias;

    }

    private bool ExtraerStatus(object status)
    {
      if( status.ToString() == "Activo")
      {
        return true;
      }else{
        return false;
      }
    }

    /// <summary>
    /// Para la respuesta se necesita algo para agarrar el resultado
    /// </summary>
    /// <returns></returns>
    public override ArrayList GetResult()
    {
      return resultado;
    }

    /// <summary>
    /// Metodo que asigna al lugar turistico los necesario para ejecutarse
    /// </summary>
    /// <param name="propiedad">Objeto lugar</param>
    public override void SetPropiedad(object propiedad)
    {
      datosDeLaPresentacion.Add(propiedad);
    }

    public TimeSpan ExtraerTimeSpan(String hora)
    {
      return TimeSpan.Parse(hora);
    }

    public int ExtraerDiaSemana(String Dia)
    {
      if (Dia.Equals("Domingo"))
      {
        return 0;
      }
      else if (Dia.Equals("Lunes"))
      {
        return 1;
      }
      else if (Dia.Equals("Martes"))
      {
        return 2;
      }
      else if (Dia.Equals("Miercoles"))
      {
        return 3;
      }
      else if (Dia.Equals("Jueves"))
      {
        return 4;
      }
      else if (Dia.Equals("Viernes"))
      {
        return 5;
      }
      else if (Dia.Equals("Sabado"))
      {
        return 6;
      }

      return 0;
    }

    private byte[] ExtraerFoto(object fotoEnObjeto)
    {
      return Encoding.ASCII.GetBytes(fotoEnObjeto.ToString());
    }
  }
}
