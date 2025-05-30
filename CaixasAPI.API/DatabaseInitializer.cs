using CaixasAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CaixasAPI.API
{
    public static class DatabaseInitializer
    {
        public static IHost InitializeDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();

                    context.Database.Migrate();

                    Console.WriteLine("Banco de dados inicializado com sucesso.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao inicializar o banco de dados: {ex.Message}");
                }
            }
            return host;
        }
    }
}
