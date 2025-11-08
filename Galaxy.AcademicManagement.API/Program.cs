using Galaxy.AcademicMagement.Application;
using Galaxy.AcademicMagement.Infrastructure;
using Galaxy.AcademicMagement.Infrastructure.Configurations.Auth;
using Galaxy.AcademicMagement.Infrastructure.Configurations.Observability;
using Galaxy.AcademicMagement.Infrastructure.Configurations.Seeder;
using Galaxy.AcademicManagement.API.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var licenseMediatR = builder.Configuration["MediatR:LicenseKey"];

builder.Services.AddMediatR(cfg =>
{
    cfg.LicenseKey = licenseMediatR;
    cfg.RegisterServicesFromAssemblies(Assembly.Load("Galaxy.AcademicMagement.Application"));
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSerilogElastic(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionsHandlingMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


//Sembrar Data inicial IdentitySeeder
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentitySeeder.SeedAsync(services);
}

app.Run();
