using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Commander.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace Commander
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
			services.AddDbContext<CommanderContext>(opt =>
				opt.UseMySql(Configuration.GetConnectionString("CommanderConnection"), ServerVersion.FromString("8.0.22")));

			services.AddControllers().AddNewtonsoftJson(x =>
			{
				x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			});

			services.AddScoped<ICommanderRepo, MySqlCommanderRepo>();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //Making automapper available for DI in application
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

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
