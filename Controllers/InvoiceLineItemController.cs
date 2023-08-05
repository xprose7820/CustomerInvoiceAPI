using CustomerInvoiceAPI.Data.Entities;
using CustomerInvoiceAPI.Models.CustomerAddress;
using CustomerInvoiceAPI.Models.InvoiceLineItem;
using CustomerInvoiceAPI.Services.Customer;
using CustomerInvoiceAPI.Services.Invoice;
using CustomerInvoiceAPI.Services.InvoiceLineItem;
using Microsoft.AspNetCore.Mvc;

namespace CustomerInvoiceAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]

	public class InvoiceLineItemController : ControllerBase
	{
		private readonly IInvoiceService _invoiceService;
		private readonly IInvoiceLineItemService _invoiceLineItemService;
		private readonly ILogger<CustomerController> _logger;

		public InvoiceLineItemController(IInvoiceService invoiceService, IInvoiceLineItemService invoiceLineItemService, ILogger<CustomerController> logger)
		{
			_invoiceService = invoiceService;
			_invoiceLineItemService = invoiceLineItemService;
			_logger = logger;
		}

		[HttpPost("{invoiceId}")]
		public async Task<IActionResult> Create([FromRoute] int invoiceId, InvoiceLineItemCreate model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					InvoiceEntity invoiceExists = await _invoiceService.GetInvoiceAsync(invoiceId);
					if (invoiceExists != null)
					{
						 InvoiceLineItemEntity invoiceLineItem = await _invoiceLineItemService.CreateInvoiceLineItemAsync(invoiceId, model);

						if (invoiceLineItem != null)
						{
							_logger.LogInformation("InvoiceLineItem with ID: {Id} created successfully", invoiceLineItem.Id);
							return Ok(invoiceLineItem);
						}
						else
						{
							_logger.LogError("Error occurred while creating InvoiceLineItem.");
							return BadRequest("An error occurred while trying to create the InvoiceLineItem.");
						}

					}
					else
					{
						_logger.LogWarning("Invoice with ID: {Id} not found.", invoiceId);
						return NotFound();

					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error occurred while creating InvoiceLineItem.");
					return BadRequest("An error occurred while trying to create the InvoiceLineItem.");

				}
			}
			else
			{
				_logger.LogWarning("Invalid model state for the creation of a new InvoiceLineItem");
				return BadRequest(ModelState);
			}
		}
		[HttpGet("{invoiceLineItemId}")]

		public async Task<IActionResult> Get([FromRoute] int invoiceLineItemId)
		{
			try
			{
				InvoiceLineItemEntity invoiceLineItemExists = await _invoiceLineItemService.GetInvoiceLineItemAsync(invoiceLineItemId);
				if (invoiceLineItemExists != null)
				{
					_logger.LogInformation("InvoiceLineItem with ID: {Id} found.", invoiceLineItemExists.Id);
					return Ok(invoiceLineItemExists);
				}
				else
				{
					_logger.LogWarning("InvoiceLineItem with ID: {Id} not found.", invoiceLineItemExists);
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving InvoiceLineItem.");
				return BadRequest("An error occurred while trying to retrieve the InvoiceLineItem.");
			}
		}
		[HttpGet("list/{invoiceId}")]

		public async Task<IActionResult> GetInvoiceLineItemsByInvoiceId([FromRoute] int invoiceId)
		{
			try
			{
				InvoiceEntity invoiceExists = await _invoiceService.GetInvoiceAsync(invoiceId);
				if (invoiceExists != null)
				{
					IEnumerable<InvoiceLineItemList> details = await _invoiceLineItemService.GetInvoiceLineItemsByInvoiceIdAsync(invoiceId);

					if (details != null)
					{
						return Ok(details);
					}
					else
					{
						return NotFound();
					}

				}
				else
				{
					_logger.LogWarning("Invoice with ID: {Id} not found.", invoiceId);
					return NotFound();

				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving InvoiceLineItem.");
				return BadRequest("An error occurred while trying to retrieve the InvoiceLineItem.");

			}
		}
		[HttpPut("{invoiceLineItemId}")]
		public async Task<IActionResult> Update([FromRoute] int invoiceLineItemId, [FromBody] InvoiceLineItemUpdate model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					InvoiceLineItemEntity invoiceLineItemExists = await _invoiceLineItemService.GetInvoiceLineItemAsync(invoiceLineItemId);
					if (invoiceLineItemExists != null)
					{

						await _invoiceLineItemService.UpdateInvoiceLineItemAsync(invoiceLineItemId, model);
						_logger.LogInformation("InvoiceLineItem with ID: {Id} updated.", invoiceLineItemExists.Id);
						return Ok(invoiceLineItemExists);

					}
					else
					{
						_logger.LogWarning("InvoiceLineItem with ID: {Id} not found.", invoiceLineItemId);
						return NotFound();
					}

				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error occurred while retrieving InvoiceLineItem.");
					return BadRequest("An error occurred while trying to retrieve the InvoiceLineItem.");
				}
			}
			else
			{
				_logger.LogWarning("Invalid model state for the creation of a new InvoiceLineItem");
				return BadRequest(ModelState);
			}
		}

		[HttpDelete("{invoiceLineItemId}")]
		public async Task<IActionResult> Delete([FromRoute] int invoiceLineItemId)
		{
			try
			{
				InvoiceLineItemEntity invoiceLineItemExists = await _invoiceLineItemService.GetInvoiceLineItemAsync(invoiceLineItemId);
				if (invoiceLineItemExists != null)
				{
					await _invoiceLineItemService.DeleteInvoiceLineItemAsync(invoiceLineItemId);

					_logger.LogInformation("InvoiceLineItem with ID: {Id} found.", invoiceLineItemExists.Id);
					return Ok(invoiceLineItemExists);
				}
				else
				{
					_logger.LogWarning("InvoiceLineItem with ID: {Id} not found.", invoiceLineItemExists);
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving InvoiceLineItem.");
				return BadRequest("An error occurred while trying to retrieve the InvoiceLineItem.");
			}
		}

	}
}
