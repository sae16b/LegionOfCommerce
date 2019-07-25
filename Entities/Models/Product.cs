using System;
using System.Collections.Generic;

namespace Entities.Models
{
  public partial class Product
  {
    public Product()
    {
      Order = new HashSet<Order>();
      ProductAddress = new HashSet<ProductAddress>();
      ProductReviews = new HashSet<ProductReviews>();
      QuestionAnswer = new HashSet<QuestionAnswer>();
      ShoppingCartItem = new HashSet<ShoppingCartItem>();
    }

    public int ProductId { get; set; }
    public int SellerId { get; set; }
    public string MainImgUrl { get; set; }
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

    public virtual User Seller { get; set; }
    public virtual ICollection<Order> Order { get; set; }
    public virtual ICollection<ProductAddress> ProductAddress { get; set; }
    public virtual ICollection<ProductReviews> ProductReviews { get; set; }
    public virtual ICollection<QuestionAnswer> QuestionAnswer { get; set; }
    public virtual ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }
  }
}
