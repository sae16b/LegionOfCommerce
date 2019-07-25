using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	[Table("address")]
	public partial class Address
	{
		public Address()
		{
			OrderAddress = new HashSet<OrderAddress>();
			ProductAddress = new HashSet<ProductAddress>();
			UserAddress = new HashSet<UserAddress>();
		}

		public int AddressId { get; set; }
		public string StreetAddress { get; set; }
		public string CountryCode { get; set; }
		public string AdministrativeArea { get; set; }
		public string Locality { get; set; }
		public string PostalCode { get; set; }
		public string Premise { get; set; }

		public virtual ICollection<OrderAddress> OrderAddress { get; set; }
		public virtual ICollection<ProductAddress> ProductAddress { get; set; }
		public virtual ICollection<UserAddress> UserAddress { get; set; }
	}
}
