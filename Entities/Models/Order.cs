using System;
using System.Collections.Generic;

namespace Entities.Models
{
	public partial class Order
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public string SellerId { get; set; }
		public string BuyerId { get; set; }
		public string ProductCondition { get; set; }
		public float ProductPrice { get; set; }
		public string ProductTitle { get; set; }
		public DateTime Date { get; set; }
		public string State { get; set; }
		public string UserType { get; set; }
		public float ShippingCost { get; set; }
		public string PaymentMethod { get; set; }

		public virtual Product Product { get; set; }
		public virtual OrderAddress OrderAddress { get; set; }
	}
}
