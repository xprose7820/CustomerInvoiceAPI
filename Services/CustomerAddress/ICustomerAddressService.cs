using CustomerInvoiceAPI.Data.Entities;
using CustomerInvoiceAPI.Models.CustomerAddress;

namespace CustomerInvoiceAPI.Services.CustomerAddress
{
	public interface ICustomerAddressService
	{
		Task<CustomerAddressEntity> CreateCustomerAddressAsync(int customerId, CustomerAddressCreate model);
		Task<CustomerAddressEntity> GetCustomerAddressAsync(int customerAddressId);
		Task<IEnumerable<CustomerAddressList>> GetAddressesByCustomerIdAsync(int customerId);
		Task<CustomerAddressEntity> UpdateCustomerAddressAsync(int customerAddressId, CustomerAddressUpdate model);
		Task<CustomerAddressEntity> DeleteCustomerAddressAsync(int customerAddressId);
	}
}
