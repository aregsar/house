using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;



namespace house.Helpers
{
    public static class RouteHelper
    {
       
        public static void Get(this IRouteBuilder routes
                                , string url
                                , string routeName) => Map(routes, url, routeName, "GET");


        public static void Post(this IRouteBuilder routes
                                , string url
                                , string routeName) => Map(routes, url, routeName, "POST");


        public static void Put(this IRouteBuilder routes
                               , string url
                               , string routeName) => Map(routes, url, routeName, "PUT");


        public static void Delete(this IRouteBuilder routes
                                 , string url
                                 , string routeName) => Map(routes, url, routeName, "DELETE");

        public static void Patch(this IRouteBuilder routes
                               , string url
                               , string routeName) => Map(routes, url, routeName, "PATCH");


        public static void Get(this IRouteBuilder routes
                                , string url
                                , string endpoint
                                , string routeName) => MapWithRouteName(routes, url, routeName, endpoint, "GET");


        public static void Post(this IRouteBuilder routes
                                , string url
                                , string endpoint
                                , string routeName) => MapWithRouteName(routes, url, routeName, endpoint, "POST");


        public static void Put(this IRouteBuilder routes
                               , string url
                               , string endpoint
                               , string routeName) => MapWithRouteName(routes, url, routeName, endpoint, "PUT");


        public static void Delete(this IRouteBuilder routes
                                 , string url
                                 , string endpoint
                                 , string routeName) => MapWithRouteName(routes, url, routeName, endpoint, "DELETE");


        public static void Patch(this IRouteBuilder routes
                               , string url
                               , string endpoint
                               , string routeName) => MapWithRouteName(routes, url, routeName, endpoint, "Patch");


        public static void CatchAll(this IRouteBuilder routes
                                    , string controller
                                    , string action)
        {
            routes.MapRoute("Catch.All"
                            , "/{*path}"
                            , defaults: new { controller = controller, action = action });
        }


        private static void Map(IRouteBuilder routes
                                , string url
                                , string routeName
                                , string httpVerb)
        {
            string[] parts = routeName.Split('.');

            routes.MapRoute(
                name: routeName,
                template: url,
                defaults: new { controller = parts[0], action = parts[1] },
                constraints: new { HttpMethod = new HttpMethodRouteConstraint(httpVerb) }
            );
        }



        private static void MapWithRouteName(IRouteBuilder routes
                                        , string url
                                        , string routeName
                                        , string endpoint
                                        , string httpVerb)
        {
            string[] parts = endpoint.Split('.');

            routes.MapRoute(
                name: routeName,
                template: url,
                defaults: new { controller = parts[0], action = parts[1] },
                constraints: new { HttpMethod = new HttpMethodRouteConstraint(httpVerb) }
            );
        }

    }

}
