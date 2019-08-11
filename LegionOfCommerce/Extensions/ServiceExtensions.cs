using Contracts;
using Entities;
using Entities.Models;
using LegionOfCommerce.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using System;
using System.Text;

namespace LegionOfCommerce.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureApplicationSettings(this IServiceCollection services, IConfiguration config)
		{
			services.Configure<AppSettings>(config.GetSection("ApplicationSettings"));
		}
		public static void ConfigureCors(this IServiceCollection services, IConfiguration config)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder.WithOrigins(config["ApplicationSettings:ClientUrl"])
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
				options.User.RequireUniqueEmail = true;
			});
		}
		public static void ConfigureUnitOfWork(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}

		public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration config)
		{
			var key = EncodingHelper.GetEncodedJWTKey(config);
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = true,
				RequireExpirationTime = false,
				ClockSkew = TimeSpan.Zero
			};

			services.AddSingleton(tokenValidationParameters);

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = tokenValidationParameters;
			});
		}
	}
}