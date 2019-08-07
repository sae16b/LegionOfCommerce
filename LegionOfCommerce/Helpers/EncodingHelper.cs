using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegionOfCommerce.Helpers
{
	public static class EncodingHelper
	{
		// This will return the encoded JWT key for the application,
		// given the application's configuration file
		public static byte[] GetEncodedJWTKey(IConfiguration config)
		{
			return Encoding.UTF8.GetBytes(config["ApplicationSettings:JWTSecret"].ToString()); ;
		}
		// or given the AppSettings object
		public static byte[] GetEncodedJWTKey(AppSettings appSettings)
		{
			return Encoding.UTF8.GetBytes(appSettings.JWTSecret); ;
		}
	}
}
