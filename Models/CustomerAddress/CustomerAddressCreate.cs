using CustomerInvoiceAPI.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerInvoiceAPI.Models.CustomerAddress
{
	public class CustomerAddressCreate
	{
			[StringLength(200)]
		public string AddressLine1 { get; set; }

		[StringLength(200)]
		public string? AddressLine2 { get; set; }

		[StringLength(100)]
		public string City { get; set; }

		[StringLength(100)]
		public string State { get; set; }

		[StringLength(20)]
		public string PostalCode { get; set; }

		[StringLength(100)]
		public string Country { get; set; }

		public AddressType Type { get; set; }
	}
}

