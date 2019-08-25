using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
	public class ProductImage
	{
		public int ProductImageId { get; set; }
		public int ProductId { get; set; }
		public string ProductImageUrl { get; set; }

		public virtual Product Product { get; set; }
	}
}
