using Entities.Extensions;
using Xunit;

namespace Entities.UnitTests
{
	public class StringExtensionsUnitTests
	{
		[Theory]
		[InlineData("abc@yahoo.com", true)]
		[InlineData("abcyahoo.com", false)]
		[InlineData("abcyahoocom", false)]
		[InlineData("@yahoo.com", false)]
		public void IsEmail_ShouldVerifyEmails(string email, bool expected)
		{
			// Action
			bool actual = email.IsEmail();
			// Assertion
			Assert.Equal(expected, actual);
		}
	}
}
