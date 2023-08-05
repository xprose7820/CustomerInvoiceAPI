using CustomerInvoiceAPI.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerInvoiceAPI.Models.InvoiceLineItem
{
	public class InvoiceLineItemList
	{
		public int Id { get; set; }

		public int InvoiceId { get; set; }  // Foreign key to Invoice
		public string Description { get; set; }

		public decimal Price { get; set; }

		public int Quantity { get; set; }
	}
}
