using BackOffice_COCO_TRIP.Datos.Entidades;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace BackOffice_COCO_TRIP.Comun.Helpers
{
  public static class HtmlHelpers
  {

    public static MvcHtmlString DropDownList(this HtmlHelper helper, string name, IList<Categoria> list, object htmlAttributes)
    {
      TagBuilder dropdown = new TagBuilder("select");
      dropdown.Attributes.Add("name", name);
      dropdown.Attributes.Add("id", name);
      StringBuilder options = new StringBuilder();
      options = options.Append("<option value='0-0'> Ninguno - Categoria Principal </option>");
      foreach (var item in list)
      {
        options = options.Append($"<option value='{item.Id}-{item.Nivel}'> {item.Name} </option>");
      }
      dropdown.InnerHtml = options.ToString();
      dropdown.MergeAttributes(new RouteValueDictionary(htmlAttributes));
      return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
    }

    public static MvcHtmlString DropDownListCategoriesEvents(this HtmlHelper helper, string name, Evento evento, IList<Categoria> list, object htmlAttributes)
    {
      TagBuilder dropdown = new TagBuilder("select");
      dropdown.Attributes.Add("name", name);
      dropdown.Attributes.Add("id", name);
      StringBuilder options = new StringBuilder();
      foreach (var item in list)
      {
        if (evento != null && evento.IdCategoria == item.Id)
        {
          options = options.Append($"<option value='{item.Id}' selected> {item.Name} </option>");
        }
        else
        {
          options = options.Append($"<option value='{item.Id}'> {item.Name} </option>");
        }
      }
      dropdown.InnerHtml = options.ToString();
      dropdown.MergeAttributes(new RouteValueDictionary(htmlAttributes));
      return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
    }

    public static MvcHtmlString DropDownListLocalidadesEventos(this HtmlHelper helper, string name, Evento evento, IList<Localidad> list, object htmlAttributes)
    {
      TagBuilder dropdown = new TagBuilder("select");

      dropdown.Attributes.Add("name", name);
      dropdown.Attributes.Add("id", name);
      StringBuilder options = new StringBuilder();
      foreach (var item in list)
      {
        if (evento != null && evento.IdLocalidad == item.Id)
        {
          options = options.Append($"<option value='{item.Id}' selected> {item.Nombre} </option>");
        }

        else

          options = options.Append($"<option value='{item.Id}'> {item.Nombre} </option>");
      }
       
      
      dropdown.InnerHtml = options.ToString();
      dropdown.MergeAttributes(new RouteValueDictionary(htmlAttributes));
      return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
    }
  }
}
