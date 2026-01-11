using Cold.Auth.Api;
using Cold.Catalog.Api;
using Cold.Contracts.Api;
using Cold.Deliveries.Api;
using Cold.Packages.Api;
using Cold.Shared;
using Microsoft.AspNetCore.Identity;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthModule(builder.Configuration)
    .AddCatalogModule()
    .AddContractsModule()
    .AddDeliveriesModule()
    .AddPackagesModule()
    .AddSharedFramework(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    string[] roles = { "Admin", "Employee", "Supplier" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(role));
        }
    }
}

app.UseSharedFramework();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthModule();
app.UseCatalogModule();
app.UseContractsModule();
app.UseDeliveriesModule();
app.UsePackagesModule();

app.MapControllers();
app.MapGet("/ping", ctx => ctx.Response.WriteAsync("pong"));

app.Run();