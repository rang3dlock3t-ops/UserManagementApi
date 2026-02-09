
using Microsoft.AspNetCore.Identity;
using UserManagementApi.Middleware;
using UserManagementApi.Models;

namespace UserManagementApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
            builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Use(async(context, next) =>
            {
                try
                {
                    await next.Invoke();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });


            app.MapControllers();

            app.Run();
        }
    }
}
