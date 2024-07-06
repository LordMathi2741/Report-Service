using Application.DTO.Mapper;
using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(dbOptions =>
{
    if (builder.Environment.IsDevelopment())
    {
        dbOptions.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
    }
    else if (builder.Environment.IsProduction())
    {
        dbOptions.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors();
    }
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Reports API",
        Description = "Reports Rest API Documentation",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "LordMathi2741",
            Url = new Uri("https://github.com/LordMathi2741")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
        
    });
});

builder.Services.AddScoped(typeof(IInfrastructureRepository<>),typeof(InfrastructureRepository<>));
builder.Services.AddScoped<IReportDomainRepository,ReportDomainRepository>();
builder.Services.AddScoped<IUserDomainRepository, UserDomainRepository>();
builder.Services.AddAutoMapper(typeof(ModelToResponse), typeof(RequestToModel));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseAuthorization();

app.UseHttpsRedirection();


app.Run();

