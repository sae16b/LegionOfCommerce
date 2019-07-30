using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	public partial class Address
	{
		public int AddressId { get; set; }
		public string StreetAddress { get; set; }
		public string CountryCode { get; set; }
		public string AdministrativeArea { get; set; }
		public string Locality { get; set; }
		public string PostalCode { get; set; }
		public string Premise { get; set; }

		public virtual OrderAddress OrderAddress { get; set; }
		public virtual ProductAddress ProductAddress { get; set; }
		public virtual UserAddress UserAddress { get; set; }
	}
}
