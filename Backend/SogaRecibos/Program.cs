using SogaRecibos.Extensions;
using SogaRecibos.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// --- Add services to the container ---
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

// --- Configure the HTTP request pipeline ---
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

