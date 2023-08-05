using CustomerInvoiceAPI.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerInvoiceAPI.Models.Invoice
{
	public class InvoiceList
	{
		public DateTime InvoiceDate { get; set; }

		public int CustomerId { get; set; }  // Foreign key to Customer

		public decimal TotalAmount { get; set; }
		public bool IsDeleted { get; set; } = false;

	}
}
