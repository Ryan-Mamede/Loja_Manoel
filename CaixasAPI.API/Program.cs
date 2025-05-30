using CaixasAPI.Domain.Services;
using CaixasAPI.Infrastructure.Data;
using CaixasAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CaixasAPI", Version = "v1" });
    });


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IEmpacotamentoService, EmpacotamentoService>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CaixasAPI v1");
        c.RoutePrefix = string.Empty;
    });

app.MapControllers();

try
{


    Console.WriteLine("Aplicação iniciada com sucesso!");
}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao iniciar a aplicação: {ex.Message}");
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
