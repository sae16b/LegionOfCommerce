using Contracts;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
		public static void ConfigureDefaultIdentity(this IServiceCollection services)
		{
			services.AddDefaultIdentity<User>()
				.AddEntityFrameworkStores<LegionContext>();
		}
		public static void ConfigureIdentityOptions(this IServiceCollection services)
		{
			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 6;
			});
		}
		public static void ConfigureUnitOfWork(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}
	}
}