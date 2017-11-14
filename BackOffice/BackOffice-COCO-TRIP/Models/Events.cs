using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackOffice_COCO_TRIP.Models
{
    public class Events
    {


      private int _id;

    [JsonProperty(PropertyName = "IdEventos")]
    public int Id
    {
      get { return _id; }
      set { _id = value; }
    }

    private string _nameEvent;

    [JsonProperty(PropertyName = "nombreEvento")]
    [Display(Name = "Evento")]
    [Required(ErrorMessage = "Debe llenar este campo")]

    public string NameEvent
    {
      get { return _nameEvent; }
      set { _nameEvent = value; }
    }

    private string _descriptionEvent;

    [JsonProperty(PropertyName = "descripcionEvento")]
    [Display(Name = "Descripcion")]
    [Required(ErrorMessage = "Debe llenar este campo")]

    public string descriptionEvent
    {
      get { return _descriptionEvent; }
      set { _descriptionEvent = value; }
    }

    private int _priceEvent;
    [JsonProperty(PropertyName = "precioEvento")]
    [Display(Name = "Precio")]
    [Required(ErrorMessage = "Debe llenar este campo")]
    public int priceEvent
    {
      get { return _priceEvent; }
      set { _priceEvent = value; }
    }

    private TimestampAttribute _timeInitEvent;
    [JsonProperty(PropertyName = "fechaInicioEvento")]
    [Display(Name = "Comienza")]
    [Required(ErrorMessage = "Debe llenar este campo")]
    public TimestampAttribute timeInitEvent
    {
      get { return _timeInitEvent; }
      set { _timeInitEvent = value; }
    }

    private TimestampAttribute _timeEndEvent;
    [JsonProperty(PropertyName = "fechaInicioEvento")]
    [Display(Name = "Termina")]
    [Required(ErrorMessage = "Debe llenar este campo")]
    public TimestampAttribute timeEndEvent
    {
      get { return _timeEndEvent; }
      set { _timeEndEvent = value; }
    }

    private DateFormatHandling _dateInitEvent;
    [JsonProperty(PropertyName = "fechaInicioEvento")]
    [Display(Name = "Fecha de inicio")]
    [Required(ErrorMessage = "Debe llenar este campo")]
    public DateFormatHandling dateInitEvent
    {
      get { return _dateInitEvent; }
      set { _dateInitEvent = value; }
    }

    private DateFormatHandling _dateEndEvent;
    [JsonProperty(PropertyName = "fechaFinEvento")]
    [Display(Name = "Fecha de Cierre")]
    [Required(ErrorMessage = "Debe llenar este campo")]
    public DateFormatHandling dateEndEvent
    {
      get { return _dateEndEvent; }
      set { _dateEndEvent = value; }
    }

    private string _picEvent;
    [JsonProperty(PropertyName = "imagenEvento")]
    [Display(Name = "Imagen")]
    [Required(ErrorMessage = "Debe llenar este campo")]
    public string picEvent
    {
      get { return _picEvent; }
      set { _picEvent = value; }
    }

    private int _locationEvent;
    [JsonProperty(PropertyName = "LocalidadEvento")]
    [Required]
    public int locationEvent
    {
      get { return _locationEvent; }
      set { _locationEvent = value; }
    }

    private int _categoryEvent;
    [JsonProperty(PropertyName = "CategoriaEvento")]
    [Required]
    public int categoryEvent
    {
      get { return _categoryEvent; }
      set { _categoryEvent = value; }
    }

    }
}
