using System.Web;
using System.Web.Mvc;

namespace M8_Gestion_de_Actividades_y_Localidades
{
  public class FilterConfig
  {
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
      filters.Add(new HandleErrorAttribute());
    }
  }
}
