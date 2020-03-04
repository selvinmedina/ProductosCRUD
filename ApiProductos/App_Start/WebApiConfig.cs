using System.Web.Http;

namespace ApiProductos
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "Saber si existe",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { code = RouteParameter.Optional },
               constraints: new { id = @"\d+" }
               );
        }
    }
}
