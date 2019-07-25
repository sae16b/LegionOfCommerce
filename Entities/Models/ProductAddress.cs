using System;
using System.Collections.Generic;

namespace Entities.Models
{
  public partial class ProductAddress
  {
    public int ProductAddressId { get; set; }
    public int AddressId { get; set; }
    public int ProductId { get; set; }

    public virtual Address Address { get; set; }
    public virtual Product Product { get; set; }
  }
}
