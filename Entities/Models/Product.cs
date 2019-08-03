using System;
using System.Collections.Generic;

namespace Entities.Models
{
	public partial class Product
	{
		public Product()
		{
			ProductReviews = new HashSet<ProductReview>();
			QuestionAnswers = new HashSet<QuestionAnswer>();
			Orders = new HashSet<Order>();
			ShoppingCartItems = new HashSet<ShoppingCartItem>();
		}

		public int ProductId { get; set; }
		public string SellerId { get; set; }
		//public string MainImgUrl { get; set; }
		public float Rating { get; set; }
		public int RatingsCount { get; set; }
		public int Quantity { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public float Price { get; set; }
		public float? SalePercentage { get; set; }
		public float ShippingCost { get; set; }
		public string State { get; set; }
		public DateTime CreationDate { get; set; }
		public string Condition { get; set; }

		public virtual ProductAddress ProductAddress { get; set; }

		public virtual User Seller { get; set; }
		public virtual ICollection<ProductReview> ProductReviews { get; set; }
		public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
		public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
	}
}
