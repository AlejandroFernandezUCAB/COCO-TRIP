using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BackOffice_COCO_TRIP.Models.Peticion
{
  /// <summary>
  /// Realiza las peticiones HTTP al servicio web de COCO-TRIP
  /// </summary>
  public class PeticionLugares
  {
    private HttpClient cliente; //Almacena la direccion del servicio web y realiza la peticion
    private Task<HttpResponseMessage> mensajeAsincrono; //Almacena el resultado a medida que es recibido

    private const string Controlador = "M7_LugaresTuristicos";
    private string direccionBase;

    /// <summary>
    /// Getter y setters de DireccionBase
    /// </summary>
    public string DireccionBase { get => direccionBase; set => direccionBase = value; }

    /// <summary>
    /// Constructor que instancia el cliente que realizara pedidos al servicio web
    /// </summary>
    public PeticionLugares()
    {
      direccionBase = Negocio.Registro.ApiRestBaseUri;

      cliente = new HttpClient();
      cliente.BaseAddress = new Uri(DireccionBase); //Sujeto a cambios -> localhost:puerto que decidan en Slack
      cliente.DefaultRequestHeaders.Accept.Clear();
      cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    // GET

    /// <summary>
    /// Consulta la lista de lugares turisticos por rango de ID
    /// </summary>
    /// <param name="desde">Limite inferior</param>
    /// <param name="hasta">Limite superior</param>
    /// <returns>Lista de lugares turisticos en formato JSON</returns>
    public string GetLista(int desde, int hasta)
    {
      mensajeAsincrono = cliente.GetAsync($"{DireccionBase}/api/{Controlador}/GetLista?desde={desde}&hasta={hasta}");
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        var respuesta = mensaje.Content.ReadAsStringAsync();
        respuesta.Wait();

        return respuesta.Result;
      }
      else
      {
        return mensaje.StatusCode.ToString();
      }
    }

    /// <summary>
    /// Consulta los datos de un lugar turistico a excepcion del detalle de las actividades
    /// </summary>
    /// <param name="id">Id del lugar turistico</param>
    /// <returns>Lugar Turistico en formato JSON</returns>
    public string GetLugar(int id)
    {
      mensajeAsincrono = cliente.GetAsync($"{DireccionBase}/api/{Controlador}/GetLugar?id={id}");
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        var respuesta = mensaje.Content.ReadAsStringAsync();
        respuesta.Wait();

        return respuesta.Result;
      }
      else
      {
        return mensaje.StatusCode.ToString();
      }
    }

    /// <summary>
    /// Consulta todos los datos de un lugar turistico, incluyendo el detalle de las actividades
    /// </summary>
    /// <param name="id">Id del lugar turistico</param>
    /// <returns>Lugar Turistico en formato JSON</returns>
    public string GetLugarActividades(int id)
    {
      mensajeAsincrono = cliente.GetAsync($"{DireccionBase}/api/{Controlador}/GetLugarActividades?id={id}");
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        var respuesta = mensaje.Content.ReadAsStringAsync();
        respuesta.Wait();

        return respuesta.Result;
      }
      else
      {
        return mensaje.StatusCode.ToString();
      }
    }

    /// <summary>
    /// Consulta los datos de las actividades de un lugar turistico
    /// </summary>
    /// <param name="id">Id de lugar turistico</param>
    /// <returns>Lista de actividades en formato JSON</returns>
    public string GetActividades(int id)
    {
      mensajeAsincrono = cliente.GetAsync($"{DireccionBase}/api/{Controlador}/GetActividades?id={id}");
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        var respuesta = mensaje.Content.ReadAsStringAsync();
        respuesta.Wait();

        return respuesta.Result;
      }
      else
      {
        return mensaje.StatusCode.ToString();
      }
    }

    /// <summary>
    /// Consulta las categorias
    /// </summary>
    /// <returns>Lista de categorias en formato JSON</returns>
    public string GetCategoria()
    {
      mensajeAsincrono = cliente.GetAsync($"{DireccionBase}/api/{Controlador}/GetCategoria");
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        var respuesta = mensaje.Content.ReadAsStringAsync();
        respuesta.Wait();

        return respuesta.Result;
      }
      else
      {
        return mensaje.StatusCode.ToString();
      }
    }

    /// <summary>
    /// Consulta las subcategorias de una categoria
    /// </summary>
    /// <param name="id">Id de la categoria</param>
    /// <returns>Lista de subcategorias en formato JSON</returns>
    public string GetSubCategoria(int id)
    {
      mensajeAsincrono = cliente.GetAsync($"{DireccionBase}/api/{Controlador}/GetSubCategoria?id={id}");
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        var respuesta = mensaje.Content.ReadAsStringAsync();
        respuesta.Wait();

        return respuesta.Result;
      }
      else
      {
        return mensaje.StatusCode.ToString();
      }
    }

    // POST

    /// <summary>
    /// Solicita al servicio web la insercion del lugar turistico
    /// </summary>
    /// <param name="lugar">LugarTuristico</param>
    /// <returns>Id del lugar turistico insertado</returns>
    public int PostLugar(LugarTuristico lugar)
    {
      mensajeAsincrono = cliente.PostAsJsonAsync($"{DireccionBase}/api/{Controlador}/PostLugar", lugar);
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        var respuesta = mensaje.Content.ReadAsAsync<int>();
        respuesta.Wait();

        return respuesta.Result;
      }
      else
      {
        return (int)mensaje.StatusCode * (-1);
      }
    }

    /// <summary>
    /// Solicita al servicio web la insercion de la actividad
    /// </summary>
    /// <param name="actividad">Actividad</param>
    /// <returns>Id de la actividad insertada</returns>
    public int PostActividad(Actividad actividad, int id)
    {
      mensajeAsincrono = cliente.PostAsJsonAsync($"{DireccionBase}/api/{Controlador}/PostActividad?id={id}", actividad);
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        var respuesta = mensaje.Content.ReadAsAsync<int>();
        respuesta.Wait();

        return respuesta.Result;
      }
      else
      {
        return (int)mensaje.StatusCode * (-1);
      }
    }

    /// <summary>
    /// Solicita al servicio web la insercion de un horario asociado a un lugar turistico
    /// </summary>
    /// <param name="horario">Horario</param>
    /// <param name="id">Id lugar turistico</param>
    /// <returns>Id del horario insertado</returns>
    public int PostHorario(Horario horario, int id)
    {
      mensajeAsincrono = cliente.PostAsJsonAsync($"{DireccionBase}/api/{Controlador}/PostHorario?id={id}", horario);
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        var respuesta = mensaje.Content.ReadAsAsync<int>();
        respuesta.Wait();

        return respuesta.Result;
      }
      else
      {
        return (int)mensaje.StatusCode * (-1);
      }
    }

    /// <summary>
    /// Solicita al servicio web la insercion de una foto asociada a un lugar turistico
    /// </summary>
    /// <param name="foto">Foto</param>
    /// <param name="id">Id lugar turistico</param>
    /// <returns>Id de la foto insertada</returns>
    public int PostFoto(Foto foto, int id)
    {
      mensajeAsincrono = cliente.PostAsJsonAsync($"{DireccionBase}/api/{Controlador}/PostFoto?id={id}", foto);
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        var respuesta = mensaje.Content.ReadAsAsync<int>();
        respuesta.Wait();

        return respuesta.Result;
      }
      else
      {
        return (int)mensaje.StatusCode * (-1);
      }
    }

    /// <summary>
    /// Solicita al servicio la insercion de una categoria o subcategoria a un lugar turistico existente
    /// </summary>
    /// <param name="id">Id lugar turistico</param>
    /// <param name="idCategoria">Id de la categoria</param>
    /// <returns>Estado de la peticion</returns>
    public string PostCategoria(int id, int idCategoria)
    {
      mensajeAsincrono = cliente.PostAsync($"{DireccionBase}/api/{Controlador}/PostCategoria?id={id}&idCategoria={idCategoria}", null);
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        return "Exito";
      }
      else
      {
        return mensaje.StatusCode.ToString();
      }
    }

    // PUT

    /// <summary>
    /// Solicita al servicio la actualizacion de los datos de un lugar turistico
    /// </summary>
    /// <param name="lugar">LugarTuristico</param>
    /// <returns>Estado de la peticion</returns>
    public string PutLugar(LugarTuristico lugar)
    {
      mensajeAsincrono = cliente.PutAsJsonAsync($"{DireccionBase}/api/{Controlador}/PutLugar", lugar);
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        return "Exito";
      }
      else
      {
        return mensaje.StatusCode.ToString();
      }
    }

    /// <summary>
    /// Solicita al servicio la actualizacion del estado del lugar turistico
    /// </summary>
    /// <param name="id">Id lugar turistico</param>
    /// <param name="activar">Estado del lugar turistico</param>
    /// <returns>Estado de la peticion</returns>
    public string PutActivarLugar(int id, bool activar)
    {
      mensajeAsincrono = cliente.PutAsync($"{DireccionBase}/api/{Controlador}/PutActivarLugar?id={id}&activar={activar}", null);
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        return "Exito";
      }
      else
      {
        return mensaje.StatusCode.ToString();
      }
    }

    /// <summary>
    /// Solicita al servicio la actualizacion del estado de la actividad
    /// </summary>
    /// <param name="id">Id de la actividad</param>
    /// <param name="activar">Estado de la actividad</param>
    /// <returns>Estado de la peticion</returns>
    public string PutActivarActividad(int id, bool activar)
    {
      mensajeAsincrono = cliente.PutAsync($"{DireccionBase}/api/{Controlador}/PutActivarActividad?id={id}&activar={activar}", null);
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        return "Exito";
      }
      else
      {
        return mensaje.StatusCode.ToString();
      }
    }

    /// <summary>
    /// Solicita al servicio la eliminacion de la actividad
    /// </summary>
    /// <param name="id">Id de la actividad</param>
    /// <returns>Estado de la peticion</returns>
    public string DeleteActividad(int id)
    {
      mensajeAsincrono = cliente.DeleteAsync($"{DireccionBase}/api/{Controlador}/DeleteActividad?id={id}");
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        return "Exito";
      }
      else
      {
        return mensaje.StatusCode.ToString();
      }
    }

    /// <summary>
    /// Solicita al servicio la eliminacion de una foto
    /// </summary>
    /// <param name="id">Id de la foto</param>
    /// <returns>Estado de la peticion</returns>
    public string DeleteFoto(int id)
    {
      mensajeAsincrono = cliente.DeleteAsync($"{DireccionBase}/api/{Controlador}/DeleteFoto?id={id}");
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        return "Exito";
      }
      else
      {
        return mensaje.StatusCode.ToString();
      }
    }

    /// <summary>
    /// Solicita al servicio la eliminacion del horario
    /// </summary>
    /// <param name="id">Id del horario</param>
    /// <returns>Estado de la peticion</returns>
    public string DeleteHorario(int id)
    {
      mensajeAsincrono = cliente.DeleteAsync($"{DireccionBase}/api/{Controlador}/DeleteHorario?id={id}");
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        return "Exito";
      }
      else
      {
        return mensaje.StatusCode.ToString();
      }
    }

    /// <summary>
    /// Solicita al servicio la eliminacion de la categoria
    /// </summary>
    /// <param name="id">Id lugar turistico</param>
    /// <param name="idCategoria">Id cateogria</param>
    /// <returns>Estado de la peticion</returns>
    public string DeleteCategoria(int id, int idCategoria)
    {
      mensajeAsincrono = cliente.DeleteAsync($"{DireccionBase}/api/{Controlador}/DeleteCategoria?id={id}&idCategoria={idCategoria}");
      mensajeAsincrono.Wait();

      var mensaje = mensajeAsincrono.Result;

      if (mensaje.IsSuccessStatusCode)
      {
        return "Exito";
      }
      else
      {
        return mensaje.StatusCode.ToString();
      }
    }
  }
}
