using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	[Table("user")]
	public partial class User
	{
		public User()
		{
			CustomerReview = new HashSet<CustomerReview>();
			Product = new HashSet<Product>();
			Review = new HashSet<Review>();
			ShoppingCartItem = new HashSet<ShoppingCartItem>();
			UserAddress = new HashSet<UserAddress>();
		}

		public int UserId { get; set; }
		public string Email { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string ProfilePicture { get; set; }
		public string Fname { get; set; }
		public string Lname { get; set; }
		public int? ResidenceAddressId { get; set; }
		public int? BillingAddressId { get; set; }
		public DateTime CreationDate { get; set; }

		public virtual ICollection<CustomerReview> CustomerReview { get; set; }
		public virtual ICollection<Product> Product { get; set; }
		public virtual ICollection<Review> Review { get; set; }
		public virtual ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }
		public virtual ICollection<UserAddress> UserAddress { get; set; }
	}
}
