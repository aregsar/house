using System;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using house.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace house
{
    public class Startup
    {

        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public Startup( ILogger<Startup> logger
                      , IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }


        // Configure DI container
        public void ConfigureServices(IServiceCollection services)
        {
            _logger.LogDebug("ConfigureServices");

            _logger.LogDebug($"Default Log Level: {_configuration.GetSection("Logging").GetValue<string>("LogLevel:Default")}");

            services.AddDbContext<HouseDbContext>(options =>
                                                  options.UseSqlite(_configuration.GetConnectionString("House")));

            services.AddScoped<HouseRepository, HouseRepository>();

            services.AddIdentity<AppUser, IdentityRole<int>>()
                    .AddEntityFrameworkStores<HouseDbContext>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;
            });

            services.AddAuthentication()
                      .AddCookie()
                      .AddJwtBearer();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;         
                options.LoginPath = "/Signin/New";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    });
        }


        // Configure Global Middleware
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            _logger.LogDebug("Configure");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                UseGlobalExceptionHandler(app);
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //keep UseAuthentication before UseMvc
            app.UseAuthentication();
            app.UseMvc(Routes.BuildRoutes);  

        }

        //this method will globally handle logging unhandled execptions 
        //it will respond json response for ajax calls that use json accept header
        //otherwise it will redirect to an error page
        private void UseGlobalExceptionHandler(IApplicationBuilder app)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>().Error;

                    string errorDetails = $@"{exception.Message}
                                             {Environment.NewLine}
                                             {exception.StackTrace}";

                    int statusCode = (int)HttpStatusCode.InternalServerError;

                    context.Response.StatusCode = statusCode;

                    bool requiresJsonResponse = context.Request
                                                       .GetTypedHeaders()
                                                       .Accept
                                                       .Any(t => t.Suffix.Value?.ToUpper() == "JSON"
                                                              || t.SubTypeWithoutSuffix.Value?.ToUpper() == "JSON");

                    context.Response.ContentType = "application/json";

                    var problemDetails = new ProblemDetails
                    {
                        Title = "Unexpected error",
                        Status = statusCode,
                        Detail = errorDetails,
                        Instance = Guid.NewGuid().ToString()
                    };

                    var json = JsonConvert.SerializeObject(problemDetails);

                    _logger.LogError(json);

                    if (requiresJsonResponse)
                    {
                        await context.Response
                                     .WriteAsync(json, Encoding.UTF8);
                    }
                    else
                    {
                        context.Response.Redirect("/Home/Error");

                        await Task.CompletedTask;
                    }
                });
            });
        }


    }
}
