using System;

namespace Entities
{
	public class AppSettings
	{
		public string ClientUrl { get; set; }
		public string JWTSecret { get; set; }
		public TimeSpan TokenLifetime { get; set; }
	}
}
