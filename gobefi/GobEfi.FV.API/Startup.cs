using AutoMapper;
using GobEfi.FV.API.Filters;
using GobEfi.FV.API.Helpers;
using GobEfi.FV.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GobEfi.FV.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        //prueba
        public IConfiguration Configuration { get; }
        //readonly string allowSpecificOrigins = "_allowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers(opt => opt.Filters.Add(typeof(ExceptionFIlter)))
                .AddNewtonsoftJson();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton<IElectroMobilidadService, ElectroMobilidadService>();
            services.AddSingleton<IFileStorage, FileStorage>();
            services.AddSingleton<ISectorPublicoService, SectorPublicoService>();
            services.AddSingleton<IReporteService, ReporteService>();
            services.AddSingleton(Configuration.GetSection("ImpersonalizacionSettings").Get<ImpersonalizacionSettings>());
            services.AddSingleton(Configuration.GetSection("DirectorioArchivos").Get<DirectorioArchivos>());
            services.AddTransient<MyActionFilter>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                       ClockSkew = TimeSpan.Zero
                   }
               );
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("C:\\Logs\\FlotaVehicular\\log-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            
            app.UseCors(builder=>builder
            .WithOrigins("http://apirequest.io", "http://localhost:8100", "http://flotavehicular.minenergia.qa", "https://flotavehicular.minenergia.cl/")
            //.WithHeaders(new[] { "authorization", "content-type", "accept", "origin" })
            //.WithMethods(new[] { "GET", "POST", "PUT", "DELETE", "OPTIONS" })
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true)
            );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
