using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cakeworld.Models;
using Microsoft.EntityFrameworkCore;
using cakeworld.Services.MailService;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using cakeworld.Services.JWT_Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using cakeworld.Services;

namespace cakeworld
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OnlineDBContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("OnlineDBContext")));
            services.AddControllers();
            services.AddTransient<MakePayment>();
            services.AddScoped<IJWTService, JWTService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                   // IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddTransient<IMailService, MailService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

           // app.UseCors(options => 
          //  options.WithOrigins("http://localhost:3000")
            // .AllowAnyMethod()
             //.AllowAnyHeader());

            app.UseCors(Options =>
            Options.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


           // app.UseStaticFiles(new StaticFileOptions
           // {
           //     FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Images")),
             //   RequestPath = "/Images"
            //});


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
