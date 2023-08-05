using CustomerInvoiceAPI.Data;
using CustomerInvoiceAPI.Data.Entities;
using CustomerInvoiceAPI.Models.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerInvoiceAPI.Services.Customer
{

	public class CustomerService : ICustomerService
	{

		private readonly ApplicationDbContext _context;
		private readonly ILogger<CustomerService> _logger;
		public CustomerService(ApplicationDbContext context, ILogger<CustomerService> logger)
		{
			_context = context;

			_logger = logger;

		}

		public async Task<CustomerEntity> CreateCustomerAsync(CustomerCreate newCustomer)
		{

			try
			{
				bool emailExists = await CheckEmailExistsAsync(newCustomer.Email);
				if (!emailExists)
				{
					CustomerEntity customer = new CustomerEntity
					{
						FirstName = newCustomer.FirstName,
						LastName = newCustomer.LastName,
						Email = newCustomer.Email,
						Phone = newCustomer.Phone,
						

					};
					_context.Customers.Add(customer);
					await _context.SaveChangesAsync();

					_logger.LogInformation("Customer {Id} created successfully.", customer.Id);
					return customer;
				}
				else
				{

					_logger.LogWarning("A customer with this email already exists.");
					return null;
				}

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating customer.");
				throw;
			}
		}
		public async Task<CustomerEntity> GetCustomerAsync(int customerId)
		{
			if (customerId <= 0)			{
				throw new ArgumentException("Invalid customerId. The customerId must be a positive integer.");
			}

			try
			{
				CustomerEntity customerEntity = await _context.Customers.Where(g => g.Id == customerId).FirstOrDefaultAsync();

				if (customerEntity == null)
				{
					// Log a warning to indicate that the customer was not found
					_logger.LogWarning("Customer with ID: {Id} not found.", customerId);
				}

				return customerEntity;
			}
			catch (Exception ex)
			{
				// Log the exception details and rethrow it to be handled at a higher level
				_logger.LogError(ex, "Error occurred while retrieving customer with ID: {Id}.", customerId);
				throw;
			}
		}
		public async Task<CustomerEntity> RemoveCustomerAsync(int customerId)
		{
			if (customerId <= 0)
			{
				throw new ArgumentException("Invalid customerId. The customerId must be a positive integer.");
			}

			try
			{
				CustomerEntity customerExists = await _context.Customers.Where(g => g.Id == customerId).FirstOrDefaultAsync();
				if (customerExists == null)
				{
					_logger.LogWarning("Customer with ID: {Id} not found.", customerId);
					return null;


				}
				customerExists.IsDeleted = true;
				await _context.SaveChangesAsync();

				return customerExists;


			}
			catch (Exception ex)
			{
				// Log the exception details and rethrow it to be handled at a higher level
				_logger.LogError(ex, "Error occurred while retrieving customer with ID: {Id}.", customerId);
				throw;

			}
		}
		// assume customer exists
		public async Task<CustomerEntity> UpdateCustomerAsync(int customerId, CustomerUpdate updateCustomer)
		{
			try
			{

				CustomerEntity customerExists = await _context.Customers.Where(g => g.Id == customerId).FirstOrDefaultAsync();
				customerExists.FirstName = updateCustomer.FirstName;
				customerExists.LastName = updateCustomer.LastName;
				customerExists.Email = updateCustomer.Email;
				customerExists.Phone = updateCustomer.Phone;

				await _context.SaveChangesAsync();
				return customerExists;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving customer with ID: {Id}.", customerId);
				throw;

			}
		}
		public async Task<bool> CheckEmailExistsAsync(string email)
		{
			CustomerEntity existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);

			return existingCustomer != null;
		}
		public async Task<IEnumerable<CustomerList>> GetAllCustomersAsync()
		{
			try
			{
				IEnumerable<CustomerList> details = await _context.Customers.Select(c => new CustomerList
				{
					Id = c.Id,
					FirstName = c.FirstName,
					LastName = c.LastName,
					Email = c.Email,
					Phone = c.Phone,

				}).ToListAsync();
				return details;

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while retrieving customers.");

				// Optionally, you might choose to rethrow the exception,
				// or you might return an empty list or null, depending on how you want your application to behave
				throw;
			}

		}
		public async Task<IEnumerable<CustomerList>> GetActiveCustomersAsync()
		{
			try
			{
				List<CustomerList> details = await _context.Customers.Where(c => c.IsDeleted == false).Select(c => new CustomerList
				{
					Id = c.Id,
					FirstName = c.FirstName,
					LastName = c.LastName,
					Email = c.Email,
					Phone = c.Phone,

				}).ToListAsync();
				return details;

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while retrieving customers.");

				// Optionally, you might choose to rethrow the exception,
				// or you might return an empty list or null, depending on how you want your application to behave
				throw;
			}

		}
		public async Task<IEnumerable<CustomerList>> GetDeletedCustomersAsync()
		{
			try
			{
				List<CustomerList> details = await _context.Customers.Where(c => c.IsDeleted == true).Select(c => new CustomerList
				{
					Id = c.Id,
					FirstName = c.FirstName,
					LastName = c.LastName,
					Email = c.Email,
					Phone = c.Phone,

				}).ToListAsync();
				return details;

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while retrieving customers.");

				// Optionally, you might choose to rethrow the exception,
				// or you might return an empty list or null, depending on how you want your application to behave
				throw;
			}

		}


	}
}
