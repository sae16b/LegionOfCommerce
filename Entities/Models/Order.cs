using System;
using System.Collections.Generic;

namespace Entities.Models
{
  public partial class Order
  {
    public Order()
    {
      OrderAddress = new HashSet<OrderAddress>();
    }

    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int SellerId { get; set; }
    public int BuyerId { get; set; }
    public string ProductCondition { get; set; }
    public float ProductPrice { get; set; }
    public string ProductTitle { get; set; }
    public DateTime Date { get; set; }
    public string State { get; set; }
    public string UserType { get; set; }
    public float ShippingCost { get; set; }
    public string PaymentMethod { get; set; }

    public virtual Product Product { get; set; }
    public virtual ICollection<OrderAddress> OrderAddress { get; set; }
  }
}
