using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MyERP.API.Filters;
using MyERP.API.Middlewares;
using MyERP.API.Modules;
using MyERP.Repository;
using MyERP.Service.Mappings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// add jwt bearer - appsettings

// rate limiter

// add output cache

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MapProfile));

// NotFoundFilter
builder.Services.AddScoped(typeof(NotFoundFilter<>));

// DbContext
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

// Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Custom Exception
app.UseCustomException();

// use authentication
app.UseAuthentication();

// use authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
