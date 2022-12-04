using ASP34;
using ASP34.Configuration;
using ASP34.Contracts.Models.Devices;
using ASP34.Contracts.Validation;
using ASP34.Data;
using ASP34.Data.Repos;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HomeApi",
        Version = "v1"
    });
});

builder.Configuration
    .AddJsonFile("HomeOptions.json");
    //.AddJsonFile("appsettings.json")
    //.AddJsonFile("appsettings.Development.json");


builder.Services.Configure<HomeOptions>(builder.Configuration);

var assembly = Assembly.GetAssembly(typeof(MappingProfile));
builder.Services.AddAutoMapper(assembly);

builder.Services.AddSingleton<IDeviceRepository, DeviceRepository>();
builder.Services.AddSingleton<IRoomRepository, RoomRepository>();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HomeApiContext>(options =>
    options.UseSqlServer(connection),
    ServiceLifetime.Singleton);

builder.Services.AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssemblyContaining<AddDeviceRequestValidator>());

builder.Services.Configure<Address>
    (builder.Configuration.GetSection("Address"));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
