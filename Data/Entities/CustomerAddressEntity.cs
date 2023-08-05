using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomerInvoiceAPI.Data.Entities
{
	public class CustomerAddressEntity
	{

		[Key]
		public int Id { get; set; }
		[Required]
		[ForeignKey("Customer")]
		public int CustomerId { get; set; }
		[JsonIgnore]
		public virtual CustomerEntity? Customer { get; set; }
		[Required]
		[StringLength(200)]
		public string AddressLine1 { get; set; }

		[StringLength(200)]
		public string? AddressLine2 { get; set; }

		[Required]
		[StringLength(100)]
		public string City { get; set; }

		[Required]
		[StringLength(100)]
		public string State { get; set; }

		[Required]
		[StringLength(20)]
		public string PostalCode { get; set; }

		[Required]
		[StringLength(100)]
		public string Country { get; set; }

		[Required]
		public AddressType Type { get; set; }
	}
	public enum AddressType
	{
		Billing,
		Shipping
	}

}
