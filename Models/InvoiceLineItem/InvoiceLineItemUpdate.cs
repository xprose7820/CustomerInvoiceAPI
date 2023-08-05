using System.ComponentModel.DataAnnotations;

namespace CustomerInvoiceAPI.Models.InvoiceLineItem
{
	public class InvoiceLineItemUpdate
	{
		[StringLength(100)]
		public string Description { get; set; }

		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		public int Quantity { get; set; }
	}
}
