using CustomerInvoiceAPI.Data;
using CustomerInvoiceAPI.Data.Entities;
using CustomerInvoiceAPI.Models.CustomerAddress;
using CustomerInvoiceAPI.Models.InvoiceLineItem;
using CustomerInvoiceAPI.Services.CustomerAddress;
using Microsoft.EntityFrameworkCore;

namespace CustomerInvoiceAPI.Services.InvoiceLineItem
{
	public class InvoiceLineItemService : IInvoiceLineItemService
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<CustomerAddressService> _logger;
		public InvoiceLineItemService(ApplicationDbContext context, ILogger<CustomerAddressService> logger)
		{
			_context = context;

			_logger = logger;

		}
		// assume invoice exists
		public async Task<InvoiceLineItemEntity> CreateInvoiceLineItemAsync(int invoiceId, InvoiceLineItemCreate model)
		{
			try
			{

				InvoiceLineItemEntity invoiceLineItem = new InvoiceLineItemEntity
				{
					InvoiceId = invoiceId,
					Description = model.Description,
					Price = model.Price,
					Quantity = model.Quantity,

				};
				_context.InvoicesLineItems.Add(invoiceLineItem);
				await _context.SaveChangesAsync();
				return invoiceLineItem;

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating InvoiceLineItem.");
				throw;

			}

		}
		public async Task<InvoiceLineItemEntity> GetInvoiceLineItemAsync(int invoiceLineItemId)
		{
			if (invoiceLineItemId <= 0)
			{
				throw new ArgumentException("Invalid InvoiceLineItemId. The InvoiceLineItemId must be a positive integer.");
			}
			try
			{
				InvoiceLineItemEntity invoiceLineItemEntity = await _context.InvoicesLineItems.Where(g => g.Id == invoiceLineItemId).FirstOrDefaultAsync();

				if (invoiceLineItemEntity == null)
				{
					// Log a warning to indicate that the customer was not found
					_logger.LogWarning("InvoiceLineItem with ID: {Id} not found.", invoiceLineItemId);
				}

				return invoiceLineItemEntity;
			}
			catch (Exception ex)
			{
				// Log the exception details and rethrow it to be handled at a higher level
				_logger.LogError(ex, "Error occurred while retrieving InvoiceLineItem with ID: {Id}.", invoiceLineItemId);
				throw;
			}
		}
		public async Task<IEnumerable<InvoiceLineItemList>> GetInvoiceLineItemsByInvoiceIdAsync(int invoiceId)
		{
			try
			{
				List<InvoiceLineItemList> details = await _context.Invoices.Where(i => i.Id == invoiceId).Include(c => c.InvoiceLineItems)
					.SelectMany(c => c.InvoiceLineItems)
					.Select(a => new InvoiceLineItemList
					{
						Id = a.Id,
						InvoiceId = a.InvoiceId,
						Description = a.Description,
						Price = a.Price,
						Quantity = a.Quantity,


					}).ToListAsync();
				return details;
			}
			catch (Exception ex)
			{

				_logger.LogError(ex, "An error occurred while retrieving InvoiceLineItem.");

				// Optionally, you might choose to rethrow the exception,
				// or you might return an empty list or null, depending on how you want your application to behave
				throw;
			}
		}

		public async Task<InvoiceLineItemEntity> UpdateInvoiceLineItemAsync(int invoiceLineItemId, InvoiceLineItemUpdate model)
		{
			try
			{
				InvoiceLineItemEntity invoiceLineItemExists = await _context.InvoicesLineItems.Where(i => i.Id == invoiceLineItemId).FirstOrDefaultAsync();

				invoiceLineItemExists.Description = model.Description;
				invoiceLineItemExists.Price = model.Price;
				invoiceLineItemExists.Quantity = model.Quantity;

				await _context.SaveChangesAsync();
				return invoiceLineItemExists;

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving InvoiceLineItem with ID: {Id}.", invoiceLineItemId);
				throw;

			}

		}
		public async Task<InvoiceLineItemEntity> DeleteInvoiceLineItemAsync(int invoiceLineItemId)
		{
			try
			{
				InvoiceLineItemEntity invoiceLineItemExists = await _context.InvoicesLineItems.Where(g => g.Id == invoiceLineItemId).FirstOrDefaultAsync();
				_context.InvoicesLineItems.Remove(invoiceLineItemExists);
				await _context.SaveChangesAsync();

				return invoiceLineItemExists;


			}
			catch (Exception ex)
			{
				// Log the exception details and rethrow it to be handled at a higher level
				_logger.LogError(ex, "Error occurred while retrieving InvoiceLineItem with ID: {Id}.", invoiceLineItemId);
				throw;

			}
		}




	}
}
