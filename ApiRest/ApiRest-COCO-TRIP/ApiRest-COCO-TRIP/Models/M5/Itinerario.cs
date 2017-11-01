using System;
using System.Collections.Generic;


namespace Service1.Models
{
   public class Itinerario
   {
     int it_id;
     int it_idUsuario;
     string it_nombre;
     DateTime it_fechaInicio;
     DateTime it_fechaFin;

    public int It_id { get => it_id; set => it_id = value; }
    public string It_nombre { get => it_nombre; set => it_nombre = value; }
    public DateTime It_fechaInicio { get => it_fechaInicio; set => it_fechaInicio = value; }
    public DateTime It_fechaFin { get => it_fechaFin; set => it_fechaFin = value; }
    public int It_idUsuario { get => it_idUsuario; set => it_idUsuario = value; }
  }

}
