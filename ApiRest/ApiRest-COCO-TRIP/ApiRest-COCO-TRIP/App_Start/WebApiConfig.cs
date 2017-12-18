using System.Net.Http.Headers;
using System.Web.Http;

namespace ApiRest_COCO_TRIP
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
          // Configuración y servicios de API web
          // Rutas de API
          //var cors = new EnableCorsAttribute("*", "*", "*");
          config.MapHttpAttributeRoutes();
          config.EnableCors();
          config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{action}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
          config.Formatters.JsonFormatter.SupportedMediaTypes
        .Add(new MediaTypeHeaderValue("text/html"));

        }
    }
}
