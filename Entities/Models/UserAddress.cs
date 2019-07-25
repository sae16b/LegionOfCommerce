using System;
using System.Collections.Generic;

namespace Entities.Models
{
  public partial class UserAddress
  {
    public int UserAddressId { get; set; }
    public int UserId { get; set; }
    public string Type { get; set; }
    public int AddressId { get; set; }

    public virtual Address Address { get; set; }
    public virtual User User { get; set; }
  }
}
