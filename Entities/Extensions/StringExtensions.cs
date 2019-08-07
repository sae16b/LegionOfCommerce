using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Entities.Extensions
{
	public static class StringExtensions
	{
		// Checks if string is an email or not
		public static bool IsEmail(this string str)
		{
			try
			{
				MailAddress address = new MailAddress(str);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
