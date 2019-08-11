using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
	public class UserRegistrationModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
	public class UserLoginModel
	{
		public string EmailOrUserName { get; set; }
		public string Password { get; set; }
	}
	public class AuthResult
	{
		public string Token { get; set; }
		public string RefreshToken { get; set; }
		public bool Success { get; set; }
		public IEnumerable<string> Errors { get; set; }
	}
	public class AuthRequest
	{
		public string Token { get; set; }
		public string RefreshToken { get; set; }
	}
}
