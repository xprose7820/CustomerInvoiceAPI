using System.ComponentModel.DataAnnotations;

namespace CustomerInvoiceAPI.Models.Invoice
{
	public class InvoiceCreate
	{
		
		[DataType(DataType.Currency)]
		public decimal TotalAmount { get; set; }
	
	}
}
