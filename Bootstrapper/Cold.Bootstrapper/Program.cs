using Cold.Catalog.Api;
using Cold.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCatalogModule()
    .AddSharedFramework(builder.Configuration);

var app = builder.Build();

app.UseCatalogModule();
app.UseSharedFramework();

app.MapControllers();
app.MapGet("/ping", ctx => ctx.Response.WriteAsync("pong"));

app.Run();