using Contracts;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using System;

namespace LegionOfCommerce.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureCors(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials());
			});
		}
		public static void ConfigureIISIntegration(this IServiceCollection services)
		{
			services.Configure<IISOptions>(options =>
			{

			});
		}
		public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
		{
			string connectionString = config["sqlConnection:connectionString"];
			services.AddDbContext<LegionContext>(o => o.UseMySql(connectionString));
		}
		public static void ConfigureUnitOfWork(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}
	}
}