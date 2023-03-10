using Microsoft.OpenApi.Models;
using WSClinica.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<CentroAtencionService, CentroAtencionService>();
builder.Services.AddScoped<CitasService, CitasService>();
builder.Services.AddScoped<MedicoService, MedicoService>();
builder.Services.AddScoped<PacienteService, PacienteService>();
builder.Services.AddScoped<SalaService, SalaService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Clinica API",
        Description = "ASP.NET Core Web API Clinica",
        Contact = new OpenApiContact
        {
            Name = "Alexander Escobar",
            Email = "<alex.escobar104@hotmail.com>",
            Url = new Uri("https://github.com/AlexanderEscobar104")
        }
    });
});

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
