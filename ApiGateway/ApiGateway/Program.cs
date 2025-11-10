using Microsoft.OpenApi.Models;
using Yarp.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);

// YARP
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateway", Version = "v1" });
});

var app = builder.Build();

app.UseRouting();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI(
    );

app.MapReverseProxy();

app.Run();
