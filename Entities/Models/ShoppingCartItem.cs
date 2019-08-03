using System;
using System.Collections.Generic;

namespace Entities.Models
{
	public partial class ShoppingCartItem
	{
		public int ShoppingCartItemId { get; set; }
		public string UserId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }

		public virtual Product Product { get; set; }
		public virtual User User { get; set; }
	}
}
