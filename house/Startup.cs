using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using house.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using System.Linq;

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

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

            app.UseMvc(Routes.BuildRoutes);     
        }

        //this method will globally handle logging unhandled execptions 
        //it will respond json response for api an ajax calls that use json accept header
        //otherwise it will redirect to error page
        public void UseGlobalExceptionHandler(IApplicationBuilder app)
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

                    if (requiresJsonResponse)
                    {
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

                        await context.Response
                                     .WriteAsync(json, Encoding.UTF8)
                                     .ConfigureAwait(false);

                    }
                    else
                    {
                        context.Response.Redirect("/Home/Error");

                        //dont really need this, purely for symmetry.
                        await Task.CompletedTask;
                    }
                });
            });
        }


    }
}
