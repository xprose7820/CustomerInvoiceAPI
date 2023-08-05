using CustomerInvoiceAPI.Data;
using CustomerInvoiceAPI.Data.Entities;
using CustomerInvoiceAPI.Models.CustomerAddress;
using CustomerInvoiceAPI.Services.Customer;
using Microsoft.EntityFrameworkCore;

namespace CustomerInvoiceAPI.Services.CustomerAddress
{
	public class CustomerAddressService : ICustomerAddressService

	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<CustomerAddressService> _logger;
		public CustomerAddressService(ApplicationDbContext context, ILogger<CustomerAddressService> logger)
		{
			_context = context;

			_logger = logger;

		}
		public async Task<CustomerAddressEntity> CreateCustomerAddressAsync(int customerId, CustomerAddressCreate model)
		{
			try

			{
				bool addressExists = await CheckAddressExistsAsync(customerId, model.AddressLine1, model.PostalCode, model.Type);
				if (!addressExists)
				{
					CustomerAddressEntity customerAddress = new CustomerAddressEntity
					{
						CustomerId = customerId,
						AddressLine1 = model.AddressLine1,
						AddressLine2 = model.AddressLine2,
						City = model.City,
						State = model.State,
						PostalCode = model.PostalCode,
						Country = model.Country,
						Type = model.Type,

					};
					_context.CustomerAddresses.Add(customerAddress);
					await _context.SaveChangesAsync();

					_logger.LogInformation("CustomerAddress {Id} created successfully.", customerAddress.Id);
					return customerAddress;
				}
				else
				{

					_logger.LogWarning("A customer with this address already exists.");
					return null;
				}


			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating address.");
				throw;

			}

		}
		public async Task<bool> CheckAddressExistsAsync(int customerId, string addressLine1, string postalCode, AddressType type)
		{
			return await _context.Customers.Include(c => c.Addresses).Where(c => c.Id == customerId)
				.SelectMany(c => c.Addresses)
				.AnyAsync(a => a.AddressLine1 == addressLine1 && a.PostalCode == postalCode && a.Type == type);


		}
		public async Task<CustomerAddressEntity> GetCustomerAddressAsync(int customerAddressId)
		{
			if (customerAddressId <= 0)
			{
				throw new ArgumentException("Invalid customerId. The customerId must be a positive integer.");
			}
			try
			{
				CustomerAddressEntity customerAddressEntity = await _context.CustomerAddresses.Where(g => g.Id == customerAddressId).FirstOrDefaultAsync();

				if (customerAddressEntity == null)
				{
					_logger.LogWarning("Customer with ID: {Id} not found.", customerAddressId);
				}

				return customerAddressEntity;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving customer with ID: {Id}.", customerAddressId);
				throw;
			}


		}

		//assuming customer exists
		public async Task<IEnumerable<CustomerAddressList>> GetAddressesByCustomerIdAsync(int customerId)
		{
			try
			{
				List<CustomerAddressList> details = await _context.Customers.Where(c => c.Id == customerId).Include(c => c.Addresses)
					.SelectMany(c => c.Addresses)
					.Select(a => new CustomerAddressList
					{
						Id = a.Id,
						CustomerId = customerId,
						AddressLine1 = a.AddressLine1,
						AddressLine2 = a.AddressLine2,
						City = a.City,
						PostalCode = a.PostalCode,
						State = a.State,
						Country = a.Country,
						Type = a.Type

					}).ToListAsync();
				return details;
			}
			catch (Exception ex)
			{

				_logger.LogError(ex, "An error occurred while retrieving customers.");

							throw;
			}
		}

		public async Task<CustomerAddressEntity> UpdateCustomerAddressAsync(int customerAddressId, CustomerAddressUpdate model)
		{
			try
			{
				CustomerAddressEntity customerAddressExists = await _context.CustomerAddresses.Where(c => c.Id == customerAddressId).FirstOrDefaultAsync();
				customerAddressExists.AddressLine1 = model.AddressLine1;
				customerAddressExists.AddressLine2 = model.AddressLine2;
				customerAddressExists.City = model.City;
				customerAddressExists.State = model.State;
				customerAddressExists.PostalCode = model.PostalCode;
				customerAddressExists.Country = model.Country;
				customerAddressExists.Type = model.Type;

				await _context.SaveChangesAsync();
				return customerAddressExists;

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving customer with ID: {Id}.", customerAddressId);
				throw;

			}
		}
		public async Task<CustomerAddressEntity> DeleteCustomerAddressAsync(int customerAddressId)
		{
			try
			{
				CustomerAddressEntity customerAddressExists = await _context.CustomerAddresses.Where(g => g.Id == customerAddressId).FirstOrDefaultAsync();
				_context.CustomerAddresses.Remove(customerAddressExists);
				await _context.SaveChangesAsync();

				return customerAddressExists;


			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving customer with ID: {Id}.", customerAddressId);
				throw;

			}

		}


	}
}
