using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Datadog.Trace;
using System.Globalization;

namespace WeatherServiceDotNetCore
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherServiceDotNetCore", Version = "v1" });
            });

            services.AddSingleton(Tracer.Instance);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherServiceDotNetCore v1"));
            }

            // app.UseHttpsRedirection();

            // add a quick tracing middleware
            app.Use(async (context, next) =>
            {
                using var scope = Tracer.Instance.StartActive("aspnetcore.middleware");
                scope.Span.SetTag(Tags.HttpMethod, context.Request.Method);
                scope.Span.SetTag(Tags.HttpUrl, context.Request.Path);

                await next.Invoke();

                scope.Span.SetTag(Tags.HttpStatusCode, context.Response.StatusCode.ToString("N0", CultureInfo.InvariantCulture));
            });

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
