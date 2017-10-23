using System.Web;
using System.Web.Optimization;

namespace M8_Gestion_de_Actividades_y_Localidades
{
  public class BundleConfig
  {
    // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
    public static void RegisterBundles(BundleCollection bundles)
    {
      // JQUERY CUSTOM PLANTILLA
      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/jquery-{version}.js",
                  "~/Scripts/custom.js"));
      
      
     

      bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                  "~/Scripts/jquery.validate*"));

      // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
      // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
      bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                  "~/Scripts/modernizr-*"));

      bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

      // Style Plantilla
      bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/custom.css",
                "~/Content/font-awesome.css",
                "~/Content/global.css",
                "~/Content/sidebar.css"));
    }
  }
}
