using CustomerInvoiceAPI.Data.Entities;
using CustomerInvoiceAPI.Models.InvoiceLineItem;

namespace CustomerInvoiceAPI.Services.InvoiceLineItem
{
	public interface IInvoiceLineItemService
	{
		Task<InvoiceLineItemEntity> CreateInvoiceLineItemAsync(int invoiceId, InvoiceLineItemCreate model);
		Task<InvoiceLineItemEntity> GetInvoiceLineItemAsync(int invoiceLineItemId);
		Task<IEnumerable<InvoiceLineItemList>> GetInvoiceLineItemsByInvoiceIdAsync(int invoiceLineItemId);
		Task<InvoiceLineItemEntity> UpdateInvoiceLineItemAsync(int invoiceLineItemId, InvoiceLineItemUpdate model);
		// will be "Hard Delete" 
		Task<InvoiceLineItemEntity> DeleteInvoiceLineItemAsync(int invoiceLineItem);
	}
}
