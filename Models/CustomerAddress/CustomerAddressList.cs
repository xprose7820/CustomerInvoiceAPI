﻿using CustomerInvoiceAPI.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerInvoiceAPI.Models.CustomerAddress
{
	public class CustomerAddressList
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public string AddressLine1 { get; set; }

		public string? AddressLine2 { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public string PostalCode { get; set; }

		public string Country { get; set; }

		public AddressType Type { get; set; }
	}
}
