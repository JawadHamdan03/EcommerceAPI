using Ecommerce.BLL.Services;
using Ecommerce.BLL.Services.Classes;
using Ecommerce.BLL.Services.Interfaces;
using Ecommerce.DAL.Data;
using Ecommerce.DAL.Repositery;
using Ecommerce.DAL.Repositery.Classes;
using Ecommerce.DAL.Repositery.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Ecommerce.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbcontext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICategoryRepositery,CategoryRepositery>();
            builder.Services.AddScoped<ICategoryService,CategoryService>();
            builder.Services.AddScoped<IBrandRepositery,BrandRepositery>();
            builder.Services.AddScoped<IBrandService,BrandService>();
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapScalarApiReference();
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
