using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	public partial class User
	{
		public User()
		{
			CustomerReviews = new HashSet<CustomerReview>();
			Products = new HashSet<Product>();
			Reviews = new HashSet<Review>();
			ShoppingCartItems = new HashSet<ShoppingCartItem>();
			Orders = new HashSet<Order>();
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

		public virtual UserAddress UserAddress { get; set; }

		public virtual ICollection<CustomerReview> CustomerReviews { get; set; }
		public virtual ICollection<Product> Products { get; set; }
		public virtual ICollection<Review> Reviews { get; set; }
		public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
		public virtual ICollection<Order> Orders { get; set; }

		public string GetUserNameAndEmail()
		{
			return Username + Email;
		}
	}
}
