using CustomerInvoiceAPI.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerInvoiceAPI.Models.InvoiceLineItem
{
	public class InvoiceLineItemCreate
	{

		
		[Required]
		[StringLength(100)]
		public string Description { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		[Required]
		public int Quantity { get; set; }
	}
}
