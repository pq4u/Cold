using Cold.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedFramework(builder.Configuration);

var app = builder.Build();

app.UseSharedFramework();

app.MapControllers();
app.MapGet("/ping", ctx => ctx.Response.WriteAsync("pong"));

app.Run();