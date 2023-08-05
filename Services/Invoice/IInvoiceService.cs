using CustomerInvoiceAPI.Data.Entities;
using CustomerInvoiceAPI.Models.Invoice;

namespace CustomerInvoiceAPI.Services.Invoice
{
	public interface IInvoiceService
	{
		Task<InvoiceEntity> CreateInvoiceAsync(int customerId, InvoiceCreate model);
		Task<InvoiceEntity> GetInvoiceAsync(int invoiceId);
		Task<IEnumerable<InvoiceList>> GetAllInvoiceAsync();
		Task<IEnumerable<InvoiceList>> GetInvoicesByCustomerId(int customerId);
		Task<InvoiceEntity> UpdateInvoiceAsync(int invoiceId, InvoiceUpdate model);
		// will soft delete Invoice 
		Task<InvoiceEntity> DeleteInvoiceAsync(int invoiceId);
	}
}
