using Microsoft.EntityFrameworkCore;
using nps_backend_susana.Model.Interfaces;
using nps_backend_susana.Model.Repositories;
using nps_backend_susana.Services;

namespace nps_backend_susana
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("MinhaConexao");
            builder.Services.AddDbContext<Contexto>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<INpsLogRepository, NpsLogRepository>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            builder.Services.AddHttpClient();

            builder.Services.AddControllers();

            builder.Services.AddScoped<NpsLogService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();
            app.UseCors("AllowAllOrigins"); // Habilita o CORS para permitir requisições do React

            app.MapControllers();

            app.Run();
        }
    }
}