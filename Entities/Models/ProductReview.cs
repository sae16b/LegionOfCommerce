using System;
using System.Collections.Generic;

namespace Entities.Models
{
	public partial class ProductReview
	{
		public int ProductReviewId { get; set; }
		public int ReviewId { get; set; }
		public int TargetProductId { get; set; }

		public virtual Review Review { get; set; }
		public virtual Product TargetProduct { get; set; }
	}
}
