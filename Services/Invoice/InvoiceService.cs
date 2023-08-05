using CustomerInvoiceAPI.Data;
using CustomerInvoiceAPI.Data.Entities;
using CustomerInvoiceAPI.Models.CustomerAddress;
using CustomerInvoiceAPI.Models.Invoice;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace CustomerInvoiceAPI.Services.Invoice
{
	public class InvoiceService : IInvoiceService
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<InvoiceService> _logger;
		public InvoiceService(ApplicationDbContext context, ILogger<InvoiceService> logger)
		{
			_context = context;

			_logger = logger;

		}
		// assumes customer exists
		public async Task<InvoiceEntity> CreateInvoiceAsync(int customerId, InvoiceCreate model)
		{
			try

			{
				InvoiceEntity invoice = new InvoiceEntity
				{
					CustomerId = customerId,
					InvoiceDate = DateTime.Now,
					TotalAmount = model.TotalAmount,

				};
				_context.Invoices.Add(invoice);
				await _context.SaveChangesAsync();
				return invoice;

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating address.");
				throw;

			}

		}
		public async Task<InvoiceEntity> GetInvoiceAsync(int invoiceId)
		{
			if (invoiceId <= 0)
			{
				throw new ArgumentException("Invalid customerId. The customerId must be a positive integer.");
			}
			try
			{
				InvoiceEntity invoiceEntity = await _context.Invoices.Where(g => g.Id == invoiceId).FirstOrDefaultAsync();

				if (invoiceEntity == null)
				{
					// Log a warning to indicate that the customer was not found
					_logger.LogWarning("Customer with ID: {Id} not found.", invoiceId);
				}

				return invoiceEntity;
			}
			catch (Exception ex)
			{
				// Log the exception details and rethrow it to be handled at a higher level
				_logger.LogError(ex, "Error occurred while retrieving customer with ID: {Id}.", invoiceId);
				throw;
			}

		}
		public async Task<IEnumerable<InvoiceList>> GetAllInvoiceAsync()
		{

			try
			{
				List<InvoiceList> details = await _context.Invoices
					.Select(invoice => new InvoiceList
					{
						CustomerId = invoice.CustomerId,
						InvoiceDate = invoice.InvoiceDate,
						TotalAmount = invoice.TotalAmount,
						IsDeleted = invoice.IsDeleted,
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

		public async Task<IEnumerable<InvoiceList>> GetInvoicesByCustomerId(int customerId)
		{
			try
			{
				List<InvoiceList> details = await _context.Customers.Where(c => c.Id == customerId).Include(c => c.Invoices)
					.SelectMany(c => c.Invoices)
					.Select(a => new InvoiceList
					{
						CustomerId = a.CustomerId,
						InvoiceDate = a.InvoiceDate,
						TotalAmount = a.TotalAmount,
						IsDeleted = a.IsDeleted,

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

		// assume invoice exists
		public async Task<InvoiceEntity> UpdateInvoiceAsync(int invoiceId, InvoiceUpdate model)
		{
			try
			{
				InvoiceEntity invoiceExists = await _context.Invoices.Where(c => c.Id == invoiceId).FirstOrDefaultAsync();

				invoiceExists.TotalAmount = model.TotalAmount;

				await _context.SaveChangesAsync();

				return invoiceExists;

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving customer with ID: {Id}.", invoiceId);
				throw;

			}
		}
		public async Task<InvoiceEntity> DeleteInvoiceAsync(int invoiceId)
		{
			if (invoiceId <= 0)
			{
				throw new ArgumentException("Invalid customerId. The customerId must be a positive integer.");
			}

			try
			{
				InvoiceEntity invoiceExists = await _context.Invoices.Where(g => g.Id == invoiceId).FirstOrDefaultAsync();
				if (invoiceExists == null)
				{
					_logger.LogWarning("Customer with ID: {Id} not found.", invoiceId);
					return null;


				}
				invoiceExists.IsDeleted = true;
				await _context.SaveChangesAsync();

				return invoiceExists;


			}
			catch (Exception ex)
			{
				// Log the exception details and rethrow it to be handled at a higher level
				_logger.LogError(ex, "Error occurred while retrieving customer with ID: {Id}.", invoiceId);
				throw;

			}
		}

	}
}
