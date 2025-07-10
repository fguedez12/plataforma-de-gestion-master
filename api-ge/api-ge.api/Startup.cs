using api_gestiona.Entities;
using api_gestiona.Filters;
using api_gestiona.Helpers;
using api_gestiona.Services;
using api_gestiona.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Globalization;
using System.Text;

namespace api_gestiona
{
    public static class Startup
    {
        public static WebApplication InitializeApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            return app;
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            ConfigurationManager _configuration = builder.Configuration;
            builder.Services.AddControllers(options => {
                options.Filters.Add(typeof(ExceptionFilter));
                options.Conventions.Add(new SwaggerGroupVersion());
            }).AddNewtonsoftJson();
            builder.Services.AddScoped<IMedicionInteligenteService,MedicionInteligenteService>();
            builder.Services.AddScoped<ITipoDocumentoService, TipoDocumentoService>();
            builder.Services.AddScoped<IDocumentosService, DocumentosService>();
            builder.Services.AddScoped<IDivisionService, DivisionService>();
            builder.Services.AddScoped<IServicioService, ServicioService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IInstitucionService, InstitucionService>();
            builder.Services.AddScoped<IFlotaVehicularService, FlotaVehicularService>();
            builder.Services.AddScoped<IMedidorService,MedidorService>();
            builder.Services.AddScoped<IPaisService, PaisService>();
            builder.Services.AddScoped<IAeropuertoService, AeropuertoService>();
            builder.Services.AddScoped<IViajeService, ViajeService>();
            builder.Services.AddScoped<ISistemasService, SistemasService>();
            builder.Services.AddScoped<IPlanGestionService, PlanGestionService>();
            builder.Services.AddScoped<IFiltrosService, FiltrosService>();
            builder.Services.AddScoped<IComprasService, ComprasService>();
            builder.Services.AddTransient<ApplicationValidation>();
            builder.Services.AddTransient<Constants>();
            builder.Services.AddAutoMapper(typeof(Startup));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Api Gestiona Energia",
                    Version = "v1"
                });
                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Api Gestiona Energia",
                    Version = "v2"
                });
                c.OperationFilter<AddRequiredHeaderParameter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });

            });
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = false,
                                ValidateAudience = false,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(
                                        Encoding.UTF8.GetBytes(_configuration["jwt:key"])),
                                ClockSkew = TimeSpan.Zero
                            }
            );

            builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                var sites = _configuration.GetSection("CorsOrigins:sites").Get<string[]>();
                builder
                    .WithOrigins(sites)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders(new string[] { "totalRecords" });
            }));
            builder.Services.AddAutoMapper(typeof(Startup));
            builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.Console()
                .ReadFrom.Configuration(ctx.Configuration)
            );
        }

        private static void Configure(WebApplication app)
        {
            var cultureInfo = new CultureInfo("es-CL");
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("QA"))
            {
                app.UseSwagger();

                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "api-gestionaenergia v1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "api-gestionaenergia v2");
                });

                app.UseExceptionHandler(c => c.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerPathFeature>();
                    var response = new { message = exception.Error.Message };
                    await context.Response.WriteAsJsonAsync(response);
                }));
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            app.UseAuthentication();

            app.MapControllers();

      
        }
    }
}
