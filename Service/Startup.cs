using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Newtonsoft.Json.Serialization;
using Prometheus;

namespace Service
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
			services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

			services.AddControllers()
			.AddNewtonsoftJson()
			.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
			
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service", Version = "v1" });
				var xmlFile = $"Service.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				
			}
			app.UseSwagger();
			app.UseSwaggerUI(c => 
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service v1");
				c.RoutePrefix = string.Empty;
			});

			var counter = Metrics.CreateCounter("profanity_counter", "Counts requests to the People API endpoints", new CounterConfiguration
			{
				LabelNames = new[] { "method", "endpoint" }
			});
			app.Use((context, next) =>
			{
				counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
				return next();
			});
			app.UseMetricServer();
			app.UseHttpMetrics();

			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseCors("MyPolicy");

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
