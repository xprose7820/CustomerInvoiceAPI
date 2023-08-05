using CustomerInvoiceAPI.Data.Entities;
using CustomerInvoiceAPI.Models.Customer;

namespace CustomerInvoiceAPI.Services.Customer
{
	public interface ICustomerService
	{
		Task<CustomerEntity> CreateCustomerAsync(CustomerCreate newCustomer);
		Task<CustomerEntity> GetCustomerAsync(int customerId);

		// will implement a "Soft Delete" for Customer and Invoice entities
		// for CustomerAddress and InvoiceLineItem, will have basic delete functionality
		Task<CustomerEntity> RemoveCustomerAsync(int customerId);
		Task<CustomerEntity> UpdateCustomerAsync(int customerId, CustomerUpdate existingCustomer);
		Task<IEnumerable<CustomerList>> GetAllCustomersAsync();
		Task<IEnumerable<CustomerList>> GetActiveCustomersAsync();
		Task<IEnumerable<CustomerList>> GetDeletedCustomersAsync();

	}
}
