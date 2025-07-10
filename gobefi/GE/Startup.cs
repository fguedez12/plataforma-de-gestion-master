using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Services;
using GobEfi.Web.Core.Configuration;
using FluentValidation.AspNetCore;
using GobEfi.Web.Models.EmailModels;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Extensions;
using FluentValidation;
using GobEfi.Web.Models.CompraModels;
using GobEfi.Web.Core.Validation;
using GobEfi.Web.Models.NumeroClienteModels;
using ReflectionIT.Mvc.Paging;

namespace GobEfi.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("*").WithMethods("*").WithHeaders("*");
                });
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("Default")));

            services.AddIdentity<Usuario, Rol>(
                config => {
                    config.User.AllowedUserNameCharacters = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789-._@+";
                    config.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<SpanishIdentityErrorDescriber>();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            });

            services.Configure<EmailConfigurationModel>(Configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                //options.IdleTimeout = TimeSpan.FromSeconds(5);
                options.Cookie.Name = "GE.Session";
            });

            ProfileConfiguration.Register(services);
            RepositoryConfiguration.Register(services);
            ServiceConfiguration.Register(services);
            ValidationConfiguration.Register(services);
            AppServiceConfiguration.Register(services);

            services.AddScoped<IUserClaimsPrincipalFactory<Usuario>, ClaimsPrincipalFactory>();

            // Obtenidos desde appsettings.json
            services.AddSingleton(Configuration.GetSection("ApiConfiguration").Get<ApiConfiguration>());
            services.AddSingleton(Configuration.GetSection("DirectorioArchivos").Get<DirectorioArchivos>());
            services.AddSingleton(Configuration.GetSection("ImpersonalizacionSettings").Get<ImpersonalizacionSettings>());

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            })            
            .AddFluentValidation()
            .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1)
            .AddJsonOptions(opt => 
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            .AddViewLocalization()
            .AddDataAnnotationsLocalization()
            
            .ConfigureModelBindingMessages();

            services.AddTransient<IValidator<CompraForRegister>, CompraForRegisterValidator > ();
            services.AddTransient<IValidator<CompraForEdit>, CompraForEditValidator>();
            services.AddTransient<IValidator<NumeroClienteModel>, NumeroClienteModelValidator>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministradorRole", policy => policy.RequireRole(Constants.Claims.ES_ADMINISTRADOR));
                options.AddPolicy("RequireGestorServicioRole", policy => policy.RequireRole(Constants.Claims.ES_GESTORSERVICIO));
                options.AddPolicy("RequireGestorUnidadRole", policy => policy.RequireRole(Constants.Claims.ES_GESTORUNIDAD));
                options.AddPolicy("RequireGestorConsultaRole", policy => policy.RequireRole(Constants.Claims.ES_GESTORCONSULTA));
                options.AddPolicy("RequireAuditorRole", policy => policy.RequireRole(Constants.Claims.ES_AUDITOR));
                options.AddPolicy("RequireUsuarioRole", policy => policy.RequireRole(Constants.Claims.ES_USUARIO));
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Home/Error");
                app.UseHsts();
            }
            app.UseCors(builder => builder.WithOrigins("*").WithMethods("*").WithHeaders("*"));
            app.UseHttpsRedirection();
            if (env.IsDevelopment())
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = ctx =>
                    {
                        ctx.Context.Response.Headers.Append("Cache-Control", "no-cache, no-store");
                        ctx.Context.Response.Headers.Append("Expires", "-1");
                    }
                });
            }
            else
            {
                app.UseStaticFiles();
            }
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=MiUnidad}/{action=Index}/{id?}");
            });

            app.Use(async (context, next) =>
            {


                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN"); // Evitar el ClickJacking



                await next();
            });
        }
    }
}
