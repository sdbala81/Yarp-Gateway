using Yarp.ReverseProxy.Transforms;
using YarpApiGateway;
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
    .LoadFromMemory(Routes.GetRoutes(), Clusters.GetClusters())
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