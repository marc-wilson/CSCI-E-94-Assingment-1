using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TicTacToe
{
	/// <summary>
	/// Main Startup Class. This is the entry point of the application.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Hosting Environment class with environemnt variables and other information.
		/// </summary>
		public IHostingEnvironment HostingEnvironment { get; }

		/// <summary>
		/// Method to insantiate the Startup class with the Configuration and Hosting Environment
		/// </summary>
		/// <param name="configuration"></param>
		/// <param name="hostingEnvironment"></param>
		public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
		{
			// Set the Configuration
			Configuration = configuration;
			// Set the HostingEnvironemnt
			HostingEnvironment = hostingEnvironment;
		}

		/// <summary>
		/// Configuration of the application
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// Method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
		{
			// Set dot net core compatibility version to 2.1
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			// Add Swagger UI Config method
			services.AddSwaggerGen(ConfigureSwaggerUI);
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			// If development, provide an exception page
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}
			// Enable default files for the application
			app.UseDefaultFiles();
			// Use static files for the application
			app.UseStaticFiles();
			// Use Https Redirection
			app.UseHttpsRedirection();
			// Use MVC
			app.UseMvc();

			// Add Swagger configuration and documentation
			app.UseSwagger(c =>
			{
				c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
				{
					swaggerDoc.Host = httpReq.Host.Value;
				});
			});

			// Configure swagger endpoint
			app.UseSwaggerUI(c =>
		   {
			   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tic Tac Toe Swagger");
		   });

		}

		/// <summary>
		/// Method to configure Swager UI
		/// </summary>
		/// <param name="swaggerGenOptions"></param>
		public void ConfigureSwaggerUI(SwaggerGenOptions swaggerGenOptions)
		{
			// Set swagger options for Swagger Doc
			swaggerGenOptions.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
			{
				Title = "Tic Tac Toe Swagger",
				Version = "v1"
			});

			// Set file path where swagger lives.
			var filePath = Path.Combine(HostingEnvironment.ContentRootPath, "tictactoeswagger.config");
			swaggerGenOptions.IncludeXmlComments(filePath);
		}
	}
}
