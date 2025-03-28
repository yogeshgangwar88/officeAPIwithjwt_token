
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using officeapi.Services;
using RepoLibrary.DBContext;
using RepoLibrary.Interfaces;
using RepoLibrary.Repository;
using ServiceLibrary.Interfaces;
using ServiceLibrary.Services;
using System;
using System.Text;

namespace officeapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Constr")));
            builder.Services.AddMvc(services => services.EnableEndpointRouting = false).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            builder.Services.AddAutoMapper(typeof(Program));
            // add custom services in DI //
            builder.Services.AddScoped<ILoginRepo, LoginRepo>();
            builder.Services.AddScoped<ILogin, LoginService>();

            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped<IProductService, ProductService>();

           //////////////////////////////////////////
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200") // Add your client-side application's origin here
                               .WithMethods("GET", "POST", "PUT", "DELETE")
                               .AllowAnyHeader();
                    });
            });

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}
            //else
            //    app.UseExceptionHandler();
            app.UseHsts();
            app.UseHttpsRedirection();
            if (!Directory.Exists(Path.Combine(builder.Environment.ContentRootPath, "UserImages")))
            {
                Directory.CreateDirectory(Path.Combine(builder.Environment.ContentRootPath, "UserImages"));
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "UserImages")),
                RequestPath = "/staticImages"

            });
            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");
          
            app.UseAuthentication();
            app.UseAuthorization();
            ///custom middleware ///
            app.UseCustomMiddleware(builder.Configuration);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}
