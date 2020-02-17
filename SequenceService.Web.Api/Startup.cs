using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sequence.Data;
using Sequence.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Serilog;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Sequence.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<ISequenceService, SequenceService>();
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IProcessedSequenceDto, ProcessedSequenceDto>();
            services.AddTransient<ISorter, PartitionSorter> ();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sequence Sort API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable Swagger Middleware.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sequence Sort API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            // Simple handle all unhandled exceptions and write to file (Serilog config in appsettings)
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                // Log with Serilog 
                // We could also easily log to centralized log server over HTTP
                Log.Logger.Error(exception, null);

                // Send the result back in JSON
                // Would need to do something for production with this
                var result = JsonConvert.SerializeObject(new { error = exception.Message });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
