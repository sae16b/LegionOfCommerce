using Contracts.Services;
using LegionOfCommerce.Extensions;
using LegionOfCommerce.Middlewares;
using LegionOfCommerce.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LegionOfCommerce
{
	public class Startup
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.ConfigureApplicationSettings(Configuration);
			services.ConfigureCors(Configuration);
			services.ConfigureIISIntegration();
			services.ConfigureSqlContext(Configuration);
			services.ConfigureUnitOfWork();
			services.ConfigureDefaultIdentity();
			services.ConfigureIdentityOptions();
			services.ConfigureJwtAuthentication(Configuration);
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			logger.Info("Services have loaded in");

			services.AddScoped<IUserService, UserService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.Use((context, next) =>
			{
				context.Response.Headers["Access-Control-Allow-Origin"] = Configuration["ApplicationSettings:ClientUrl"];
				return next.Invoke();
			});

			// This middleware is used to validate the preflight CORS request from the client
			app.UseOptions();

			app.UseAuthentication();
			app.UseMvc();
		}
	}
}
