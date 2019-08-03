using LegionOfCommerce.Extensions;
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
			services.ConfigureCors();
			services.ConfigureIISIntegration();
			services.ConfigureSqlContext(Configuration);
			services.ConfigureUnitOfWork();
			services.ConfigureDefaultIdentity();
			services.ConfigureIdentityOptions();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			logger.Info("Services have loaded in");
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseAuthentication();

			app.UseMvc();
		}
	}
}
