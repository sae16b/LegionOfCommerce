using System;
using System.Collections.Generic;

namespace Entities.Models
{
  public partial class CustomerReview
  {
    public int CustomerReviewId { get; set; }
    public int ReviewId { get; set; }
    public int TargetUserId { get; set; }

    public virtual Review Review { get; set; }
    public virtual User TargetUser { get; set; }
  }
}
