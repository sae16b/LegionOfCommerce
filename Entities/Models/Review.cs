using System;
using System.Collections.Generic;

namespace Entities.Models
{
  public partial class Review
  {
    public Review()
    {
      CustomerReview = new HashSet<CustomerReview>();
      ProductReviews = new HashSet<ProductReviews>();
    }

    public int ReviewId { get; set; }
    public int Rating { get; set; }
    public string Body { get; set; }
    public DateTime CreationDate { get; set; }
    public int AmountUseful { get; set; }
    public int AuthorId { get; set; }
    public string Title { get; set; }

    public virtual User Author { get; set; }
    public virtual ICollection<CustomerReview> CustomerReview { get; set; }
    public virtual ICollection<ProductReviews> ProductReviews { get; set; }
  }
}
