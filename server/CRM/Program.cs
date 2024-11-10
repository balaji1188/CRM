
using CRM.DBContext;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace CRM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAllOrigins",
                    configurePolicy: policy =>
                    {
                        policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            builder.Services.AddDbContext<PersonDBContext>(options =>
                                    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration["RedisCacheServerUrl"]; 
            });
            builder.Services.AddMemoryCache();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            

            var app = builder.Build();
            app.UseCors("AllowAllOrigins");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
          
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.UseRouting();
         
            app.Run();
        }
    }
}
