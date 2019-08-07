using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
	public class UserRegistrationModel
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
	public class UserLoginModel
	{
		public string EmailOrUserName { get; set; }
		public string Password { get; set; }
	}
}
