using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;
using YarpApiGateway.Error;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment.EnvironmentName;

var configuration = builder.Configuration;

configuration.AddYamlFile("appsettings.yaml")
    .AddYamlFile($"appsettings.{environment}.yaml", false);

// Add services to the container.

// Add services to the container.
var services = builder.Services;

// services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAd"));

//services.AddMicrosoftIdentityWebApiAuthentication(configuration);

// services.AddAuthorization();

// builder.Services.AddControllers();

services.AddReverseProxy()
    //.LoadFromConfig(configuration.GetSection("ReverseProxy"));
    .LoadFromMemory(GetRoutes(), GetClusters())
    .AddTransforms(transformBuilderContext => { transformBuilderContext.AddPathRemovePrefix("/api"); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

// app.UseAuthentication();
// app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();

// app.UseRouting();
// app.MapControllers();
app.MapReverseProxy();
app.Run();

RouteConfig[] GetRoutes()
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
        }
    };
}

ClusterConfig[] GetClusters()
{
    return new[]
    {
        new ClusterConfig
        {
            ClusterId = "StudentService",
            Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "StudentService", new DestinationConfig
                        { Address = "http://localhost:8000/" }
                }
            }
        }
    };
}