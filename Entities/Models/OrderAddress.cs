using System;
using System.Collections.Generic;

namespace Entities.Models
{
  public partial class OrderAddress
  {
    public int OrderAddressId { get; set; }
    public int AddressId { get; set; }
    public int OrderId { get; set; }

    public virtual Address Address { get; set; }
    public virtual Order Order { get; set; }
  }
}
