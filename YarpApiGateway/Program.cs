using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment.EnvironmentName;

builder.Configuration.AddYamlFile("appsettings.yaml")
    .AddYamlFile($"appsettings.{environment}.yaml", optional: false);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

// app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.MapReverseProxy();
app.Run();