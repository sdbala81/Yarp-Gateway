using Yarp.ReverseProxy.Configuration;

namespace YarpApiGateway;

public static class Routes
{
    public static RouteConfig[] GetRoutes()
    {
        return new[]
        {
            new RouteConfig
            {
                RouteId = "StudentService", // Forces a new route id each time GetRoutes is called.
                ClusterId = "StudentService",
                //AuthorizationPolicy = "Default",
                Match = new RouteMatch
                {
                    // Path or Hosts are required for each route. This catch-all pattern matches all request paths.
                    Path = "/api/students"
                }
            },
            new RouteConfig
            {
                RouteId = "CourseService", // Forces a new route id each time GetRoutes is called.
                ClusterId = "CourseService",
                //AuthorizationPolicy = "Default",
                Match = new RouteMatch
                {
                    // Path or Hosts are required for each route. This catch-all pattern matches all request paths.
                    Path = "/api/courses"
                }
            }
        };
    }
}