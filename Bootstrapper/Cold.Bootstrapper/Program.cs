using Cold.Catalog.Api;
using Cold.Contracts.Api;
using Cold.Shared;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCatalogModule()
    .AddContractsModule()
    .AddSharedFramework(builder.Configuration);

var app = builder.Build();

app.UseCatalogModule();
app.UseContractsModule();
app.UseSharedFramework();

app.MapControllers();
app.MapGet("/ping", ctx => ctx.Response.WriteAsync("pong"));

app.Run();