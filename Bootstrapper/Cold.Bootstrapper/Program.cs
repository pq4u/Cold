using Cold.Catalog.Api;
using Cold.Contracts.Api;
using Cold.Deliveries.Api;
using Cold.Packages.Api;
using Cold.Shared;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCatalogModule()
    .AddContractsModule()
    .AddDeliveriesModule()
    .AddPackagesModule()
    .AddSharedFramework(builder.Configuration);

var app = builder.Build();

app.UseCatalogModule();
app.UseContractsModule();
app.UseDeliveriesModule();
app.UsePackagesModule();
app.UseSharedFramework();

app.MapControllers();
app.MapGet("/ping", ctx => ctx.Response.WriteAsync("pong"));

app.Run();