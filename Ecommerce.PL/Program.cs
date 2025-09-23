using Ecommerce.BLL.Services;
using Ecommerce.BLL.Services.Classes;
using Ecommerce.BLL.Services.Interfaces;
using Ecommerce.DAL.Data;
using Ecommerce.DAL.Models;
using Ecommerce.DAL.Repositery;
using Ecommerce.DAL.Repositery.Classes;
using Ecommerce.DAL.Repositery.Interfaces;
using Ecommerce.DAL.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

namespace Ecommerce.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbcontext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICategoryRepositery,CategoryRepositery>();
            builder.Services.AddScoped<ICategoryService,CategoryService>();
            builder.Services.AddScoped<IBrandRepositery,BrandRepositery>();
            builder.Services.AddScoped<IBrandService,BrandService>();
            builder.Services.AddScoped<ISeedData,SeedData>();
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddEntityFrameworkStores<AppDbcontext>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("jwtOptions")["SecretKey"]))
            };
        });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapScalarApiReference();
                app.MapOpenApi();
            }

            var scope = app.Services.CreateScope();
           var objSeedData= scope.ServiceProvider.GetRequiredService<ISeedData>();
            await objSeedData.SeedDataModelsAsync();
            await objSeedData.IdentitySeedDataAsync();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
